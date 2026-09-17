using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CService.Core.Entities.General
{
    public class VatRate : BaseEntity
    {
        public decimal Rate { get; set; }

        [NotMapped]
        public string Description => $"%{Rate} KDV";
    }
}
