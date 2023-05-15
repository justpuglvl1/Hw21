using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Interface;
using Test.Domain.Enum;
using Test.Domain.Extensions;
using Test.Domain.Response;
using Test.Domain.ViewModel;
using Test.Helpers;
using Test.Models;
using Test.Service.Interfaces;

namespace Test.Service.Implementations
{
    internal class AuthorService : IAuthorService
    {
        private readonly IBaseRepository<Author> _userRepository;

        public async Task<IBaseResponse<Author>> Create(AuthorViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<Author>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.UserAlreadyExist
                    };
                }

                user = new Author()
                {
                    Name = model.Name,
                    Password = HashPassword.HashPas(model.Password),
                    Role = Enum.Parse<Role>(model.Role).ToString(),
                };

                await _userRepository.Create(user);

                return new BaseResponse<Author>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Author>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Create(User): {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var baseResponse = new BaseResponse<bool>()
                {
                    Data = true
                };
                var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    baseResponse.Data = false;
                    return baseResponse;
                }

                await _userRepository.Delete(user);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Delete(User): {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<AuthorViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new AuthorViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToListAsync();

                return new BaseResponse<IEnumerable<AuthorViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<AuthorViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
