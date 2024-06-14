using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class CountryMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CountryCode { get; set; }

    public virtual ICollection<CompanyClient> CompanyClients { get; } = new List<CompanyClient>();

    public virtual ICollection<StateMaster> StateMasters { get; } = new List<StateMaster>();
}
