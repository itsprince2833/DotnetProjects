using System;
using System.Collections.Generic;

namespace GrpcServer.Models;

public partial class LinkType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}
