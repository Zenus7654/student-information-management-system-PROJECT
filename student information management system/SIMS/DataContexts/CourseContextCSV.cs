using SIMS.Abstractions;
using SIMS.Models;

namespace SIMS.DataContexts
{
    public class CourseContextCSV : ICourse
    {
		private int nextCourseId = 1;

		public List<Course> Courses { get; set; }

		private readonly string filePath;

		public CourseContextCSV(string filePath)
		{
			this.filePath = filePath;
			Courses = ReadDataFromFile(filePath);
		}

		public string FilePath { get; set; }
		public List<Course> ListCourses { get; set; }

		public void InsertCourse(Course course)
		{
			course.CourseId = nextCourseId++; // Assign the next available Id and increment the counter
			Courses.Add(course);
			WriteDataToFile(filePath);
		}

		public void UpdateCourse(int courseId, Course updatedCourse)
		{
			Course existingCourse = Courses.FirstOrDefault(p => p.CourseId == courseId);

			if (existingCourse != null)
			{
				existingCourse.CourseName = updatedCourse.CourseName;
				existingCourse.Credits = updatedCourse.Credits;
				existingCourse.Description = updatedCourse.Description;

				WriteDataToFile(filePath);
			}
			else
			{
				Console.WriteLine($"Course with Id {courseId} not found.");
			}
		}

		public void RemoveCourse(int courseId)
		{
			Course selectedCourseToRemove = Courses.FirstOrDefault(p => p.CourseId == courseId);

			if (selectedCourseToRemove != null)
			{
				Courses.Remove(selectedCourseToRemove);
				WriteDataToFile(filePath);
			}
			else
			{
				Console.WriteLine($"Course with Id {courseId} not found.");
			}
		}

		public List<Course> ReadDataFromFile(string filePath)
		{
			Courses = new List<Course>();
			nextCourseId = 1; // Reset the counter

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
							Course course = new Course
							{
								CourseId = int.Parse(values[0]),
								CourseName = values[1],
								Credits = int.Parse(values[2]),
								Description = (values[3]),
							};

							Courses.Add(course);

							// Update nextCourseId if needed
							if (course.CourseId >= nextCourseId)
							{
								nextCourseId = course.CourseId + 1;
							}
						}
					}
				}
			}
			else { Console.WriteLine($"No file found with PATH: \"{filePath}\""); }

			if (Courses.Count == 0) { Console.WriteLine("No courses found."); }
			return Courses;
		}

		public void WriteDataToFile(string filePath)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				// Write header
				writer.WriteLine("CourseId,CourseName,Credits,Description");

				// Write data rows
				foreach (var course in Courses)
				{
					writer.WriteLine($"{course.CourseId},{course.CourseName},{course.Credits},{course.Description}");
				}
			}
		}

        public void ViewCourseInfo()
        {
            // Logic to view course information
        }
    }
}
