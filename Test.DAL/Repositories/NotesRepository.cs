using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Interface;
using Test.Models;

namespace Test.DAL.Repositories
{
    public class NotesRepository : IBaseRepository<Notes>
    {
        private readonly ApplicationDbContext _db;

        public NotesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Notes entity)
        {
            await _db.Notes.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Notes> Get(int id)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Notes> GetAll()
        {
            return _db.Notes;
        }

        public async Task<bool> Delete(Notes entity)
        {
            _db.Notes.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Notes> GetByName(string name)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Notes> Update(Notes entity)
        {
            _db.Notes.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
