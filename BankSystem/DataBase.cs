using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    /// <summary>
    /// здесь храниться вся информация о клиентах и их счетах
    /// </summary>
    public class DataBase : IDataBase
    {
        /// <summary>
        /// массивы с клиентами и счетами
        /// </summary>
        public List<BankAccount> BankAccountsCount { get; set; }
        public List<Client> ClientsCount { get; set; }

        /// <summary>
        /// конструктор инициализирует массивы и подписиваеться на эвенты создание клиента и счета
        /// </summary>
        public DataBase()
        {
            BankAccountsCount = new List<BankAccount>();
            ClientsCount = new List<Client>();
            BankAccount.CreateBankAccount += AddBankAccount;
            Client.CreateClient += AddClient;
        }

        /// <summary>
        /// метод добаление нового клиента в массив клиенты
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            ClientsCount.Add(client);
        }

        /// <summary>
        /// метод добаление нового счета в массив счета
        /// </summary>
        /// <param name="bankAccount"></param>
        public void AddBankAccount(BankAccount bankAccount)
        {
            BankAccountsCount.Add(bankAccount);
        }
    }
}
