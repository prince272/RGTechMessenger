using FluentValidation;
using RGTechMessenger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Conversations
{
    public class CreatePrivateConversationForm
    {
        public long UserId { get; set; }
    }

    public class CreatePrivateConversationFormValidator : AbstractValidator<CreatePrivateConversationForm>
    {
        public CreatePrivateConversationFormValidator()
        {
        }
    }
}
