using DDD.Application.Common.Errors;
using DDD.Application.DI;
using DDD.Infrastructure.DI;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

//builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    var (statusCode, message) = exception switch
    {
        IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
        _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
    };

    return Results.Problem(statusCode: statusCode, title: message);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
