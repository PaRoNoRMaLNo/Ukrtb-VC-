using MySqlConnector;
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
    public partial class Cab_open_oboryd : ContentPage
    {
        private string cab;
        public Cab_open_oboryd(string cab)
        {
            InitializeComponent();
            this.cab = cab;
            Load_oboryd();
        }
        private void Load_oboryd()
        {
            Oboryd_panel.Children.Clear();
            using (MySqlConnection connection = new MySqlConnection("server=mysql77.hostland.ru;userid=host1821757_manan;database=host1821757_manandb;password=Id564876681;"))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"SELECT Equipment_number,Equipment_name,Equipment_specific,VC_Cab.Cab_number FROM VC_Equipment 
            INNER join VC_Cab	on VC_Equipment.Equipment_cab_id = VC_Cab.Cab_id WHERE VC_Cab.Cab_number = '{cab}'", connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Oboryd oboryd = new Oboryd();
                        oboryd.FindByName<Label>("Equipment_number").Text = reader[0].ToString();
                        oboryd.FindByName<Label>("Equipment_name").Text = reader[1].ToString();
                        oboryd.FindByName<Label>("Equipment_specific").Text = reader[2].ToString();
                        oboryd.FindByName<Label>("Cab").Text = reader[3].ToString();
                        Oboryd_panel.Children.Add(oboryd);
                    }
                }

            }
        }
    }
}