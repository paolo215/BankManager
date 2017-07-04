using System;
using System.Collections.Generic;
using System.Text;
using BankManager_Csharp.Models;

namespace BankManager_Csharp_CLI
{

    class RunBankManagerCLI
    {
        public static void Main(String [] args)
        {
            new BankManagerCLI().run();
        }
    }

    class BankManagerCLI
    {
        private BankManager bankManager;

        public BankManagerCLI()
        {
            bankManager = new BankManager();
        }

        public void run()
        {
            int answer = mainMenu();
            String username = null;

            if(answer == 1)
            {
                username = promptString("Enter your username: ");
                String password = promptString("Enter your password: ");

                Response response = bankManager.authenticateUser(username, password);

                Console.WriteLine(response.message);
                if (response.isSuccessful == false)
                {
                    run();
                }

            }
            else if (answer == 2)
            {
                username = promptString("Enter your username: ");
                while (bankManager.checkAccountExists(username))
                {
                    Console.WriteLine("Username already exists. Please choose a different username.");
                    username = promptString("Enter your username: ");
                }

                String password = promptString("Enter your password: ");
                String firstName = promptString("Enter your first name: ");
                String lastName = promptString("Enter your last name: ");
                String address = promptString("Enter your address: ");


                AccountResponse accountResponse = bankManager.createAccount(username, password, firstName, lastName, address);

                Console.WriteLine(accountResponse.response.message);
                if (accountResponse.response.isSuccessful == false)
                {
                    run();
                }

            } else if(answer ==3)
            {
                Console.WriteLine("Have a great day! :)");
                return;
            }


            dashboard(username);
        }

        public void dashboard(String username)
        {
            Account account = bankManager.getUserAccount(username);

            Console.WriteLine("===== Dashboard ====");
            Console.WriteLine("Username: " + account.username);
            Console.WriteLine("First name: " + account.firstName);
            Console.WriteLine("Last name: " + account.lastName);
            Console.WriteLine("Address: " + account.address);
            Console.WriteLine("Balance: " + account.balance);

            int answer = dashboardMenu();

            if(answer == 1 || answer == 2)
            {
                if(answer == 1)
                {
                    Console.WriteLine("=== Withdraw ===");
                }
                else if(answer == 2)
                {
                    Console.WriteLine("=== Deposit ===");
                }

                int amount = promptNumber("Enter a number: ");

                Response response = null;

                if(answer == 1)
                {
                    response = bankManager.withdraw(account, amount);
                }
                else if(answer == 2)
                {
                    response = bankManager.deposit(account, amount);
                }

                Console.WriteLine(response.message);
            }
            else if(answer == 3)
            {
                List<Transaction> history = account.history;

                foreach(Transaction transaction in history) {
                    Console.WriteLine(transaction.prettyPrint());
                }
            }
            else if(answer == 4)
            {
                Console.WriteLine("Logging out. Have a nice day! :)");
                return;
            }

            dashboard(username);
        }

        public int dashboardMenu()
        {
            int answer = -1;
            while (answer <= 0 || answer > 4)
            {
                Console.WriteLine("Choose what to do:\n"
                    + "1.) Withdraw\n"
                    + "2.) Deposit\n"
                    + "3.) See history\n"
                    + "4.) Exit");


                answer = promptNumber("Enter a number: ");

            }
            return answer;
        }

        public int mainMenu()
        {
            int answer = -1;
            while (answer <= 0 || answer > 3)
            {
                Console.WriteLine("Choose what to do:\n"
                    + "1.) Log in\n"
                    + "2.) Register\n"
                    + "3.) Exit\n");


                answer = promptNumber("Enter a number: ");
                
            }
            return answer;
        }

        public int promptNumber(String message)
        {
            while (true)
            {
                try
                {
                    Console.Write(message);
                    var answer = Console.ReadLine();

                    return int.Parse(answer);
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            
        }

        public String promptString(String message)
        {
            String answer = null;
            do
            {
                Console.Write(message);
                answer = Console.ReadLine();
            } while (answer == String.Empty);

            return answer;
        }



    }
}
