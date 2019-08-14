using System;
using System.Windows;
using WpfPersonApplication.Models;

namespace WpfPersonApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListOfPeople list = new ListOfPeople();
        string errorString = "";
        int removeLocation = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowIsLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dob;
            int age = 0;
            //if valid info collect and add to list
            if (firstNameTextBox.Text.Length != 0 && lastNameTextBox.Text.Length != 0 && DateTime.TryParse(dobTextBox.Text, out dob) && (genderTextBox.Text.Length != 0 && (genderTextBox.Text.ToLower() == "f" || genderTextBox.Text.ToLower() == "m")))
            {
                age = DateTime.Today.Year - dob.Year;
                Person person = new Person();
                person.FirstName = firstNameTextBox.Text;
                person.LastName = lastNameTextBox.Text;
                person.DateOfBirth = dob.ToString("MM-dd-yyyy");
                person.Age = age;
                person.Gender = genderTextBox.Text;
                if (errorLabel.IsVisible)
                    errorLabel.Visibility = Visibility.Hidden;
                ClearTextBoxes();
                //if updating a person in the list
                if (AddButton.Content.ToString() == "Update")
                {
                    AddButton.Content = "Add";
                    list.RemovePerson(removeLocation);
                    personDataGrid.Items.RemoveAt(removeLocation);
                    list.InsertPerson(person, removeLocation);
                    personDataGrid.Items.Insert(removeLocation, person);
                    delButton.Visibility = Visibility.Hidden;
                    deselectButton.Visibility = Visibility.Hidden;
                }
                //if adding a new person
                else
                {
                    list.AddPerson(person);
                    personDataGrid.Items.Add(person);
                }
            }
            else
            {
                //display error to user
                if (firstNameTextBox.Text.Length == 0)
                    errorString = "First Name Error";
                if (lastNameTextBox.Text.Length == 0)
                    errorString = "Last Name Error";
                if (!DateTime.TryParse(dobTextBox.Text, out dob) || dob.ToString() == "1/1/0001 12:00:00 AM")
                    errorString = "Date Of Birth Error";
                if (genderTextBox.Text.Length == 0 || (genderTextBox.Text.ToLower() != "f" || genderTextBox.Text.ToLower() != "m"))
                    errorString = "Gender Error must be \"m\" or \"f\"";

                errorLabel.DataContext = errorString;
                errorLabel.Visibility = Visibility.Visible;
            }
        }

        private void ClearTextBoxes()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            dobTextBox.Text = "";
            genderTextBox.Text = "";
        }

        //put selected persons info in to text boxes and bring up delete and deslect buttons
        private void editPerson_Clicked(object sender, RoutedEventArgs e)
        {
            int index = personDataGrid.SelectedIndex;
            Person person = list.LookUpPerson(index);
            firstNameTextBox.Text = person.FirstName;
            lastNameTextBox.Text = person.LastName;
            dobTextBox.Text = person.DateOfBirth;
            genderTextBox.Text = person.Gender;

            removeLocation = list.LookUpPersonIndex(person);

            delButton.Visibility = Visibility.Visible;
            deselectButton.Visibility = Visibility.Visible;
            AddButton.Content = "Update";
            
        }

        //remove person from list and data grid and reset buttons
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            list.RemovePerson(removeLocation);
            personDataGrid.Items.RemoveAt(removeLocation);
            AddButton.Content = "Add";
            delButton.Visibility = Visibility.Hidden;
            deselectButton.Visibility = Visibility.Hidden;
            ClearTextBoxes();
        }

        //Clear text fields and reset buttons
        private void DeselectButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            AddButton.Content = "Add";
            delButton.Visibility = Visibility.Hidden;
            deselectButton.Visibility = Visibility.Hidden;
        }
    }
}
