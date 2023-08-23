using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HabitTrackerMinimalAPI.HabitTracker
{
    public class HabitTrackerDBContext:DbContext
    {
        public HabitTrackerDBContext(DbContextOptions<HabitTrackerDBContext> options) : base(options) { }
        public DbSet<Habit> Habits { get; set; }
    }
}
