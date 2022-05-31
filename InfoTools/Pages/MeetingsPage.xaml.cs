using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfoTools.Client;
using InfoTools.Models;

namespace InfoTools.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingsPage : ContentPage
    {
        private Cli Client { get; set; }
        public MeetingsPage(dynamic client)
        {
            InitializeComponent();
            this.Client = client;
            this.Main();
        }

        public async void Main()
        {
            List<ViewCell> list = new List<ViewCell>();
            List<MeetingFull> meetings = await this.Client.MeetingClient.GetUserMeetings();
            foreach (Meeting item in meetings)
            {
                var layout = new StackLayout() { VerticalOptions = LayoutOptions.Center, };

                var tgr = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
                tgr.Tapped += (s, e) => this.OnLayoutClick(this.Client, item.id);
                layout.GestureRecognizers.Add(tgr);

                //var command = new Command();
                //layout.GestureRecognizers.Add(new TapGestureRecognizer() { NumberOfTapsRequired = 1, Command = command });

                layout.Children.Add(new Label()
                {
                    AutomationId = item.id,
                    Text = item.adress + ", " + item.zip,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Start
                });

                layout.Children.Add(new Label()
                {
                    Text = Convert.ToDateTime(item.date).ToLocalTime().ToString(),
                    TextColor = Color.FromHex("#f35e20"),
                    VerticalOptions = LayoutOptions.End,
                });


                list.Add(new ViewCell { View = layout });
            }

            Content = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection("Liste de vos rendez-vous") { list }
                }
            };
        }

        public async void OnLayoutClick(dynamic client, string itemId)
        {
            await Navigation.PushAsync(new InfoMeetingPage(client, itemId));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeetingCreatePage(this.Client));
        }
    }
}