using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkyProject1;
//OBJECTIVE 4
public class EmployeeQueries
{
    private readonly ApplicationDbContext _context;

    public EmployeeQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. Get all employees with employee number, name, department, and salary
    public void GetAllEmployees()
    {
        var employees = _context.Employees
            .Include(e => e.Department)
            .Select(e => new
            {
                e.EmployeeNumber,
                e.EmployeeName,
                DepartmentName = e.Department.DepartmentName,
                e.Salary
            }).ToList();

        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.EmployeeNumber} - {emp.EmployeeName} - {emp.DepartmentName} - ${emp.Salary}");
        }
    }

    // 2. Get employee details by unique employee number
    

    // 3. Get all employees with one or more pending vacation requests
    public void GetEmployeesWithPendingRequests()
    {
        var employees = _context.Employees
            .Where(e => e.VacationRequests.Any(vr => vr.RequestStateId == 1))
            .Select(e => new { e.EmployeeNumber, e.EmployeeName })
            .ToList();

        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.EmployeeNumber} - {emp.EmployeeName} has pending requests.");
        }
    }

    // 4. Get all approved vacation history for an employee
    public void GetEmployeeVacationHistory(string employeeNumber)
    {
        var vacationHistory = _context.VacationRequests
            .Where(vr => vr.EmployeeNumber == employeeNumber && vr.RequestStateId == 2)
            .Select(vr => new
            {
                vr.VacationTypeCode,
                vr.Description,
                Duration = (vr.EndDate - vr.StartDate).TotalDays + " days",
                ApprovedBy = _context.Employees
                                     .Where(emp => emp.EmployeeNumber == vr.ApprovedBy)
                                     .Select(emp => emp.EmployeeName)
                                     .FirstOrDefault() ?? "N/A"
            })
            .ToList();

        if (!vacationHistory.Any())
        {
            Console.WriteLine("No approved vacation history found for this employee.");
            return;
        }

        foreach (var record in vacationHistory)
        {
            Console.WriteLine($"Vacation Type: {record.VacationTypeCode}\nDescription: {record.Description}\nDuration: {record.Duration}\nApproved By: {record.ApprovedBy}\n---");
        }
    }


    // 5. Get all pending vacation requests that need approval
    public void GetPendingRequestsForApproval()
    {
        var pendingRequests = _context.VacationRequests
            .Include(vr => vr.Employee)
            .Where(vr => vr.RequestStateId == 1)
            .Select(vr => new
            {
                vr.Description,
                vr.Employee.EmployeeNumber,
                vr.Employee.EmployeeName,
                vr.SubmissionDate,
                Duration = (vr.EndDate - vr.StartDate).TotalDays + " days",
                vr.StartDate,
                vr.EndDate,
                vr.Employee.Salary
            }).ToList();

        foreach (var request in pendingRequests)
        {
            Console.WriteLine($"Request Description: {request.Description}\nEmployee: {request.EmployeeNumber} - {request.EmployeeName}\nSubmitted On: {request.SubmissionDate:yyyy-MM-dd}\nDuration: {request.Duration}\nStart Date: {request.StartDate:yyyy-MM-dd}\nEnd Date: {request.EndDate:yyyy-MM-dd}\nSalary: ${request.Salary}\n---");
        }
    }

    public void GetEmployeeByNumber(string employeeNumber)
    {
        var employee = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .FirstOrDefault(e => e.EmployeeNumber == employeeNumber);

        if (employee == null)
        {
            Console.WriteLine("❌ Employee not found!");
            return;
        }

        // Fetch the manager's name if ReportedTo is not null
        string managerName = "N/A";
        if (!string.IsNullOrEmpty(employee.ReportedTo))
        {
            var manager = _context.Employees
                .FirstOrDefault(m => m.EmployeeNumber == employee.ReportedTo);
            managerName = manager?.EmployeeName ?? "N/A";
        }

        Console.WriteLine("\n===== Employee Details =====");
        Console.WriteLine($"Employee Number: {employee.EmployeeNumber}");
        Console.WriteLine($"Employee Name: {employee.EmployeeName}");
        Console.WriteLine($"Department Name: {employee.Department?.DepartmentName ?? "N/A"}");
        Console.WriteLine($"Position Name: {employee.Position?.PositionName ?? "N/A"}");
        Console.WriteLine($"Reported To: {managerName}");
        Console.WriteLine($"Total Vacation Days Left: {employee.VacationDaysLeft}");
    }




}
