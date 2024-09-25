using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Models;
using SIMS.Services;
using Microsoft.Extensions.Logging;

namespace SIMS.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly StudentService _studentService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(StudentService studentService, ILogger<DetailsModel> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        public Student Student { get; set; }

        public IActionResult OnGet(int id)
        {
            Student = _studentService.GetStudentById(id);

            if (Student == null)
            {
                return NotFound();
            }

            // Log information to verify
            _logger.LogInformation($"Student details: Id = {Student.StudentId}, Name = {Student.StudentName}");

            return Page();
        }
    }
}
