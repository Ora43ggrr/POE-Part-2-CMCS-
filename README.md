# POE Part 2
Contract Monthly Claim System (CMCS)

A secure web application for managing monthly contract claims, built with ASP.NET Core MVC.

 Features

- **User Authentication & Role Management** - Secure login with different access levels
- **Claim Submission** - Easy form-based claim submission with document upload
- **Role-Based Dashboard** - Personalized dashboards for Lecturers, Coordinators, and Managers
- **Claim Review Workflow** - Multi-step approval process with status tracking
- **Document Management** - Secure file upload and management
- **Real-time Notifications** - In-app alerts for important updates
- **Audit Trail** - Complete history of all claim status changes

 User Roles

- **Lecturers** - Submit claims, view personal history, upload documents
- **Program Coordinators** - Review and approve claims, manage workflow
- **Academic Managers** - Final approval, system oversight, reporting

Tech Stack

- **Backend:** ASP.NET Core 8.0 MVC
- **Frontend:** HTML5, CSS3, JavaScript, Bootstrap 5.3
- **Authentication:** ASP.NET Core Identity
- **Database:** SQL Server with Entity Framework Core

 Project Structure
CMCS/
├── Controllers/ # MVC Controllers
├── Models/ # Data models and view models
├── Views/ # Razor pages
├── Services/ # Business logic
├── wwwroot/ # Static files (CSS, JS, images)
└── Program.cs # Application entry point
