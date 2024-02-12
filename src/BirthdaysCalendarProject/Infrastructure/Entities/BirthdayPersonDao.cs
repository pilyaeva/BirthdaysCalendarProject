using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdaysCalendarProject.Infrastructure.Entities
{
    public class BirthdayPersonDao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public byte[] Avatar { get; set; }
    }
}
