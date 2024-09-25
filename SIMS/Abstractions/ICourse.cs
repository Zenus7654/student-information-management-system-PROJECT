using SIMS.Models;
namespace SIMS.Abstractions
{
    public interface ICourse
    {
		public List<Course> Courses { get; set; }

		void InsertCourse(Course course);
        void RemoveCourse(int courseId);
        void UpdateCourse(int courseId, Course course);
        void ViewCourseInfo();
    }
}
