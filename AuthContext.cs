using Microsoft.EntityFrameworkCore;
namespace AuthMVCEntity.Models
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}