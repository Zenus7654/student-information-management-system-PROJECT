using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Models;
using SIMS.Services;
using Microsoft.Extensions.Logging;

namespace SIMS.Pages.Teachers
{
    public class DetailsModel : PageModel
    {
        private readonly TeacherService _teacherService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(TeacherService teacherService, ILogger<DetailsModel> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        public Teacher Teacher { get; set; }

        public IActionResult OnGet(int id)
        {
            Teacher = _teacherService.GetTeacherById(id);

            if (Teacher == null)
            {
                return NotFound();
            }

            // Log thông tin để kiểm tra
            _logger.LogInformation($"Teacher details: Id = {Teacher.TeacherId}, Name = {Teacher.TeacherName}");

            return Page();
        }
    }
}
