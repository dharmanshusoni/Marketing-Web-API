using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class StateMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CountryId { get; set; }

    public virtual ICollection<CityMaster> CityMasters { get; } = new List<CityMaster>();

    public virtual ICollection<CompanyClient> CompanyClients { get; } = new List<CompanyClient>();

    public virtual CountryMaster? Country { get; set; }
}
