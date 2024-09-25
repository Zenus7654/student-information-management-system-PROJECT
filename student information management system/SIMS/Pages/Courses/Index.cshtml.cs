using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Models;
using SIMS.Services;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CourseService _service;
        public List<Course> CourseList { get; set; }
        public string SearchTerm { get; set; }
        public int courseIndex = 0;

        [BindProperty]
        public Course NewCourse { get; set; }

        public IndexModel(CourseService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            CourseList = _service.GetCourses();
        }

        public void OnPostSearch(string searchTerm)
        {
            SearchTerm = searchTerm;
            CourseList = _service.GetCourses();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                try
                {
                    CourseList = CourseList
                    .Where(s => s.CourseName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"No course name with '{searchTerm}'.";
                }
            }
        }

        public void OnGetEdit(int id, bool showEditForm)
        {
            courseIndex = id;
            NewCourse = _service.GetCourseById(id);
            CourseList = _service.GetCourses();

            // Save the edit form display information to ViewData
            ViewData["ShowEditForm"] = showEditForm;
        }

        public IActionResult OnPostCreate()
        {
            if ( NewCourse == null)
            {
                return Page();
            }

            _service.AddCourse(NewCourse);

            TempData["SuccessMessage"] = $"Course \"{NewCourse.CourseName}\" has been added successfully.";
            return RedirectToAction("Get");
        }

        public IActionResult OnPostEdit()
        {
            if ( NewCourse == null)
            {
                return Page();
            }

            _service.UpdateCourse(NewCourse.CourseId, NewCourse);
            TempData["SuccessMessage"] = $"Course {NewCourse.CourseId}, \"{NewCourse.CourseName}\" has been updated successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteCourse(id);
            return RedirectToAction("Get");
        }
    }
}
