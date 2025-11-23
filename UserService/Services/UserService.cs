using UserService.Data;
using UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        private readonly RabbitMQPublisher _publisher;

        public UserService(UserDbContext context, RabbitMQPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User GetById(int id) => _context.Users.Find(id);

        public User Add(string name, string email)
        {
            var user = new User { Name = name, Email = email };
            _context.Users.Add(user);
            _context.SaveChanges();

            _publisher.PublishUserCreated(user);
            return user;
        }

        public User Update(int id, string name, string email)
        {
            var user = _context.Users.Find(id);
            if (user == null) return null;
            user.Name = name;
            user.Email = email;
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
