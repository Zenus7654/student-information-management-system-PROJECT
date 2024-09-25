using SIMS.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Models
{
    public class Teacher //: User
    {
        private int teacherId;
        private string teacherName;
        private DateTime dateOfBirth;
        private bool gender;
        private string teacherCourse;

        [Key]
        public int TeacherId { get; set; } = 0;
        //[Required]
        public string TeacherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string TeacherCourse { get; set; }
    }
}
