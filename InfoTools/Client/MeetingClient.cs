using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTools.Models;
using Newtonsoft.Json;

namespace InfoTools.Client
{
    class MeetingClient
    {
        private Cli Client { get; set; }
        public List<MeetingFull> Meetings { get; set; }

        public MeetingClient(Cli client)
        {
            this.Client = client;
            this.Meetings = new List<MeetingFull>();
        }

        public async Task<List<MeetingFull>> GetMeetings()
        {
            ParseRequestResult res = await this.Client.GetRequest("/v1/meetings");
            if(res.Sucess)
            {
                foreach(dynamic meetingData in res.Content)
                {
                    List<User> users = new List<User>();
                    foreach (dynamic userData in meetingData.users)
                    {
                        users.Add(
                            new User(
                                Convert.ToString(userData.id),
                                Convert.ToString(userData.name),
                                Convert.ToString(userData.surname),
                                Convert.ToString(userData.mail),
                                Convert.ToInt16(userData.type)
                                )
                            );
                    }
                    Meeting meeting = new Meeting(
                        Convert.ToString(meetingData.id),
                        Convert.ToDateTime(meetingData.date),
                        Convert.ToString(meetingData.zip),
                        Convert.ToString(meetingData.adress)
                        );
                    this.Meetings.Add(new MeetingFull(meeting, users));
                }
            }
            return this.Meetings;
        }


        public async Task<List<MeetingFull>> GetUserMeetings()
        {
            List<MeetingFull> UserMeetings = new List<MeetingFull>();
            ParseRequestResult res = await this.Client.GetRequest("/v1/oauth/@me");
            if (res.Sucess)
            {
                foreach (dynamic meetingData in res.Content.meetings)
                {
                    UserMeetings.Add(
                        await this.GetMeetingWithId(Convert.ToString(meetingData.id))
                        );
                }
            }
            return UserMeetings;
        }


        public async Task<dynamic> GetMeetingWithId(string meetingId)
        {
            ParseRequestResult res = await this.Client.GetRequest("/v1/meetings/" + meetingId);
            if (res.Sucess)
            {
                dynamic meetingData = res.Content;
                Meeting meeting = new Meeting(
                    Convert.ToString(meetingData.id),
                    Convert.ToString(meetingData.date),
                    Convert.ToString(meetingData.zip),
                    Convert.ToString(meetingData.adress)
                    );

                List<User> users = new List<User>();
                foreach (dynamic userData in meetingData.users)
                {
                    users.Add(
                        new User(
                            Convert.ToString(userData.id),
                            Convert.ToString(userData.name),
                            Convert.ToString(userData.surname),
                            Convert.ToString(userData.mail),
                            Convert.ToInt16(userData.type)
                            )
                        );
                }

                return new MeetingFull(meeting, users);
            }
            return null;
        }

        public async Task<MeetingFull> PostMeeting(MeetingFull meeting)
        {
            ParseRequestResult res = await this.Client.PostRequest("/v1/meetings", meeting);
            meeting.id = res.Content.id;
            return meeting;
        }

        public async Task<MeetingFull> PutMeeting(MeetingFull meeting)
        {
            await this.Client.PutRequest("/v1/meetings/" + meeting.id, meeting);
            return meeting;
        }

        public async Task<dynamic> DeleteMeeting(string meetingId)
        {
            ParseRequestResult res = await this.Client.DeleteRequest("/v1/meetings/" + meetingId);
            if (res.Sucess)
            {
                return null;
            }
            return meetingId;
        }
    }
}
