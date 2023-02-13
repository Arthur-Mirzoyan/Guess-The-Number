using System;
using Xamarin.Forms;

namespace GuessTheNumber
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        string gameMode = "user";

        private async void Next_Page(object sender, EventArgs e)
        {
            if (gameMode == "user") await Navigation.PushAsync(new NavigationPage(new UserPage()));
            else if (gameMode == "cpu") await Navigation.PushAsync(new NavigationPage(new CpuPage()));
            else if (gameMode == "userCpu") await Navigation.PushAsync(new NavigationPage(new UserCpuPage()));
        }

        private void Choose_Game_Mode(object sender, EventArgs e)
        {
            gameMode = ((ImageButton)sender).BindingContext as string;
            if (gameMode == "user") info.Text = "You will guess My number";
            else if (gameMode == "cpu") info.Text = "I will guess Your number";
            else if (gameMode == "userCpu") info.Text = "We are playing together";
        }
    }
}
