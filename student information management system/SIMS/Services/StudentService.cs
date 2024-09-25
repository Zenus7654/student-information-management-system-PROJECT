using SIMS.Abstractions;
using SIMS.Models;

namespace SIMS.Services
{
	public class StudentService
	{
		private readonly IStudent _studentContext;

		public List<Student> Students { get; private set; }

		public StudentService(IStudent context)
		{
			_studentContext = context ?? throw new ArgumentNullException(nameof(context));
			Students = GetStudents();
		}

		public List<Student> GetStudents()
		{
			return _studentContext.Students?.ToList() ?? new List<Student>();
		}

		public Student GetStudentById(int id)
		{
			return _studentContext.Students?.FirstOrDefault(s => s.StudentId == id);
		}

		public void AddStudent(Student student)
		{
			if (student == null) throw new ArgumentNullException(nameof(student));

			// Check if a student with the same ID already exists
			var existingStudent = _studentContext.Students?.FirstOrDefault(s => s.StudentId == student.StudentId);
			if (existingStudent == null)
			{
				// Proceed with insertion if no conflicts
				_studentContext.InsertStudent(student);
				Students = GetStudents(); // Refresh the list after addition
			}
		}

		public void UpdateStudent(int id, Student student)
		{
			if (student == null) throw new ArgumentNullException(nameof(student));

			// Check if the student with the given ID exists
			var existingStudent = _studentContext.Students?.FirstOrDefault(s => s.StudentId == id);
			if (existingStudent != null)
			{
				_studentContext.UpdateStudent(id, student);
				Students = GetStudents(); // Refresh the list after update
			}
		}

		public void DeleteStudent(int id)
		{
			var student = _studentContext.Students?.FirstOrDefault(s => s.StudentId == id);
			if (student != null)
			{
				_studentContext.DeleteStudent(id);
				Students = GetStudents(); // Refresh the list after deletion
			}
		}
	}
}

