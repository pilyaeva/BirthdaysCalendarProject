namespace BirthdaysCalendarProject.Infrastructure.Repositories.Models
{
    public class BirthdayPersonDtoOut
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public byte[] Avatar { get; set; }
    }
}
