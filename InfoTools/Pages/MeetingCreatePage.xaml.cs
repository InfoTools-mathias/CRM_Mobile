using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfoTools.Models;
using InfoTools.Client;

namespace InfoTools.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingCreatePage : ContentPage
    {
        private Cli Client { get; set; }
        public MeetingCreatePage(dynamic client)
        {
            InitializeComponent();
            this.Client = client;
        }

        private async void create_submit_Clicked(object sender, EventArgs e)
        {
            DateTime date = entry_date.Date.Add(entry_time.Time);

            Meeting meeting = new Meeting("", date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), Convert.ToString(entry_zip.Text), Convert.ToString(entry_adress.Text));
            List<User> users = new List<User>();
            users.Add(this.Client.User);

            MeetingFull FinalMeeting = new MeetingFull(meeting, users);

            await this.Client.MeetingClient.PostMeeting(FinalMeeting);
        }
    }
}