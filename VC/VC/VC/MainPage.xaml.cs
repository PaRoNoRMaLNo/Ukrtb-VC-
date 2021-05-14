using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;

namespace VC
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Load_cabs();
        }

        private async Task<string> Load_cab(string cab)
        {
            Cabinet_panel.Children.Clear();
            //Task<string> empty = null;
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT Cab_name,Cab_prepod,Cab_number,Cab_image FROM VC_Cab where Cab_number like '%{cab}%'", connection);
                MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        View1 view1 = new View1();
                        view1.FindByName<Label>("Name_cab").Text = await reader.GetFieldValueAsync<string>(0);
                        view1.FindByName<Label>("Number_cab").Text = await reader.GetFieldValueAsync<string>(2);
                        view1.FindByName<Label>("Prep_cab").Text = await reader.GetFieldValueAsync<string>(1);
                        view1.FindByName<Image>("Image_cab").Source = await reader.GetFieldValueAsync<string>(3);
                        Cabinet_panel.Children.Add(view1);
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        private void Load_cabs()
        {
            using (MySqlConnection connection = new MySqlConnection("server=mysql77.hostland.ru;userid=host1821757_manan;database=host1821757_manandb;password=Id564876681;"))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT Cab_number FROM VC_Cab", connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Search_cabinet.Items.Add(reader[0].ToString());
                    }
                }

            }
            Search_cabinet.SelectedIndex = 0;
        }


        async void Search_cabinet_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Load_activity.IsRunning = true;
            if (Search_cabinet.Items[Search_cabinet.SelectedIndex].ToString() == "Все")
            {
                await Load_cab("");
            }
            else
            {
                await Load_cab(Search_cabinet.Items[Search_cabinet.SelectedIndex].ToString());
            }

            Load_activity.IsRunning = false;
        }
    }
}
