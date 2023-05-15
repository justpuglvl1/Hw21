using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Response;
using Test.Domain.ViewModel;
using Test.Models;

namespace Test.Service.Interfaces
{
    public interface INotesService
    {
        BaseResponse<IEnumerable<Notes>> GetContacts();

        Task<IBaseResponse<Notes>> GetContactById(int id);

        Task<IBaseResponse<Notes>> GetContactByName(string name);

        Task<IBaseResponse<bool>> DeleteContact(int id);

        Task<IBaseResponse<Notes>> CreateContact(NotesViewModel cvm);

        Task<IBaseResponse<Notes>> Edit(int id, NotesViewModel model);
        Task<IBaseResponse<Notes>> Edit(NotesViewModel model);
    }
}
