using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace WPFExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum HouseType
        {
            CityHouse = 0,
            Rural = 1,
            LargeRural = 2
        }
        EventsIdx events = new EventsIdx();
        Customer emptyCust = new Customer();
        MyProp emptyProp = new MyProp();
        ObservableCollection<Schedule> scheduleDisplay = null;
        string name = string.Empty;
        double card = 0;
        int houseSize = 0;
        int tankSize = 0;
        bool haveWell = false;
        string slot = string.Empty;
        Dictionary<string, int> slotList = new Dictionary<string, int>();
        string binFileName = "schedule.xml";
        int mainTypeID = -1;

        public ObservableCollection<Schedule> ScheduleDisplay { get => scheduleDisplay; set => scheduleDisplay = value; }
        public Customer EmptyCust { get => emptyCust; set => emptyCust = value; }
        public MyProp EmptyProp { get => emptyProp; set => emptyProp = value; }

        public MainWindow()
        {
            InitializeComponent();
            
            ComboStart();
            ScheduleDisplay = new ObservableCollection<Schedule>();
            ReadFromXML();
           
            DataContext = this;
           
            

        }

        public void ComboStart()
        {
            int workingHrs = 8; //services take 1hr. how many slots you'll have daily.
            int startHr = 9; //first service slot hour
            string slotStr = string.Empty;
            slotList = new Dictionary<string, int>();
            appointmentsCombo.Items.Clear();
            for (int i = 0; i < workingHrs; i++)
            {
                slotStr = string.Format($"{startHr + i}:00 hrs.");
                slotList.Add(slotStr, i);
                appointmentsCombo.Items.Add($"{slotStr}");

            }


        }
        public void ComboUpdate()
        {
            if (events.Count != 0)
            {
                for (int i = 0; i < events.Count; i++)
                {
                    appointmentsCombo.Items.Remove(events[i].Slot);
                }
            }
        }



        private void TypeCheck(object sender, RoutedEventArgs e)
        {
            RadioButton radioBtn = (RadioButton)sender;
            labType.Foreground = Brushes.Black;
            switch (radioBtn.Name)
            {
                case "city":
                    mainTypeID = 0;
                    yes.IsEnabled = false;
                    no.IsEnabled = false;
                    break;
                case "rural":
                    mainTypeID = 1;
                    yes.IsEnabled = true;
                    no.IsEnabled = true;
                    break;
                case "large":
                    mainTypeID = 2;
                    yes.IsEnabled = false;
                    no.IsEnabled = false;
                    break;
                default:
                    yes.IsEnabled = false;
                    no.IsEnabled = false;
                    break;
            }

        }
        private void WellCheck(object sender, RoutedEventArgs e)
        {
            RadioButton radioBtn = (RadioButton)sender;
            labWell.Foreground = Brushes.Black;
            switch (radioBtn.Name)
            {
                case "yes":
                    haveWell = true;
                    break;
                case "no":
                    haveWell = false;
                    break;
                default:
                    break;

            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            valid = ValidateData();
            txtDisplay.Text = "";
            if (valid)
            {
                appointmentsCombo.Items.Remove(appointmentsCombo.SelectedItem);
                appointmentsCombo.SelectedIndex = -1;
                Schedule sc = CreateAppointment(mainTypeID, houseSize, tankSize, haveWell, name, card, slot);
               
                SaveToXML(sc);
                Clear();
                valid = false;

            }
        }
        private bool ValidateData()
        {
            bool valid = true;
            if (appointmentsCombo.SelectedIndex == -1 || appointmentsCombo.Items.IsEmpty)
            {
                labAppt.Foreground = Brushes.Red;
                valid = false;
            }
            else
            {
                slot = appointmentsCombo.Text;
            }
            if (string.IsNullOrEmpty(nameTxt.Text) || nameTxt.Text.Length <= 2)
            {
                nameTxt.Foreground = Brushes.Red;
                labName.Foreground = Brushes.Red;
                valid = false;
            }
            else { name = nameTxt.Text; }
            if (!double.TryParse(cardNumberTxt.Text, out card) || card > 9999999999999999 || card < 1000000000000000)
            {
                cardNumberTxt.Foreground = Brushes.Red;
                labCard.Foreground = Brushes.Red;
                valid = false;
            }



            if ((bool)!city.IsChecked)
            {
                if ((bool)!rural.IsChecked)
                {
                    if ((bool)!large.IsChecked)
                    {
                        labType.Foreground = Brushes.Red;
                        valid = false;
                    }
                }
                else
                {
                    if ((bool)!yes.IsChecked)
                    {
                        if ((bool)!no.IsChecked)
                        {
                            labWell.Foreground = Brushes.Red;
                            valid = false;
                        }
                    }
                }
            }
            if (!int.TryParse(sizeTxt.Text, out houseSize) || houseSize < 50 || houseSize > 50000)
            {
                sizeTxt.Foreground = Brushes.Red;
                labHSize.Foreground = Brushes.Red;
                valid = false;
            }
            else
            {
                sizeTxt.Text = houseSize.ToString();
            }
            if (!int.TryParse(tankTxt.Text, out tankSize) || tankSize < 30 || tankSize > 100)
            {
                tankTxt.Foreground = Brushes.Red;
                labTSize.Foreground = Brushes.Red;
                valid = false;
            }
            else
            {
                tankTxt.Text = tankSize.ToString();
            }

            return valid;
        }

        private void appointmentsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            labAppt.Foreground = Brushes.Black;
        }

        private void nameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameTxt.Foreground = Brushes.Black;
            labName.Foreground = Brushes.Black;
        }

        private void cardNumberTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            cardNumberTxt.Foreground = Brushes.Black;
            labCard.Foreground = Brushes.Black;
        }

        private void sizeTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            sizeTxt.Foreground = Brushes.Black;
            labHSize.Foreground = Brushes.Black;
        }

        private void tankTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            tankTxt.Foreground = Brushes.Black;
            labTSize.Foreground = Brushes.Black;
        }
        private void searchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            tankTxt.Foreground = Brushes.Black;
            labTSize.Foreground = Brushes.Black;
        }

        private Schedule CreateAppointment(int typeID, int houseSize, int tankSize, bool wellB, string name, double card, string slot)
        {
            var houseType = Enum.GetNames(typeof(HouseType));
            //Now we create properties/houses according to the info of the user
            Property ps = null;
            switch (typeID)
            {
                case (int)HouseType.CityHouse:
                    ps = new City() { HouseSize = houseSize, TankSize = tankSize, Type = (string)houseType[typeID], TypeID = typeID };
                    break;
                case (int)HouseType.Rural:
                    ps = new Rural() { HouseSize = houseSize, TankSize = tankSize, Type = (string)houseType[typeID], TypeID = typeID, Well = wellB };
                    break;
                case (int)HouseType.LargeRural:
                    ps = new LRural() { HouseSize = houseSize, TankSize = tankSize, Type = (string)houseType[typeID], TypeID = typeID };
                    break;
                default:

                    break;
            }
            //now we create the customer with its associated property
            Customer cust = new Customer(name, card, ps);
            //finally we create the appointment and insert it on an Schedule array.
            Schedule sc = new Schedule() { Customer = cust, Slot = slot, SlotID = slotList[slot] };


            return sc;

        }

        private void SaveToXML(Schedule sc)
        {
            events.Add(sc);
            ScheduleDisplay.Add(sc);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EventsIdx));
                TextWriter textWriter = new StreamWriter("schedule.xml");
                serializer.Serialize(textWriter, events);
                textWriter.Close();
                btnRead.IsEnabled = true;
            }catch (IOException e)
            {
                txtDisplay.Text = e.ToString();
            }
        }
        private void Clear()
        {
            name = string.Empty;
            card = 0;
            houseSize = 0;
            tankSize = 0;
            haveWell = false;
            slot = string.Empty;
            mainTypeID = -1;
            appointmentsCombo.SelectedIndex = -1;
            nameTxt.Text = string.Empty;
            cardNumberTxt.Text = string.Empty;
            sizeTxt.Text = string.Empty;
            tankTxt.Text = string.Empty;
            rural.IsChecked = false;
            city.IsChecked = false; ;
            large.IsChecked = false;
            yes.IsChecked = false;
            no.IsChecked = false;
            yes.IsEnabled = false;
            no.IsEnabled = false;
            txtDisplay.Text = string.Empty;
            labAppt.Foreground = Brushes.Black;
            labCard.Foreground = Brushes.Black;
            labHSize.Foreground = Brushes.Black;
            labName.Foreground = Brushes.Black;
            labTSize.Foreground = Brushes.Black;
            labType.Foreground = Brushes.Black;
            labWell.Foreground = Brushes.Black;
            searchTxt.Text = "";
            ScheduleGrid.ItemsSource = scheduleDisplay;
            

        }
        private void PrintToBox()
        {
            //goes on the list and prints all scheduled appointments
            events.Sort();
            txtDisplay.Text = "----------Printing Agenda for Today---------- \n";
            for (int i = 0; i < events.Count; i++)
            {

                txtDisplay.Text += string.Format($"{events[i].ToString()}\n");
                txtDisplay.Text += string.Format($"{events[i].Customer.ToString()}\n");
                events[i].Customer.Property.PS();
                txtDisplay.Text += events[i].Customer.Property.SrvMsg;
                txtDisplay.Text += "\n-------------------- \n";
                ScheduleDisplay.Add(events[i]);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {

            txtDisplay.Text = "";
            Clear();
            bool read = ReadFromXML();
            if (read)
            {
                PrintToBox();
            }
        }

        private bool ReadFromXML()
        {
            ScheduleDisplay.Clear();
            bool read = false;
            events = new EventsIdx();
            try {
                if (File.Exists(binFileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(EventsIdx));
                    TextReader textReader = new StreamReader("schedule.xml");
                    events = (EventsIdx)serializer.Deserialize(textReader);
                    textReader.Close();
                    ComboUpdate();
                    read = true;
                }
                else
                {
                    btnRead.IsEnabled = false;
                    txtDisplay.Text = "You have nothing stored yet.";
                }
            }
            catch (IOException e)
            {
                txtDisplay.Text = e.ToString();

            }
            return read;

        }

        private void btnEmpty_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(binFileName))
            {
                File.Delete(binFileName);
                ScheduleDisplay.Clear();
                Clear();
                txtDisplay.Text = "You have nothing stored yet.";
                btnRead.IsEnabled = false;
                ComboStart();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchTxt.Text = searchTxt.Text.Trim();
            if (searchTxt.Text == "")
            {
                ScheduleGrid.ItemsSource = scheduleDisplay;
            }
            else { 
                var query = from schedule in ScheduleDisplay
                        where schedule.Customer.Property.Type.ToUpper().Contains(searchTxt.Text.ToUpper().Trim().Replace(" ",string.Empty))
                        orderby schedule.Customer.Name ascending
                        select schedule;
                ScheduleGrid.ItemsSource = query;
            }
            
        }
    }
}
