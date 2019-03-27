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
using System.ComponentModel;
namespace _312551U2Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Contact contact = new Contact();
        public MainWindow()
        {
            InitializeComponent();
            contact.ReadFromFile(tblOutput);
        }

        private void ButtonUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
            contact.GetAge();
            contact.ReadFromFile(tblOutput);
            
        }
        public void UpdateInfo()
        {
            if (((bool)rbFirstName.IsChecked) == true)
            {
                contact.firstName = txtbxInput.Text;
            }
            if (((bool)rbLastName.IsChecked) == true)
            {
                contact.lastName = txtbxInput.Text;
            }
            if (((bool)rbEmail.IsChecked) == true)
            {
                contact.email = txtbxInput.Text;
            }
            if(((bool)rbBirthday.IsChecked == true))
            {
                DateTime.TryParse(txtbxInput.Text, out Contact.birthday);
            }
        }

        private void ClearInput_Click (object sender, RoutedEventArgs e)
        {
            txtbxInput.Text = null;
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //contact.WriteToFile();
        }
    }
    class Contact
    {
        public string firstName;
        public string lastName;
        public static DateTime birthday = new DateTime();
        DateTime currentDate = DateTime.Now;
        public string email;
        public int age;
        //System.IO.StreamReader sr = new System.IO.StreamReader("contact.txt");
        
        public void ReadFromFile(TextBlock tbl)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("contact.txt");
            string savedInfo = sr.ReadLine();
            firstName = savedInfo.Substring(0, savedInfo.IndexOf(","));
            savedInfo = savedInfo.Substring(savedInfo.IndexOf(",") + 1);
            lastName = savedInfo.Substring(0, savedInfo.IndexOf(","));
            savedInfo = savedInfo.Substring(savedInfo.IndexOf(",") + 1);
            int.TryParse(savedInfo.Substring(savedInfo.IndexOf(",") + 1), out age);
            savedInfo = savedInfo.Substring(savedInfo.IndexOf(",") + 1);
            email = savedInfo.Substring(savedInfo.IndexOf(",") + 1);

            tbl.Text = firstName + lastName + age + email;

        }
        public void WriteToFile()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("contact.txt");
            try
            {
                sw.WriteLine  (firstName + "," + lastName + "," + age + "," + email);
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public int GetAge()
        {
            if ((currentDate.DayOfYear - birthday.DayOfYear) < 0)
            {
                age = (currentDate.Year - birthday.Year -1);
            }
            else
                age = (currentDate.Year - birthday.Year);
            return age;
        }
        
    }
}
