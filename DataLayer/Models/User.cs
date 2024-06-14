using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class User
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
