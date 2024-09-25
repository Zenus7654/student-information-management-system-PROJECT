namespace SIMS.Models
{
    public class Course
    {
        private int courseId;
        private string courseName;
        private int credits;
        private string description;
        private string courseTeacher; // New property
        private string department;    // New property

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public string CourseMajor { get; set; }    // New property
    }
}
