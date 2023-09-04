using FluentValidation;
using RGTechMessenger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Conversations
{
    public class CreateGroupConversationForm
    {
    }

    public class CreateGroupConversationFormValidator : AbstractValidator<CreateGroupConversationForm>
    {
        public CreateGroupConversationFormValidator()
        {
        }
    }
}
