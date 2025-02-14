using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using SkyProject1;

class Program
{
    static void Main()
    {
        using var context = new ApplicationDbContext();
        var vacationService = new VacationService(context);
        var employeeQueries = new EmployeeQueries(context);
        


        // Ensure essential data exists
        if (!context.Departments.Any()) AddDepartments(context);
        if (!context.Positions.Any()) AddPositions(context);
        if (!context.Employees.Any()) AddEmployees(context);
        if (!context.RequestStates.Any()) AddRequestStates(context);

        // CRUD Operations Menu
        while (true)
        {
            Console.WriteLine("\n===== Vacation Management Operations Menu =====");
            Console.WriteLine("1. Add Departments, Positions, Employees");
            Console.WriteLine("2. Update Employee Info");
            Console.WriteLine("3. Approve/Decline Vacation Request");
            Console.WriteLine("4. Submit Vacation Request");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. Get all Employees Info");
            Console.WriteLine("7. Get Employees with pending requests");
            Console.WriteLine("8. Get all approved vacation history for an employee");
            Console.WriteLine("9. Get all pending vacation requests that need approval");
            Console.WriteLine("10. Get Employee by Number");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            


            switch (choice)
            {
                case "1": AddDepartments(context); AddPositions(context); AddEmployees(context); break;
                case "2":
                    Console.Write("Enter Employee Number to update: ");
                    string empNum = Console.ReadLine();
                    UpdateEmployeeInfo(context, empNum);
                    break;
                case "3":
                    vacationService.ProcessPendingRequests();
                    break;
                case "4":
                    vacationService.SubmitVacationRequest();
                    break;
                case "5":
                    return;
                case "6":
                    employeeQueries.GetAllEmployees();
                    break;
                case "7":
                    employeeQueries.GetEmployeesWithPendingRequests();
                    break;
                case "8":
                    Console.Write("Enter Employee Number: ");
                    string NUMNUM = Console.ReadLine();
                    employeeQueries.GetEmployeeVacationHistory(NUMNUM);
                    break;
                case "9":
                    employeeQueries.GetPendingRequestsForApproval();
                    break;
                case"10":
                    Console.Write("Enter Employee Number: ");
                    string NANA = Console.ReadLine();
                    employeeQueries.GetEmployeeByNumber(NANA);
                    break;

                default:
                    Console.WriteLine("❌ Invalid choice. Try again.");
                    break;
            }
        }
    }

    public static void AddDepartments(ApplicationDbContext context)
    {
        var departments = Enumerable.Range(1, 20)
            .Select(i => new Department { DepartmentName = $"Department {i}" })
            .ToList();
        context.Departments.AddRange(departments);
        context.SaveChanges();
        Console.WriteLine("✅ Departments Added!");
    }

    public static void AddPositions(ApplicationDbContext context)
    {
        var positions = Enumerable.Range(1, 20)
            .Select(i => new Position { PositionName = $"Position {i}" })
            .ToList();
        context.Positions.AddRange(positions);
        context.SaveChanges();
        Console.WriteLine("✅ Positions Added!");
    }

    public static void AddEmployees(ApplicationDbContext context)
    {
        var employees = new List<Employee>
        {
        new Employee { EmployeeNumber = "EMP003", EmployeeName = " Smith", DepartmentId = 2, PositionId = 3,GenderCode="M",ReportedTo="Elisa", Salary = 5500, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP004", EmployeeName = " Johnson", DepartmentId = 3, PositionId = 2,GenderCode="M",ReportedTo="Nancy", Salary = 6000, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP005", EmployeeName = " Brown", DepartmentId = 4, PositionId = 5,GenderCode="F",ReportedTo="Hala", Salary = 6500, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP006", EmployeeName = " White", DepartmentId = 5, PositionId = 2,GenderCode="F", ReportedTo="Moh",Salary = 7000, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP008", EmployeeName = "Jane Smith", DepartmentId = 2, PositionId = 3,GenderCode="M",ReportedTo="Elisa", Salary = 5500, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP009", EmployeeName = "Mike Johnson", DepartmentId = 3, PositionId = 2,GenderCode="F",ReportedTo="Nancy", Salary = 6000, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP010", EmployeeName = "Alice Brown", DepartmentId = 4, PositionId = 5,GenderCode="M",ReportedTo="Hala", Salary = 6500, VacationDaysLeft = 24 },
        new Employee { EmployeeNumber = "EMP011", EmployeeName = "Bob White", DepartmentId = 5, PositionId = 2,GenderCode="M", ReportedTo="Moh",Salary = 7000, VacationDaysLeft = 24 },

        };

        context.Employees.AddRange(employees);
        context.SaveChanges();
        Console.WriteLine("✅ Employees Added!");
    }

    public static void UpdateEmployeeInfo(ApplicationDbContext context, string employeeNumber)
    {
        var employee = context.Employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);
        if (employee == null)
        {
            Console.WriteLine("❌ Employee Not Found!");
            return;
        }

        Console.WriteLine($"Updating details for {employee.EmployeeName}");
        Console.Write("Enter new Department Id: ");
        employee.DepartmentId = int.Parse(Console.ReadLine());

        Console.Write("Enter new Position Id: ");
        employee.PositionId = int.Parse(Console.ReadLine());

        Console.Write("Enter new Salary: ");
        employee.Salary = decimal.Parse(Console.ReadLine());

        context.SaveChanges();
        Console.WriteLine("✅ Employee details updated!");
    }

    public static void AddRequestStates(ApplicationDbContext context)
    {
        var requestStates = new List<RequestState>
        {
            new RequestState { StateName = "Submitted" },
            new RequestState { StateName = "Approved" },
            new RequestState { StateName = "Declined" }
        };

        context.RequestStates.AddRange(requestStates);
        context.SaveChanges();
        Console.WriteLine("✅ Request States Added!");
    }
}
