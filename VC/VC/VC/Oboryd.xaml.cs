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
    public partial class Oboryd : ContentView
    {
        public Oboryd()
        {
            InitializeComponent();
        }
        internal string equipment_id;
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Oboryd_open oboryd_Open = new Oboryd_open(Cab.Text);
            oboryd_Open.FindByName<Entry>("Oboryd_number").Text = Equipment_number.Text;
            oboryd_Open.FindByName<Picker>("Oboryd_cab").SelectedItem = Cab.Text;
            oboryd_Open.FindByName<Editor>("Oboryd_specific").Text = Equipment_specific.Text;
            oboryd_Open.FindByName<Editor>("Oboryd_name").Text = Equipment_name.Text;
            oboryd_Open.equipment_id = equipment_id;
            await Navigation.PushModalAsync(oboryd_Open);
        }
    }
}