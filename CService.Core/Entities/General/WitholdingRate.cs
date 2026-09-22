using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CService.Core.Entities.General
{
    public class WitholdingRate : BaseEntity
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Rate { get; set; } /* Örneğin 3/10 için 3 girilir. */
        [NotMapped]
        public string Display => $"{Code} - {Description} ({Rate}/10)";
    }
}
