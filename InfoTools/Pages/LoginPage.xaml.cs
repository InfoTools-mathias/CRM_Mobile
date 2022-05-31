using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfoTools.Client;
using InfoTools.Models;

namespace InfoTools.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            string mail = Convert.ToString(login_mail.Text);
            string pass = Convert.ToString(login_password.Text);

            if (mail.Length == 0)
            {
                login_mail.Placeholder = "Merci de donner votre adresse mail";
                login_mail.PlaceholderColor = Color.Red;
            }
            else if (pass.Length == 0)
            {
                login_password.Placeholder = "Merci de donner votre adresse mot de passe";
                login_password.PlaceholderColor = Color.Red;
            }
            else
            {
                Cli client = new Cli();
                dynamic res = await client.Authenticate(mail, pass);
                
                if(res != null)
                {
                    Console.WriteLine("Connecté");
                    await Navigation.PushAsync(new MeetingsPage(client));
                }
                else
                {
                    login_mail.Text = "";
                    login_password.Text = "";
                    Console.WriteLine("Mauvais credential");
                }
            }
        }
    }
}