using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SIMS.Abstractions;
using SIMS.Models;

namespace SIMS.Services
{
    public class AdminService
    {
        public class LoginModel : PageModel
        {
            private readonly AdminService _adminService;

            public LoginModel(AdminService adminService)
            {
                _adminService = adminService;
            }

            [BindProperty]
            public string Username { get; set; }

            [BindProperty]
            public string Password { get; set; }

            public string ErrorMessage { get; set; }

            public IActionResult OnPost()
            {
                if (_adminService.ValidateAdmin(Username, Password))
                {
                    // Handle successful login
                    return RedirectToPage("/Admin/Dashboard");
                }
                else
                {
                    ErrorMessage = "Invalid username or password.";
                    return Page();
                }
            }
        }

        private readonly IAdmin _adminContext;

        // Constructor to inject the IAdmin dependency
        public AdminService(IAdmin adminContext)
        {
            _adminContext = adminContext;
        }

        // Method to validate an admin's credentials
        public bool ValidateAdmin(string username, string password)
        {
            return _adminContext.ValidateAdmin(username, password);
        }

        // Method to add a new admin
        public void AddAdmin(Administrator admin)
        {
            _adminContext.AddAdmin(admin);
        }

        // Method to get an admin by username
        public Administrator GetAdminByUsername(string username)
        {
            return _adminContext.GetAdminByUsername(username);
        }
    }
}
