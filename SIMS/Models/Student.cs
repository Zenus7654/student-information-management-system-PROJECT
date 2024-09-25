using SIMS.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Models
{
    public class Student //: User
    {
        private int studentId;
        private string studentName;
        private DateTime dateOfBirth;
        private bool gender;
        private string Coursemajor;

        [Key]
        public int StudentId { get; set; } = 0;
        //[Required]
        public string StudentName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string CourseMajor { get; set; }
    }
}
