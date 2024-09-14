using System;
using System.Collections.Generic;

namespace ApiSpaDemo.Models;

public partial class Noticium
{
    public int NoticiaId { get; set; }

    public string Titulo { get; set; } = null!;

    public DateOnly FechaPublicacion { get; set; }

    public string? RutaImagen { get; set; }

    public string RutaPdf { get; set; } = null!;
}
