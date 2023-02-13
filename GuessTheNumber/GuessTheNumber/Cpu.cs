using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace GuessTheNumber
{
    class Cpu
    {
        private static int _step = 1;
        public List<int> _variants = Enumerable.Range(1023, 8854).Where(HasDistinctDigits).ToList();
        Label Label1;
        Label guess;
        Label Num_List;
        Entry Exists;
        Entry OnPlace;
        public Cpu(Entry Exists = null, Entry OnPlace = null, Label guess = null, Label Label1 = null, Label Num_List = null)
        {
            this.Label1 = Label1;
            this.guess = guess;
            this.Num_List = Num_List;
            this.Exists = Exists;
            this.OnPlace = OnPlace;
        }
        public void Start()
        {
            Label1.Text = "Steps left : 7";
            guess.Text = $"{_variants.First()}";
            Num_List.Text = $"Possible variants left : {_variants.Count()}";
            Exists.Text = "";
            OnPlace.Text = "";
        }

        async public void Replay()
        {
            bool answer = await App.Current.MainPage.DisplayAlert($"Your number was {_variants.First()}.", "Would you like to play again", "Yes", "No");
            if (answer)
            {
                _step = 1;
                _variants = Enumerable.Range(1023, 8854).Where(HasDistinctDigits).ToList();
                Start();
            }
            else
            {
                Page page = App.Current.MainPage.Navigation.NavigationStack.Last();
                App.Current.MainPage.Navigation.RemovePage(page);
            }
        }

        private static int[] GetDigits(int number)
        {
            int a = number / 1000;
            int b = (number / 100) % 10;
            int c = (number / 10) % 10;
            int d = number % 10;

            return new[] { a, b, c, d };
        }

        private static bool HasDistinctDigits(int number)
        {
            return GetDigits(number).Distinct().Count() == 4;
        }

        private static bool MatchesFilter(int number, int filter, int matches, int exactMatches)
        {
            int[] numberDigits = GetDigits(number);
            int[] filterDigits = GetDigits(filter);

            int numberSimilarities = 0;
            int numberExactSimilarities = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (numberDigits[i] == filterDigits[j])
                    {
                        numberSimilarities++;
                        if (i == j)
                        {
                            numberExactSimilarities++;
                        }
                    }
                }
            }

            return numberSimilarities == matches && numberExactSimilarities == exactMatches;
        }

        public void Answer(int? exists = null, int? onplace = null)
        {
            int target = _variants.First();

            try
            {
                int matches; int exactMatches;
                if (Exists == null)
                { matches = (int)exists; exactMatches = (int)onplace; }
                else
                { matches = int.Parse(Exists.Text); exactMatches = int.Parse(OnPlace.Text); }


                if (exactMatches > matches || matches > 4)
                {
                    Label1.Text = "Invalid input";
                }

                if (exactMatches == 4)
                {
                    Replay();
                    return;
                }

                _variants = _variants.Where(number => MatchesFilter(number, target, matches, exactMatches)).ToList();
                _step++;

                Label1.Text = $"Steps left : {8 - _step}";
                guess.Text = $"{_variants.First()}";
                Exists.Text = "";
                OnPlace.Text = "";
                Num_List.Text = $"Possible variants left : {_variants.Count()}";
            }
            catch (Exception) { }
        }
    }
}
