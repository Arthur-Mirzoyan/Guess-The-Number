using System;
using System.Linq;

using Xamarin.Forms;

namespace GuessTheNumber
{
    class User
    {
        Random rnd = new Random();
        int guess = 0;
        int number = 0;
        int steps = 0;
        int exist = 0;
        int place = 0;
        Label Num_List;
        Label Label1;
        Entry Input;

        public User(Label Num_List, Label Label1, Entry Input)
        {
            this.Num_List = Num_List;
            this.Label1 = Label1;
            this.Input = Input;
        }

        public void Start()
        {
            exist = 0; place = 0; steps = 0;
            Num_List.Text = string.Empty;
            Label1.Text = "Steps left : 7";

            do
            {
                number = rnd.Next(1023, 9876);
            } while (IsValid(number));

            //Label1.Text = number.ToString(); // Number show
        }

        public async void Replay(string result)
        {
            bool answer = await App.Current.MainPage.DisplayAlert($"You {result}.", "Would you like to play again", "Yes", "No");
            if (answer) Start();
            else
            {
                Page page = App.Current.MainPage.Navigation.NavigationStack.Last();
                App.Current.MainPage.Navigation.RemovePage(page);
            }
        }
        public bool IsValid(in int num)
        {
            int[] arr = new int[4] { num % 10, num / 10 % 10, num / 100 % 10, num / 1000 };
            return arr[3] == 0 || arr.Distinct().Count() != 4;
        }

        public void Checking(in int guess)
        {
            int[] arr = new int[4] { guess % 10, guess / 10 % 10, guess / 100 % 10, guess / 1000 };
            int[] arrn = new int[4] { number % 10, number / 10 % 10, number / 100 % 10, number / 1000 };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i] == arrn[j])
                    {
                        if (i == j) place++;
                        exist++;
                        break;
                    }
                }
            }
        }

        public void Check_Clicked()
        {
            if (Input.Text == string.Empty || Input.Text.Contains(",") || Input.Text.Contains("."))
            {
                Label1.Text = "Invalid Input";
                Input.Text = string.Empty;
            }
            else
            {
                guess = Convert.ToInt32(Input.Text);

                if (IsValid(guess)) Label1.Text = "Invalid Number";
                else
                {
                    Checking(guess);
                    Num_List.Text += $"\n {guess} - {exist} exists - {place} in place";
                    steps++;
                    Label1.Text = $"Steps left : {7 - steps}";
                }

                Input.Text = string.Empty;
                place = 0; exist = 0;

                if (steps <= 7 && guess == number) Replay("Won");
                else if (steps >= 7 && guess != number) Replay($"Lost.\nThe number was {number}");
            }
        }
    }
}