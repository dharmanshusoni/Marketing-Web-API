using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public int? Industry { get; set; }

    public decimal Experience { get; set; }

    public int ExpectedSalary { get; set; }

    public int QualificationId { get; set; }

    public string? Qualification { get; set; }

    public int GenderId { get; set; }

    public string? LanguagesIds { get; set; }

    public string? SkillsIds { get; set; }

    public string? OtherSkills { get; set; }

    public string Email { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }
}
