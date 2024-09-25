using SIMS.Models;

namespace SIMS.Abstractions
{
    public interface ITeacher
    {
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }

        public bool RegisterTeaching(Course course) 
        {
            return false;
        }
        void InsertTeacher(Teacher teacher);
        void UpdateTeacher(int teacherId, Teacher teacher);
        void DeleteTeacher(int teacherId);
        void ViewCourses() { }
        void ViewTeachers() { }
        void ViewGrades(int course_id) { }

    }
}
