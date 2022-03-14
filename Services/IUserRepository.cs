using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    interface IUserRepository
    {
        User GetUser(string username, string password);
    }
}
