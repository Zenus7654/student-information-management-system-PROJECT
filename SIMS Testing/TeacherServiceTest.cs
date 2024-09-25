using Moq;
using SIMS.Abstractions;
using SIMS.Models;
using SIMS.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace SIMS.Testing
{
    public class TeacherServiceTest
    {
        private readonly TeacherService _TeacherService;
        private readonly Mock<ITeacher> _mockTeacherContext;

        public TeacherServiceTest()
        {
            // Initialize the mock object
            _mockTeacherContext = new Mock<ITeacher>();

            // Inject the mock dependency into the StudentService
            _TeacherService = new TeacherService(_mockTeacherContext.Object);
        }

        [Fact]
        public void GetTeachers_ShouldReturnTeacherList()
        {
            // Arrange
            var Teachers = new List<Teacher>
            {
                new Teacher { TeacherId = 1, TeacherName = "Teacher 1" },
                new Teacher { TeacherId = 2, TeacherName = "Student 2" }
            };

            _mockTeacherContext.Setup(m => m.Teachers).Returns(Teachers);

            // Act
            var result = _TeacherService.GetTeachers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetTeacherById_ShouldReturnTeacher_WhenTeacherExists()
        {
            // Arrange
            var Teachers = new List<Teacher>
            {
                new Teacher { TeacherId = 1, TeacherName = "Teacher 1" }
            };

            _mockTeacherContext.Setup(m => m.Teachers).Returns(Teachers);

            // Act
            var result = _TeacherService.GetTeacherById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TeacherId);
        }

        [Fact]
        public void GetTeacherById_ShouldReturnNull_WhenTeacherDoesNotExist()
        {
            // Arrange
            var Teachers = new List<Teacher>();

            _mockTeacherContext.Setup(m => m.Teachers).Returns(Teachers);

            // Act
            var result = _TeacherService.GetTeacherById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddTeacher_ShouldAddTeacher_WhenTeacherIsValid()
        {
            // Arrange
            var newTeacher = new Teacher { TeacherId = 3, TeacherName = "Teacher 3" };

            _mockTeacherContext.Setup(m => m.Teachers).Returns(new List<Teacher>());
            _mockTeacherContext.Setup(m => m.InsertTeacher(It.IsAny<Teacher>())).Verifiable();

            // Act
            _TeacherService.AddTeacher(newTeacher);

            // Assert
            _mockTeacherContext.Verify(m => m.InsertTeacher(It.Is<Teacher>(s => s.TeacherId == 3)), Times.Once);
        }

        [Fact]
        public void UpdateTeacher_ShouldUpdateTeacher_WhenTeacherExists()
        {
            // Arrange
            var existingTeacher = new Teacher { TeacherId = 1, TeacherName = "Teacher 1" };
            var updatedTeacher = new Teacher { TeacherId = 1, TeacherName = "Updated Teacher 1" };

            _mockTeacherContext.Setup(m => m.Teachers).Returns(new List<Teacher> { existingTeacher });
            _mockTeacherContext.Setup(m => m.UpdateTeacher(It.IsAny<int>(), It.IsAny<Teacher>())).Verifiable();

            // Act
            _TeacherService.UpdateTeacher(1, updatedTeacher);

            // Assert
            _mockTeacherContext.Verify(m => m.UpdateTeacher(1, It.Is<Teacher>(s => s.TeacherName == "Updated Teacher 1")), Times.Once);
        }

        [Fact]
        public void DeleteSTeacher_ShouldRemoveTeacher_WhenTeacherExists()
        {
            // Arrange
            var existingTeacher = new Teacher { TeacherId = 1, TeacherName = "Teacher 1" };

            _mockTeacherContext.Setup(m => m.Teachers).Returns(new List<Teacher> { existingTeacher });
            _mockTeacherContext.Setup(m => m.DeleteTeacher(It.IsAny<int>())).Verifiable();

            // Act
            _TeacherService.DeleteTeacher(1);

            // Assert
            _mockTeacherContext.Verify(m => m.DeleteTeacher(1), Times.Once);
        }

        // Additional tests for RegisterProgram, ViewCourses, and ViewGrades can be added here
    }
}
