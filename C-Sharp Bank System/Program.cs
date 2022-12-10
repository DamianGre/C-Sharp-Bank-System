using System.Net;

namespace BankSystem
{
    class Account
    {
        public List<double> DepositHistory = new List<double>();

        public List<DateTime> DateDepositHistory = new List<DateTime>();

        public double balance, credit;
        public string setName, setPassword;
        public bool creditCheck = false;

        public string userNameToTransfer;
        public double transferAmount;

        public bool userIsLogged = false;
        public bool transferAction = false;

        public Account(double balance, string setName, string setPassword, double Credit, bool creditCheck)
        {
            this.balance = 0;
            this.setName = setName;
            this.setPassword = setPassword;
            this.credit = 0;
            this.creditCheck = creditCheck;
        }
        public void Deposit()
        {
            Console.Write("Enter amount to Deposit: \n");
            double transactionHistoryPlus = Convert.ToDouble(Console.ReadLine());

            if (transactionHistoryPlus > 0)
            {
                DateTime dateTime = DateTime.Now;

                this.balance += transactionHistoryPlus;
                DepositHistory.Add(transactionHistoryPlus);
                DateDepositHistory.Add(dateTime);
                Console.WriteLine("Amount successfully deposited!\n");
            }
            else
            {
                Console.WriteLine("Invalid amount - You can't deposit 0 or less then 0");
            }
        }
        public void Withdraw()
        {
            Console.Write("Enter amount to withdraw: \n");
            double transactionHistoryMinus = Convert.ToDouble(Console.ReadLine());
            if (transactionHistoryMinus <= 0)
            {
                Console.WriteLine("You can't withdraw minus or 0 value");
            }
            else
            {
                if (this.balance >= transactionHistoryMinus)
                {
                    this.balance -= transactionHistoryMinus;
                    DepositHistory.Add(transactionHistoryMinus * -1);


                    DateTime dateTime = DateTime.Now;
                    DateDepositHistory.Add(dateTime);
                    Console.WriteLine("Amount successfully withdrawn!");
                }
                else
                {
                    Console.WriteLine("Not enough funds to withdraw!\n");
                }
            }
        }
        public void ShowBalanceAmount()
        {
            Console.WriteLine(balance);
        }
        public void History()
        {
            for (int x = 0; x < DepositHistory.Count || x < DateDepositHistory.Count; x++)
            {
                if (DepositHistory[x] < 0)
                {
                    Console.WriteLine("In day: " + DateDepositHistory[x]
                            + ". Withdraw amount: " + DepositHistory[x]);
                }
                else
                {
                    Console.WriteLine("In day: " + DateDepositHistory[x]
                            + ". Account credited with: " + DepositHistory[x]);
                }
            }

            Console.WriteLine("Present balance: " + balance);
            Console.WriteLine(" ");
        }
        public double Credit() //User can take only ONE credit!
        {
            if (creditCheck == false)
            {
                Console.WriteLine("You can take only one credit! \n");
                Console.WriteLine("Credit amount: ");
                double Creditamount = Convert.ToDouble(Console.ReadLine());
                DepositHistory.Add(Creditamount);

                DateTime dateTime = DateTime.Now;
                DateDepositHistory.Add(dateTime);

                credit = Creditamount;
                this.balance = this.balance + Creditamount;

                if (Creditamount > 0)
                {
                    creditCheck = true;
                }
            }
            else
            {
                Console.WriteLine("You can't take another credit. The bank only allows take 1 credit.");
            }

            return 0;
        }
        public double Creditpayment()
        {
            if (credit > 0)
            {
                Console.WriteLine("The credit taken:{0}", credit);
                Console.WriteLine("Enter credit repay amount:");
                double DepositAmt = Convert.ToDouble(Console.ReadLine());

                if (balance >= DepositAmt)
                {
                    DepositHistory.Add(DepositAmt * -1);

                    DateTime dateTime = DateTime.Now;
                    DateDepositHistory.Add(dateTime);

                    credit = credit - DepositAmt;
                    this.balance = this.balance - DepositAmt;
                    return credit;
                }
                else
                    Console.WriteLine("Not enough founds!");
            }
            else
                Console.WriteLine("You don't have any credit.");
            return 0;
        }

        public void SetRecipientName()
        {
            Console.Write("\nEnter recipient's name: ");
            this.userNameToTransfer = Console.ReadLine();
        }
        public void TransferToOtherUser()
        {       
            Console.Write("Enter Amount: ");
            this.transferAmount = Convert.ToDouble(Console.ReadLine());

            if (this.transferAmount <= 0)
            {
                Console.Write("You can't send 0 or less value!");
                return;
            }
            else if (this.transferAmount > this.balance)
            {
                Console.Write("Not enough founds!");
                return;
            }
            else
            {
                this.balance = this.balance - this.transferAmount;

                DepositHistory.Add(transferAmount * -1);
                DateTime dateTime = DateTime.Now;
                DateDepositHistory.Add(dateTime);
                transferAction = true;
            }
        }

