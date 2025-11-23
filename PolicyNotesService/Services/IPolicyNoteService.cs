using PolicyNotesService.Models;

namespace PolicyNotesService.Services;

public interface IPolicyNoteService
{
    Task<PolicyNote> AddNoteAsync(PolicyNote note);
    Task<List<PolicyNote>> GetAllNotesAsync();
    Task<PolicyNote?> GetNoteByIdAsync(int id);
}
