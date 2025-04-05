using NeilMichaelportelliEPSolution.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeilMichaelPortelliEPSolution.Domain.Repositories
{
    public interface IPollRepository
    {
        Task<List<Poll>> GetPolls();
        Task<Poll?> GetPollById(int id);
        Task<Poll> CreatePoll(string title, string option1, string option2, string option3);
        Task<bool> Vote(int pollId, int option);
        Task UpdatePoll(Poll poll);
    }

}