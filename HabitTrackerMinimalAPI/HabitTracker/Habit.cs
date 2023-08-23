namespace HabitTrackerMinimalAPI.HabitTracker
{
    public class Habit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
