﻿using FluentValidation;
using RGTechMessenger.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Accounts
{
    public class SendPasswordResetTokenForm
    {
        public string Username { get; set; } = default!;

        [JsonIgnore]
        public ContactType UsernameType => ValidationHelper.GetContactType(Username);
    }

    public class SendPasswordResetTokenFormValidator : AbstractValidator<SendPasswordResetTokenForm>
    {
        public SendPasswordResetTokenFormValidator()
        {
            RuleFor(_ => _.Username).NotEmpty().Username();
        }
    }
}
