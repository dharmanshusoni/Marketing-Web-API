using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }

    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserFirstName { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public string UserEmailId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? UserImage { get; set; }

        public string? UserPhone { get; set; }

        public int UserTypeId { get; set; }

    }
}
