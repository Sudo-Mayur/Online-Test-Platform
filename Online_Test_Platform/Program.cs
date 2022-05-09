using Microsoft.EntityFrameworkCore;
using Online_Test_Platform.Models;
using Online_Test_Platform.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//1. REgister the DbContext in Dependency COntainer as a Service
builder.Services.AddDbContext<TestPlatformContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnStr"));
});

// COfigure Sessions
// The Session Time out is 20 Mins for Idle Request
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});


// 2.Register Custom Service in DI COntainer
builder.Services.AddScoped<IService<UserInfo, int>, UserInfoService>();
builder.Services.AddScoped<IService<Question, int>, QuestionService>();
builder.Services.AddScoped<IService<TestReport, int>, TestReportService>();
builder.Services.AddScoped<IService<UserAnswer, int>, UserAnswerService>();



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Use the Sessin Middleware
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();


