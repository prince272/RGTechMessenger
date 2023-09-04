﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Accounts
{
    public class RefreshSessionForm
    {
        public string RefreshToken { get; set; } = default!;
    }

    public class RefreshSessionFormValidator : AbstractValidator<RefreshSessionForm>
    {
        public RefreshSessionFormValidator()
        {
            RuleFor(_ => _.RefreshToken).NotEmpty();
        }
    }
}
