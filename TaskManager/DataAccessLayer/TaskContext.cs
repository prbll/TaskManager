using System.Data.Entity;
using TaskManager.DataAccessLayer.Models;

namespace TaskManager.DataAccessLayer
{
    public class TaskContext : DbContext
    {
        public TaskContext() : base("DbConnection")
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}