
using System;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }


        private void Signup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Signup());
            //DisplayAlert("Signup_Clicked", "Signup_Clicked", "OK");
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Login_Clicked", "Login_Clicked", "OK");
        }
    }
}
