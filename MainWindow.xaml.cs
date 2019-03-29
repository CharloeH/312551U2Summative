/*Sebastian Horton
 * Friday March 29th, 2019
 * The user inputs information and can press a "get age" button.
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
            if (!File.Exists("contact.txt") )
            {

                contact.WriteToFile();
                contact.ReadFromFile(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail);
            }
            else 
                contact.ReadFromFile(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            contact.getContact(txtbxFirstName, txtbxLastName, txtbxBirthday, txtbxEmail, e);
            contact.WriteToFile();
        }

        private void BtnDisplayAge_Click(object sender, RoutedEventArgs e)
        {
            contact.getBirthday(txtbxBirthday);
            contact.getAge(txtblOutput);
        }
    }
    class Contact
    {
        ///declared memeber variables
        private string _firstName = "Sebastian";
        private string _lastName = "Horton";
        private DateTime _birthday = new DateTime(2002, 6, 2);
        private string _email = "sebastian@hobro.ca";
        private int age;

        public void getContact(TextBox firstName, TextBox lastName, TextBox birthday, TextBox email, CancelEventArgs e)
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
                cancelClosing(e, mbr);
            }

            bool isNumber = DateTime.TryParse(birthday.Text, out _birthday); ///checks for a valid birthday in YYYY/MM/DD
            if (isNumber == false)
            {
                MessageBoxResult mbr = MessageBox.Show("Please enter a birthday in YYYY/MM/DD format.", "warning", MessageBoxButton.OK);
                cancelClosing(e, mbr);
            }

            if (email.Text.Contains("@")) ///checks for valid a email adress
                _email = email.Text;
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Please enter a valid email adress.", "warning", MessageBoxButton.OK);
                cancelClosing(e, mbr);
            }
        }

        private static void cancelClosing(CancelEventArgs e, MessageBoxResult mbr)
        {
            if (mbr == MessageBoxResult.OK)
                e.Cancel = true;
        }

        public void getBirthday(TextBox birthday)
        {
            bool isNumber = DateTime.TryParse(birthday.Text, out _birthday); ///checks for a valid birthday in YYYY/MM/DD
            if (isNumber == false)
                MessageBox.Show("Please enter a birthday in YYYY/MM/DD format.");
        }

        public void ReadFromFile(TextBox firstName, TextBox lastName, TextBox birthday, TextBox email)
        {
            try
            {
                ///taking the stored information from the text file and converting it into variables
                System.IO.StreamReader sr = new System.IO.StreamReader("contact.txt");
                string s = sr.ReadLine(); ///temportary string containg all of the information

                _firstName = s.Substring(0, s.IndexOf(",")); ///adding the firstName to the _firstName private variable
                s = s.Substring(s.IndexOf(",") + 1); ///removing the firstName information from the string

                _lastName = s.Substring(0, s.IndexOf(",")); ///adding the lastName to the _lastName private variable
                s = s.Substring(s.IndexOf(",") + 1); ///removing the lastName information from the string

                DateTime.TryParse(s.Substring(0, s.IndexOf(",")), out _birthday); ///adding the birthday to the _birthday private variable
                s = s.Substring(s.IndexOf(",") + 1); ///removing the birthday information from the string

                _email = s.Substring(s.IndexOf(",") + 1); ///adding the only remaining information (the email) to the _email private variable 

                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ///adding the stored information into the text fields
            firstName.Text = _firstName;
            lastName.Text = _lastName;
            birthday.Text = _birthday.ToShortDateString(); ///converting the _birthday into shortDate to remove the time
            email.Text = _email;
        }

        public void WriteToFile()
        {
            try
            {
                ///writing the user's input information into the text file
                System.IO.StreamWriter sw = new System.IO.StreamWriter("contact.txt");
                sw.WriteLine(_firstName + "," + _lastName + "," + _birthday + "," + _email);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void getAge(TextBlock txtbl)
        {
            DateTime currentDate = DateTime.Now;
            ///checks if the user's birthday has occured yet this year
            if ((currentDate.DayOfYear - _birthday.DayOfYear) < 0) ///if the user's birthday hasn't happened this year before the current date, the difference will be negative
                age = (currentDate.Year - _birthday.Year) - 1;
            else
                age = currentDate.Year - _birthday.Year; ///if the user's birthday has happened this year before the current date, the difference will be positive
            if (age <= 116)
                txtbl.Text = "Age: " + age.ToString();
            else
                txtbl.Text = "Age: dead";
        }
    }
}

