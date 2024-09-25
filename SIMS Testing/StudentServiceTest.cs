using Moq;
using SIMS.Abstractions;
using SIMS.Models;
using SIMS.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace SIMS.Testing
{
	public class StudentServiceTest
	{
		private readonly StudentService _studentService;
		private readonly Mock<IStudent> _mockStudentContext;

		public StudentServiceTest()
		{
			// Initialize the mock object
			_mockStudentContext = new Mock<IStudent>();

			// Inject the mock dependency into the StudentService
			_studentService = new StudentService(_mockStudentContext.Object);
		}

		[Fact]
		public void GetStudents_ShouldReturnStudentList()
		{
			// Arrange
			var students = new List<Student>
			{
				new Student { StudentId = 1, StudentName = "Student 1" },
				new Student { StudentId = 2, StudentName = "Student 2" }
			};

			_mockStudentContext.Setup(m => m.Students).Returns(students);

			// Act
			var result = _studentService.GetStudents();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
		}

		[Fact]
		public void GetStudentById_ShouldReturnStudent_WhenStudentExists()
		{
			// Arrange
			var students = new List<Student>
			{
				new Student { StudentId = 1, StudentName = "Student 1" }
			};

			_mockStudentContext.Setup(m => m.Students).Returns(students);

			// Act
			var result = _studentService.GetStudentById(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.StudentId);
		}

		[Fact]
		public void GetStudentById_ShouldReturnNull_WhenStudentDoesNotExist()
		{
			// Arrange
			var students = new List<Student>();

			_mockStudentContext.Setup(m => m.Students).Returns(students);

			// Act
			var result = _studentService.GetStudentById(1);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void AddStudent_ShouldAddStudent_WhenStudentIsValid()
		{
			// Arrange
			var newStudent = new Student { StudentId = 3, StudentName = "Student 3" };

			_mockStudentContext.Setup(m => m.Students).Returns(new List<Student>());
			_mockStudentContext.Setup(m => m.InsertStudent(It.IsAny<Student>())).Verifiable();

			// Act
			_studentService.AddStudent(newStudent);

			// Assert
			_mockStudentContext.Verify(m => m.InsertStudent(It.Is<Student>(s => s.StudentId == 3)), Times.Once);
		}

		[Fact]
		public void UpdateStudent_ShouldUpdateStudent_WhenStudentExists()
		{
			// Arrange
			var existingStudent = new Student { StudentId = 1, StudentName = "Student 1" };
			var updatedStudent = new Student { StudentId = 1, StudentName = "Updated Student 1" };

			_mockStudentContext.Setup(m => m.Students).Returns(new List<Student> { existingStudent });
			_mockStudentContext.Setup(m => m.UpdateStudent(It.IsAny<int>(), It.IsAny<Student>())).Verifiable();

			// Act
			_studentService.UpdateStudent(1, updatedStudent);

			// Assert
			_mockStudentContext.Verify(m => m.UpdateStudent(1, It.Is<Student>(s => s.StudentName == "Updated Student 1")), Times.Once);
		}

		[Fact]
		public void DeleteStudent_ShouldRemoveStudent_WhenStudentExists()
		{
			// Arrange
			var existingStudent = new Student { StudentId = 1, StudentName = "Student 1" };

			_mockStudentContext.Setup(m => m.Students).Returns(new List<Student> { existingStudent });
			_mockStudentContext.Setup(m => m.DeleteStudent(It.IsAny<int>())).Verifiable();

			// Act
			_studentService.DeleteStudent(1);

			// Assert
			_mockStudentContext.Verify(m => m.DeleteStudent(1), Times.Once);
		}

		// Additional tests for RegisterProgram, ViewCourses, and ViewGrades can be added here
	}
}
