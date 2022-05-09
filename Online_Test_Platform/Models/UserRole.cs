using System;
using System.Collections.Generic;

namespace Online_Test_Platform.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserInfos = new HashSet<UserInfo>();
        }

        public int RoleId { get; set; }
        public string? Discription { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
