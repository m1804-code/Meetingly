using meetingly_webapi.Data;
using meetingly_webapi.Enums;
using meetingly_webapi.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MeetinglyDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.SerializerOptions.MaxDepth = 64;
});

var app = builder.Build();
InitializeDatabase(app);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meetingly API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void InitializeDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetService<MeetinglyDbContext>()!;
        dbContext.Users.AddRangeAsync(
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Doe" },
                new User { Id = 3, Name = "John Smith" });
        dbContext.ScheduledDates.AddRange(
            new ScheduledDate
            {
                Topic = "Test Topic 1",
                Description = "Test Description 1",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                AvailabilityType = AvailabilityType.Propositon,
                EventType = EventType.CityParty,
                Status = Status.New,
                Source = Source.User,
                UserId = 1
            },
            new ScheduledDate
            {
                Topic = "Test Topic 2",
                Description = "Test Description 2",
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(4),
                AvailabilityType = AvailabilityType.RatherAvailable,
                EventType = EventType.None,
                Status = Status.New,
                Source = Source.User,
                UserId = 2
            },
            new ScheduledDate
            {
                Topic = "Test Topic 3",
                Description = "Test Description 3",
                StartDate = DateTime.UtcNow.AddDays(4),
                EndDate = DateTime.UtcNow.AddDays(6),
                AvailabilityType = AvailabilityType.Propositon,
                EventType = EventType.HomeParty,
                Status = Status.Accepted,
                Source = Source.User,
                UserId = 2
            }
            );

        dbContext.SaveChanges();
    }
}