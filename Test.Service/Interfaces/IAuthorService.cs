using Microsoft.AspNet.Identity;
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
    public interface IAuthorService
    {
        Task<IBaseResponse<Author>> Create(AuthorViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<AuthorViewModel>>> GetUsers();

        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
