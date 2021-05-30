using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;

namespace VC.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        public bool Check_connect()
        {
            
            WebClient webClient = new WebClient();
            try
            {
                string a = webClient.DownloadString("https://ukrtb.ru/");
            }
            catch
            {
                return false;
            }

            return true;

        }
        protected async override void OnResume()
        {
            base.OnResume();
            if (Check_connect())
            {
                SetContentView(Resource.Layout.splash);
                Task work = new Task(() => { SimulatedStartUpAsync(); });
                work.Start();
            }
            else
            {
                SetContentView(Resource.Layout.check_internet);
                while (await Task.Run(() => !Check_connect())) { }
                Task work = new Task(() => { SimulatedStartUpAsync(); });
                work.Start();
            }

            
        }

        private async void SimulatedStartUpAsync()
        {
            await Task.Delay(4000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
