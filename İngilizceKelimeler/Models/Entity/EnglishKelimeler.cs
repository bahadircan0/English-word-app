using System;
using System.Collections.Generic;

namespace İngilizceKelimeler.Models.Entity;

public partial class EnglishKelimeler
{
    
    public int KelimeId { get; set; }

    public string? Kelimeİng { get; set; }

    public string? KelimeTur { get; set; }

    public bool? KelimeVisible { get; set; }

    public int? KelimeBilmeSayisi { get; set; }

    public int? KelimeBilememeSayisi { get; set; }
 
}
