using System;
using System.Collections.Generic;
using System.Text;

namespace InfoTools.Models
{
    class MeetingFull : Meeting
    {
        public List<User> users { get; set; }

        public MeetingFull(Meeting meeting, List<User> users)
            : base(meeting.id, meeting.date, meeting.zip, meeting.adress)
        {
            this.users = users;
        }
    }
}
