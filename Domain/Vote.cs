using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeilMichaelPortelliEPSolution.Domain
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PollId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Option { get; set; }  // Assuming it's stored as string (1, 2, 3)

        public DateTime DateVoted { get; set; }

        [ForeignKey("PollId")]
        public Poll Poll { get; set; }
    }
}
