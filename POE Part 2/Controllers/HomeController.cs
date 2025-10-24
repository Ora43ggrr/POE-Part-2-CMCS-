using Microsoft.AspNetCore.Mvc;
using POE_Part_2.Models;
using POE_Part_2.Services;

namespace POE_Part_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataService _dataService; // Data service instance

        public HomeController(DataService dataService)
        {
            _dataService = dataService; // Inject data service via constructor
        }

        public IActionResult Index() // Login page
        {
            HttpContext.Session.Clear(); // Clear any existing session
            return View(); // Return login view
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _dataService.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("UserRole", user.Role);
                TempData["Success"] = $"Welcome back, {user.Username}!";
                return RedirectToAction("Dashboard");
            }
            TempData["Error"] = "Invalid email or password";
            return View("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string userRole, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userRole) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "All fields are required";
                return View("Register");
            }

            if (password != confirmPassword)
            {
                TempData["Error"] = "Passwords do not match";
                return View("Register");
            }

            if (_dataService.Users.Any(u => u.Email == email))
            {
                TempData["Error"] = "Email already registered";
                return View("Register");
            }

            var newUser = new User
            {
                Id = _dataService.Users.Count + 1,
                Username = username,
                Email = email,
                Password = password,
                Role = userRole
            };

            _dataService.Users.Add(newUser);
            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Index");
        }

        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userName = HttpContext.Session.GetString("UserName");
            var userRole = HttpContext.Session.GetString("UserRole");
            var userClaims = _dataService.Claims.Where(c => c.LecturerName == userName).ToList();
            var userDocuments = _dataService.Documents.ToList();

            ViewBag.PendingClaims = userClaims.Count(c => c.Status == "Submitted");
            ViewBag.ApprovedClaims = userClaims.Count(c => c.Status == "Approved");
            ViewBag.TotalAmount = userClaims
                .Where(c => c.Status == "Approved" || c.Status == "Paid")
                .Sum(c => c.TotalAmount);
            ViewBag.DocumentCount = userDocuments.Count;
            ViewBag.UserClaims = userClaims;
            ViewBag.UserRole = userRole;

            if (userRole == "ProgrammeCoordinator" || userRole == "AcademicManager")
            {
                ViewBag.TotalPendingClaims = _dataService.Claims.Count(c => c.Status == "Submitted");
                ViewBag.TotalClaims = _dataService.Claims.Count;
            }

            return View();
        }

        public IActionResult Claims()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userName = HttpContext.Session.GetString("UserName");
            var userClaims = _dataService.Claims.Where(c => c.LecturerName == userName).ToList();
            return View(userClaims);
        }

        public IActionResult SubmitClaim()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(int claimMonth, int claimYear, decimal hoursWorked,
            decimal hourlyRate, string description, List<IFormFile> supportingDocuments)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            try
            {
                if (claimMonth < 1 || claimMonth > 12)
                {
                    TempData["Error"] = "Please select a valid month";
                    return View();
                }

                if (claimYear < 2020 || claimYear > 2030)
                {
                    TempData["Error"] = "Please enter a valid year between 2020 and 2030";
                    return View();
                }

                if (hoursWorked <= 0 || hoursWorked > 200)
                {
                    TempData["Error"] = "Hours worked must be between 1 and 200";
                    return View();
                }

                if (hourlyRate <= 0 || hourlyRate > 1000)
                {
                    TempData["Error"] = "Hourly rate must be between R1 and R1000";
                    return View();
                }

                if (supportingDocuments != null && supportingDocuments.Count > 0)
                {
                    foreach (var file in supportingDocuments)
                    {
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            TempData["Error"] = $"File {file.FileName} exceeds 5MB size limit.";
                            return View();
                        }

                        var allowedExtensions = new[] { ".pdf", ".jpg", ".png", ".docx", ".xlsx" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            TempData["Error"] = $"File {file.FileName} has unsupported format.";
                            return View();
                        }
                    }
                }

                var newClaim = new Claim
                {
                    Id = _dataService.Claims.Count + 1,
                    ClaimId = $"CLM-{claimYear}-{(_dataService.Claims.Count + 1).ToString("D3")}",
                    LecturerName = HttpContext.Session.GetString("UserName"),
                    Month = claimMonth,
                    Year = claimYear,
                    HoursWorked = hoursWorked,
                    HourlyRate = hourlyRate,
                    TotalAmount = hoursWorked * hourlyRate,
                    Description = description,
                    Status = "Submitted",
                    SubmissionDate = DateTime.Now
                };

                _dataService.Claims.Add(newClaim);

                if (supportingDocuments != null && supportingDocuments.Count > 0)
                {
                    foreach (var file in supportingDocuments)
                    {
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        var newDocument = new Document
                        {
                            Id = _dataService.Documents.Count + 1,
                            FileName = file.FileName,
                            StoredFileName = $"{Guid.NewGuid()}{fileExtension}",
                            DocumentType = "Supporting Document",
                            Description = $"Supporting document for claim {newClaim.ClaimId}",
                            FileSize = file.Length,
                            UploadDate = DateTime.Now,
                            ClaimId = newClaim.Id
                        };
                        _dataService.Documents.Add(newDocument);
                    }
                }

                TempData["Success"] = $"Claim {newClaim.ClaimId} submitted successfully!";
                return RedirectToAction("Claims");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error submitting claim: " + ex.Message;
                return View();
            }
        }

        public IActionResult Documents()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userDocuments = _dataService.Documents.ToList();
            return View(userDocuments);
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument(IFormFile file, string documentType, string documentDescription)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Please select a file to upload";
                return RedirectToAction("Documents");
            }

            if (file.Length > 5 * 1024 * 1024)
            {
                TempData["Error"] = "File size exceeds 5MB limit";
                return RedirectToAction("Documents");
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".png", ".docx", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["Error"] = "File type not supported.";
                return RedirectToAction("Documents");
            }

            try
            {
                var newDocument = new Document
                {
                    Id = _dataService.Documents.Count + 1,
                    FileName = file.FileName,
                    StoredFileName = $"{Guid.NewGuid()}{fileExtension}",
                    DocumentType = documentType,
                    Description = documentDescription,
                    FileSize = file.Length,
                    UploadDate = DateTime.Now
                };

                _dataService.Documents.Add(newDocument);
                TempData["Success"] = "Document uploaded successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error uploading document: " + ex.Message;
            }

            return RedirectToAction("Documents");
        }

        public IActionResult ReviewClaims()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole != "ProgrammeCoordinator" && userRole != "AcademicManager")
            {
                TempData["Error"] = "Access denied.";
                return RedirectToAction("Dashboard");
            }

            var pendingClaims = _dataService.Claims.Where(c => c.Status == "Submitted").ToList();
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult UpdateClaimStatus(int claimId, string status)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "ProgrammeCoordinator" && userRole != "AcademicManager")
            {
                TempData["Error"] = "Access denied.";
                return RedirectToAction("Dashboard");
            }

            var claim = _dataService.Claims.FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = status;
                TempData["Success"] = $"Claim {claim.ClaimId} status updated to {status}.";
            }
            else
            {
                TempData["Error"] = "Claim not found.";
            }

            return RedirectToAction("ReviewClaims");
        }

        public IActionResult AllClaims()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "ProgrammeCoordinator" && userRole != "AcademicManager")
            {
                TempData["Error"] = "Access denied.";
                return RedirectToAction("Dashboard");
            }

            var allClaims = _dataService.Claims.OrderByDescending(c => c.SubmissionDate).ToList();
            return View(allClaims);
        }

        public IActionResult ClaimDetails(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");

            var claim = _dataService.Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                TempData["Error"] = "Claim not found";
                return RedirectToAction("Claims");
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            var userName = HttpContext.Session.GetString("UserName");

            if (userRole != "ProgrammeCoordinator" && userRole != "AcademicManager" && claim.LecturerName != userName)
            {
                TempData["Error"] = "Access denied.";
                return RedirectToAction("Claims");
            }

            var documents = _dataService.Documents.Where(d => d.ClaimId == id).ToList();
            ViewBag.Documents = documents;
            return View(claim);
        }

        public IActionResult Notifications()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToAction("Index");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            var userName = HttpContext.Session.GetString("UserName");
            HttpContext.Session.Clear();
            TempData["Success"] = $"You have been logged out successfully. Goodbye, {userName}!";
            return RedirectToAction("Index");
        }

        // ✅ NEW ERROR HANDLER
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                    ViewBag.ErrorMessage = "Page not found.";
                else if (statusCode == 500)
                    ViewBag.ErrorMessage = "Internal server error. Please try again later.";
                else
                    ViewBag.ErrorMessage = $"Unexpected error occurred (Code: {statusCode}).";
            }
            else
            {
                ViewBag.ErrorMessage = "An unexpected error occurred.";
            }

            return View("Error");
        }
    }
}
