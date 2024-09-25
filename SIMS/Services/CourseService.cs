using SIMS.Abstractions;
using SIMS.Models;
using Microsoft.Extensions.Logging;

namespace SIMS.Services
{
    public class CourseService
    {
        private readonly ICourse _courseContext;
        private readonly ILogger<CourseService> _logger;

        public List<Course> Courses { get; private set; }

        public CourseService(ICourse context, ILogger<CourseService> logger)
        {
            _courseContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Courses = GetCourses();
        }

        public List<Course> GetCourses()
        {
            return _courseContext.Courses?.ToList() ?? new List<Course>();
        }

        public Course GetCourseById(int id)
        {
            if (_courseContext.Courses == null)
            {
                _logger.LogError("CourseContext.Courses is null");
                return null;
            }

            var course = _courseContext.Courses.FirstOrDefault(c => c.CourseId == id);
            _logger.LogInformation(course != null ? "Course found" : "Course not found");
            return course;
        }

        public void AddCourse(Course course)
        {
            if (course == null) 
                throw new ArgumentNullException(nameof(course));

            var existingCourse = _courseContext.Courses?.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existingCourse == null)
            {
                _courseContext.InsertCourse(course);
                Courses = GetCourses(); // Refresh the list after addition
                _logger.LogInformation($"Course with ID {course.CourseId} added successfully.");
            }
            else
            {
                _logger.LogWarning($"Course with ID {course.CourseId} already exists.");
            }
        }

        public void UpdateCourse(int id, Course course)
        {
            if (course == null) 
                throw new ArgumentNullException(nameof(course));

            var existingCourse = _courseContext.Courses?.FirstOrDefault(c => c.CourseId == id);
            if (existingCourse != null)
            {
                _courseContext.UpdateCourse(id, course);
                Courses = GetCourses(); // Refresh the list after update
                _logger.LogInformation($"Course with ID {id} updated successfully.");
            }
            else
            {
                _logger.LogWarning($"Course with ID {id} does not exist.");
            }
        }

        public void DeleteCourse(int id)
        {
            if (_courseContext.Courses == null)
            {
                _logger.LogError("CourseContext.Courses is null");
                return;
            }

            var course = _courseContext.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course != null)
            {
                _courseContext.RemoveCourse(id);
                Courses = GetCourses(); // Refresh the list after deletion
                _logger.LogInformation($"Course with ID {id} deleted successfully.");
            }
            else
            {
                _logger.LogWarning($"Course with ID {id} not found.");
            }
        }
    }
}
