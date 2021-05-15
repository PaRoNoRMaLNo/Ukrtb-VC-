using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VC
{
    public partial class Cab_open : ContentPage
    {
        public Cab_open()
        {
            InitializeComponent();
        }
        internal string cab;

        private async void Open_cab_oboryd_Clicked(object sender, EventArgs e)
        {
            if (cab != null)
            {
                Cab_open_oboryd cab_Open_Oboryd = new Cab_open_oboryd(cab);
                await Navigation.PushModalAsync(cab_Open_Oboryd);
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async Task<string> Save()
        {
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"UPDATE `VC_Cab` SET 
                                    `Cab_name`= '{Cab_name.Text}',
                                    `Cab_prepod`='{Cab_prepod.Text}',
                                    `Cab_master`= '{Cab_master.SelectedIndex}',
                                    `Cab_count_stud`='{Cab_count_stud.Text}',
                                    `Cab_etaz`= '{Cab_etaz.SelectedItem}'
                                    WHERE Cab_number = '{cab}'", connection);
                if (await command.ExecuteNonQueryAsync() == 1)
                {
                    await DisplayAlert("Сообщение","Сохранено","Ок");
                }
                else
                {
                    await DisplayAlert("Сообщение", "Произошла ошибка, проверьте данные", "Ок");
                }
            }
            return "";
        }

        private async void Save_cab_Clicked(object sender, EventArgs e)
        {
            if(cab != null)
                if(Cab_name.Text!="" && Cab_prepod.Text != "")
                {
                    Activ_indicator_cab.IsRunning = true;
                    await Save();
                    Activ_indicator_cab.IsRunning = false;
                }
                else
                {
                    await DisplayAlert("Ошибка", "Проверьте данные", "Ок");
                }
            else
            {
                if(Cab_name.Text!=null && Cab_count_stud.Text != null && Cab_etaz.SelectedItem !="" && Cab_prepod.Text !=null && Cab_master.SelectedItem !="")
                    await Insert();
            }
        }
        private async Task<string> Insert()
        {
            using (MySqlConnection connection = new MySqlConnection(Connect.String))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand($@"INSERT INTO `VC_Cab`(
                                        `Cab_number`, 
                                        `Cab_name`, 
                                        `Cab_prepod`,
                                        `Cab_master`, 
                                        `Cab_count_stud`, 
                                        `Cab_etaz`) 
                                        VALUES 
                                        ('{Cab_number.Text}',
                                         '{Cab_name.Text}',
                                         '{Cab_prepod.Text}',
                                         '{Cab_master.SelectedIndex}',
                                         '{Cab_count_stud.Text}',
                                         '{Cab_etaz.SelectedItem}')", connection);
                if (await command.ExecuteNonQueryAsync() == 1)
                {
                    await DisplayAlert("Сообщение", "Кабинет добавлен", "Ок");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Сообщение", "Произошла ошибка, проверьте данные", "Ок");
                }
            }
            return "";
        }
    }
}
