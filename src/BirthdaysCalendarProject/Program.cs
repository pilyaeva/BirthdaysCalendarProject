using BirthdaysCalendarProject.Infrastructure.DbContexts;
using BirthdaysCalendarProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("BirthdayPersonsConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped<IBirthdayPersonsRepository, BirthdayPersonsRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BirthdayPersons}/{action=Index}/{id?}");

app.Run();
