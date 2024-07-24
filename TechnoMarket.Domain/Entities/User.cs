using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TechnoMarket.Domain.Entities
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        [Unique]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; } = true;

        public UserRole Role { get; set; }

        public Guid? StoreId { get; set; } = null;

        public Store? Store { get; set; } = null;

        public List<int> ProductsPurchased { get; set; } = new List<int>();

        public List<int> ProductsFavorites { get; set; } = new List<int>();

        public List<int> StoresFavorites { get; set; } = new List<int>();

    }

    public enum UserRole
    {
        Customer,
        Seller,
        Admin,
        SuperAdmin,
    }

}