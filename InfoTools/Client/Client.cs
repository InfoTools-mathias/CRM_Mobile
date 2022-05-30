using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using InfoTools.Models;
using System.Net.Http.Headers;

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
        readonly string host = "http://localhost:5000";
        
        public HttpClient client = new HttpClient();
        public User user = null;

        public Cli() { }

        private async Task<ParseRequestResult> ParseRequest(HttpResponseMessage res)
        {
            ParseRequestResult result = new ParseRequestResult(
                res.IsSuccessStatusCode,
                JsonConvert.DeserializeObject(
                    await res.Content.ReadAsStringAsync()
                    )
                );
            return result;
        }


        public async Task<ParseRequestResult> GetRequest(string url)
        {
            HttpResponseMessage response = await client.GetAsync(this.host + url);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> PostRequest(string url, object data)
        {
            HttpContent PostData = new StringContent(content: JsonConvert.SerializeObject(data), encoding: Encoding.UTF8, mediaType: "application/json");
            HttpResponseMessage response = await client.PostAsync(this.host + url, PostData);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> PutRequest(string url, object data)
        {
            HttpContent PostData = new StringContent(content: JsonConvert.SerializeObject(data), encoding: Encoding.UTF8, mediaType: "application/json");
            HttpResponseMessage response = await client.PutAsync(this.host + url, PostData);
            return await this.ParseRequest(response);
        }

        public async Task<ParseRequestResult> DeleteRequest(string url)
        {
            HttpResponseMessage response = await client.GetAsync(this.host + url);
            return await this.ParseRequest(response);
        }

        public async Task<dynamic> Authenticate(string mail, string password)
        {
            string BasicToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(mail + ":" + password));
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", BasicToken);

            ParseRequestResult Res = await this.PostRequest("/api/v1/oauth/password", new object());
            if(Res.Sucess == false) return null;
            
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToString(Res.Content.token));

            dynamic UserData = (await this.GetRequest("/api/v1/oauth/@me")).Content;
            if (UserData != null)
            {
                this.user = new User(UserData.id, UserData.name, UserData.surname, UserData.mail, UserData.type, UserData.password);
                return this.user;
            }
            return null;
        }
    }
}
