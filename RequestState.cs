using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class RequestState
    {
        [Key]
        public int StateId { get; set; } // PK, Identity

        [Required, MaxLength(10)]
        public string StateName { get; set; } // Submitted, Approved, Declined
        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    
}
}

