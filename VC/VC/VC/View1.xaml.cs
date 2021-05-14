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

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Cab_open cab_Open = new Cab_open();
            cab_Open.FindByName<Image>("Cab_image").Source = Image_cab.Source;
            await Navigation.PushModalAsync(cab_Open);
        }
    }
}