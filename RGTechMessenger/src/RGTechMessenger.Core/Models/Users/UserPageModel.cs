using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Users
{
    public class UserPageModel : UserListModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
