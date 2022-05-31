using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class Meeting
    {
        public string id { get; set; }
        public string date { get; set; }
        public string zip { get; set; }
        public string adress { get; set; }

        public Meeting(string id, string date, string zip, string adress)
        {
            this.id = id;
            this.date = date;
            this.zip = zip;
            this.adress = adress;
        }
    }
}
