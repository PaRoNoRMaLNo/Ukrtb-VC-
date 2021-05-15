using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
namespace VC
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Cabinet> Cabinets;

        

        public ObservableCollection<Cabinet> cabinet
        {
            get { return Cabinets; }
            set
            {
                Cabinets = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("places"));
            }
        }

        private void Load_cab_master()
        {
            using (MySqlConnection connection = new MySqlConnection("server=mysql77.hostland.ru;userid=host1821757_manan;database=host1821757_manandb;password=Id564876681;"))
            {
                
                 connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT Cab_name,Cab_prepod,Cab_number,Cab_image FROM VC_Cab where Cab_master = 1", connection);
                MySqlDataReader reader =  command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cabinets.Add(new Cabinet
                        {
                            Name = reader[0].ToString(),
                            Prep_cab = reader[1].ToString(),
                            Number = reader[2].ToString(),
                            Image = reader[3].ToString() == "" ? "https://ukrtb.ru/bitrix/templates/ukrtb/images/slider/slide-2.png" : reader[3].ToString(),
                        });
                    }
                }
               
            }
        }
        //private void DoSimpleCommand()
        //{
        //    Load_Data($"where Event_date = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}'");
        //}
        //private Command simpleCommand;
        //public Command SimpleCommand
        //{
        //    get { return simpleCommand; }
        //}
        //private void DoTomorowCommand()
        //{
        //    DateTime date_tomorow = DateTime.Now.AddDays(1);
        //    Load_Data($"where Event_date = '{date_tomorow.Year}-{date_tomorow.Month}-{date_tomorow.Day}'");
        //}
        //private Command tomorowCommand;
        //public Command TomorowCommand
        //{
        //    get { return tomorowCommand; }
        //}
        //private void DoWeekCommand()
        //{
        //    DateTime date_un3 = DateTime.Now.AddDays(-3);
        //    DateTime date_up6 = DateTime.Now.AddDays(6);
        //    Load_Data($"where Event_date between '{date_un3.Year}-{date_un3.Month}-{date_un3.Day}' and '{date_up6.Year}-{date_up6.Month}-{date_up6.Day}'");
        //}
        //private Command weekCommand;
        //public Command WeekCommand
        //{
        //    get { return weekCommand; }
        //}
        public void Load_Data()
        {

            Cabinets.Add(new Cabinet
            {
                Name = "Web",
                Etaz = "3",
                Prep_cab = "Safarov",
                Number = "308",
                Image = "https://ukrtb.ru/workshops/img/gallery/303/1.jpg"
            });
            Cabinets.Add(new Cabinet
            {
                Name = "Web",
                Etaz = "3",
                Prep_cab = "Safarov",
                Number = "308",
                Image = "https://ukrtb.ru/workshops/img/gallery/303/1.jpg"
            });
            Cabinets.Add(new Cabinet
            {
                Name = "Web",
                Etaz = "3",
                Prep_cab = "Safarov",
                Number = "308",
                Image = "https://ukrtb.ru/workshops/img/gallery/303/1.jpg"
            });
        }
        public  MainViewModel()
        {
            Cabinets = new ObservableCollection<Cabinet>();
            //simpleCommand = new Command(DoSimpleCommand);
            //tomorowCommand = new Command(DoTomorowCommand);
            //weekCommand = new Command(DoWeekCommand);
            Load_cab_master();
        }
    }
}
