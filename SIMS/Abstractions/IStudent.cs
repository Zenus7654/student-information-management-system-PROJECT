using SIMS.Models;

namespace SIMS.Abstractions
{
    public interface IStudent
    {
        public List<Student> Students { get; set; }

        void InsertStudent(Student student);
        void UpdateStudent(int studentId, Student student);
        void DeleteStudent(int studentId);
        void RegisterProgram(int programId);
        void ViewCourses();
        void ViewGrades(int courseId);
    }
}
