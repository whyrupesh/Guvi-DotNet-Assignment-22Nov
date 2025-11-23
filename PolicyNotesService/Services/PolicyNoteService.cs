using PolicyNotesService.Models;
using PolicyNotesService.Repositories;

namespace PolicyNotesService.Services;

public class PolicyNoteService : IPolicyNoteService
{
    private readonly IPolicyNoteRepository _repository;

    public PolicyNoteService(IPolicyNoteRepository repository)
    {
        _repository = repository;
    }

    public Task<PolicyNote> AddNoteAsync(PolicyNote note)
        => _repository.AddAsync(note);

    public Task<List<PolicyNote>> GetAllNotesAsync()
        => _repository.GetAllAsync();

    public Task<PolicyNote?> GetNoteByIdAsync(int id)
        => _repository.GetByIdAsync(id);
}
