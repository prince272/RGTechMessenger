using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Exceptions;
using RGTechMessenger.Core.Extensions.FileStorage;
using RGTechMessenger.Core.Extensions.Identity;
using RGTechMessenger.Core.Mappers;
using RGTechMessenger.Core.Models.Accounts;
using RGTechMessenger.Core.Models.Conversations;
using RGTechMessenger.Core.Models.Users;
using RGTechMessenger.Core.Repositories;
using RGTechMessenger.Core.Shared;
using RGTechMessenger.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Services
{
    public interface IUserService : IDisposable, IAsyncDisposable
    {
        Task<UserPageModel> GetUsersAsync(UserSearch search, int pageNumber, int pageSize);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserMapper _modelMapper;
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IServiceProvider _validatorProvider;

        public UserService(
            ILogger<UserService> logger,
            IUserMapper modelMapper,
            IUserContext userContext,
            IUserRepository userRepository,
            IClientRepository clientRepository,
            IServiceProvider validatorProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _modelMapper = modelMapper ?? throw new ArgumentNullException(nameof(modelMapper));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));
        }

        public async Task<UserPageModel> GetUsersAsync(UserSearch search, int pageNumber, int pageSize)
        {
            if (search == null) throw new ArgumentNullException(nameof(search));
            var predicate = search.Build();

            var page = (await _userRepository.GetManyAsync(pageNumber, pageSize, predicate: predicate));
            var pageModel = await _modelMapper.MapAsync(page);
            return pageModel;
        }

        private readonly CancellationToken cancellationToken = default;
        private bool disposed = false;

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // myResource.Dispose();
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!disposed)
            {
                disposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        protected ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                //  await myResource.DisposeAsync();
                cancellationToken.ThrowIfCancellationRequested();
            }

            return ValueTask.CompletedTask;
        }
    }
}
