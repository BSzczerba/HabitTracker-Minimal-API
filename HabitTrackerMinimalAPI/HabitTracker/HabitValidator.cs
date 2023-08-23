using FluentValidation;

namespace HabitTrackerMinimalAPI.HabitTracker
{
    public class HabitValidator: AbstractValidator<Habit>
    {
        public HabitValidator() 
        {
            RuleFor(t => t.Name).NotEmpty().MaximumLength(25);
            RuleFor(t => t.StartTime).NotNull();
            RuleFor(t => t.FinishTime).NotNull();
            RuleFor(t => t.IsCompleted).NotNull();
        }
    }
}
