using System.Collections.Generic;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        User Add(string name, string email);
        User Update(int id, string name, string email);
        bool Delete(int id);
    }
}
