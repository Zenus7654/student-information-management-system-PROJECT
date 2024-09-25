using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Services;
using SIMS.Models;
using Microsoft.Extensions.Logging;

namespace SIMS.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly CourseService _context;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(CourseService context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Course Course { get; set; }

        public IActionResult OnGet(int id)
        {
            Course = _context.GetCourseById(id); // Sử dụng CourseService

            if (Course == null)
            {
                return NotFound();
            }

            // Log thông tin để kiểm tra
            _logger.LogInformation($"Course details: Id = {Course.CourseId}, Name = {Course.CourseName}");

            return Page();
        }

    }
}
