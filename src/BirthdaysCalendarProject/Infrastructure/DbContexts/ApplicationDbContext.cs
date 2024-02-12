using BirthdaysCalendarProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthdaysCalendarProject.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BirthdayPersonDao> BirthdayPersons { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
