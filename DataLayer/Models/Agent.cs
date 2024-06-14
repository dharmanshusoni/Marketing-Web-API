using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string AgentDetail { get; set; } = null!;

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public string? ProfileImageUrl { get; set; }
    public string? agentType { get; set; }
}
