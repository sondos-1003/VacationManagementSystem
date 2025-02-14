using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkyProject1
{
    public class Employee
    {
        [Key, MaxLength(6)]
        public string EmployeeNumber { get; set; } // PK (not identity)

        [Required, MaxLength(20)]
        public string EmployeeName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Required, MaxLength(1)]
        public string GenderCode { get; set; } // (M/F)

        [MaxLength(6)]
        public string ReportedTo { get; set; } // Nullable FK to Employee (Manager)

        [Range(0, 24)]
        public int VacationDaysLeft { get; set; } = 24; // Default to 24

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }




        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();

        // ✅ Required by EF Core
        public Employee() { }

        // ✅ Your custom constructor for manual object creation
        public Employee(string employeeNumber, string name, int departmentId, int positionId, string genderCode,string reporto,int vacatioleft,decimal salo)
        {
            EmployeeNumber = employeeNumber;
            EmployeeName = name;
            DepartmentId = departmentId;
            PositionId = positionId;
            GenderCode = genderCode;
            ReportedTo = reporto;
            VacationDaysLeft =vacatioleft= 24;
            Salary = salo;
        }

    }

    }

