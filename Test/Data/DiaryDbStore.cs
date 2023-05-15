using Microsoft.EntityFrameworkCore;
using Test.DAL;
using Test.Models;

namespace Test.Data
{
    public class DiaryDbStore : IDiary
    {
        private readonly ApplicationDbContext _db;

        public DiaryDbStore(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Notes>> AllNotesAsync()
        {
            return await _db.Notes.ToListAsync();
        }

        public async Task<Notes> GetNoteByIdAsync(int id)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddNoteAsync(Notes note)
        {
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Notes note)
        {
            _db.Notes.Update(note);
            await _db.SaveChangesAsync();
        }
    }
}
