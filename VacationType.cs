using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class VacationType
    {
        [Key, MaxLength(1)]
        public string VacationTypeCode { get; set; } // PK (S, U, A, O, B)

        [Required, MaxLength(20)]
        public string VacationTypeName { get; set; }
        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    }
}
