using ContactList.Entities;
using ContactList.SqlDAL;
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

namespace ContactList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlDAO sqlDAO = new SqlDAO();
        List<Contact> contactList = new List<Contact>();

        public MainWindow()
        {
            InitializeComponent();
                 
            contactList = sqlDAO.GetContacts().ToList();

            list.ItemsSource = contactList;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            contactList = sqlDAO.GetContactByLetters(tbSearch.Text).ToList();

            list.ItemsSource = contactList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            sqlDAO.AddContact(tbAddingName.Text, tbAddingNumber.Text); 
            
            contactList = sqlDAO.GetContacts().ToList();

            list.ItemsSource = contactList;
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)list.SelectedItem;

            if (selectedContact == null)
            {
                tbSelectedName.Text = "";
            }
            else
            {
                tbSelectedName.Text = selectedContact.Name;
            }

            if (selectedContact == null)
            {
                tbSelectedNumber.Text = "";
            }
            else
            {
                tbSelectedNumber.Text = selectedContact.PhoneNumber;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Contact selectedContact = (Contact)list.SelectedItem;

            sqlDAO.EditContact(selectedContact.ID, tbSelectedName.Text, tbSelectedNumber.Text);

            contactList = sqlDAO.GetContacts().ToList();

            list.ItemsSource = contactList;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Contact selectedContact = (Contact)list.SelectedItem;

            sqlDAO.DeleteContact(selectedContact.ID);

            contactList = sqlDAO.GetContacts().ToList();

            list.ItemsSource = contactList;
        }
    }
}
