namespace BirthdaysCalendarProject.Infrastructure.Repositories.Models
{
    public class BirthdayPersonDtoIn
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
