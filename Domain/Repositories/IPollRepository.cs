using System.Collections.Generic;
using System.Threading.Tasks;
using NeilMichaelPortelliEPSolution.Domain;

namespace NeilMichaelPortelliEPSolution.Domain.Repositories
{
    public interface IPollRepository
    {
        Task<Poll> CreatePoll(string title, string option1, string option2, string option3);
        Task<List<Poll>> GetPolls();
        Task<Poll> GetPollById(int id);
        Task<bool> Vote(int pollId, string userId, int option);
        Task<bool> HasUserVoted(int pollId, string userId);
        Task SaveChanges();
        Task CreatePoll(Poll poll);
        Task Vote(Vote vote);
    }
}
