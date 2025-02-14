# Vacation Management System
A C# project utilizing Entity Framework Core and LINQ for handling employee management and vacation requests in a company.

# Features
Employee Management: View and update employee details, including name, department, position, and reporting structure.
Vacation Requests: Employees can submit, view, approve, or decline vacation requests.
Database Design: Uses Code-First approach with EF Core and migrations to create the database.
CRUD Operations: Full implementation for managing employees and vacation requests.
LINQ Queries: Efficient queries to retrieve employee information and vacation request statuses.
# Objectives
1. Database Design & Migration
Implements Code-First approach using EF Core.
Defines entities like Employee, Department, Position, VacationRequest, etc.
2. CRUD Operations
Add, update, delete, and retrieve employees and vacation requests.
Update employee vacation balance upon approval of requests.
3. Vacation Request Workflow
Employees submit vacation requests while ensuring no overlapping dates.
Managers approve or decline requests.
4. LINQ-Based Queries
Retrieve employee details by their unique ID.
Display employees with pending vacation requests.
Fetch vacation history with details such as type, duration, and approver.
List pending vacation requests requiring action.
# Technologies Used
C# (.NET Core)
Entity Framework Core (EF Core)
LINQ
SQL Server (Database)
