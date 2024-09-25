using SIMS.Abstractions;
using SIMS.DataContexts;
using SIMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<IStudent>(provider =>
{
    string filePath = "DataCSV/Students.csv";

    return new StudentContextCSV(filePath);
});

builder.Services.AddScoped<TeacherService>();

builder.Services.AddScoped<ITeacher>(provider =>
{
    string filePath = "DataCSV/Teachers.csv";

    return new TeacherContextCSV(filePath);
});

builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<ICourse>(provider =>
{
	string filePath = "DataCSV/Courses.csv";

	return new CourseContextCSV(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
