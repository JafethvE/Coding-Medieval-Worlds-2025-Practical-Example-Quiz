using System.Collections.Generic;

namespace nl.jafeth.van.elten.coding.medieval.worlds.practical.example.quiz
{
    public class QuizQuestion
    {
        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> IncorrectAnswers { get; set; }

        public QuizQuestion(string question, string correctAnswer, List<string> incorrectAnswers)
        {
            this.Question = question;
            this.CorrectAnswer = correctAnswer;
            this.IncorrectAnswers = incorrectAnswers;
        }
    }
}