using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VC
{
    public partial class Oboryd_open : ContentPage
    {
        private string Cab;
        public Oboryd_open(String cab)
        {
            InitializeComponent();
            this.Cab = cab;
            Load_cab(cab);
        }
        internal string equipment_id;
        private void Load_cab(string cab)
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
                        Oboryd_cab.Items.Add(reader[0].ToString());
                    }
                }

            }
            Oboryd_cab.SelectedItem = cab;
        }

        private async Task<string> Save()
        {
            //Task<string> empty = null;
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"UPDATE `VC_Equipment` SET 
                                        `Equipment_cab_id`= (SELECT `Cab_id` FROM `VC_Cab` WHERE `Cab_number` = '{Oboryd_cab.SelectedItem}'),
                                        `Equipment_number`='{Oboryd_number.Text}',
                                        `Equipment_name`='{Oboryd_name.Text}',
                                        `Equipment_specific`='{Oboryd_specific.Text}' 
                                        WHERE `Equipment_id` = '{equipment_id}'", connection);
                if (await command.ExecuteNonQueryAsync() == 1)
                {
                    await DisplayAlert("Сообщение", "Сохранено", "Ок");
                }
                else
                {
                    await DisplayAlert("Сообщение", "Произошла ошибка, проверьте данные", "Ок");
                }
            }
            return "";
        }
        private async Task<string> Delete()
        {
            //Task<string> empty = null;
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"DELETE FROM `VC_Equipment` 
                                    WHERE `Equipment_id` ='{equipment_id}'", connection);
                if (await command.ExecuteNonQueryAsync() == 1)
                {
                    await DisplayAlert("Сообщение", "Удалено", "Ок");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Сообщение", "Произошла ошибка", "Ок");
                }
            }
            return "";
        }

        private async Task<string> Insert()
        {
            //Task<string> empty = null;
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"INSERT INTO `VC_Equipment`
                        (`Equipment_cab_id`, 
                        `Equipment_number`, 
                        `Equipment_name`, 
                        `Equipment_specific`) 
                                                VALUES 
                        ((SELECT `Cab_id` FROM `VC_Cab` WHERE `Cab_number` = '{Oboryd_cab.SelectedItem}'),
                         '{Oboryd_number.Text}',
                         '{Oboryd_name.Text}',
                         '{Oboryd_specific.Text}')", connection);
                if (await command.ExecuteNonQueryAsync() == 1)
                {
                    await DisplayAlert("Сообщение", "Оборудование добавлено", "Ок");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Сообщение", "Произошла ошибка, проверьте данные", "Ок");
                }
            }
            return "";
        }
        private async void Delete_oboryd_Clicked(object sender, EventArgs e)
        {
            if(equipment_id != null)
                await Delete();
        }

        private async void Save_oboryd_Clicked(object sender, EventArgs e)
        {
            if (Cab != "")
                if (Oboryd_name.Text != "" && Oboryd_number.Text != "")
                    await Save();
                else
                    await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
            else
                if(Oboryd_name.Text !="" && Oboryd_number.Text != null && Oboryd_cab.SelectedItem != "")
                    await Insert();
                else
                await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
        }
    }
}
