using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.ViewModel
{
    public class TestResultViewModel
    {
        public DateTime TestDate { get; set; }
        public int TotalQuestions { get; set; }
        public int NumberCorrect { get; set; }
        public double Result { get; set; }
    }
}
