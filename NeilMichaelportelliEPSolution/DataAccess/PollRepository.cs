using Microsoft.EntityFrameworkCore;
using NeilMichaelportelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.EntityFrameworkCore;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PollRepository : IPollRepository
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
            DateCreated = DateTime.UtcNow,
            Option1VotesCount = 0,
            Option2VotesCount = 0,
            Option3VotesCount = 0
        };

        _context.Polls.Add(poll);
        await _context.SaveChangesAsync();
        return poll;
    }

    public async Task<List<Poll>> GetPolls()
    {
        return await _context.Polls.ToListAsync();
    }

    public async Task<Poll?> GetPollById(int id)
    {
        return await _context.Polls.FindAsync(id);
    }

    public async Task<bool> Vote(int pollId, int option)
    {
        var poll = await _context.Polls.FindAsync(pollId);
        if (poll == null) return false;

        switch (option)
        {
            case 1:
                poll.Option1VotesCount++;
                break;
            case 2:
                poll.Option2VotesCount++;
                break;
            case 3:
                poll.Option3VotesCount++;
                break;
            default:
                return false;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public Task UpdatePoll(Poll poll)
    {
        throw new NotImplementedException();
    }
}
