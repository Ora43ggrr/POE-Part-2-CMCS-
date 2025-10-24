namespace POE_Part_2.Models
{
    public class Claim
    {
        public int Id { get; set; } // Claim ID
        public string ClaimId { get; set; } // Formatted claim ID (e.g., CLM-2025-001)
        public string LecturerName { get; set; } // Lecturer's name
        public int Month { get; set; } // Claim month (1-12)
        public int Year { get; set; } // Claim year
        public decimal HoursWorked { get; set; } // Total hours worked
        public decimal HourlyRate { get; set; } // Hourly rate
        public decimal TotalAmount { get; set; } // Calculated total amount
        public string Description { get; set; } // Description of work
        public string Status { get; set; } // Claim status (Submitted, Approved, Rejected, Paid)
        public DateTime SubmissionDate { get; set; } // Date when claim was submitted
        public List<string> SupportingDocuments { get; set; } = new List<string>(); // List of document file names
    }
}