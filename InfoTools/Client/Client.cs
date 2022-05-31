using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using InfoTools.Models;
using System.Collections.Generic;

namespace InfoTools.Client
{
    class ParseRequestResult
    {
        public bool Sucess { get; set; }
        public dynamic Content { get; set; }
        public ParseRequestResult(bool sucess, dynamic content)
        {
            this.Sucess = sucess;
            this.Content = content;
        }
    }

    class Cli
    {
        private string Host { get; set; }
        
        public HttpClient Client { get; set; }
        public ConnectedUser User { get; set; }
        public MeetingClient MeetingClient { get; set; }

        public Cli()
        {
            this.Host = "http://10.0.2.2:5000/api";
            this.Client = new HttpClient();
            this.MeetingClient = new MeetingClient(this);
        }

        private async Task<ParseRequestResult> ParseRequest(HttpResponseMessage res)
        {
            ParseRequestResult result = new ParseRequestResult(
                res.IsSuccessStatusCode,
                JsonConvert.DeserializeObject(await res.Content.ReadAsStringAsync()));
            //Console.WriteLine(result.Sucess);
            return result;
        }


        public async Task<ParseRequestResult> GetRequest(string url)
        {
            HttpResponseMessage response = await this.Client.GetAsync(this.Host + url);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> PostRequest(string url, dynamic data)
        {
            HttpContent PostData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this.Client.PostAsync(this.Host + url, PostData);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> PutRequest(string url, object data)
        {
            HttpContent PostData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            Console.WriteLine(PostData);
            HttpResponseMessage response = await this.Client.PutAsync(this.Host + url, PostData);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> DeleteRequest(string url)
        {
            HttpResponseMessage response = await this.Client.DeleteAsync(this.Host + url);
            return await this.ParseRequest(response);
        }

        public async Task<dynamic> Authenticate(string mail, string password)
        {
            string BasicToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(mail + ":" + password));
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", BasicToken);
            ParseRequestResult Res = await this.PostRequest("/v1/oauth/password", new object());
            if(Res.Sucess == false)
            {
                return null;
            }
            
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToString(Res.Content.token));

            dynamic UserData = (await this.GetRequest("/v1/oauth/@me")).Content;
            if (UserData != null)
            {
                User MyUser = new User(
                    Convert.ToString(UserData.id),
                    Convert.ToString(UserData.name),
                    Convert.ToString(UserData.surname),
                    Convert.ToString(UserData.mail),
                    Convert.ToInt16(UserData.type)
                    );
                List<Meeting> Meetings = new List<Meeting>();
                foreach (dynamic meeting in UserData.meetings)
                {
                    Meetings.Add(
                        new Meeting(
                            Convert.ToString(meeting.id),
                            Convert.ToString(meeting.date),
                            Convert.ToString(meeting.zip),
                            Convert.ToString(meeting.adress)
                            )
                        );
                }
                ConnectedUser CoUser = new ConnectedUser(MyUser, Meetings);
                this.User = CoUser;
                return this.User;
            }
            return null;
        }
    }
}
