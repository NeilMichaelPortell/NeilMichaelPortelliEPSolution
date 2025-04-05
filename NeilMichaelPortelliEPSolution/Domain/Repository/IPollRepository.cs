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
        Task<bool> Vote(int pollId, string selectedOption, int option);
        Task UpdatePoll(Poll poll);
        Task<bool> HasUserVoted(int id, string? userId);
        Task Vote(int id, string selectedOption, string? userId);
    }

}