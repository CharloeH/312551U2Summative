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

        Contact contact;
        string firstName;
        string lastName;
        DateTime birthday;
        string email;
        public MainWindow()
        {
            
            InitializeComponent();
            contact = new Contact(firstName, lastName, birthday, email);
            contact.ReadFromFile();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            GetInfo();
            //contact.WriteToFile();
        }
        public void GetInfo()
        {
            firstName = txtbxFirstName.Text;

            lastName = txtbxLastName.Text;

            bool isNumber = DateTime.TryParse(txtbxBirthday.Text, out birthday);

            if (isNumber == false)
                MessageBox.Show("Please enter a birthday in YYYY/MM/DD format.");

            email = txtbxEmail.Text;
            Console.WriteLine(firstName + lastName + birthday + email);
        }

        
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            contact.WriteToFile(firstName, lastName, birthday, email);
            Console.WriteLine(firstName +  lastName + birthday + email);
        }

        private void BtnDisplayAge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    class Contact
    {
        private string _firstName;
        private string _lastName;
        private DateTime _birthday;
        private string _email;
        DateTime currentDate = DateTime.Now;
        public int age;
        
        public Contact(string firstName, string lastName, DateTime birthday, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthday = birthday;
            _email = email;
        }
        public void ReadFromFile()
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("contact.txt");
                string s = sr.ReadLine();
                _firstName = s.Substring(0, s.IndexOf(","));
                s = s.Substring(s.IndexOf(",") + 1);
                _lastName = s.Substring(0, s.IndexOf(","));
                s = s.Substring(s.IndexOf(",") + 1);
                int.TryParse(s.Substring(s.IndexOf(",") + 1), out age);
                s = s.Substring(s.IndexOf(",") + 1);
                _email = s.Substring(s.IndexOf(",") + 1);
                sr.Close();
                Console.WriteLine(_firstName + _lastName + _email + age);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void WriteToFile(string firstName, string lastName, DateTime birthday, string email)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter("contact.txt");
                sw.WriteLine(firstName + "," + lastName + "," + age + "," + email);
                sw.Flush();
                sw.Close();
                Console.WriteLine(_firstName + _lastName + age + _email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public int GetAge()
        {
            if ((currentDate.DayOfYear - _birthday.DayOfYear) < 0)
            {
                age = (currentDate.Year - _birthday.Year - 1);
            }
            else
                age = (currentDate.Year - _birthday.Year);
            return age;
        }

    }
}

