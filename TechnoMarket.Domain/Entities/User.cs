﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TechnoMarket.Domain.Entities
{
    public class User
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        //public string LastName { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Password { get; set; }

        /*public bool Active { get; set; } = true;

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
        public string PostalCode { get; set; }
        //public virtual Role RoleUser { get; set; } = new Role(3, "Customer");

        //public virtual Store? Store { get; set; } = null;

        public List<int> ProductsPurchased { get; set; } = new List<int>();

        public HashSet<int> ProductsFavorites { get; set; } = new HashSet<int>();

        public HashSet<int> StoresFavotires { get; set; } = new HashSet<int>();

        public User(string firstName)
        {
            FirstName = firstName;
        }*/
    }
}
