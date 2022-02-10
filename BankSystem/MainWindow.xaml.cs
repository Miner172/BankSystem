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

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CloseBankAccountBtn.Visibility = Visibility.Hidden;

            Client client = new Client("matis");
            Client client2 = new Client("matis2");
            BankAccount bankAccount1 = new BankAccount(client, 2000, true);
            BankAccount bankAccount2 = new BankAccount(client, 123, false);
            BankAccount bankAccount3 = new BankAccount(client, 123, false);
            BankAccount bankAccount4 = new BankAccount(client, 21300, false);
            BankAccount bankAccount5 = new BankAccount(client, 12, false);
            BankAccount bankAccount1312 = new BankAccount(client2, 100, true);

            ClientsComboBox.ItemsSource = DataBase.ClientsCount;
            FromWhomComboBox.ItemsSource = DataBase.BankAccountsCount;
            WhomComboBox.ItemsSource = DataBase.BankAccountsCount;
        }

        private void ClientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ClientsComboBox.SelectedIndex;
            BankAccountsListView.ItemsSource = DataBase.ClientsCount[index].bankAccounts;
        }

        private void BankAccountsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BankAccountsListView.SelectedItems.Count != 0)
                CloseBankAccountBtn.Visibility = Visibility.Visible;
            else
                CloseBankAccountBtn.Visibility = Visibility.Hidden;
        }

        private void CloseBankAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccount bankAccount = (BankAccount)BankAccountsListView.SelectedItem;
            bankAccount.CloseOpenBankAccount();
            BankAccountsListView.Items.Refresh();
        }

        private void TranslationMoneyBankAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MoneyCountTextBox.Text != string.Empty && FromWhomComboBox.SelectedIndex != -1 && WhomComboBox.SelectedIndex != -1)
            {
                BankAccount fromWhomBankAccount = (BankAccount)FromWhomComboBox.SelectedItem;
                BankAccount whomBankAccount = (BankAccount)WhomComboBox.SelectedItem;

                int money = int.Parse(MoneyCountTextBox.Text);

                MessageBox.Show($"{fromWhomBankAccount.TranslationMoney(whomBankAccount, money)}");
                BankAccountsListView.Items.Refresh();
            }
        }

        private void SkipMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var bankAccount in DataBase.BankAccountsCount)
            {
                bankAccount.EndOfTheMonth(1);
            }

            BankAccountsListView.Items.Refresh();
        }
    }
}
