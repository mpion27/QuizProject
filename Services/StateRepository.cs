using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    public class StateRepository : IStateRepository
    {
        private readonly StatesAndCapitalsContext db = new StatesAndCapitalsContext();
        public IEnumerable<State> GetRandomStates(int number)
        {
            return db.States.OrderBy(t => Guid.NewGuid()).Take(number);
        }
    }
}
