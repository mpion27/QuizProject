using Microsoft.AspNetCore.Mvc;
using QuizProject.Models;
using QuizProject.Services;
using QuizProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Controllers
{
    public class QuizController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly ITestResultRepository _testResultRepository;

        public QuizController()
        {
            _stateRepository = new StateRepository();
            _testResultRepository = new TestResultRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TakeQuiz(int id)
        {
            QuestionAnswerQuizViewModel QuizVM = new QuestionAnswerQuizViewModel();
            TempData["quizID"] = id;

            switch (id)
            {
                case 1: //State quiz

                    var StatetList = _stateRepository.GetRandomStates(10);

                    QuizVM.QuestionDescription = "State";
                    QuizVM.Name = "States and Capitals Quiz";
                    QuizVM.AnswerDescription = "Capital";
                    foreach (var s in StatetList)
                    {
                        QuestionAnswerViewModel qvm = new QuestionAnswerViewModel()
                        {
                            Id = s.StateId,
                            Question = s.State1,
                            CorrectAnswer = s.Capital,
                            IsCorrect = false
                        };

                        QuizVM.QuestionsListViewModel.Add(qvm);
                    }

                    break;
            }
            return View(QuizVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizResult(QuestionAnswerQuizViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return RedirectToAction("TakeQuiz", new { id = TempData["quizID"] });
            }

            foreach (var q in model.QuestionsListViewModel)
            {
                if (q.ProvidedAnswer == null) { q.ProvidedAnswer = ""; }

                if (string.Equals(q.CorrectAnswer.Trim(), q.ProvidedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    model.NumberCorrect++;
                    q.IsCorrect = true;
                }
            }
            
            var UserId = User.FindFirst("UserId").Value;
            TestResult tr = new TestResult()
            {
                UserId = int.Parse(UserId),
                TestDateTime = DateTime.Now,
                TotalQuestions = model.QuestionsListViewModel.Count,
                NumberCorrect = model.NumberCorrect
            };
            _testResultRepository.SavetTestResult(tr);

            return View(model);
        }
    }
}
