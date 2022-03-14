using System;
using System.Collections.Generic;

#nullable disable

namespace QuizProject.Models
{
    public partial class User
    {
        public User()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
