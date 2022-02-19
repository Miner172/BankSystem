using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public interface IDataBase
    {
        List<BankAccount> BankAccountsCount { get; set; }

        List<Client> ClientsCount { get; set; }

        void AddClient(Client client);

        void AddBankAccount(BankAccount bankAccount);
    }
}
