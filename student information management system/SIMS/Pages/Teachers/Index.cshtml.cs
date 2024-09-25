using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIMS.Models;
using SIMS.Services;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly TeacherService _service;
        public List<Teacher> TeacherList { get; set; }
        public string SearchTerm { get; set; }
        public int teacherIndex = 0;

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public Teacher NewTeacher { get; set; }

        public IndexModel(TeacherService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            TeacherList = _service.GetTeachers();
        }

        public void OnPostSearch(string searchTerm)
        {
            SearchTerm = searchTerm;
            TeacherList = _service.GetTeachers();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                try
                {
                    TeacherList = TeacherList
                    .Where(s => s.TeacherName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"No teacher name with '{searchTerm}'.";
                }
            }
        }

        public void OnGetEdit(int id, bool showEditForm)
        {
            teacherIndex = id;
            NewTeacher = _service.GetTeacherById(id);
            TeacherList = _service.GetTeachers();

            // Save the edit form display information to ViewData
            ViewData["ShowEditForm"] = showEditForm;
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid || NewTeacher == null)
            {
                return Page();
            }

            _service.AddTeacher(NewTeacher);

            TempData["SuccessMessage"] = $"Teacher \"{NewTeacher.TeacherName}\" has been added successfully.";
            return RedirectToAction("Get");
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid || NewTeacher == null)
            {
                return Page();
            }

            _service.UpdateTeacher(NewTeacher.TeacherId, NewTeacher);
            TempData["SuccessMessage"] = $"Teacher {NewTeacher.TeacherId}, \" {NewTeacher.TeacherName} \" has been updated successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteTeacher(id);
            return RedirectToAction("Get");
        }
    }
}
