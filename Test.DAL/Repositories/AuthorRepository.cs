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
    public class AuthorRepository : IBaseRepository<Author>
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Author entity)
        {
            await _db.Author.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Author entity)
        {
            _db.Author.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Author> Get(int id)
        {
            return await _db.Author.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Author> GetAll()
        {
            return _db.Author;
        }

        public async Task<Author> GetByName(string name)
        {
            return await _db.Author.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Author> Update(Author entity)
        {
            _db.Author.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
