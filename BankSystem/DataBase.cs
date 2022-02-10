using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    static class DataBase
    {
        static public List<BankAccount> BankAccountsCount { get; set; }
        static public List<Client> ClientsCount { get; set; }

        static DataBase()
        {
            BankAccountsCount = new List<BankAccount>();
            ClientsCount = new List<Client>();
        }

        static public void AddClient(Client client)
        {
            ClientsCount.Add(client);
        }

        static public void AddBankAccount(BankAccount bankAccount)
        {
            BankAccountsCount.Add(bankAccount);
        }
    }
}
