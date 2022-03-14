using System;
using System.Collections.Generic;

#nullable disable

namespace QuizProject.Models
{
    public partial class TestResult
    {
        public int TestResultId { get; set; }
        public int? UserId { get; set; }
        public DateTime? TestDateTime { get; set; }
        public int? TotalQuestions { get; set; }
        public int? NumberCorrect { get; set; }

        public virtual User User { get; set; }
    }
}
