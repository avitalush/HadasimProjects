using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class VaccineDetail
{
    public int VaccinationId { get; set; }

    public int? UserId { get; set; }

    public string? Manufacturer { get; set; }

    public DateTime? ReceivingVaccine { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
