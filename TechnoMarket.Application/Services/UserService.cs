using System;
using System.Collections.Generic;
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

        public UserModel GetByEmail(string email)
        {
            var user = _repository.GetByEmail(email);
            UserModel model = new UserModel()
            {
                Email = email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Id = user.Id
            };
            return model;
        }

        public List<User> Get()
        {
            return _repository.Get();
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

        public UserModel? CheckCredentials(CredentialsRequest credentials)
        {
            var user = GetByEmail(credentials.Email);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = _passwordService.VerifyPassword(user.Password, credentials.Password);
            if (isPasswordValid)
            {
                user.Password = string.Empty;
                return user;
            }
            return null;
        }

        public void Update(UserUpdateDTO user, Guid id)
        {
            var existingUser = _repository.GetById(id);

            if (existingUser == null)
            {
                throw new ApplicationException($"No se encontró ningún usuario con Id {id}");
            }

            existingUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = user.Password;
            }
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            _repository.Update(existingUser);
        }

        public User GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public bool VerifyPassword(Guid userId, string password)
        {
            var user = _repository.GetById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _passwordService.VerifyPassword(password, user.Password);
        }

        public void ChangeActive(Guid id)
        {
            _repository.ChangeActive(id);
        }

    }
}