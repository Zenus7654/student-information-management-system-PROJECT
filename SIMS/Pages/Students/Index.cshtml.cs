using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Models;
using SIMS.Services;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _service;
        public List<Student> StudentList { get; set; }
        public string SearchTerm { get; set; }
        public int studentIndex = 0;

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public Student NewStudent { get; set; }

        public IndexModel(StudentService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            StudentList = _service.GetStudents();
        }

        public void OnPostSearch(string searchTerm)
        {
            SearchTerm = searchTerm;
            StudentList = _service.GetStudents();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                try
                {
                    StudentList = StudentList
                    .Where(s => s.StudentName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"No student name with '{searchTerm}'.";
                }
            }
        }

        public void OnGetEdit(int id, bool showEditForm)
        {
            studentIndex = id;
            NewStudent = _service.GetStudentById(id);
            StudentList = _service.GetStudents();

            // Save the edit form display information to ViewData
            ViewData["ShowEditForm"] = showEditForm;
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid || NewStudent == null)
            {
                return Page();
            }

            _service.AddStudent(NewStudent);

            TempData["SuccessMessage"] = $"Student \"{NewStudent.StudentName}\" has been added successfully.";
            return RedirectToAction("Get");
        }
        
        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid || NewStudent == null)
            {
                return Page();
            }

            _service.UpdateStudent(NewStudent.StudentId, NewStudent);
            TempData["SuccessMessage"] = $"Student {NewStudent.StudentId}, \"{NewStudent.StudentName}\" has been updated successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteStudent(id);
            return RedirectToAction("Get");
        }
    }
}
