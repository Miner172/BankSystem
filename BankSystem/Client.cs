using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    /// <summary>
    /// информация о клиенте
    /// </summary>
    public class Client
    {
        /// <summary>
        /// эвент вызваеться когда создаеться клиент
        /// </summary>
        public static event Action<Client> CreateClient;

        /// <summary>
        /// конструктор вызывает эвент создание клиента и присваивает свойствам значения
        /// </summary>
        /// <param name="Name"></param>
        public Client(string Name)
        {
            this.Name = Name;
            this.bankAccounts = new List<BankAccount>();

            CreateClient?.Invoke(this);
        }

        /// <summary>
        /// конструктор
        /// </summary>
        private Client() : this("Имя")
        {
            bankAccounts = new List<BankAccount>();
        }

        /// <summary>
        /// содержит имя клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// содержит массив с клиентскими счетами
        /// </summary>
        public List<BankAccount> bankAccounts { get; set; }
    }
}
