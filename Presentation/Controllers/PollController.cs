using Microsoft.AspNetCore.Mvc;
using NeilMichaelPortelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
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

        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPollsAsync();
            return View(polls);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int pollId, string choice)
        {
            var vote = new Vote { PollId = pollId, Choice = choice, VoterName = "Anonymous" };
            await _pollRepository.AddVoteAsync(vote);
            await _pollRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
