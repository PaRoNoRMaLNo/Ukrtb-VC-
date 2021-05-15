using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Essentials;
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
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT Cab_name,Cab_prepod,Cab_number,Cab_image,Cab_master,Cab_etaz,Cab_count_stud FROM VC_Cab where Cab_number like '%{cab}%'", connection);
                MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        View1 view1 = new View1();
                        view1.FindByName<Label>("Name_cab").Text = await reader.GetFieldValueAsync<string>(0);
                        view1.FindByName<Label>("Number_cab").Text = await reader.GetFieldValueAsync<string>(2);
                        view1.FindByName<Label>("Prep_cab").Text = await reader.GetFieldValueAsync<string>(1);
                        try
                        {
                            view1.FindByName<Image>("Image_cab").Source = await reader.GetFieldValueAsync<string>(3);
                        }
                        catch 
                        {
                        }
                        
                        view1.Cab_master = await reader.GetFieldValueAsync<bool>(4);
                        view1.Cab_etaz = await reader.GetFieldValueAsync<int>(5);
                        view1.Cab_count_stud = await reader.GetFieldValueAsync<int>(6);
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

        private async Task<string> Load_oboryd(string oboryd_search)
        {
            Oboryd_panel.Children.Clear();
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"SELECT `Equipment_number`,`Equipment_name`,`Equipment_specific`, VC_Cab.Cab_number,`Equipment_id` FROM `VC_Equipment` 
                            INNER join VC_Cab on Equipment_cab_id = VC_Cab.Cab_id 
                                where 	`Equipment_number` like '%{oboryd_search}%' 
                                or `Equipment_name` like '%{oboryd_search}%' 
                                or `Equipment_specific` like '%{oboryd_search}%'", connection);
                MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Oboryd oboryd = new Oboryd();
                        oboryd.FindByName<Label>("Equipment_number").Text = await reader.GetFieldValueAsync<string>(0);
                        oboryd.FindByName<Label>("Equipment_name").Text = await reader.GetFieldValueAsync<string>(1);
                        oboryd.FindByName<Label>("Equipment_specific").Text = await reader.GetFieldValueAsync<string>(2);
                        oboryd.FindByName<Label>("Cab").Text = await reader.GetFieldValueAsync<string>(3);
                        oboryd.equipment_id = Convert.ToString(await reader.GetFieldValueAsync<int>(4));
                        Oboryd_panel.Children.Add(oboryd);
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        private async void Search_obor_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_activ_oboryd.IsRunning = true;
                await Load_oboryd(Search_obor.Text);
            Load_activ_oboryd.IsRunning = false;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            Search_cabinet_SelectedIndexChanged(sender, e);
        }

        private async void ContentPage_Appearing_1(object sender, EventArgs e)
        {
            Load_activ_oboryd.IsRunning = true;
                await Load_oboryd(Search_obor.Text);
            Load_activ_oboryd.IsRunning = false;
        }

        private void All_cab_Clicked(object sender, EventArgs e)
        {
            this.CurrentPage = Page_cab;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://study.ukrtb.ru/rasp"));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Oboryd_open(""));
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Cab_open cab_Open = new Cab_open();
            cab_Open.FindByName<StackLayout>("Number_panel").IsVisible = true;
            await Navigation.PushModalAsync(cab_Open);
        }

        private async Task<string> Load_message(DateTime date)
        {
            Message_panel.Children.Clear();
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"SELECT * FROM `Message_VC` where `Message_date` between '{date.Year}-{date.Month}-{date.Day} 00:00:00' and '{date.Year}-{date.Month}-{date.Day} 23:59:59'", connection);
                MySqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Request request = new Request();
                        request.FindByName<Label>("Message_id").Text = Convert.ToString(await reader.GetFieldValueAsync<int>(0));
                        request.FindByName<Label>("Message_prepod").Text = await reader.GetFieldValueAsync<string>(1);
                        request.FindByName<Label>("Message_text").Text = await reader.GetFieldValueAsync<string>(2);
                        request.FindByName<Label>("Message_date").Text = Convert.ToDateTime(await reader.GetFieldValueAsync<DateTime>(3)).ToString();
                        request.FindByName<Switch>("Message_status").IsToggled = await reader.GetFieldValueAsync<string>(4) == "Выполнен" ? true : false;
                        request.FindByName<Label>("Message_cab").Text = await reader.GetFieldValueAsync<string>(5);
                        Message_panel.Children.Add(request);
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }


        private async void Date_Messages_DateSelected(object sender, DateChangedEventArgs e)
        {
            Activ_indicator_messages.IsRunning = true;
                await Load_message(Date_Messages.Date);
            Activ_indicator_messages.IsRunning = false;
        }

        private async void Messages_page_Appearing(object sender, EventArgs e)
        {
            Activ_indicator_messages.IsRunning = true;
                await Load_message(Date_Messages.Date);
            Activ_indicator_messages.IsRunning = false;
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://ukrtb.ru/"));
        }
    }
}
