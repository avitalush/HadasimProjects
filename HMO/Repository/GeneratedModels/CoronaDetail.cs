using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class CoronaDetail
{
    public int UserId { get; set; }

    public DateTime? PositiveAnswerDate { get; set; }

    public DateTime? RecoveryDate { get; set; }

    public int Code { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; } 
}
