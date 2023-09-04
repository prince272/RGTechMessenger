﻿using AutoMapper;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Extensions.Identity;
using RGTechMessenger.Core.Models.Users;
using RGTechMessenger.Core.Repositories;
using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Mappers
{
    public interface IUserMapper
    {
        Task<UserWithSessionModel> MapAsync(User user, UserSessionInfo session, CancellationToken cancellationToken = default);

        Task<UserModel> MapAsync(User user, CancellationToken cancellationToken = default);

        Task<UserPageModel> MapAsync(IPageable<User> users, CancellationToken cancellationToken = default);

        Task<UserListModel> MapAsync(IEnumerable<User> users, CancellationToken cancellationToken = default);
    }

    public class UserMapper : IUserMapper
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IClientRepository _clientRepository;

        public UserMapper(IMapper mapper, IUserRepository userRepository, IUserContext userContext, IClientRepository clientRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<UserWithSessionModel> MapAsync(User user, UserSessionInfo session, CancellationToken cancellationToken = default)
        {
            var model = _mapper.Map(session, _mapper.Map<UserWithSessionModel>(user));
            model.Online = await _clientRepository.AnyAsync(_ => _.Active && _.UserId == user.Id, cancellationToken);
            model.Roles = (await _userRepository.GetRolesAsync(user, cancellationToken)).Select(_ => _.Camelize()).ToArray();
            return model;
        }

        public async Task<UserModel> MapAsync(User user, CancellationToken cancellationToken = default)
        {
            var model = _mapper.Map<UserModel>(user);
            model.Online = await _clientRepository.AnyAsync(_ => _.Active && _.UserId == user.Id, cancellationToken);
            model.Roles = (await _userRepository.GetRolesAsync(user, cancellationToken)).Select(_ => _.Camelize()).ToArray();
            return model;
        }

        public async Task<UserPageModel> MapAsync(IPageable<User> users, CancellationToken cancellationToken = default)
        {
            if (users == null) throw new ArgumentNullException(nameof(users));

            var pageModel = await MapAsync<UserPageModel>(users, cancellationToken);
            pageModel.PageNumber = users.PageNumber;
            pageModel.PageSize = users.PageSize;
            pageModel.TotalPages = users.TotalPages;
            pageModel.TotalItems = users.TotalItems;
            return pageModel;
        }

        public Task<UserListModel> MapAsync(IEnumerable<User> users, CancellationToken cancellationToken = default)
        {
            return MapAsync<UserListModel>(users, cancellationToken);
        }

        public async Task<TListModel> MapAsync<TListModel>(IEnumerable<User> users, CancellationToken cancellationToken = default)
            where TListModel : UserListModel
        {
            if (users == null) throw new ArgumentNullException(nameof(users));

            var items = new List<UserModel>();

            foreach (var user in users)
            {
                var userModel = await MapAsync(user, cancellationToken);
                items.Add(userModel);
            }

            var profileListModel = Activator.CreateInstance<TListModel>();
            profileListModel.Items = items;
            return profileListModel;
        }
    }
}
