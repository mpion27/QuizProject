using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizProject.Models;

namespace QuizProject.Services
{
    interface ITestResultRepository
    {
        TestResult GetLastTestResult(int UserId);
        void SavetTestResult(TestResult tr);
    }
}
