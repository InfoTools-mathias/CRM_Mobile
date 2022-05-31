using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfoTools.Models;
using System;
using InfoTools.Client;

namespace InfoTools.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoMeetingPage : ContentPage
    {
        private MeetingFull Meeting { get; set; }
        private Cli Client { get; set; }

        public InfoMeetingPage(dynamic client, string meetingId)
        {
            InitializeComponent();
            this.Client = client;
            this.Main(meetingId);
        }

        public async void Main(string meetingId)
        {
            MeetingFull meeting = await this.Client.MeetingClient.GetMeetingWithId(meetingId);
            this.Meeting = meeting;

            DateTime dateTime = Convert.ToDateTime(meeting.date);

            entry_adress.Text = meeting.adress;
            entry_zip.Text = meeting.zip;
            entry_date.Date = dateTime;
            entry_time.Time = dateTime.TimeOfDay;
        }

        private async void delete_submit_Clicked(object sender, EventArgs e)
        {
            dynamic res = await this.Client.MeetingClient.DeleteMeeting(this.Meeting.id);
            if(res == null)
            {
                await Navigation.PushAsync(new MeetingsPage(this.Client));
            }
        }

        private async void edit_submit_Clicked(object sender, EventArgs e)
        {
            MeetingFull meetingData = this.Meeting;
            DateTime date = entry_date.Date.Add(entry_time.Time);

            meetingData.adress = entry_adress.Text;
            meetingData.zip = entry_zip.Text;
            meetingData.date = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            
            dynamic res = await this.Client.MeetingClient.PutMeeting(meetingData);
            if (res == null)
            {
                await Navigation.PushAsync(new MeetingsPage(this.Client));
            }
        }
    }
}