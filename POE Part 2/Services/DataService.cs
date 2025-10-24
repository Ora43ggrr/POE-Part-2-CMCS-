using POE_Part_2.Models;

namespace POE_Part_2.Services
{
    public class DataService
    {
        // In-memory data storage (replace with database in production)
        public List<User> Users { get; set; } = new List<User>();
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public List<Document> Documents { get; set; } = new List<Document>();

        public DataService()
        {
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Clear existing data to avoid duplicates
            Users.Clear();
            Claims.Clear();
            Documents.Clear();

            // Add sample users with different roles
            Users.Add(new User
            {
                Id = 1,
                Username = "Dr. Sarah Johnson",
                Email = "lecturer@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            Users.Add(new User
            {
                Id = 2,
                Username = "Prof. Michael Brown",
                Email = "coordinator@university.com",
                Password = "password123",
                Role = "ProgrammeCoordinator"
            });

            Users.Add(new User
            {
                Id = 3,
                Username = "Dr. Emily Wilson",
                Email = "manager@university.com",
                Password = "password123",
                Role = "AcademicManager"
            });

            // Add additional lecturers for testing
            Users.Add(new User
            {
                Id = 4,
                Username = "Dr. Robert Davis",
                Email = "robert@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            Users.Add(new User
            {
                Id = 5,
                Username = "Prof. Jennifer Lee",
                Email = "jennifer@university.com",
                Password = "password123",
                Role = "Lecturer"
            });

            // Add sample claims showing the complete workflow - ALL NEW CLAIMS START AS "SUBMITTED"
            Claims.Add(new Claim
            {
                Id = 1,
                ClaimId = "CLM-2025-001",
                LecturerName = "Dr. Sarah Johnson",
                Month = 9,
                Year = 2025,
                HoursWorked = 45,
                HourlyRate = 350,
                TotalAmount = 15750,
                Description = "September teaching hours - Advanced Mathematics",
                Status = "Submitted", // PENDING - waiting for review
                SubmissionDate = DateTime.Now.AddDays(-1)
            });

            Claims.Add(new Claim
            {
                Id = 2,
                ClaimId = "CLM-2025-002",
                LecturerName = "Dr. Sarah Johnson",
                Month = 8,
                Year = 2025,
                HoursWorked = 40,
                HourlyRate = 350,
                TotalAmount = 14000,
                Description = "August teaching hours - Statistics",
                Status = "Approved", // APPROVED by coordinator/manager
                SubmissionDate = DateTime.Now.AddDays(-10)
            });

            Claims.Add(new Claim
            {
                Id = 3,
                ClaimId = "CLM-2025-003",
                LecturerName = "Dr. Sarah Johnson",
                Month = 7,
                Year = 2025,
                HoursWorked = 38,
                HourlyRate = 350,
                TotalAmount = 13300,
                Description = "July teaching hours - Calculus",
                Status = "Rejected", // REJECTED by coordinator/manager
                SubmissionDate = DateTime.Now.AddDays(-20)
            });

            // Add claims from other lecturers showing pending status
            Claims.Add(new Claim
            {
                Id = 4,
                ClaimId = "CLM-2025-004",
                LecturerName = "Dr. Robert Davis",
                Month = 9,
                Year = 2025,
                HoursWorked = 42,
                HourlyRate = 380,
                TotalAmount = 15960,
                Description = "September teaching hours - Computer Science",
                Status = "Submitted", // PENDING - waiting for review
                SubmissionDate = DateTime.Now.AddDays(-2)
            });

            Claims.Add(new Claim
            {
                Id = 5,
                ClaimId = "CLM-2025-005",
                LecturerName = "Prof. Jennifer Lee",
                Month = 9,
                Year = 2025,
                HoursWorked = 39,
                HourlyRate = 400,
                TotalAmount = 15600,
                Description = "September teaching hours - Business Management",
                Status = "Submitted", // PENDING - waiting for review
                SubmissionDate = DateTime.Now.AddDays(-3)
            });

            Claims.Add(new Claim
            {
                Id = 6,
                ClaimId = "CLM-2025-006",
                LecturerName = "Dr. Robert Davis",
                Month = 8,
                Year = 2025,
                HoursWorked = 41,
                HourlyRate = 380,
                TotalAmount = 15580,
                Description = "August teaching hours - Data Structures",
                Status = "Approved", // APPROVED
                SubmissionDate = DateTime.Now.AddDays(-12)
            });

            Claims.Add(new Claim
            {
                Id = 7,
                ClaimId = "CLM-2025-007",
                LecturerName = "Prof. Jennifer Lee",
                Month = 8,
                Year = 2025,
                HoursWorked = 37,
                HourlyRate = 400,
                TotalAmount = 14800,
                Description = "August teaching hours - Marketing Principles",
                Status = "Paid", // PAID - final status
                SubmissionDate = DateTime.Now.AddDays(-15)
            });

            // Add sample documents for testing - some linked to claims, some standalone
            Documents.Add(new Document
            {
                Id = 1,
                FileName = "September_Timesheet.pdf",
                StoredFileName = "doc1.pdf",
                DocumentType = "Supporting Document",
                Description = "Supporting document for CLM-2025-001",
                FileSize = 2400000,
                UploadDate = DateTime.Now.AddDays(-2),
                ClaimId = 1 // Linked to claim 1
            });

            Documents.Add(new Document
            {
                Id = 2,
                FileName = "Qualification_Certificate.jpg",
                StoredFileName = "doc2.jpg",
                DocumentType = "Qualification",
                Description = "PhD Qualification Certificate",
                FileSize = 1800000,
                UploadDate = DateTime.Now.AddDays(-5)
            });

            Documents.Add(new Document
            {
                Id = 3,
                FileName = "Contract_Agreement.pdf",
                StoredFileName = "doc3.pdf",
                DocumentType = "Contract",
                Description = "Employment Contract 2025",
                FileSize = 3200000,
                UploadDate = DateTime.Now.AddDays(-10)
            });

            Documents.Add(new Document
            {
                Id = 4,
                FileName = "August_Invoice.xlsx",
                StoredFileName = "doc4.xlsx",
                DocumentType = "Supporting Document",
                Description = "Supporting document for CLM-2025-002",
                FileSize = 850000,
                UploadDate = DateTime.Now.AddDays(-8),
                ClaimId = 2 // Linked to claim 2
            });

            Documents.Add(new Document
            {
                Id = 5,
                FileName = "Student_Feedback.pdf",
                StoredFileName = "doc5.pdf",
                DocumentType = "Supporting Document",
                Description = "Supporting document for CLM-2025-004",
                FileSize = 1200000,
                UploadDate = DateTime.Now.AddDays(-1),
                ClaimId = 4 // Linked to claim 4
            });
        }

        // Helper methods for data operations
        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id); // Find user by ID
        }

