using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WPF.Data.Interfaces;
using Test.WPF.Models;

namespace Test.WPF.Data
{
    public class DiaryDataTest : IDiaryData
    {
        readonly ObservableCollection<Notes> data;

        public DiaryDataTest()
        {
            data = new ObservableCollection<Notes>();
        }

        public IEnumerable<Notes> AllNotes()
        {
            return data;
        }

        public Notes GetNoteById(int id)
        {
            var note = data.FirstOrDefault(x => x.Id == id);
            return note;
        }

        public void AddNote(Notes note)
        {
            data.Add(note);
        }

        public void DeleteNote(int id)
        {
            var note = data.FirstOrDefault(x => x.Id == id);

            if (note != null)
                data.Remove(note);
        }

        public void UpdateNote(Notes note)
        {
            var currentNote = data.FirstOrDefault(x => x.Id == note.Id);

            if (currentNote != null)
            {
                int noteIndex = data.IndexOf(currentNote);
                data[noteIndex] = note;
            }
        }
    }
}
