using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class DataBase : IDataBase
    {
        public List<BankAccount> BankAccountsCount { get; set; }
        public List<Client> ClientsCount { get; set; }

        public DataBase()
        {
            BankAccountsCount = new List<BankAccount>();
            ClientsCount = new List<Client>();
            BankAccount.CreateBankAccount += AddBankAccount;
            Client.CreateClient += AddClient;
        }

        public void AddClient(Client client)
        {
            ClientsCount.Add(client);
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            BankAccountsCount.Add(bankAccount);
        }
    }
}
