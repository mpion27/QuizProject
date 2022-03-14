using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ITestResultRepository _testResultRepository;
        public ProfileController() {
            _testResultRepository = new TestResultRepository();
        }

        public IActionResult Index()
        {
            if(User.FindFirst("UserId") == null) return View();

            var UserId = User.FindFirst("UserId").Value;

            TestResult t = _testResultRepository.GetLastTestResult(int.Parse(UserId));

            if (t == null)
            {
                return View();
            }
            TestResultViewModel viewModel = new TestResultViewModel()
            {
                TestDate = t.TestDateTime.Value,
                TotalQuestions = t.TotalQuestions.Value,
                NumberCorrect = t.NumberCorrect.Value
            };

            if (t.NumberCorrect == 0)
            {
                viewModel.Result = 0;
            }
            else
            {
                viewModel.Result = (viewModel.NumberCorrect * 100) / viewModel.TotalQuestions;
            }
            return View(viewModel);
        }
    }
}
