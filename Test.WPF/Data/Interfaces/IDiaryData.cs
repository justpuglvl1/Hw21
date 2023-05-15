using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPF.Models;

namespace Test.WPF.Data.Interfaces
{
    public interface IDiaryData
    {
        IEnumerable<Notes> AllNotes();

        Notes GetNoteById(int id);

        void AddNote(Notes note);

        void DeleteNote(int id);

        void UpdateNote(Notes note);
    }
}
