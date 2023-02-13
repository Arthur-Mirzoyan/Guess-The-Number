using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessTheNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CpuPage : ContentPage
    {
        Cpu cpu;

        public CpuPage()
        {
            InitializeComponent();
            cpu = new Cpu(Exists, OnPlace, guess, Label1, Num_List);
            cpu.Start();
        }
        private void Answer(object sender, EventArgs e)
        {
            cpu.Answer();
        }
    }
}