﻿using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class GenderMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Actiive { get; set; }
}
