using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.Services
{
    public class UserService : IUserService
    {
        private readonly PasswordService _passwordService;
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
            _passwordService = new PasswordService();

        }

        public User GetByEmail(string email)
        {
            try
            {
                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("El formato del correo electrónico es inválido.");
                }

                var user = _repository.GetByEmail(email);

                if (user == null)
                {
                    throw new Exception("No se encontró ningún usuario con ese correo electrónico.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }

        public List<UserDTO> Get()
        {
            var users = _repository.Get();
            return users.Select(u => new UserDTO
            {
                Id = u.Id.ToString(),
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Role = u.Role,
                Store = u.Store != null ? new StoreUserDTO
                {
                    Id = u.Store.Id.ToString(),
                    Name = u.Store.Name
                } : null
            }).ToList();
        }

        public UserModel CreateUser(UserCreateDTO userDTO)
        {

            string hashedPassword = _passwordService.HashPassword(userDTO.Password);
            var user = new User
            {
                Email = userDTO.Email,
                Password = hashedPassword,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
            };

            _repository.AddUser(user);

            return new UserModel
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        public User? DeleteUser(Guid id)
        {
            return _repository.DeleteUser(id);
        }

        public User? CheckCredentials(CredentialsRequest credentials)
        {
            try
            {
                var user = GetByEmail(credentials.Email);

                if (user == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                bool isPasswordValid = _passwordService.VerifyPassword(user.Password, credentials.Password);
                if (!isPasswordValid)
                {
                    throw new Exception("Credenciales inválidas.");
                }

                user.Password = string.Empty; // Limpiar la contraseña antes de retornar
                return user;
            }
            catch (Exception ex)
            {
                // Puedes registrar la excepción si es necesario
                // LogException(ex);

                throw new Exception($"Error en CheckCredentials: {ex.Message}");
            }
        }

        public void Update(UserUpdateDTO user, Guid id, int Opt)
        {
            var existingUser = _repository.GetById(id);

            if (existingUser == null)
            {
                throw new ApplicationException($"No se encontró ningún usuario con Id {id}");
            }

            if (Opt == 1)
            {
                existingUser.Email = user.Email;
                existingUser.Password = _passwordService.HashPassword(user.Password);
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
            }
            else
            {
                existingUser.Email = user.Email;
                existingUser.Password = _passwordService.HashPassword(user.Password);
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Role = UserRole.Seller;
            }
            

            _repository.Update(existingUser);
        }

        public void UpdateUser(UserModel user, Guid id)
        {
            User existingUser = _repository.GetById(id);

            if (existingUser == null)
            {
                throw new ApplicationException($"No se encontró ningún usuario con Id {id}");
            }

            existingUser.Email = user.Email;
            existingUser.Password = _passwordService.HashPassword(user.Password);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;

            _repository.Update(existingUser);
        }

        public User GetById(Guid id)
        {
            User user = _repository.GetById(id);

            if (user == null)
            {
                throw new Exception("No se encontró ningún usuario con ese ID.");
            }

            return user;
        }

        public void ChangeActive(Guid id)
        {
            _repository.ChangeActive(id);
        }

    }
}