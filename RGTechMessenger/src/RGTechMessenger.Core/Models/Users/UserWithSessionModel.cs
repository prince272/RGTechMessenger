﻿using AbstractProfile = AutoMapper.Profile;
using RGTechMessenger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGTechMessenger.Core.Extensions.Identity;

namespace RGTechMessenger.Core.Models.Users
{
    public class UserWithSessionModel : UserModel
    {
        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? TokenType { get; set; }
    }

    public class UserSessionProfile : AbstractProfile
    {
        public UserSessionProfile()
        {
            CreateMap<User, UserWithSessionModel>();
            CreateMap<UserSessionInfo, UserWithSessionModel>();
        }
    }
}

