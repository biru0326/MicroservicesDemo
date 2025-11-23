using System.Collections.Generic;
using System.Linq;
using UserService.Models;

namespace Day3.CleanApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Ram" },
            new User { Id = 2, Name = "Shyam" }
        };

        public List<User> GetAll() => _users;

        public User GetById(int id) => _users.FirstOrDefault(x => x.Id == id);

        public User Add(User user)
        {
            user.Id = _users.Max(x => x.Id) + 1;
            _users.Add(user);
            return user;
        }

        public User Update(int id, string name)
        {
            var u = _users.FirstOrDefault(x => x.Id == id);
            if (u == null) return null;

            u.Name = name;
            return u;
        }

        public bool Delete(int id)
        {
            var u = _users.FirstOrDefault(x => x.Id == id);
            if (u == null) return false;

            _users.Remove(u);
            return true;
        }
    }
}
