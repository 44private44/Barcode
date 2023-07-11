using System;
using System.Collections.Generic;

namespace Barcoad_Entities.DataModels;

public partial class FriendsDatum
{
    public long FriendId { get; set; }

    public string? Name { get; set; }

    public int? Order { get; set; }
}
