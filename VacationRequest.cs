using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class VacationRequest
    {
        [Key]
        public int RequestId { get; set; } // PK, Identity
        
        [Required]
        public DateTime SubmissionDate { get; set; }

        [Required, MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("VacationType")]
        [MaxLength(1)]
        public string VacationTypeCode { get; set; }
        public VacationType VacationType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TotalVacationDays { get; set; }

        [Required]
        public int RequestStateId { get; set; }
        //public RequestState RequestState { get; set; }

        [MaxLength(6)]
        public string? ApprovedBy { get; set; }

        [MaxLength(6)]
        public string? DeclinedBy { get; set; }
        public RequestState RequestState { get; set; }
        [ForeignKey("Employee")]
        [MaxLength(6)]
        public string EmployeeNumber { get; set; }
        public Employee Employee { get; set; }
    }
}
