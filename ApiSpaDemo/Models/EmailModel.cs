using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSpaDemo.Models
{
    public class EmailModel
    {
        public string? EmailToId { get; set; }
        public string? EmailToNombre { get; set; }
        public string? EmailAsunto { get; set; }
        public string? EmailCuerpo { get; set; }
    }
}