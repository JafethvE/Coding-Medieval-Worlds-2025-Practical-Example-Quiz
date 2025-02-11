using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using nl.jafeth.van.elten.coding.medieval.worlds.practical.example.quiz;
using nl.jafeth.van.elten.coding.medieval.worlds.practical.example.enums;

namespace nl.jafeth.van.elten.coding.medieval.worlds.practical.example.game.logic
{
    public class QuizManager : MonoBehaviour
    {
        private List<QuizQuestion> AllQuestions;
        private List<QuizQuestion> QuestionsForCurrentQuiz = new List<QuizQuestion>();
        private QuizQuestion CurrentQuestion;
        private List<QuizQuestion> CorrectlyAnsweredQuestions = new List<QuizQuestion>();
        private List<QuizQuestion> IncorrectlyAnsweredQuestions = new List<QuizQuestion>();

        private bool playing = false;

        [SerializeField]
        private TextMeshProUGUI QuestionText;

        [SerializeField]
        private TextMeshProUGUI ResultText;

        [SerializeField]
        private TextMeshProUGUI TopLeftButtonText;

        [SerializeField]
        private TextMeshProUGUI TopRightButtonText;

        [SerializeField]
        private TextMeshProUGUI BottomLeftButtonText;

        [SerializeField]
        private TextMeshProUGUI BottomRightButtonText;

        [SerializeField]
        private GameObject ReplayButton;

        [SerializeField]
        private int AmountOfQuestionsPerQuiz;

        private Button CurrentRightAnswerButton;

        void Start()
        {
            LoadAllQuestions();
            PickQuestionsForCurrentQuiz();
            PickNextQuestion();
        }
        private void LoadAllQuestions()
        {
            AllQuestions = JsonConvert.DeserializeObject<List<QuizQuestion>>(Resources.Load<TextAsset>("QuizData/QuizData").text);
        }

        private void PickQuestionsForCurrentQuiz()
        {
            playing = true;
            List<QuizQuestion> allAvailableQuestions = new List<QuizQuestion>(AllQuestions);
            int i = 0;
            while (i < AmountOfQuestionsPerQuiz && allAvailableQuestions.Count > 0)
            {
                int randomIndex = Random.Range(0, allAvailableQuestions.Count);
                QuestionsForCurrentQuiz.Add(allAvailableQuestions[randomIndex]);
                allAvailableQuestions.RemoveAt(randomIndex);
            }
        }

        void PickNextQuestion()
        {
            int randomIndex = Random.Range(0, QuestionsForCurrentQuiz.Count);
            CurrentQuestion = QuestionsForCurrentQuiz[randomIndex];
            QuestionsForCurrentQuiz.RemoveAt(randomIndex);

            DisplayCurrentQuestion();
        }
        private void DisplayCurrentQuestion()
        {
            QuestionText.text = CurrentQuestion.Question;

            List<TextMeshProUGUI> allAvailableButtons = new List<TextMeshProUGUI>
            {
                TopLeftButtonText,
                TopRightButtonText,
                BottomLeftButtonText,
                BottomRightButtonText
            };

            int randomIndex = Random.Range(0, allAvailableButtons.Count);
            TextMeshProUGUI correctAnswerText = allAvailableButtons[randomIndex];

            if (correctAnswerText.Equals(TopLeftButtonText))
            {
                CurrentRightAnswerButton = Button.TopLeftButton;
            }
            else if(correctAnswerText.Equals(TopRightButtonText))
            {
                CurrentRightAnswerButton = Button.TopRightButton;
            }
            else if(correctAnswerText.Equals(BottomLeftButtonText))
            {
                CurrentRightAnswerButton = Button.BottomLeftButton;
            }
            else if(correctAnswerText.Equals(BottomRightButtonText))
            {
                CurrentRightAnswerButton = Button.BottomRightButton;
            }

            correctAnswerText.text = CurrentQuestion.CorrectAnswer;
            allAvailableButtons.RemoveAt(randomIndex);

            foreach (string incorrectAnswer in CurrentQuestion.IncorrectAnswers)
            {
                randomIndex = Random.Range(0, allAvailableButtons.Count);
                allAvailableButtons[randomIndex].text = incorrectAnswer;
                allAvailableButtons.RemoveAt(randomIndex);
            }
        }

        private void NextQuestionOrEndOfQuiz()
        {
            if(CorrectlyAnsweredQuestions.Count + IncorrectlyAnsweredQuestions.Count < AmountOfQuestionsPerQuiz)
            {
                PickNextQuestion();
            }
            else
            {
                EndOfGame();
            }
        }

        private void EndOfGame()
        {
            playing = false;
            ResultText.text = "You answered " + CorrectlyAnsweredQuestions.Count + " out of " + (CorrectlyAnsweredQuestions.Count + IncorrectlyAnsweredQuestions.Count) + " correctly!";
            ReplayButton.SetActive(true);
        }

        private void Replay()
        {
            ResultText.text = "";
            QuestionText.text = "";
            CurrentQuestion = null;
            CorrectlyAnsweredQuestions.Clear();
            IncorrectlyAnsweredQuestions.Clear();
            QuestionsForCurrentQuiz.Clear();
            ReplayButton.SetActive(false);

            PickQuestionsForCurrentQuiz();
            PickNextQuestion();
        }

        public void OnClickTopLeftButton()
        {
            if (playing)
            {
                if (CurrentRightAnswerButton.Equals(Button.TopLeftButton))
                {
                    CorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                else
                {
                    IncorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                NextQuestionOrEndOfQuiz();
            }
        }

        public void OnClickTopRightButton()
        {
            if (playing)
            {
                if (CurrentRightAnswerButton.Equals(Button.TopRightButton))
                {
                    CorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                else
                {
                    IncorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                NextQuestionOrEndOfQuiz();
            }
        }

        public void OnClickBottomLeftButton()
        {
            if (playing)
            {
                if (CurrentRightAnswerButton.Equals(Button.BottomLeftButton))
                {
                    CorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                else
                {
                    IncorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                NextQuestionOrEndOfQuiz();
            }
        }

        public void OnClickBottomRightButton()
        {
            if (playing)
            {
                if (CurrentRightAnswerButton.Equals(Button.BottomRightButton))
                {
                    CorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                else
                {
                    IncorrectlyAnsweredQuestions.Add(CurrentQuestion);
                }
                NextQuestionOrEndOfQuiz();
            }
        }

        public void OnClickReplayButton()
        {
            if(!playing)
            {
                Replay();
            }
        }
    }
}