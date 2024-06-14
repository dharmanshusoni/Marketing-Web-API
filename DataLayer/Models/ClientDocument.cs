using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class ClientDocument
{
    public int DocumentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public int? Size { get; set; }

    public int ClientId { get; set; }

    public string? Path { get; set; }

    public DateTime? UploadDate { get; set; }
}
