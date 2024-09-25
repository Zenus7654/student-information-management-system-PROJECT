using SIMS.Abstractions;
using SIMS.Models;

namespace SIMS.DataContexts
{
    public class TeacherContextCSV : ITeacher
    {
        public bool RegisterTeaching(int course_id)
        {
            return false;
        }

        public List<Course> Courses { get; set; }

        public bool SubmitGrade(Student student, Course course, decimal grade)
        {
            return false;
        }

        private int nextTeacherId = 1;

        public List<Teacher> Teachers { get; set; }

        private readonly string filePath;

        public TeacherContextCSV(string filePath)
        {
            this.filePath = filePath;
            Teachers = ReadDataFromFile(filePath);
        }

        public string FilePath { get; set; }
        public List<Student> ListStudents { get; set; }

        public void InsertTeacher(Teacher teacher)
        {
            teacher.TeacherId = nextTeacherId++; // Assign the next available Id and increment the counter
            Teachers.Add(teacher);
            WriteDataToFile(filePath);
        }

        public void UpdateTeacher(int teacherId, Teacher updatedTeacher)
        {
            Teacher existingTeacher = Teachers.FirstOrDefault(p => p.TeacherId == teacherId);

            if (existingTeacher != null)
            {
                existingTeacher.TeacherName = updatedTeacher.TeacherName;
                existingTeacher.DateOfBirth = updatedTeacher.DateOfBirth;
                existingTeacher.Gender = updatedTeacher.Gender;
                existingTeacher.TeacherCourse = updatedTeacher.TeacherCourse;

                WriteDataToFile(filePath);
            }
            else
            {
                Console.WriteLine($"Teacher with Id {teacherId} not found.");
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            Teacher selectedTeacherToRemove = Teachers.FirstOrDefault(p => p.TeacherId == teacherId);

            if (selectedTeacherToRemove != null)
            {
                Teachers.Remove(selectedTeacherToRemove);
                WriteDataToFile(filePath);
            }
            else
            {
                Console.WriteLine($"Teacher with Id {teacherId} not found.");
            }
        }

        public List<Teacher> ReadDataFromFile(string filePath)
        {
            Teachers = new List<Teacher>();
            nextTeacherId = 1; // Reset the counter

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 4)
                        {
                            Teacher teacher = new Teacher
                            {
                                TeacherId = int.Parse(values[0]),
                                TeacherName = values[1],
                                DateOfBirth = DateTime.Parse(values[2]),
                                Gender = bool.Parse(values[3]),
                                TeacherCourse = values[4]
                            };

                            Teachers.Add(teacher);

                            // Update nextTeacherId if needed
                            if (teacher.TeacherId >= nextTeacherId)
                            {
                                nextTeacherId = teacher.TeacherId + 1;
                            }
                        }
                    }
                }
            }
            else { Console.WriteLine($"No file found with PATH: \"{filePath}\""); }

            if (Teachers.Count == 0) { Console.WriteLine("No teachers found."); }
            return Teachers;
        }

        public void WriteDataToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Id,Fullname,DOB,Gender,Department");

                // Write data rows
                foreach (var teacher in Teachers)
                {
                    writer.WriteLine($"{teacher.TeacherId},{teacher.TeacherName},{teacher.DateOfBirth.ToString("dd/MM/yyyy")},{teacher.Gender},{teacher.TeacherCourse}");
                }
            }
        }

        public void ViewCourses() { }
        public void ViewStudents() { }
        public void ViewGrades(int course_id) { }

    }
}
