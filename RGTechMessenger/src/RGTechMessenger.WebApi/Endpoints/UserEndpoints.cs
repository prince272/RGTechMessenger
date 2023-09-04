using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Exceptions;
using RGTechMessenger.Core.Models.Accounts;
using RGTechMessenger.Core.Models.Medias;
using RGTechMessenger.Core.Models.Users;
using RGTechMessenger.Core.Services;
using RGTechMessenger.Core.Utilities;
using RGTechMessenger.Infrastructure.Identity;
using RGTechMessenger.WebApi.Models;
using Serilog.Sinks.File;
using System.Security.Policy;

namespace RGTechMessenger.WebApi.Endpoints
{
    public class UserEndpoints : Shared.Endpoints
    {
        public UserEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
            : base(endpointRouteBuilder)
        {
        }

        public override void Configure()
        {
            var endpoints = MapGroup("/users");
            endpoints.MapGet("/", GetUsersAsync);
        }

        public async Task<IResult> GetUsersAsync([FromServices] IUserService userService, [AsParameters] UserSearch search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            return Results.Ok(await userService.GetUsersAsync(search, pageNumber, pageSize));
        }
    }
}