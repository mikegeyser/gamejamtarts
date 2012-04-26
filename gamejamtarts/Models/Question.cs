using System.Collections.Generic;

namespace gamejamtarts.Models
{
    public class Question
    {
        public Question(string question, string answer)
        {
            Text = question;
            Answer = answer;
        }

        public string Text { get; private set; }
        public string Answer { get; private set; }        

        public static IEnumerable<Question> DefaultQuestions()
        {
            return new List<Question>()
                               {
                                   new Question("What are the rules for the jam?",
                                                "Development should only start after the theme is announced on Saturday 14 April. There is no honour in cheating. :P"),
                                   new Question("Are we working alone or in groups?",
                                                "Your choice, but team development is encouraged"),
                                    new Question("Will there be a theme?",
                                                 "Yes, the theme will be chosen on the night using a raffle like mechanic."),
                                    new Question("Can we bring creative commons assets (art/sounds/music) if we work alone?",
                                                 "Creative commons assets are fine, no matter what. Just be sure to give credit where credit is due."),
                               };
        }
    }
}
