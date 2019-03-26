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
        }

        private void ButtonUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            contact.GetAge();
            UpdateInfo();
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
            lblOutput.Text = "First Name: " + contact.firstName + "\r Last Name: " + contact.lastName + 
                "\r Age: " + contact.age + "\r Email: " + contact.email;
        }
    }
    class Contact
    {
        public string firstName;
        public string lastName;
        public DateTime birthday = new DateTime(2002,6,2);
        DateTime currentDate = DateTime.Now;
        public string email;
        public int age;

        public void ReadFromFile()
        {

        }
        public void WriteToFile()
        {

        }
        public int GetAge()
        {
            age = (currentDate.Year - birthday.Year);
            Console.WriteLine(age.ToString());
            return age;
        }
    }
}
