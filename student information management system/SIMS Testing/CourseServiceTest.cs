using Moq;
using SIMS;
using SIMS.Abstractions;
using SIMS.Models;
using SIMS.Services;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace SIMS.Testing
{

	public class CourseServiceTest
	{
		private readonly CourseService _courseService;
		private readonly Mock<ICourse> _mockCourseContext;
		private readonly Mock<ILogger<CourseService>> _mockLogger;

		public CourseServiceTest()
		{
			// Initialize mock objects
			_mockCourseContext = new Mock<ICourse>();
			_mockLogger = new Mock<ILogger<CourseService>>();

			// Inject mock dependencies into the CourseService
			_courseService = new CourseService(_mockCourseContext.Object, _mockLogger.Object);
		}

		[Fact]
		public void GetCourses_ShouldReturnCourseList()
		{
			// Arrange
			var courses = new List<Course>
			{
				new Course { CourseId = 1, CourseName = "Course 1" },
				new Course { CourseId = 2, CourseName = "Course 2" }
			};

			_mockCourseContext.Setup(m => m.Courses).Returns(courses);

			// Act
			var result = _courseService.GetCourses();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
		}

		[Fact]
		public void GetCourseById_ShouldReturnCourse_WhenCourseExists()
		{
			// Arrange
			var courses = new List<Course>
			{
				new Course { CourseId = 1, CourseName = "Course 1" }
			};

			_mockCourseContext.Setup(m => m.Courses).Returns(courses);

			// Act
			var result = _courseService.GetCourseById(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.CourseId);
		}

		[Fact]
		public void GetCourseById_ShouldReturnNull_WhenCourseDoesNotExist()
		{
			// Arrange
			var courses = new List<Course>();

			_mockCourseContext.Setup(m => m.Courses).Returns(courses);

			// Act
			var result = _courseService.GetCourseById(1);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void AddCourse_ShouldAddCourse_WhenCourseIsValid()
		{
			// Arrange
			var newCourse = new Course { CourseId = 3, CourseName = "Course 3" };

			_mockCourseContext.Setup(m => m.Courses).Returns(new List<Course>());
			_mockCourseContext.Setup(m => m.InsertCourse(It.IsAny<Course>())).Verifiable();

			// Act
			_courseService.AddCourse(newCourse);

			// Assert
			_mockCourseContext.Verify(m => m.InsertCourse(It.Is<Course>(c => c.CourseId == 3)), Times.Once);
		}

		[Fact]
		public void UpdateCourse_ShouldUpdateCourse_WhenCourseExists()
		{
			// Arrange
			var existingCourse = new Course { CourseId = 1, CourseName = "Course 1" };
			var updatedCourse = new Course { CourseId = 1, CourseName = "Updated Course 1" };

			_mockCourseContext.Setup(m => m.Courses).Returns(new List<Course> { existingCourse });
			_mockCourseContext.Setup(m => m.UpdateCourse(It.IsAny<int>(), It.IsAny<Course>())).Verifiable();

			// Act
			_courseService.UpdateCourse(1, updatedCourse);

			// Assert
			_mockCourseContext.Verify(m => m.UpdateCourse(1, It.Is<Course>(c => c.CourseName == "Updated Course 1")), Times.Once);
		}

		[Fact]
		public void DeleteCourse_ShouldRemoveCourse_WhenCourseExists()
		{
			// Arrange
			var existingCourse = new Course { CourseId = 1, CourseName = "Course 1" };

			_mockCourseContext.Setup(m => m.Courses).Returns(new List<Course> { existingCourse });
			_mockCourseContext.Setup(m => m.RemoveCourse(It.IsAny<int>())).Verifiable();

			// Act
			_courseService.DeleteCourse(1);

			// Assert
			_mockCourseContext.Verify(m => m.RemoveCourse(1), Times.Once);
		}
	}
}
