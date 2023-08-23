using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace HabitTrackerMinimalAPI.HabitTracker
{
    public static class HabitRequest
    {
        public static WebApplication RegisterEndPoints(this WebApplication app)
        {
            app.MapGet("/habittracker", HabitRequest.GetAll)
                .Produces<List<Habit>>()
                .WithTags("Habit tracker")
                .RequireAuthorization();
            app.MapGet("/habittracker/{id}", HabitRequest.GetById)
                .Produces<Habit>()
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Habit tracker"); 
            app.MapPost("/habittracker", HabitRequest.Create)
                .WithValidator<Habit>()
                .Produces<Habit>(StatusCodes.Status201Created)
                .Accepts<Habit>("application/json")
                .WithTags("Habit tracker")
                .RequireAuthorization();
            app.MapPut("/habittracker/{id}", HabitRequest.Update)
                .WithValidator<Habit>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces<Habit>(StatusCodes.Status204NoContent)
                .Accepts<Habit>("application/json")
                .WithTags("Habit tracker"); 
            app.MapDelete("/habittracker/{id}", HabitRequest.Delete)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<Habit>(StatusCodes.Status204NoContent)
                .WithTags("Habit tracker")
                .ExcludeFromDescription();
            return app;
        }
        public static IResult GetAll(IHabitService service)
        {
            var habits = service.GetAll();
            return Results.Ok(habits);
        }
        public static IResult GetById(IHabitService service, Guid id)
        {
            var habit = service.GetbyId(id);
            if (habit == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(habit);
        }
        public static IResult Create(IHabitService service, Habit habit)
        {
            service.Create(habit);
            return Results.Created($"api/library/{habit.Id}", habit);
        }
        public static IResult Update(IHabitService service, Guid id, Habit habit)
        {
            var result = service.GetbyId(id);
            if (result == null)
            {
                return Results.NotFound();
            }
            service.Update(id,habit);
            return Results.NoContent();
        }
        public static IResult Delete(IHabitService service, Guid id)
        {
            var habit = service.GetbyId(id);
            if (habit == null)
            {
                return Results.NotFound();
            }
            service.Delete(id);
            return Results.NoContent();
        }

    }
}
