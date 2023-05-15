using Test.Models;

namespace Test.Data
{
    public interface IDiary
    {
        Task<IEnumerable<Notes>> AllNotesAsync();

        Task<Notes> GetNoteByIdAsync(int id);

        Task AddNoteAsync(Notes note);

        Task DeleteNoteAsync(int id);

        Task UpdateNoteAsync(Notes note);
    }
}
