using Microsoft.EntityFrameworkCore;
using UserService.Models;

public class UserDbContext : DbContext {
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    // Add other DbSets here
}
