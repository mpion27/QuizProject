using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    interface IStateRepository
    {
        IEnumerable<State> GetRandomStates(int number);
    }
}
