using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkyProject1;
//OBJECTIVE 3
public class VacationService
{
    private readonly ApplicationDbContext _context;

    public VacationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SubmitVacationRequest()
    {
        Console.Write("Enter Employee Number: ");
        string employeeNumber = Console.ReadLine();

        var employee = _context.Employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);
        if (employee == null)
        {
            Console.WriteLine("❌ Employee not found!");
            return;
        }

        Console.Write("Enter vacation description (max 100 characters): ");
        string description = Console.ReadLine();
        if (description.Length > 100)
        {
            Console.WriteLine("❌ Description too long.");
            return;
        }

        Console.Write("Enter Vacation Type Code: ");
        string vacationTypeCode = Console.ReadLine();

        var vacationType = _context.VacationTypes.FirstOrDefault(vt => vt.VacationTypeCode == vacationTypeCode);
        if (vacationType == null)
        {
            Console.WriteLine($"❌ Vacation Type '{vacationTypeCode}' does not exist.");
            return;
        }

        Console.Write("Enter vacation start date (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.WriteLine("❌ Invalid date format.");
            return;
        }

        Console.Write("Enter vacation end date (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < startDate)
        {
            Console.WriteLine("❌ Invalid end date.");
            return;
        }

        if (_context.VacationRequests.Any(vr => vr.EmployeeNumber == employeeNumber &&
            vr.RequestStateId == 1 && ((startDate >= vr.StartDate && startDate <= vr.EndDate) ||
            (endDate >= vr.StartDate && endDate <= vr.EndDate))))
        {
            Console.WriteLine("❌ Overlapping vacation request found.");
            return;
        }

        var vacationRequest = new VacationRequest
        {
            EmployeeNumber = employeeNumber,
            Description = description,
            VacationTypeCode = vacationTypeCode,
            StartDate = startDate,
            EndDate = endDate,
            TotalVacationDays = (endDate - startDate).Days + 1,
            RequestStateId = 1,
            SubmissionDate = DateTime.Now
        };

        _context.VacationRequests.Add(vacationRequest);
        _context.SaveChanges();
        Console.WriteLine("✅ Vacation request submitted!");
    }

    public void ProcessPendingRequests()
    {
        var pendingRequests = _context.VacationRequests
            .Where(vr => vr.RequestStateId == 1)
            .ToList();

        if (!pendingRequests.Any())
        {
            Console.WriteLine("✅ No pending vacation requests.");
            return;
        }

        Console.WriteLine("\n===== Pending Requests =====");
        foreach (var pendingRequest in pendingRequests)
        {
            Console.WriteLine($"Request ID: {pendingRequest.RequestId}, Employee: {pendingRequest.EmployeeNumber}");
        }

        Console.Write("Enter Your Name: ");
        string employeeName = Console.ReadLine().Trim();

        if (string.IsNullOrWhiteSpace(employeeName))
        {
            Console.WriteLine("❌ Invalid input. Employee name cannot be empty.");
            return;
        }

        Console.Write("Enter Request ID to process: ");
        if (!int.TryParse(Console.ReadLine(), out int requestId))
        {
            Console.WriteLine("❌ Invalid input.");
            return;
        }

        var request = _context.VacationRequests.Include(vr => vr.Employee)
                                               .FirstOrDefault(vr => vr.RequestId == requestId);
        if (request == null)
        {
            Console.WriteLine("❌ Request not found.");
            return;
        }

        Console.Write("Approve (A) or Decline (D)? ");
        string action = Console.ReadLine().ToUpper();

        if (action == "A")
        {
            Approve(request, employeeName);
        }
        else if (action == "D")
        {
            Decline(request, employeeName);
        }
        else
        {
            Console.WriteLine("❌ Invalid action. Please enter 'A' for Approve or 'D' for Decline.");
        }
    }


    private void Approve(VacationRequest request, string approverName)
    {
        if (request.Employee.VacationDaysLeft >= request.TotalVacationDays)
        {
            request.Employee.VacationDaysLeft -= request.TotalVacationDays;
            request.RequestStateId = 2; // Approved
            request.ApprovedBy = approverName;
            _context.SaveChanges();
            Console.WriteLine($"✅ Request Approved by {approverName}!");
        }
        else
        {
            Console.WriteLine("❌ Not enough vacation days available.");
        }
    }

    private void Decline(VacationRequest request, string declinerName)
    {
        request.RequestStateId = 3; // Declined
        request.DeclinedBy = declinerName;
        _context.SaveChanges();
        Console.WriteLine($"✅ Request Declined by {declinerName}.");
    }

    

}