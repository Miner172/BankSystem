using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    /// <summary>
    /// информация о счете
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// эвент вызываеться когда создаеться счет
        /// </summary>
        static public event Action<BankAccount> CreateBankAccount;
        /// <summary>
        /// эвент вызываеться когда счет открываеться
        /// </summary>
        static public event Action<BankAccount> OpenBankAccountEvent;
        /// <summary>
        /// эвент вызываеться когда счет закрываеться
        /// </summary>
        static public event Action<BankAccount> CloseBankAccountEvent;
        /// <summary>
        /// эвент вызываеться когда происходит перевод средств
        /// </summary>
        static public event Action<BankAccount, BankAccount, float> TranslationMoneyEvent;
        /// <summary>
        /// статическое свойство random
        /// </summary>
        static Random random;

        /// <summary>
        /// статический конструктор инициализирует статическое свойство random
        /// </summary>
        static BankAccount()
        {
            random = new Random();
        }

        /// <summary>
        /// конструктор вызывает эвент создание счета и присваивает свойствам значения
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="Money"></param>
        /// <param name="IsDepsite"></param>
        /// <param name="Code"></param>
        public BankAccount(Client Owner, float Money, bool IsDepsite, string Code)
        {
            this.Owner = Owner;
            this.Money = Money;
            this.IsOpen = true;
            this.IsDeposite = IsDepsite;
            this.Code = Code;

            if (this.Owner.bankAccounts.Count + 1 >= 4)
                throw new MaxBankAccountException("Слишком много счетов на одного клиента", this.Owner.Name);

            this.Owner.bankAccounts.Add(this);
            CreateBankAccount?.Invoke(this);
        }

        /// <summary>
        /// метод вызываеться в конце месяца и на депозитные счета ложаться деньги
        /// </summary>
        /// <param name="Percent"></param>
        public void EndOfTheMonth(int Percent)
        {
            if (IsDeposite && IsOpen)
                this.Money = this.Money * (100 + Percent) / 100;
        }

        /// <summary>
        /// метод вызыветься при переводе средств а тажке вызывет эвент перевода средств
        /// </summary>
        /// <param name="Recipient"></param>
        /// <param name="MoneyString"></param>
        /// <returns></returns>
        public string TranslationMoney(BankAccount Recipient, string MoneyString)
        {
            try
            {
                float Money = Convert.ToSingle(MoneyString);
                if (Recipient != this)
                {
                    if (Recipient.IsOpen)
                    {
                        if (Money < this.Money && Money >= 0)
                        {
                            Recipient.Money += Money;
                            this.Money -= Money;
                            TranslationMoneyEvent?.Invoke(this, Recipient, Money);
                            return "Все прошло успешно";
                        }
                        else
                            return "Недостаточно средств или нельзя переводить в минус";
                    }
                    else
                        return "Нельзя перевести деньги на закрытый счет";
                }
                else
                    return "Нельзя перевести деньги самому себе";
            }
            catch (FormatException)
            {
                return "можно исползовать только цифры и запятные";
            }
            catch (OverflowException)
            {
                return "слишком большое или слишком маленкое число";
            }
            catch (Exception)
            {
                return "ошибка...";
            }
        }

        /// <summary>
        /// открывает или закрывает счет и вызвает эвенты открытия или закрытия счета
        /// </summary>
        public void CloseOpenBankAccount()
        {
            if (this.IsOpen)
            {
                this.IsOpen = false;
                CloseBankAccountEvent?.Invoke(this);
            }
            else
            {
                this.IsOpen = true;
                OpenBankAccountEvent?.Invoke(this);
            }
        }

        /// <summary>
        /// свойство хранит код счета
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// свойство хранит владельца счета
        /// </summary>
        public Client Owner { get; private set; }
        /// <summary>
        /// свойство хранит деньги счета
        /// </summary>
        public float Money { get; set; }
        /// <summary>
        /// свойство хранит информацию депозитный ли счет
        /// </summary>
        public bool IsDeposite { get; set; }
        /// <summary>
        /// свойство хранит информацию открытый ли счет
        /// </summary>
        public bool IsOpen { get; set; }
    }
}
