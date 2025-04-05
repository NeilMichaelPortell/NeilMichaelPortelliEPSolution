using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NeilMichaelPortelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using NeilMichaelPortelliEPSolution.Presentation.Filters;

namespace NeilMichaelPortelliEPSolution.Presentation.Controllers
{
    [Authorize] // Ensures that the user must be logged in to access these actions
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        // GET: Poll/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string option1Text, string option2Text, string option3Text)
        {
            await _pollRepository.CreatePoll(title, option1Text, option2Text, option3Text);
            return RedirectToAction(nameof(Index));
        }

        // GET: Poll/Index
        public async Task<IActionResult> Index()
        {
            var polls = await _pollRepository.GetPolls();
            return View(polls.OrderByDescending(p => p.DateCreated).ToList());
        }

        // GET: Poll/Vote/{id}
        public async Task<IActionResult> Vote(int id)
        {
            // Check if the user has already voted on this poll (using a session or other tracking mechanism)
            var userId = User.Identity.Name; // Assuming username is used as unique user identifier
            var hasVoted = await _pollRepository.HasUserVoted(id, userId);

            if (hasVoted)
            {
                TempData["ErrorMessage"] = "You have already voted in this poll.";
                return RedirectToAction(nameof(Index));
            }

            var poll = await _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }

        [ServiceFilter(typeof(EnsureUserHasNotVotedFilter))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vote(int id, int option)
        {
            var userId = User.Identity.Name; 

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var poll = await _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound(); 
            }

            if (await _pollRepository.HasUserVoted(id, userId))
            {
                TempData["Message"] = "You have already voted in this poll.";
                return RedirectToAction("Index");
            }
            bool voteSuccess = await _pollRepository.Vote(id, userId, option);
            if (!voteSuccess)
            {
                TempData["ErrorMessage"] = "Invalid vote or something went wrong!";
            }

            // Redirect back to the Index page after voting
            return RedirectToAction("Index");
        }



        //public async Task<IActionResult> Stats(int id)
        //{
        //    var poll = await _pollRepository.GetPollById(id);
        //    if (poll == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new StatsModel
        //    {
        //        Title = poll.Title,
        //        Option1Text = poll.Option1Text,
        //        Option2Text = poll.Option2Text,
        //        Option3Text = poll.Option3Text,
        //        Option1Votes = poll.Option1VotesCount,
        //        Option2Votes = poll.Option2VotesCount,
        //        Option3Votes = poll.Option3VotesCount
        //    };

        //    return View(model);
        //}
    }
}
