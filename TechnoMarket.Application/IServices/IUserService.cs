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
        public void PromoteToAdmin(Guid userId);
        public User GetByEmail(string email);
        public List<UserDTO> Get();
        public UserModel CreateUser(UserCreateDTO userDTO);
        public void UpdateUser(UserModel user, Guid id);
        public User? DeleteUser(Guid id);
        public User? CheckCredentials(CredentialsRequest credentials);
        public User GetById(Guid id);
        public void Update(UserUpdateDTO user, Guid id);
        public void ChangeActive(Guid id);
    }
}