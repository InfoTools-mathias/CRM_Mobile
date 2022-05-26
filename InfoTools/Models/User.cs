using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public string Password { get; set; }

        public User(string id, string name, string surname, string email, int type, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Type = type;
            Password = password;
        }
    }
}
