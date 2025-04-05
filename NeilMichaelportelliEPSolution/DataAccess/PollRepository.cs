using Microsoft.EntityFrameworkCore;
using NeilMichaelportelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain;
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

    // Implemented Vote method with user check
    public async Task<bool> Vote(int pollId, string userId, int option)
    {
        var poll = await _context.Polls.FindAsync(pollId);
        if (poll == null) return false;

        // Check if the user has already voted on this poll
        if (await HasUserVoted(pollId, userId))
        {
            return false;  // User already voted
        }

        // Increment the vote count for the selected option
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
                return false;  // Invalid option
        }

        // Record the vote for the user
        var vote = new Vote
        {
            PollId = pollId,
            UserId = userId,
            Option = option.ToString(),  // Store the option number as a string (1, 2, 3)
            DateVoted = DateTime.UtcNow
        };

        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();
        return true;
    }

    // Implemented HasUserVoted method
    public async Task<bool> HasUserVoted(int pollId, string userId)
    {
        // Check if there's any vote record for this poll by this user
        var vote = await _context.Votes
            .FirstOrDefaultAsync(v => v.PollId == pollId && v.UserId == userId);

        return vote != null;  // Return true if the user has voted, otherwise false
    }

    // Stub for updating a poll (implement as needed)
    public async Task UpdatePoll(Poll poll)
    {
        _context.Polls.Update(poll);
        await _context.SaveChangesAsync();
    }

    public async Task Vote(int id, string selectedOption, string? userId)
    {
        // Find the poll by its ID
        var poll = await _context.Polls.FindAsync(id);
        if (poll == null)
        {
            throw new InvalidOperationException("Poll not found");
        }

        // Check if the user has already voted in this poll
        if (await HasUserVoted(id, userId))
        {
            throw new InvalidOperationException("User has already voted");
        }

        // Increment the appropriate vote count based on the selected option
        switch (selectedOption)
        {
            case "1":
                poll.Option1VotesCount++;
                break;
            case "2":
                poll.Option2VotesCount++;
                break;
            case "3":
                poll.Option3VotesCount++;
                break;
            default:
                throw new ArgumentException("Invalid option selected");
        }

        // Record the user's vote
        var vote = new Vote
        {
            PollId = id,
            UserId = userId,
            Option = selectedOption, // Storing the option as a string (1, 2, 3)
            DateVoted = DateTime.UtcNow
        };

        // Add the vote to the Votes table
        _context.Votes.Add(vote);

        // Save changes to the database
        await _context.SaveChangesAsync();
    }

}
