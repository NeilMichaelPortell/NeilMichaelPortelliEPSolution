using Microsoft.AspNetCore.Mvc.RazorPages;
using NeilMichaelportelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StatsModel : PageModel
{
    private readonly PollRepository _pollRepository;
    private IPollRepository pollRepository;

    public List<Poll> Polls { get; set; }
    public string Option3Text { get; internal set; }
    public int Option1Votes { get; internal set; }
    public int Option3Votes { get; internal set; }
    public int Option2Votes { get; internal set; }
    public string Option2Text { get; internal set; }
    public string Option1Text { get; internal set; }
    public string Title { get; internal set; }

    public StatsModel(PollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }

    public StatsModel(IPollRepository pollRepository)
    {
        this.pollRepository = pollRepository;
    }

    public async Task OnGet()
    {
        Polls = await _pollRepository.GetPolls();
    }

    internal void IncrementVote(int optionNumber)
    {
        throw new NotImplementedException();
    }
}
