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

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Oboryd_open());
        }
    }
}