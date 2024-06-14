using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class CityMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StateId { get; set; }

    public virtual ICollection<CompanyClient> CompanyClients { get; } = new List<CompanyClient>();

    public virtual StateMaster? State { get; set; }
}
