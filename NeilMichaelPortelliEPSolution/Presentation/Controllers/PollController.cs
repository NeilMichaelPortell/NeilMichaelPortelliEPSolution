using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeilMichaelPortelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using NeilMichaelPortelliEPSolution.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NeilMichaelPortelliEPSolution.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string option1Text, string option2Text, string option3Text)
        {
            await _pollRepository.CreatePoll(title, option1Text, option2Text, option3Text);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPolls();
            return View(polls.OrderByDescending(p => p.DateCreated).ToList());
        }

        public async Task<IActionResult> Vote(int id)
        {
            var poll = await _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }
        //public async Task<IActionResult> Stats(int id)
        //{
        //    var poll = await _pollRepository.GetPollById(id);
        //    if (poll == null)
        //    {
        //        return NotFound();
        //    }

        //    // Create the StatsModel using the constructor
        //    var model = new StatsModel(
        //        poll.Title,
        //        poll.Option1Text,
        //        poll.Option2Text,
        //        poll.Option3Text,
        //        poll.Option1VotesCount,
        //        poll.Option2VotesCount,
        //        poll.Option3VotesCount,
        //        poll.DateCreated
        //    );

        //    return View(model);
        //}

    }
}
