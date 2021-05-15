using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View1 : ContentView
    {
        public View1()
        {
            InitializeComponent();
        }
        internal bool Cab_master;
        internal int Cab_count_stud;
        internal int Cab_etaz;
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Cab_open cab_Open = new Cab_open();
            cab_Open.FindByName<Editor>("Cab_name").Text = Name_cab.Text;
            cab_Open.FindByName<Entry>("Cab_count_stud").Text = Cab_count_stud.ToString();
            cab_Open.FindByName<Picker>("Cab_etaz").SelectedItem = Cab_etaz.ToString();
            cab_Open.FindByName<Entry>("Cab_prepod").Text = Prep_cab.Text;
            cab_Open.FindByName<Picker>("Cab_master").SelectedItem = Cab_master == true ? "Да" : "Нет";
            cab_Open.FindByName<Image>("Cab_image").Source = Image_cab.Source;
            cab_Open.cab = Number_cab.Text;
            await Navigation.PushModalAsync(cab_Open);
        }
    }
}