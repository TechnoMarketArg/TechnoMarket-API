using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Application.IServices
{
    public interface IUserService
    {
        public UserModel GetByEmail(string email);
        public List<User> Get();
        public UserModel CreateUser(UserCreateDTO userDTO);
        public User? DeleteUser(Guid id);
        public UserModel? CheckCredentials(CredentialsRequest credentials);
        public User GetById(Guid id);
        public void Update(UserUpdateDTO user, Guid id);
        public bool VerifyPassword(Guid userId, string password);
        public void ChangeActive(Guid id);
    }
}
