namespace SIMS.Models
{
    public class Program
    {
        private int programId;
        private string programName;
        private List<Course> listCourses;
        private List<Student> listStudents;
        private string programDescription;

       
        public List<Course> ListCourses { get; set; }
        public List<Student> ListStudents { get; set; }
        public string Description { get; set; }

        public List<Course> ViewCourses()
        {
            // Logic to view list of courses in the program
            return ListCourses;
        }

        public List<Student> ViewStudents()
        {
            // Logic to view list of students in the program
            return ListStudents;
        }
    }
}
