using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InfoTools.Client;

namespace InfoTools.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void Submit_Clicked(object sender, System.EventArgs e)
        {
            string mail = login_mail.Text;
            string pass = login_password.Text;


            //if(mail.Length == 0)
            //{
            //    login_mail.Placeholder = "Merci de donner votre adresse mail";
            //    login_mail.PlaceholderColor = Color.Red;
            //    return;
            //}
            //if (pass.Length == 0)
            //{
            //    login_password.Placeholder = "Merci de donner votre adresse mot de passe";
            //    login_password.PlaceholderColor = Color.Red;
            //    return;
            //}
            Console.WriteLine(mail + " " + pass);

            Cli client = new Cli();
            dynamic res = await client.Authenticate(mail, pass);
            Console.WriteLine(res);
        }
    }
}