        public override string ToString()
        {
            return " ";
        }
    }
    class Bank : Account
    {
        //Pola klasy
        public bool accountBlock = false;
        public Bank(double balance, string setName, string setPassword, bool accountBlock, double credit, bool creditCheck) : base(balance, setName, setPassword, credit, creditCheck)
        {
            this.accountBlock = accountBlock;
        }

        public override string ToString()
        {
            return ". Name: " + setName + ", Password: "+ setPassword + ", Balance: " + balance + ", Account Block(true=blocked): " + accountBlock + ", Credit(true=the credit has been taken): " + creditCheck;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Bank> bank = new List<Bank>();

            string adminName = "admin", adminPassword = "123";
            bool menuquit = true;
            bool adminMenuquit = true;
            bool loginMenu = true;

            do
            {
                Console.WriteLine("Press 1. To create account. \nPress 2. To log in as user. \nPress 3. To log in as admin. \nPress 4. To quit.\n");
                Console.Write("Select function: ");
                int menuChoose = Convert.ToInt32(Console.ReadLine());

                switch (menuChoose)
                {
                    case 1:
                        string setName;
                        bool nameDuplicatCheck = false;

                        do
                        {
                            nameDuplicatCheck = false;
                            Console.Write("\nEnter name: ");
                            setName = Console.ReadLine();
                            foreach (Bank banks in bank)
                            {
                                if (banks.setName == setName)
                                {
                                    Console.WriteLine("Name already exist - enter other name!");
                                    nameDuplicatCheck = true;
                                    break;
                                }
                            }
                        } while (nameDuplicatCheck == true);
                        
                        //Console.Write("\nEnter name: ");
                        //string setName = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string setPassword = Console.ReadLine();
                        double balanceSettoZero = 0;
                        double CreditSettoZero = 0;
                        bank.Add(new Bank(balanceSettoZero, setName, setPassword, false, CreditSettoZero, false));
                        Console.WriteLine("\nBank Account Added!\n");

                        break;

                    case 2:                        
                        Console.Write("\nEnter name: ");
                        string nameCheck = Console.ReadLine();
                           
                        
                        Console.Write("Enter password: ");
                        string passowrdCheck = Console.ReadLine();

                        bool accountChecker = true;


                        if (bank.Count == 0)
                        {
                            Console.WriteLine("Bank don't have any accounts");
                        }

                        for (int ix = 0; ix < bank.Count; ix++)
                        {                           
                            if ((bank[ix].setName == nameCheck) && (bank[ix].setPassword == passowrdCheck) && (bank[ix].accountBlock == false))
                            {
                                bank[ix].userIsLogged = true;

                                accountChecker = false;

                                Console.WriteLine("Account Found!\nName: {0}\nBalance: {1}\n", bank[ix].setName, bank[ix].balance);
                                loginMenu = true;
                                do
                                {
                                    for (ix = 0; ix < bank.Count; ix++)
                                    {
                                        if ((bank[ix].setName == nameCheck) && (bank[ix].setPassword == passowrdCheck))
                                        {
                                            Console.WriteLine("\nName: {0}\nBalance: {1}\n", bank[ix].setName, bank[ix].balance);
                                        }
                                    }

                                    Console.WriteLine("Press 1. To deposit: \nPress 2. To withdraw: \nPress 3. To check balance.\n" +
                                                "Press 4. To check transaction history.\nPress 5. To make transfer to other user \nPress 6. To take credit.\nPress 7. To repay credit\nPress 8. To log out and quit to main menu");
                                    int loginMenuChoose = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("\n");

                                    switch (loginMenuChoose)
                                    {
                                        case 1:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].Deposit();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Account not found!");
                                                }
                                            }
                                            break;

                                        case 2:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].Withdraw();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Insufficient funds!");
                                                }
                                            }
                                            break;

                                        case 3:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    Console.WriteLine("Balance: {0}", bank[accNum].balance);
                                                }

                                            }
                                            break;

