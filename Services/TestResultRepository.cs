using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly StatesAndCapitalsContext db = new StatesAndCapitalsContext();
        public TestResult GetLastTestResult(int UserId)
        {
            return db.TestResults.Where(f => f.UserId == UserId)
                .OrderByDescending(f => f.TestDateTime).FirstOrDefault();
        }

        public void SavetTestResult(TestResult tr)
        {
            db.TestResults.Add(tr);
            db.SaveChanges();
        }
    }
}
