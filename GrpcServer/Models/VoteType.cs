﻿using System;
using System.Collections.Generic;

namespace GrpcServer.Models;

public partial class VoteType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
