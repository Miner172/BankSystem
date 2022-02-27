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
using BankLibrary;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// свойство random
        /// </summary>
        private Random random;
        /// <summary>
        /// свойство содержит логи
        /// </summary>
        private List<string> logs;
        /// <summary>
        /// свойство содержит данные о клиентах и счетах
        /// </summary>
        private IDataBase dataBase;

        private void SS()
        {
            string s = "errir";
            if (s == "errir") throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// конструктор задает свойствам значения и подписываеться на разные эвенты
        /// </summary>
        /// <param name="dataBase"></param>
        public MainWindow(IDataBase dataBase)
        {
            this.dataBase = dataBase;
            InitializeComponent();

            CloseBankAccountBtn.Visibility = Visibility.Hidden;
            LogsPanel.Visibility = Visibility.Hidden;
            logs = new List<string>();
            random = new Random();

            Client client = new Client("matis");
            Client client2 = new Client("matis2");

            try
            {
                BankAccount bankAccount1 = new BankAccount(client, 2000, true, GenerateNumber());
                BankAccount bankAccount2 = new BankAccount(client, 123, false, GenerateNumber());
                BankAccount bankAccount3 = new BankAccount(client, 123, false, GenerateNumber());
                //BankAccount bankAccount4 = new BankAccount(client, 21300, false, GenerateNumber());
                BankAccount bankAccount11 = new BankAccount(client2, 12, true, GenerateNumber());
                BankAccount bankAccount1312 = new BankAccount(client2, 100, true, GenerateNumber());
            }
            catch (MaxBankAccountException e)
            {
                MessageBox.Show($"{e.Message}: {e.Name}");
            }

            ClientsComboBox.ItemsSource = dataBase.ClientsCount;
            FromWhomComboBox.ItemsSource = dataBase.BankAccountsCount;
            WhomComboBox.ItemsSource = dataBase.BankAccountsCount;
            LogsListBox.ItemsSource = logs;

            BankAccount.TranslationMoneyEvent += TranslationMoneyToLogs;
            BankAccount.OpenBankAccountEvent += CloseOrOpenBankAccountToLogs;
            BankAccount.CloseBankAccountEvent += CloseOrOpenBankAccountToLogs;
        }

        /// <summary>
        /// метод вызываеться при открытии или закрытии счета и выводит это в логи
        /// </summary>
        /// <param name="bankAccount"></param>
        private void CloseOrOpenBankAccountToLogs(BankAccount bankAccount)
        {
            if(bankAccount.IsOpen)
                logs.Add($"у {bankAccount.Owner.Name} открылся счет с кодом: {bankAccount.Code}");
            else
                logs.Add($"у {bankAccount.Owner.Name} закылся счет с кодом: {bankAccount.Code}");
            LogsListBox.Items.Refresh();
        }

        /// <summary>
        /// вызываеться когда происходит перевод между счетами и записывает это в логи
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="recipient"></param>
        /// <param name="money"></param>
        private void TranslationMoneyToLogs(BankAccount bankAccount, BankAccount recipient, float money)
        {
            logs.Add($"{bankAccount.Owner.Name} перевел деньги с счета с кодом: {bankAccount.Code} на счет {recipient.Owner.Name} с кодом: {recipient.Code} {money} рублей");
            LogsListBox.Items.Refresh();
        }

        /// <summary>
        /// метод вызываеться когда меняеться выбранный элемент в ClientsComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ClientsComboBox.SelectedIndex;
            BankAccountsListView.ItemsSource = dataBase.ClientsCount[index].bankAccounts;
        }

        /// <summary>
        /// метод вызываеться когда меняеться выбранный элемент в BankAccountsListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BankAccountsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BankAccountsListView.SelectedItems.Count != 0)
                CloseBankAccountBtn.Visibility = Visibility.Visible;
            else
                CloseBankAccountBtn.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// вызываеться при нажатии на кнопку TranslationMoneyBankAccountBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationMoneyBankAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MoneyCountTextBox.Text != string.Empty && FromWhomComboBox.SelectedIndex != -1 && WhomComboBox.SelectedIndex != -1)
            {
                BankAccount fromWhomBankAccount = (BankAccount)FromWhomComboBox.SelectedItem;
                BankAccount whomBankAccount = (BankAccount)WhomComboBox.SelectedItem;

                MessageBox.Show($"{fromWhomBankAccount.TranslationMoney(whomBankAccount, MoneyCountTextBox.Text)}");
                MoneyCountTextBox.Text = "";
                BankAccountsListView.Items.Refresh();
            }
        }

        /// <summary>
        /// вызываеться при нажатии на кнопку CloseBankAccountBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseBankAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccount bankAccount = (BankAccount)BankAccountsListView.SelectedItem;
            bankAccount.CloseOpenBankAccount();


            BankAccountsListView.Items.Refresh();
        }

        /// <summary>
        /// вызываеться при нажатии на кнопку SkipMonthBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var bankAccount in dataBase.BankAccountsCount)
            {
                bankAccount.EndOfTheMonth(1);
            }

            BankAccountsListView.Items.Refresh();
        }

        /// <summary>
        /// вызываеться при нажатии на кнопку CheckLogsBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckLogsBtn_Click(object sender, RoutedEventArgs e)
        {
            LogsPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// вызываеться при нажатии на кнопку CloseLogsBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseLogsBtn_Click(object sender, RoutedEventArgs e)
        {
            LogsPanel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// вызываеться в конструкторе MainWindow для генерирования кода
        /// </summary>
        /// <returns></returns>
        private string GenerateNumber()
        {
            int s1 = random.Next(100, 999);
            int s2 = random.Next(100, 999);
            int s3 = random.Next(100, 999);
            string code = $"{s1}{s2}{s3}";

            foreach (var BankAccount in dataBase.BankAccountsCount)
            {
                if (BankAccount.Code == code)
                {
                    return GenerateNumber();
                }
            }
            return code;
        }
    }
}
