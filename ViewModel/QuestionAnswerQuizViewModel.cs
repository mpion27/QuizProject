using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.ViewModel
{
    public class QuestionAnswerQuizViewModel
    {
        public QuestionAnswerQuizViewModel()
        {
            QuestionsListViewModel = new List<QuestionAnswerViewModel>();
        }
        public int NumberCorrect { get; set; }
        public string Name { get; set; }
        public string QuestionDescription { get; set; }
        public string AnswerDescription { get; set; }
        public List<QuestionAnswerViewModel> QuestionsListViewModel { get; set; }
    }
}
