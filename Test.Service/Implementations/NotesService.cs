using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Interface;
using Test.Domain.Enum;
using Test.Domain.Response;
using Test.Domain.ViewModel;
using Test.Models;
using Test.Service.Interfaces;

namespace Test.Service.Implementations
{
    public class NotesService : INotesService
    {
        private readonly IBaseRepository<Notes> _repository;

        public NotesService(IBaseRepository<Notes> Repository)
        {
            _repository = Repository;
        }

        public async Task<IBaseResponse<Notes>> GetContactById(int id)
        {
            var baseResponse = new BaseResponse<Notes>();
            try
            {
                var contact = await _repository.Get(id);
                if (contact == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = contact;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Notes>()
                {
                    Description = $"[GetContact] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Notes>> GetContactByName(string name)
        {
            var baseResponse = new BaseResponse<Notes>();
            try
            {
                var contact = await _repository.GetByName(name);
                if (contact == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = contact;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Notes>()
                {
                    Description = $"[GetContact] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteContact(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var contact = await _repository.Get(id);
                if (contact == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                await _repository.Delete(contact);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteContact] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Notes>> CreateContact(NotesViewModel cvm)
        {
            try
            {
                var contact = new Notes()
                {
                    Name = cvm.Name,
                    Surname = cvm.Surname,
                    Phone = cvm.Phone,
                    Address = cvm.Address,
                    Iban = cvm.Iban,
                };

                await _repository.Create(contact);

                return new BaseResponse<Notes>()
                {
                    StatusCode = StatusCode.OK,
                    Data = contact,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Notes>()
                {
                    Description = $"[CreateContact] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<IEnumerable<Notes>> GetContacts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Notes>>();
            try
            {
                var contacts = _repository.GetAll().ToList();
                if (contacts.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = contacts;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Notes>>()
                {
                    Description = $"[GetContacts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Notes>> Edit(int id, NotesViewModel model)
        {
            var baseResponse = new BaseResponse<Notes>();
            try
            {
                var contact = await _repository.Get(id);
                if (contact == null)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = StatusCode.ContactNotFound;
                    return baseResponse;
                }

                contact.Id = model.Id;
                contact.Name = model.Name;
                contact.Surname = model.Surname;
                contact.Address = model.Address;
                contact.Phone = model.Phone;
                contact.Iban = model.Iban;

                await _repository.Update(contact);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Notes>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Notes>> Edit(NotesViewModel note)
        {
            try
            {
                var baseResponse = new BaseResponse<Notes>();
                {
                    Notes nt = new Notes()
                    {
                        Address = note.Address,
                        Name = note.Name,
                        Id = note.Id,
                        Iban = note.Iban,
                        Phone = note.Phone,
                        Surname = note.Surname
                    };

                    await _repository.Update(nt);

                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Notes>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
