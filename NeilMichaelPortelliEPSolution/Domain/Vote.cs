﻿using NeilMichaelportelliEPSolution.Domain;

namespace NeilMichaelPortelliEPSolution.Domain
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string UserId { get; set; }
        public string Option { get; set; } 
        public DateTime DateVoted { get; set; }

        public Poll Poll { get; set; } 
    }
}
