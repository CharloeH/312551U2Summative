/*Sebastian Horton
 * Friday March 29th, 2019
 * The user inputs a first name, last name, birthday and email. They can press a "get age" button.
 * the program calculates the age and saves the user's input upon closing.
 * */
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
using System.IO;

namespace _312551U2Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Contact contact;

        public MainWindow()
        {

            InitializeComponent();

            contact = new Contact();
            ///checks if the user's "contact.txt" file exists, if it doesnt it creates one and adds example info to it
            if (!File.Exists("contact.txt"))
            {

                contact.WriteToFile();
                contact.ReadFromFile(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail);
            }
            else
                contact.ReadFromFile(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            contact.GetContact(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail, e);
            contact.WriteToFile();
        }

        private void BtnDisplayAge_Click(object sender, RoutedEventArgs e)
        {
            contact.GetBirthday(txtbxBirthday);
            contact.GetAge(txtblOutput);
        }
    }
    class Contact
    {
        ///declared memeber variables
        private string _firstName = "First Name";
        private string _lastName = "Last Name";
        private DateTime _birthday = new DateTime(0001, 1, 1);
        private string _email = "Email@gmail.com";
        private int _age;
        private string[] _contactInfo;
        ///other variables
        DateTime currentDate = DateTime.Now;

        public void GetContact(TextBox firstName, TextBox lastName, TextBox birthday, TextBox email, CancelEventArgs e)
        {

            ///overriding the stored information with the user's input
            if (!firstName.Text.Contains(",") || !lastName.Text.Contains(",")) ///checks for inappropriately placed commas
            {
                _firstName = firstName.Text;
                _lastName = lastName.Text;
            }
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Your name cannot contain a comma.", "warning", MessageBoxButton.OK);
                CancelClosing(e, mbr);
            }

            bool isNumber = DateTime.TryParse(birthday.Text, out _birthday); ///checks for a valid birthday in YYYY/MM/DD
            if (isNumber == false)
            {
                MessageBoxResult mbr = MessageBox.Show("Please enter a birthday in YYYY/MM/DD format.", "warning", MessageBoxButton.OK);
                CancelClosing(e, mbr);
            }

            ///checks for valid a email adress
            if (email.Text.Contains("@") & (email.Text.Contains(".com") || email.Text.Contains(".ca") || email.Text.Contains(".net")))
                _email = email.Text;
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Please enter a valid email adress.", "warning", MessageBoxButton.OK);
                CancelClosing(e, mbr);
            }
        }

        private static void CancelClosing(CancelEventArgs e, MessageBoxResult mbr)
        {
            if (mbr == MessageBoxResult.OK)
                e.Cancel = true;
        }

        public void GetBirthday(TextBox birthday)
        {
            bool isNumber = DateTime.TryParse(birthday.Text, out _birthday); ///checks for a valid birthday in YYYY/MM/DD
            if (isNumber == false)
                MessageBox.Show("Please enter a birthday in YYYY/MM/DD format.");
        }

        public void ReadFromFile(TextBox firstName, TextBox lastName, TextBox birthday, TextBox email)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("contact.txt"); ///reading user's information from the text file
                string s = sr.ReadLine();

                _contactInfo = s.Split(',');///splices the information into seperate strings 

                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ///adding the stored information into the text fields
            firstName.Text = _contactInfo[0];
            lastName.Text = _contactInfo[1];
            birthday.Text = _contactInfo[2]; 
            email.Text = _contactInfo[3];
        }

        public void WriteToFile()
        {
            try
            {
                ///writing the user's input information into the text file
                System.IO.StreamWriter sw = new System.IO.StreamWriter("contact.txt");
                sw.WriteLine(_firstName + "," + _lastName + "," + _birthday.ToShortDateString() + "," + _email);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void GetAge(TextBlock txtbl)
        {
            
            ///checks if the user's birthday has occured yet this year
            if ((currentDate.DayOfYear - _birthday.DayOfYear) < 0) ///if the user's birthday hasn't happened this year before the current date, the difference will be negative
                _age = (currentDate.Year - _birthday.Year) - 1;
            else
                _age = currentDate.Year - _birthday.Year; ///if the user's birthday has happened this year before the current date, the difference will be positive
            if (_age <= 116) ///the oldest human being (recorded) is currenty 116 years old.
                txtbl.Text = "Age: " + _age.ToString();
            else
                MessageBox.Show("Your age is not valid. Please enter a birthday that is in the last 116 years.");
        }
    }
}

