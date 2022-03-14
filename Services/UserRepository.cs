using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly StatesAndCapitalsContext db = new StatesAndCapitalsContext();
        public User GetUser(string username, string password)
        {
            return db.Users.FirstOrDefault(e => e.UserName == username && e.Password == password);
        }
    }
}
