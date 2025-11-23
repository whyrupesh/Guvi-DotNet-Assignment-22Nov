using Moq;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;

namespace PolicyNotesService.Tests.Unit;

public class PolicyNoteServiceTests
{
    private readonly Mock<IPolicyNoteRepository> _repoMock;
    private readonly PolicyNoteService _service;

    public PolicyNoteServiceTests()
    {
        _repoMock = new Mock<IPolicyNoteRepository>();
        _service = new PolicyNoteService(_repoMock.Object);
    }

    [Fact]
    public async Task AddNote_ShouldReturnCreatedNote()
    {
        var note = new PolicyNote { PolicyNumber = "P123", Note = "Hello" };
        _repoMock.Setup(r => r.AddAsync(note)).ReturnsAsync(note);

        var result = await _service.AddNoteAsync(note);

        Assert.Equal("P123", result.PolicyNumber);
        Assert.Equal("Hello", result.Note);
    }

    [Fact]
    public async Task GetAllNotes_ShouldReturnList()
    {
        var list = new List<PolicyNote> { new() { Id = 1, PolicyNumber = "P111" } };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

        var result = await _service.GetAllNotesAsync();

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
    }

    [Fact]
    public async Task GetNoteById_ShouldReturnCorrectNote()
    {
        var note = new PolicyNote { Id = 1, PolicyNumber = "P000" };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(note);

        var result = await _service.GetNoteByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}
