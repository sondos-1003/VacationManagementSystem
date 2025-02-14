using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyProject1
{
    public class Department
    {
        public int DepartmentId { get; set; }  // PK, Identity

        [Required, MaxLength(50)]
        public string DepartmentName { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
