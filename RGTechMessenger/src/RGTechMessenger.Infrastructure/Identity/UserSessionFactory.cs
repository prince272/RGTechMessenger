﻿using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Extensions.Identity;
using RGTechMessenger.Core.Utilities;
using RGTechMessenger.Infrastructure.Data;

namespace RGTechMessenger.Infrastructure.Identity
{
    public class UserSessionFactory : IUserSessionFactory
    {
        private readonly JwtBearerOptions _jwtBearerOptions;
        private readonly IOptions<UserSessionOptions> _userSessionOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly ILogger<UserSessionFactory> _logger;

        public UserSessionFactory(
            IOptionsSnapshot<JwtBearerOptions> jwtBearerOptions,
            IOptions<UserSessionOptions> userSessionOptions,
            IHttpContextAccessor httpContextAccessor,
            IUserContext userSessionContext,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory, ILogger<UserSessionFactory> logger)
        {
            _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme) ?? throw new ArgumentNullException(nameof(jwtBearerOptions));
            _userSessionOptions = userSessionOptions ?? throw new ArgumentNullException(nameof(userSessionOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _userContext = userSessionContext ?? throw new ArgumentNullException(nameof(userSessionContext));
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory ?? throw new ArgumentNullException(nameof(userClaimsPrincipalFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<UserSessionInfo> GenerateAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var currentTime = DateTimeOffset.UtcNow;
            var claims = ((ClaimsIdentity)(await _userClaimsPrincipalFactory.CreateAsync(user)).Identity!).Claims;
            var (accessToken, accessTokenExpiresAt) = GenerateAccessToken(currentTime, _userSessionOptions.Value.AccessTokenExpiresIn, ref claims);
            var (refreshToken, refreshTokenExpiresAt) = GenerateRefreshToken(currentTime, _userSessionOptions.Value.RefreshTokenExpiresIn);

            return new UserSessionInfo
            {
                TokenType = JwtBearerDefaults.AuthenticationScheme,

                AccessToken = accessToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,

                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,

                User = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme))
            };
        }

        private (string AccessToken, DateTimeOffset AccessTokenExpiresAt) GenerateAccessToken(DateTimeOffset currentTime, TimeSpan expiresIn, ref IEnumerable<Claim> claims)
        {
            if (claims == null) throw new ArgumentNullException(nameof(claims));

            var issuer = GetIssuer();
            var audience = GetAudience();

            claims = claims.Concat(new Claim[] {
                new(JwtRegisteredClaimNames.Jti, AlgorithmHelper.GenerateStamp(), ClaimValueTypes.String, issuer),
                new(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new(JwtRegisteredClaimNames.Iat, currentTime.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64, issuer),
            });

            if (_userContext.DeviceId != null)
                claims = claims.Append(new(ClaimTypes.System, _userContext.DeviceId, ClaimValueTypes.String, issuer));

            var expiresAt = currentTime.Add(expiresIn);

            var key = _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer, audience, claims, currentTime.DateTime, expiresAt.DateTime, creds);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenValue, expiresAt);
        }

        private (string RefreshToken, DateTimeOffset RefreshTokenExpiresAt) GenerateRefreshToken(DateTimeOffset currentTime, TimeSpan expiresIn)
        {
            var issuer = GetIssuer();
            var audience = GetAudience();

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Jti, AlgorithmHelper.GenerateStamp(), ClaimValueTypes.String, issuer),
                new(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new(JwtRegisteredClaimNames.Iat, currentTime.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64, issuer),
            };


            if (_userContext.DeviceId != null)
                claims = claims.Append(new(ClaimTypes.System, _userContext.DeviceId, ClaimValueTypes.String, issuer)).ToArray();

            var expiresAt = currentTime.Add(expiresIn);

            var key = _jwtBearerOptions.TokenValidationParameters.IssuerSigningKey;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer, audience, claims, currentTime.DateTime, expiresAt.DateTime, creds);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenValue, expiresAt);
        }

        private string GetIssuer()
        {
            HttpContext? context = _httpContextAccessor.HttpContext;
            Uri? issuer = context != null ? new Uri(string.Concat(context.Request.Scheme, "://", context.Request.Host.ToUriComponent()), UriKind.Absolute) : null;
            return issuer?.GetLeftPart(UriPartial.Authority) ?? throw new InvalidOperationException("Unable to determine the issuer.");
        }

        private string GetAudience()
        {
            HttpContext? context = _httpContextAccessor.HttpContext;
            Uri? audience = null;
            audience ??= context?.Request?.Headers?.Origin is StringValues origin && !StringValues.IsNullOrEmpty(origin) ? new Uri(origin.ToString(), UriKind.Absolute) : null;
            audience ??= context?.Request?.Headers?.Referer is StringValues referer && !StringValues.IsNullOrEmpty(referer) ? new Uri(referer.ToString(), UriKind.Absolute) : null;
            return audience?.GetLeftPart(UriPartial.Authority) ?? _userSessionOptions.Value.GetAudiences().First();
        }

        private string? GetDeviceId(ClaimsIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));
            return identity.FindFirst(ClaimTypes.System) is Claim claim ? claim.Value : null;
        }

        private bool VerifyDevice(string deviceId)
        {
            if (deviceId == null) throw new ArgumentNullException(nameof(deviceId));

            return string.Equals(deviceId, _userContext.DeviceId ?? "Unknown", StringComparison.Ordinal);
        }

        public async Task<bool> ValidateAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) return false;

            try
            {
                var parameters = _jwtBearerOptions.TokenValidationParameters.Clone();
                var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(accessToken, parameters);

                if (!result.IsValid)
                    return false;

                var claimsIdentity = result.ClaimsIdentity;
                if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
                    return false;

                var deviceId = GetDeviceId(claimsIdentity);
                if (deviceId == null || !VerifyDevice(deviceId)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to validate {nameof(accessToken)}: `{accessToken}`.");

                return false;
            }
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return false;

            try
            {
                var parameters = _jwtBearerOptions.TokenValidationParameters.Clone();
                var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(refreshToken, parameters);

                if (!result.IsValid) return false;

                var claimsIdentity = result.ClaimsIdentity;
                if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any()) return false;

                var deviceId = GetDeviceId(claimsIdentity);
                if (deviceId == null || !VerifyDevice(deviceId)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to validate {nameof(refreshToken)}: `{refreshToken}`.");

                return false;
            }
        }
    }
}