using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Client
    {
        public static event Action<Client> CreateClient;

        public Client(string Name)
        {
            this.Name = Name;
            this.bankAccounts = new List<BankAccount>();

            CreateClient?.Invoke(this);
        }

        private Client() : this("Имя")
        {
            bankAccounts = new List<BankAccount>();
        }

        public string Name { get; set; }
        public List<BankAccount> bankAccounts { get; set; }
    }
}
