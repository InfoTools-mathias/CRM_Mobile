using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public int type { get; set; }

        public User(string id, string name, string surname, string mail, int type)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.mail = mail;
            this.type = type;
        }

        public string GetUser()
        {
            return this.surname + " " + this.name;
        }

        public string GetUserType()
        {
            switch (this.type)
            {
                case 0: 
                    return "Administrateur";
                case 1: 
                    return "Commercial";
                case 2: 
                    return "Client";
                default: 
                    return "Prospect";
            }
        }
    }
}