                                        case 4:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].History();
                                                }
                                            }
                                            break;

                                        case 5:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    DateTime dateTime = DateTime.Now;

                                                    bool userTransferNameCheck = false;

                                                    do
                                                    {
                                                        bank[accNum].SetRecipientName();                                                        
                                                        
                                                        for (int ixUser = 0; ixUser < bank.Count; ixUser++)
                                                        {
                                                            if (bank[ixUser].setName == bank[accNum].userNameToTransfer)
                                                            {
                                                                userTransferNameCheck = true;
                                                            }
                                                        }
                                                        if(userTransferNameCheck == false)
                                                        {
                                                            Console.WriteLine("Account with that name don't exist! Enter other account name. Enter 'quit' to quit.");
                                                        }
                                                        else if(bank[accNum].setName == bank[accNum].userNameToTransfer)
                                                        {
                                                            Console.WriteLine("You can't transfer money to Yourself! Enter other account name.");
                                                            userTransferNameCheck = false;
                                                        }                                                        
                                                     } while (userTransferNameCheck == false);

                                                    bank[accNum].TransferToOtherUser();                                                       

                                                    if (bank[accNum].transferAction == true)
                                                    {
                                                        int accUserNum = -1;
                                                        for (int ixUser = 0; ixUser < bank.Count; ixUser++)
                                                        {
                                                            if (bank[ixUser].setName == bank[accNum].userNameToTransfer)
                                                            {
                                                                accUserNum = ixUser;
                                                            }
                                                        }
                                                        if (accUserNum != -1)
                                                        {

                                                            bank[accUserNum].balance += bank[accNum].transferAmount;

                                                            DateTime dateTime1 = DateTime.Now;

                                                            bank[accUserNum].DepositHistory.Add(bank[accNum].transferAmount);
                                                            bank[accUserNum].DateDepositHistory.Add(dateTime1);

                                                            bank[accNum].transferAction = false;
                                                        }
                                                    }
                                                }
                                            }
                                            break;

                                        case 6:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].Credit();
                                                }
                                                break;
                                            }

                                        case 7:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].Creditpayment();
                                                }
                                                break;
                                            }
                                        case 8:
                                            {
                                                int accNum = -1;
                                                for (ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == nameCheck)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    bank[accNum].Creditpayment();
                                                }
                                                loginMenu = false;

                                                bank[accNum].userIsLogged = false;
                                            }
                                            break;
                                    }
                                } while (loginMenu == true);
                            }
                            else if ((bank[ix].setName == nameCheck) && (bank[ix].setPassword == passowrdCheck) && (bank[ix].accountBlock == true))
                            {
                                Console.WriteLine("Account is blocked!");
                                accountChecker = false;
                            }
                        }
                        if(accountChecker == true)
                        {
                            Console.WriteLine("Account is not found or name/password is wrong!");
                        }
                        accountChecker = false;
                        break;

                    case 3:
                        Console.Write("Enter Admin name: ");
                        string adminNameCheck = Console.ReadLine();
                        Console.Write("Enter Admin password: ");
                        string adminPasswordCheck = Console.ReadLine();

                        foreach (Bank accounts in bank)
                        {
                            if (accounts.userIsLogged == true)
                            {
                                Console.WriteLine("You are logged as user, you must log out from user to log in as admin");
                                break;
                            }
                        }
                        if (adminNameCheck == adminName && adminPasswordCheck == adminPassword)
                        {
                            adminMenuquit = true;
                            do
                            {
                                Console.WriteLine("\nYou are in Admin Menu!\nPress 1. To lock user account\nPress 2. To unlock user accountn\nPress 3. To print all bank accounts\nPress 4. To quit.");
                                int adminChoose = Convert.ToInt32(Console.ReadLine());

                                switch (adminChoose)
                                {
                                    case 1:
                                        {
                                            if (bank.Count == 0)
                                            {
                                                Console.Write("No accounts to lock.");
                                                break;
                                            }

                                            Console.Write("Lock account(insert account name): ");
                                            string userNameToLock = Console.ReadLine();
                                            if (bank.Count > 0)
                                            {
                                                int accNum = -1;
                                                for (int ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == userNameToLock)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }
                                                if (accNum != -1)
                                                {
                                                    if (bank[accNum].accountBlock == true)
                                                    {
                                                        Console.Write("Account ({0}) - is already locked\n", userNameToLock);
                                                        break;
                                                    }

                                                    bank[accNum].accountBlock = true;
                                                    Console.Write("Account ({0}) - has been locked\n", userNameToLock);
                                                }
                                                else
                                                {
                                                    Console.Write("Account not found.");
                                                }
                                            }
                                        }
                                        break;

                                    case 2:
                                        {
                                            if (bank.Count == 0)
                                            {
                                                Console.Write("No accounts to Unlock.");
                                                break;
                                            }

                                            Console.Write("Unlock account(insert account name): ");
                                            string userNameToUnLock = Console.ReadLine();
                                            if (bank.Count > 0)
                                            {
                                                int accNum = -1;
                                                for (int ix = 0; ix < bank.Count; ix++)
                                                {
                                                    if (bank[ix].setName == userNameToUnLock)
                                                    {
                                                        accNum = ix;
                                                    }
                                                }

                                                if (accNum != -1)
                                                {
                                                    if (bank[accNum].accountBlock == false)
                                                    {
                                                        Console.Write("Account is not blocked.");
                                                        break;
                                                    }
                                                    bank[accNum].accountBlock = false;
                                                    Console.Write("Account ({0}) - has been unlocked\n", userNameToUnLock);
                                                }
                                                else
                                                {
                                                    Console.Write("Account not found.");
                                                }
                                            }
                                        }
                                        break;

                                    case 3:
                                        {
                                            int x = 1;
                                            foreach(Bank banks in bank)
                                            {
                                                Console.Write(x);
                                                x++;
                                                Console.WriteLine(banks.ToString());
                                            }
                                        }

                                        break;

                                    case 4:
                                        {
                                            adminMenuquit = false;
                                        }

                                        break;
                                }
                            } while (adminMenuquit == true);
                        }
                        else
                        {
                            Console.WriteLine("Wrong admin name or password!");
                        }
                        break;

                    case 4:
                        menuquit = false;
                        break;
                }
            } while (menuquit == true);
        }
    }
}
