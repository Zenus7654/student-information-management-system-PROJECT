using SIMS.Abstractions;
using SIMS.Models;
using System.Globalization;

namespace SIMS.DataContexts
{
    public class StudentContextCSV : IStudent
    {
        private int nextStudentId = 1;

        public List<Student> Students { get; set; } // Now the set accessor is public

        private readonly string filePath;

        public StudentContextCSV(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Students = ReadDataFromFile(filePath);
        }

        public void InsertStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            student.StudentId = nextStudentId++; // Assign the next available Id and increment the counter
            Students.Add(student);
            WriteDataToFile(filePath);
        }

        public void UpdateStudent(int studentId, Student updatedStudent)
        {
            if (updatedStudent == null) throw new ArgumentNullException(nameof(updatedStudent));

            Student existingStudent = Students.FirstOrDefault(p => p.StudentId == studentId);

            if (existingStudent != null)
            {
                existingStudent.StudentName = updatedStudent.StudentName;
                existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
                existingStudent.Gender = updatedStudent.Gender;
                existingStudent.CourseMajor = updatedStudent.CourseMajor;

                WriteDataToFile(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        public void DeleteStudent(int studentId)
        {
            Student selectedStudentToRemove = Students.FirstOrDefault(p => p.StudentId == studentId);

            if (selectedStudentToRemove != null)
            {
                Students.Remove(selectedStudentToRemove);
                WriteDataToFile(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        private List<Student> ReadDataFromFile(string filePath)
        {
            var students = new List<Student>();
            nextStudentId = 1; // Reset the counter

            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    reader.ReadLine(); // Skip the header line

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length >= 5)
                        {
                            var student = new Student
                            {
                                StudentId = int.Parse(values[0]),
                                StudentName = values[1],
                                DateOfBirth = DateTime.ParseExact(values[2], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                Gender = bool.Parse(values[3]),
                                CourseMajor = values[4]
                            };

                            students.Add(student);

                            if (student.StudentId >= nextStudentId)
                            {
                                nextStudentId = student.StudentId + 1;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"No file found with PATH: \"{filePath}\"");
            }

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }

            return students;
        }

        private void WriteDataToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Fullname,DOB,Gender,CourseMajor");

                foreach (var student in Students)
                {
                    writer.WriteLine($"{student.StudentId},{student.StudentName},{student.DateOfBirth:dd/MM/yyyy},{student.Gender},{student.CourseMajor}");
                }
            }
        }

        public void RegisterProgram(int programId)
        {
            // Implement the logic to register a student for a program
        }

        public void ViewCourses()
        {
            // Implement the logic to view courses for a student
        }

        public void ViewGrades(int courseId)
        {
            // Implement the logic to view grades for a course
        }
    }
}
