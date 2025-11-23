using System.Collections.Generic;
using UserService.Models;

namespace Day3.CleanApi.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User Add(User user);
        User Update(int id, string name);
        bool Delete(int id);
    }
}
