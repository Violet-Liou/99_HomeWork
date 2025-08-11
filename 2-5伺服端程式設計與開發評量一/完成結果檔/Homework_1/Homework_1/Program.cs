using Homework_1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//�bProgram.cs���H�̿�`�J���g�k���UŪ���s�u�r�ꪺ�A��
builder.Services.AddDbContext<MessageBoardContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MessageBoardConnection")));


////////////////////////////////////////////////////////////////////////////////////////////////
var app = builder.Build();

//�bProgram.cs���g�ҥ�Initializer���{��
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    SeedData.Initialize(service);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
