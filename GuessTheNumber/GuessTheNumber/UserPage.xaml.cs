using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessTheNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        User user;
        public UserPage()
        {
            InitializeComponent();
            user = new User(Num_List, Label1, Input);
            user.Start();
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            user.Check_Clicked();
        }
    }
}