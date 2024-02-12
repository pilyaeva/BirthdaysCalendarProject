using BirthdaysCalendarProject.Infrastructure.Repositories;
using BirthdaysCalendarProject.Infrastructure.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirthdaysCalendarProject.Controllers
{
    public class BirthdayPersonsController : Controller
    {
        private readonly IBirthdayPersonsRepository _birthdayPersonsRepository;

        public BirthdayPersonsController(IBirthdayPersonsRepository birthdayPersonsRepository)
        {
            _birthdayPersonsRepository = birthdayPersonsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var birthdaysPersons = await _birthdayPersonsRepository.GetAllAsync(cancellationToken);

            foreach (var birthdayPerson in birthdaysPersons)
            {
                birthdayPerson.Birthday = new DateTime(DateTime.Now.Year, birthdayPerson.Birthday.Month, birthdayPerson.Birthday.Day);

                if (birthdayPerson.Birthday <  DateTime.Now)
                {
                    birthdayPerson.Birthday = birthdayPerson.Birthday.AddYears(1);
                }
            }

            return View(birthdaysPersons.OrderBy(birthdayPerson => birthdayPerson.Birthday).ToList());
        }

        [HttpGet("Add")]
        public IActionResult AddGet()
        {
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPost(BirthdayPersonDtoIn birthdayPersonDtoIn, CancellationToken cancellationToken)
        {
            await _birthdayPersonsRepository.AddAsync(birthdayPersonDtoIn, cancellationToken);

            return Redirect("/BirthdayPersons");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> EditGet(int id, CancellationToken cancellationToken)
        {
            var birthdayPerson = await _birthdayPersonsRepository.GetByIdAsync(id, cancellationToken);

            return View(birthdayPerson);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> EditPost(int id, BirthdayPersonDtoIn birthdayPersonDtoIn, CancellationToken cancellationToken)
        {
            await _birthdayPersonsRepository.EditAsync(id, birthdayPersonDtoIn, cancellationToken);

            return Redirect("/BirthdayPersons");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _birthdayPersonsRepository.DeleteAsync(id, cancellationToken);

            return Redirect("/BirthdayPersons");
        }
    }
}
