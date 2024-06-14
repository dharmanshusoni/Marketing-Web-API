using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class CompanyClient
{
    public int CompanyClientId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string CompanyDetail { get; set; } = null!;

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public string? ProfileImageUrl { get; set; }

    public virtual CityMaster City { get; set; } = null!;

    public virtual CountryMaster Country { get; set; } = null!;

    public virtual StateMaster State { get; set; } = null!;
}
