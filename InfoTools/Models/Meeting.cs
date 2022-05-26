using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class Meeting
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Zip { get; set; }
        public string Adress { get; set; }
        public List<User> Users { get; set; }

        public Meeting(string id, DateTime date, string zip, string adress, List<User> users)
        {
            Id = id;
            Date = date;
            Zip = zip;
            Adress = adress;
            Users = users;
        }
    }
}