        public User GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email); // Find user by email
        }

        public List<Claim> GetClaimsByLecturer(string lecturerName)
        {
            return Claims.Where(c => c.LecturerName == lecturerName).ToList(); // Get claims by lecturer name
        }

        public List<Claim> GetPendingClaims()
        {
            return Claims.Where(c => c.Status == "Submitted").ToList(); // Get all pending claims
        }

        public List<Claim> GetClaimsByStatus(string status)
        {
            return Claims.Where(c => c.Status == status).ToList(); // Get claims by status
        }

        public Claim GetClaimById(int id)
        {
            return Claims.FirstOrDefault(c => c.Id == id); // Get claim by ID
        }

        public bool UpdateClaimStatus(int claimId, string status)
        {
            var claim = GetClaimById(claimId); // Find claim by ID
            if (claim != null) // If claim exists
            {
                claim.Status = status; // Update status
                return true; // Return success
            }
            return false; // Return failure
        }

        public void AddDocument(Document document)
        {
            document.Id = Documents.Count + 1; // Set new document ID
            Documents.Add(document); // Add document to list
        }

        public List<Document> GetDocumentsByType(string documentType)
        {
            return Documents.Where(d => d.DocumentType == documentType).ToList(); // Get documents by type
        }

        public List<Document> GetDocumentsByClaimId(int claimId)
        {
            return Documents.Where(d => d.ClaimId == claimId).ToList(); // Get documents linked to specific claim
        }

        public int GetPendingClaimsCount()
        {
            return Claims.Count(c => c.Status == "Submitted"); // Count pending claims
        }

        public decimal GetTotalApprovedAmount()
        {
            return Claims.Where(c => c.Status == "Approved" || c.Status == "Paid") // Get approved or paid claims
                        .Sum(c => c.TotalAmount); // Sum total amount
        }

        public int GetTotalClaimsCount()
        {
            return Claims.Count; // Get total number of claims
        }

        public int GetClaimsCountByStatus(string status)
        {
            return Claims.Count(c => c.Status == status); // Get count of claims by status
        }

        public List<Claim> GetRecentClaims(int count = 5)
        {
            return Claims.OrderByDescending(c => c.SubmissionDate) // Order by submission date
                        .Take(count) // Take specified number
                        .ToList(); // Return as list
        }

        // Method to generate next claim ID
        public string GenerateNextClaimId(int year)
        {
            var nextNumber = Claims.Count + 1; // Calculate next sequence number
            return $"CLM-{year}-{nextNumber.ToString("D3")}"; // Generate claim ID
        }

        // Method to validate file upload
        public bool ValidateFileUpload(IFormFile file, out string errorMessage)
        {
            errorMessage = null; // Initialize error message

            if (file == null || file.Length == 0) // Check if file exists
            {
                errorMessage = "Please select a file to upload"; // Set error message
                return false; // Return validation failure
            }

            if (file.Length > 5 * 1024 * 1024) // Check file size (5MB limit)
            {
                errorMessage = "File size exceeds 5MB limit"; // Set error message
                return false; // Return validation failure
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".png", ".docx", ".xlsx" }; // Allowed file extensions
            var fileExtension = Path.GetExtension(file.FileName).ToLower(); // Get file extension
            if (!allowedExtensions.Contains(fileExtension)) // Check if extension is allowed
            {
                errorMessage = "File type not supported. Please upload PDF, JPG, PNG, DOCX, or XLSX files."; // Set error message
                return false; // Return validation failure
            }

            return true; // Return validation success
        }
    }
}