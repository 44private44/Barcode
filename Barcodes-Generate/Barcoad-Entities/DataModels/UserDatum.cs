using System;
using System.Collections.Generic;

namespace Barcoad_Entities.DataModels;

public partial class UserDatum
{
    public long UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? RequestToken { get; set; }

    public DateTime? ExpirationTime { get; set; }
}
