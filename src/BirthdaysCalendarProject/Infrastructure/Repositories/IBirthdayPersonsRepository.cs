using BirthdaysCalendarProject.Infrastructure.Repositories.Models;

namespace BirthdaysCalendarProject.Infrastructure.Repositories
{
    public interface IBirthdayPersonsRepository
    {
        Task<BirthdayPersonDtoOut> AddAsync(BirthdayPersonDtoIn birthdayPersonIn, CancellationToken cancellationToken);

        Task<List<BirthdayPersonDtoOut>> GetAllAsync(CancellationToken cancellationToken);

        Task<BirthdayPersonDtoOut?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task EditAsync(int id, BirthdayPersonDtoIn birthdayPersonIn, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
