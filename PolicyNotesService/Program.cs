using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;
using PolicyNotesService.Models;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register InMemory DB
builder.Services.AddDbContext<PolicyNotesDbContext>(options =>
    options.UseInMemoryDatabase("PolicyNotesDb"));

// Register Repository + Service
builder.Services.AddScoped<IPolicyNoteRepository, PolicyNoteRepository>();
builder.Services.AddScoped<IPolicyNoteService, PolicyNoteService>();

var app = builder.Build();

// Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/notes", async (PolicyNote note, IPolicyNoteService service) =>
{
    var created = await service.AddNoteAsync(note);
    return Results.Created($"/notes/{created.Id}", created);
});

app.MapGet("/notes", async (IPolicyNoteService service) =>
{
    var notes = await service.GetAllNotesAsync();
    return Results.Ok(notes);
});

app.MapGet("/notes/{id:int}", async (int id, IPolicyNoteService service) =>
{
    var note = await service.GetNoteByIdAsync(id);
    return note is not null ? Results.Ok(note) : Results.NotFound();
});

app.Run();

// Required for Integration Tests
public partial class Program { }
