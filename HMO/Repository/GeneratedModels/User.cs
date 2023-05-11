using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? UserTz { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int? HouseNumber { get; set; }

    public string? Phon { get; set; }

    public string? HousePhon { get; set; }


    [JsonIgnore]
    public virtual ICollection<CoronaDetail> CoronaDetails { get;  } = new List<CoronaDetail>();

    public virtual ICollection<VaccineDetail> VaccineDetails { get; } = new List<VaccineDetail>();
}
