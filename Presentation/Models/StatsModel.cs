namespace NeilMichaelportelliEPSolution.Presentation.Models
{
    public class StatsModel
    {
        public string Title { get; set; }
        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }
        public int Option1Votes { get; set; }
        public int Option2Votes { get; set; }
        public int Option3Votes { get; set; }

        public StatsModel() { }

        public StatsModel(string title, string option1Text, string option2Text, string option3Text,
                          int option1Votes, int option2Votes, int option3Votes)
        {
            Title = title;
            Option1Text = option1Text;
            Option2Text = option2Text;
            Option3Text = option3Text;
            Option1Votes = option1Votes;
            Option2Votes = option2Votes;
            Option3Votes = option3Votes;
        }
    }
}
