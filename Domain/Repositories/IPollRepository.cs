using NeilMichaelPortelliEPSolution.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeilMichaelPortelliEPSolution.Domain.Repositories
{
    public interface IPollRepository
    {
        Task<List<Poll>> GetPolls();
        Task<Poll> GetPollById(int id);
        Task CreatePoll(Poll poll);
        Task Vote(Vote vote);
        Task SaveChanges();
    }
}
