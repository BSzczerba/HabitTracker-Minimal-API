using FluentValidation;
using HabitTrackerMinimalAPI.HabitTracker;

namespace HabitTrackerMinimalAPI
{
    public static class ValidatorExtension
    {
        public static RouteHandlerBuilder WithValidator<T>(this RouteHandlerBuilder builder) 
            where T : class
        {
            builder.Add(endpointBuilder =>
            {
                var orginalDelegate = endpointBuilder.RequestDelegate;
                endpointBuilder.RequestDelegate = async httpcontext =>
                {
                   var validator = httpcontext.RequestServices.GetRequiredService<IValidator<T>>();
                   httpcontext.Request.EnableBuffering();
                   var body = await httpcontext.Request.ReadFromJsonAsync<T>();

                   if (body == null)
                   {
                        httpcontext.Response.StatusCode = StatusCodes.Status404NotFound;
                        await httpcontext.Response.WriteAsync("Couldn't map body to rquest model");
                        return;
                   }
                   var validationResult = validator.Validate(body);
                   if (!validationResult.IsValid) 
                   {
                        httpcontext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpcontext.Response.WriteAsJsonAsync(validationResult.Errors);
                        return;
                   }
                   httpcontext.Request.Body.Position = 0;
                   await orginalDelegate(httpcontext);
                };
            });
            return builder;
        }
    }
}
