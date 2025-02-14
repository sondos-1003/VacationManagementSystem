using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }  // PK, Identity

        [Required, MaxLength(30)]
        public string PositionName { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
