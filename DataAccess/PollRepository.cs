using NeilMichaelPortelliEPSolution.DataAccess;
using NeilMichaelPortelliEPSolution.Domain;
using NeilMichaelPortelliEPSolution.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NeilMichaelPortelliEPSolution.DataAccess
{
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

        public async Task<Poll> GetPollById(int id)
        {
            return await context.Polls.FindAsync(id);
        }

        public async Task<bool> Vote(int pollId, string userId, int option)
        {
            Poll poll = await context.Polls.FindAsync(pollId);
            if (poll == null) return false;

            if (await HasUserVoted(pollId, userId))
            {
                return false;
            }

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

            var vote = new Vote
            {
                PollId = pollId,
                UserId = userId,
                Option = option.ToString(),
                DateVoted = DateTime.UtcNow
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasUserVoted(int pollId, string userId)
        {
            var vote = await _context.Votes
                .FirstOrDefaultAsync(v => v.PollId == pollId && v.UserId == userId);

            return vote != null;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }

        public async Task Vote(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }
    }
}
