using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.ViewModel
{
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }

        [Required]
        [DisplayName("Answer")]
        public string ProvidedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
