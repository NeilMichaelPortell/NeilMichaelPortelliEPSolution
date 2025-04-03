using NeilMichaelportelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domaian.EntityFrameworkCore;



public class PollRepository
{
    private readonly PollDbContext _context;
    public PollRepository(PollDbContext context)
    {
        _context = context;
    }

    public async Task<Poll> CreatePoll(string title, string option1, string option2, string option3)
    {
        var poll = new Poll
        {
            Title = title,
            Option1Text = option1,
            Option2Text = option2,
            Option3Text = option3,
            DateCreated = DateTime.Now
        };

        _context.Polls.Add(poll);
        await _context.SaveChangesAsync();
        return poll;
    }
}
