using Microsoft.Extensions.Options;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Medias
{
    public class UploadMediaByFileChunkForm : UploadMediaByFileContentForm
    {
        public long Offset { get; set; }
    }

    public class UploadMediaByFileChunkFormValidator : UploadMediaByFileContentFormValidator<UploadMediaByFileChunkForm>
    {
        public UploadMediaByFileChunkFormValidator(IOptions<MediaServiceOptions> mediaServiceOptions) : base(mediaServiceOptions)
        {
        }
    }
}
