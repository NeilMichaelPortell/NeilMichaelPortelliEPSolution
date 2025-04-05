using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NeilMichaelPortelliEPSolution.Domain;
using NeilMichaelportelliEPSolution.Domain;

namespace NeilMichaelPortelliEPSolution.Domain.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string _filePath;

        public PollFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<Poll> CreatePoll(string title, string option1, string option2, string option3)
        {
            var polls = await GetPolls();

            var poll = new Poll
            {
                Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1,
                Title = title,
                Option1Text = option1,
                Option2Text = option2,
                Option3Text = option3,
                DateCreated = DateTime.UtcNow,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0
            };

            polls.Add(poll); // Add the new poll
            await SavePollsToFile(polls); // Save the updated list of polls to the file
            return poll;
        }

        // GetPolls - Reads all the polls from the JSON file
        public async Task<List<Poll>> GetPolls()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Poll>(); // If the file doesn't exist, return an empty list
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<Poll>>(json) ?? new List<Poll>(); // Deserialize the JSON into a list of Polls
        }

        // GetPollById - Retrieves a poll by ID
        public async Task<Poll?> GetPollById(int id)
        {
            var polls = await GetPolls();
            return polls.FirstOrDefault(p => p.Id == id);
        }

        // Vote - Updates vote count for a poll option
        public async Task<bool> Vote(int pollId, int option)
        {
            var polls = await GetPolls();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);

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

            await SavePollsToFile(polls);
            return true;
        }

        // Helper method to save the list of polls to the JSON file
        private async Task SavePollsToFile(List<Poll> polls)
        {
            var json = JsonConvert.SerializeObject(polls, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public Task UpdatePoll(Poll poll)
        {
            throw new NotImplementedException();
        }
    }
}
