using SIMS.Abstractions;
using SIMS.Models;

namespace SIMS.Services
{
    public class TeacherService
    {
        private readonly ITeacher _teacherContext;

        public List<Teacher> Teachers { get; private set; }

        public TeacherService(ITeacher context)
        {
            _teacherContext = context ?? throw new ArgumentNullException(nameof(context));
            Teachers = GetTeachers();
        }

        public List<Teacher> GetTeachers()
        {
            return _teacherContext.Teachers?.ToList() ?? new List<Teacher>();
        }

        public Teacher GetTeacherById(int id)
        {
            return _teacherContext.Teachers?.FirstOrDefault(t => t.TeacherId == id);
        }

        public void AddTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            var existingTeacher = _teacherContext.Teachers?.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);
            if (existingTeacher == null)
            {
                _teacherContext.InsertTeacher(teacher);
                Teachers = GetTeachers(); // Refresh the list after addition
            }
            else
            {
                // Handle the case where a teacher with the same ID already exists
            }
        }

        public void UpdateTeacher(int id, Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            var existingTeacher = _teacherContext.Teachers?.FirstOrDefault(t => t.TeacherId == id);
            if (existingTeacher != null)
            {
                _teacherContext.UpdateTeacher(id, teacher);
                Teachers = GetTeachers(); // Refresh the list after update
            }
            else
            {
                // Handle the case where the teacher does not exist
            }
        }

        public void DeleteTeacher(int id)
        {
            var teacher = _teacherContext.Teachers?.FirstOrDefault(t => t.TeacherId == id);
            if (teacher != null)
            {
                _teacherContext.DeleteTeacher(id);
                Teachers = GetTeachers(); // Refresh the list after deletion
            }
            else
            {
                // Handle the case where the teacher does not exist
            }
        }
    }
}
