using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Interface;
using Test.Domain.Response;
using Test.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Test.Models;
using Test.Domain.Enum;
using Test.Helpers;
using Test.Domain.ViewModel;
using Test.DAL;

namespace Test.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Author> _userRepository;
        ApplicationDbContext _db;

        public AccountService(IBaseRepository<Author> userRepository, ApplicationDbContext db)
        {
            _userRepository = userRepository;
            _db = db;
        }

        private ClaimsIdentity Authenticate(Author user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var use = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                //var use = await _db.Authors.FirstOrDefaultAsync(x => x.Name == model.Name);
                if (use != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                Author user = new Author()
                {
                    Name = model.Name,
                    Role = Role.User.ToString(),
                    Password = HashPassword.HashPas(model.Password),
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPassword.HashPas(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

    }
}
