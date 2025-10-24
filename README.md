# POE Part 2
# Contract Monthly Claim System (CMCS)

A secure web application for managing monthly contract claims, built with ASP.NET Core MVC.

## 🚀 Features

- **User Authentication & Role Management** - Secure login with different access levels
- **Claim Submission** - Easy form-based claim submission with document upload
- **Role-Based Dashboard** - Personalized dashboards for Lecturers, Coordinators, and Managers
- **Claim Review Workflow** - Multi-step approval process with status tracking
- **Document Management** - Secure file upload and management
- **Real-time Notifications** - In-app alerts for important updates
- **Audit Trail** - Complete history of all claim status changes
- **Responsive Design** - Mobile-friendly interface that works on all devices
- **Email Integration** - System notifications via email (optional)

## 👥 User Roles & Permissions

### **Lecturers**
- Submit new monthly claims with supporting documents
- View personal claim history and status
- Upload additional supporting documents
- Track claim approval progress
- Receive notifications about claim updates

### **Program Coordinators**
- Review claims submitted by lecturers
- Approve or reject claims with comments
- Request additional information from lecturers
- Monitor claim workflow in their department
- Manage document verification

### **Academic Managers**
- Final approval authority for all claims
- Oversight of entire claim process
- Generate reports and analytics
- System configuration and user management
- Audit trail review and compliance monitoring

## 🛠️ Technology Stack

### **Backend**
- ASP.NET Core 8.0 MVC
- Entity Framework Core
- ASP.NET Core Identity
- Dependency Injection
- Repository Pattern

### **Frontend**
- HTML5 with Razor Syntax
- CSS3 with Custom Styling
- Bootstrap 5.3 Framework
- JavaScript for Interactive Features
- Responsive Grid System

### **Security**
- Role-Based Authorization
- Password Hashing
- XSS Protection
- CSRF Tokens
- Secure File Upload Validation

## 🏗️ Architecture Overview

### **MVC Pattern Implementation**
- **Models**: Data entities, view models, and validation logic
- **Views**: Razor pages with Bootstrap layout and responsive design
- **Controllers**: Handle HTTP requests, business logic coordination, and view rendering

### **Service Layer Architecture**
- Business logic separated from controllers
- Reusable services for claims, notifications, and file handling
- Interface-based design for testability and flexibility

### **Database Design**
- SQL Server with Entity Framework Code-First
- Relationships between Users, Claims, Documents, and History
- Indexed columns for optimal performance
- Audit trails for compliance and tracking

## ⚡ Getting Started

### **Prerequisites**
- .NET 8.0 SDK
- SQL Server 2019 or later
- Visual Studio 2022 or VS Code
- Git for version control

### **Installation Steps**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/ST10443081/PROG6212-POE-Part2.git
   cd PROG6212-POE-Part2
