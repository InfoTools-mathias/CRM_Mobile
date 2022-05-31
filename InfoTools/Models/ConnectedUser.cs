using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class ConnectedUser : User
    {
        public List<Meeting> meetings { get; set; }

        public ConnectedUser(User user, List<Meeting> meetings) 
            : base(user.id, user.name, user.surname, user.mail, user.type)
        {
            this.meetings = meetings;
        }
    }
}
