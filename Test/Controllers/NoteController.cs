using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Test.DAL;
using Test.Domain.ViewModel;
using Test.Models;
//using Test.Models;
using Test.Service.Interfaces;

namespace Test.Controllers
{
    public class NoteController : Controller
    {
        private readonly INotesService _contactService;
        ApplicationDbContext _db;

        public NoteController(INotesService contactService, ApplicationDbContext db)
        {
            _contactService = contactService;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _db.Notes.ToListAsync();
            return View(notes);
        }

        [HttpPut]
        public async Task<IActionResult> Details(int id)
        {
            var note = await _db.Notes.SingleAsync(x => x.Id == id);

            if (note == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(note);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Notes note)
        {
            if (ModelState.IsValid)
            {
                _db.Notes.Add(note);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Add));
            }
        }

        [HttpDelete("/Note/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _db.Notes.SingleAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _db.Notes.SingleAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            NotesViewModel nv = new NotesViewModel()
            {
                Address = note.Address,
                Name = note.Name,
                Id = note.Id,
                Iban = note.Iban,
                Phone = note.Phone,
                Surname = note.Surname
            };

            return View(nv);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(NotesViewModel note)
        {
            if (ModelState.IsValid)
            {
                await _contactService.Edit(note);
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
