using BirthdaysCalendarProject.Infrastructure.DbContexts;
using BirthdaysCalendarProject.Infrastructure.Entities;
using BirthdaysCalendarProject.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthdaysCalendarProject.Infrastructure.Repositories
{
    public class BirthdayPersonsRepository : IBirthdayPersonsRepository
    {
        private readonly ApplicationDbContext _context;

        public BirthdayPersonsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BirthdayPersonDtoOut> AddAsync(BirthdayPersonDtoIn birthdayPersonIn, CancellationToken cancellationToken)
        {
            var birthdayPersonDao = new BirthdayPersonDao
            {
                Name = birthdayPersonIn.Name,
                Birthday = DateTime.SpecifyKind(birthdayPersonIn.Birthday, DateTimeKind.Utc)
            };

            if (birthdayPersonIn.Avatar?.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await birthdayPersonIn.Avatar.CopyToAsync(memoryStream);

                    birthdayPersonDao.Avatar = memoryStream.ToArray();
                }
            }
            else
            {
                birthdayPersonDao.Avatar = new byte[0];
            }

            _context.BirthdayPersons.Add(birthdayPersonDao);
            await _context.SaveChangesAsync(cancellationToken);

            return new BirthdayPersonDtoOut
            {
                Id = birthdayPersonDao.Id,
                Name = birthdayPersonDao.Name,
                Birthday = birthdayPersonDao.Birthday
            };
        }

        public async Task<List<BirthdayPersonDtoOut>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context
                .BirthdayPersons
                .AsNoTracking()
                .Select(birthdayPerson => new BirthdayPersonDtoOut
                {
                    Id = birthdayPerson.Id,
                    Name = birthdayPerson.Name,
                    Birthday = birthdayPerson.Birthday,
                    Avatar = birthdayPerson.Avatar
                })
                .ToListAsync();
        }

        public async Task<BirthdayPersonDtoOut?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var birthdayPersonDao = await _context.BirthdayPersons.AsNoTracking().FirstOrDefaultAsync(birthdayPerson => birthdayPerson.Id == id, cancellationToken);

            if (birthdayPersonDao == null)
            {
                return null;
            }

            return new BirthdayPersonDtoOut
            {
                Id = birthdayPersonDao.Id,
                Name = birthdayPersonDao.Name,
                Birthday = birthdayPersonDao.Birthday
            };
        }

        public async Task EditAsync(int id, BirthdayPersonDtoIn birthdayPersonIn, CancellationToken cancellationToken)
        {
            var birthdayPersonDao = await _context.BirthdayPersons.FirstOrDefaultAsync(birthdayPerson => birthdayPerson.Id == id, cancellationToken);

            if (birthdayPersonDao != null)
            {
                birthdayPersonDao.Name = birthdayPersonIn.Name;
                birthdayPersonDao.Birthday = DateTime.SpecifyKind(birthdayPersonIn.Birthday, DateTimeKind.Utc);

                if (birthdayPersonIn.Avatar?.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await birthdayPersonIn.Avatar.CopyToAsync(memoryStream);

                        birthdayPersonDao.Avatar = memoryStream.ToArray();
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var birthdayPersonDao = await _context.BirthdayPersons.FirstOrDefaultAsync(birthdayPerson => birthdayPerson.Id == id, cancellationToken);

            if (birthdayPersonDao != null)
            {
                _context.BirthdayPersons.Remove(birthdayPersonDao);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
