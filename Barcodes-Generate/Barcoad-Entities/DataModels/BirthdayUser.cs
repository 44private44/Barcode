using System;
using System.Collections.Generic;

namespace Barcoad_Entities.DataModels;

public partial class BirthdayUser
{
    public string? UserName { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? UserEmail { get; set; }

    public long UserId { get; set; }
}
