using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using NeilMichaelPortelliEPSolution.Domain.Repositories;

namespace NeilMichaelPortelliEPSolution.Presentation.Filters
{
    public class EnsureUserHasNotVotedFilter : IAsyncActionFilter
    {
        private readonly IPollRepository _pollRepository;

        public EnsureUserHasNotVotedFilter(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get the poll ID from the route
            var pollId = (int)context.ActionArguments["id"];
            var userId = context.HttpContext.User.Identity.Name;  // Assuming the user is logged in

            // Check if the user has voted for this poll
            var hasVoted = await _pollRepository.HasUserVoted(pollId, userId);

            if (hasVoted)
            {
                // If the user has already voted, redirect to Index page with an error message
                context.Result = new RedirectToActionResult("Index", "Poll", new { area = "" });
                context.HttpContext.Session.SetString("ErrorMessage", "You have already voted in this poll.");
            }
            else
            {
                // Continue with the action execution if the user hasn't voted
                await next();
            }
        }
    }
}
