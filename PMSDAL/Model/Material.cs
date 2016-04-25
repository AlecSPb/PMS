using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class Material
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public double Density { get; set; }

        public double  MeltingPoint { get; set; }

        public double BoilingPoint { get; set; }

        public string SpecialProperty { get; set; }

        public DateTime UpdateTime { get; set; }

        public string InformationSource { get; set; }

        public string Remark { get; set; }

    }
}
