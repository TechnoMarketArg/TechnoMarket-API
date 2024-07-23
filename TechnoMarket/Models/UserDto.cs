using TechnoMarket.Domain.DTOs;

namespace TechnoMarket.Models
{
    public class UserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public StoreDTO Store { get; set; }
    }
}