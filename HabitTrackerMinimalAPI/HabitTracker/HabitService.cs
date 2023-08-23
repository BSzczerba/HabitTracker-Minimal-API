namespace HabitTrackerMinimalAPI.HabitTracker
{
    public interface IHabitService
    {
        void Create(Habit guid);
        void Delete(Guid guid);
        List<Habit> GetAll();
        Habit GetbyId(Guid guid);
        void Update(Guid guid, Habit edit);
    }

    public class HabitService : IHabitService
    {
        private readonly HabitTrackerDBContext _context;

        public HabitService(HabitTrackerDBContext context)
        {
            this._context = context;
        }
        public List<Habit> GetAll()
        {
            var habits = _context.Habits.ToList();            
            return habits;
        }
        public Habit GetbyId(Guid guid)
        {
            var habit = _context.Habits.FirstOrDefault(x => x.Id.Equals(guid));
            return habit;
        }
        public void Create(Habit habit)
        {
            _context.Habits.Add(habit);
            _context.SaveChanges();
        }
        public void Update(Guid guid,Habit edit)
        {
            var habit = _context.Habits.FirstOrDefault(x => x.Id.Equals(guid));
            habit.Description = edit.Description;
            habit.Name = edit.Name;
            habit.StartTime = edit.StartTime;
            habit.FinishTime = edit.FinishTime;
            habit.IsCompleted = edit.IsCompleted;
            _context.SaveChanges();
        }
        public void Delete(Guid guid)
        {
            var habit = _context.Habits.FirstOrDefault(x => x.Id.Equals(guid));
            _context.Habits.Remove(habit);
            _context.SaveChanges();
        }
    }
}
