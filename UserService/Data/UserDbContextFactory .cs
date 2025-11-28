using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserService.Data {
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext> {
        public UserDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=UserServiceDb;User Id=sa;Password=StrongPass123!;TrustServerCertificate=True;"
            );

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
