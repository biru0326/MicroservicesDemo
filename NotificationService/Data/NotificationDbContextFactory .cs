using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NotificationService.Data {
    public class NotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext> {
        public NotificationDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=NotificationDb;User Id=sa;Password=StrongPass123!;TrustServerCertificate=True;"
            );

            return new NotificationDbContext(optionsBuilder.Options);
        }
    }
}
