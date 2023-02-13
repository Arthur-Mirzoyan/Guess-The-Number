using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessTheNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCpuPage : ContentPage
    {
        User user;
        Cpu cpu;
        public UserCpuPage()
        {
            InitializeComponent();
            user = new User(Num_List, Label1, Input);
            cpu = new Cpu();
            user.Start();
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            user.Check_Clicked();
            Answer();
        }

        private async void Answer()
        {
            int exists = -1; int onplace = -1;
            string result;

            while (true)
            {
                try
                {
                    result = await DisplayPromptAsync($"{cpu._variants.First()}", "How many exist?", keyboard: Keyboard.Numeric);
                    exists = int.Parse(result);
                    result = await DisplayPromptAsync($"{cpu._variants.First()}", "How many are on their places?", keyboard: Keyboard.Numeric);
                    onplace = int.Parse(result);
                    if (exists >= 0 && exists <= 4 && onplace >= 0 && onplace <= 4 && exists >= onplace) break;
                }
                catch (Exception) { }
            }

            if (onplace == 4)
            {
                bool answer = await DisplayAlert($"Your number was {cpu._variants.First()}.", "Would you like to play again", "Yes", "No");
                if (answer)
                {
                    cpu = new Cpu();
                    user.Start();
                    return;
                }
            }

            cpu.Answer(exists, onplace);
        }
    }
}