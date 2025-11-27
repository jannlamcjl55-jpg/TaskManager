using Microsoft.EntityFrameworkCore;
namespace TaskManager.Models
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Member>Members { get; set; }
    }
}
