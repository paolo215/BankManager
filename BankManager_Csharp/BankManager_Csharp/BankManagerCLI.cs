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


        /// <summary>
        /// Instantiates command line interface for BankManager
        /// </summary>
        public BankManagerCLI()
        {
            bankManager = new BankManager();
        }


        /// <summary>
        /// Run command line interface
        /// </summary>
        public void run()
        {

            // Show print menu and asks user what to do (Log in, register, or exit the application)
            int answer = mainMenu();
            String username = null;


            // User wants to log in
            if(answer == 1)
            {
                // Prompts the user for username and password
                username = promptString("Enter your username: ");
                String password = promptString("Enter your password: ");


                // Authenticates username and password.
                Response response = bankManager.authenticateUser(username, password);


                // Display response from BankManager
                Console.WriteLine(response.message);

                // If log in is unsuccessful, return to main menu
                if (response.isSuccessful == false)
                {
                    run();
                }

            }

            // User wants to create an account
            else if (answer == 2)
            {
                
                username = promptString("Enter your username: ");

                // Keep asking the user to enter username until we found one that doesn't have account information.
                while (bankManager.checkAccountExists(username))
                {
                    Console.WriteLine("Username already exists. Please choose a different username.");
                    username = promptString("Enter your username: ");
                }

                // Prompts user for password, first name, last name, and address
                String password = promptString("Enter your password: ");
                String firstName = promptString("Enter your first name: ");
                String lastName = promptString("Enter your last name: ");
                String address = promptString("Enter your address: ");


                // Attempts to create an account
                AccountResponse accountResponse = bankManager.createAccount(username, password, firstName, lastName, address);


                // Display response from BankManager
                Console.WriteLine(accountResponse.response.message);

                // Return to main menu if account creation not sucessful
                if (accountResponse.response.isSuccessful == false)
                {
                    run();
                }

            }
            // User wants to exit
            else if(answer == 3)
            {
                Console.WriteLine("Have a great day! :)");
                return;
            }


            // User successfully logs in or creates an account. We then redirect the user to the dashboard.
            dashboard(username);
        }

        /// <summary>
        /// Displays user information and asks the user to deposit, withdraw, show transaction history, or log out.
        /// </summary>
        /// <param name="username"></param>
        public void dashboard(String username)
        {
            // Obtain user's account info
            Account account = bankManager.getUserAccount(username);

            // Show user's information
            Console.WriteLine("===== Dashboard ====");
            Console.WriteLine("Username: " + account.username);
            Console.WriteLine("First name: " + account.firstName);
            Console.WriteLine("Last name: " + account.lastName);
            Console.WriteLine("Address: " + account.address);
            Console.WriteLine("Balance: " + account.balance);


            // Asks the user what to do (Deposit, withdraw, show transaction history, or exit the application).
            int answer = dashboardMenu();

            // User wants to deposit or withdraw
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

            // User wants to show transaction history
            else if(answer == 3)
            {
                List<Transaction> history = account.history;

                foreach(Transaction transaction in history) {
                    Console.WriteLine(transaction.prettyPrint());
                }
            }
            // User wants to exit the application.
            else if(answer == 4)
            {
                Console.WriteLine("Logging out. Have a nice day! :)");
                return;
            }


            // Goes back to the dashboard 
            dashboard(username);
        }


        /// <summary>
        /// Prompts the user to withdraw, deposit, show transaction history, or exit the application
        /// </summary>
        /// <returns>int</returns>
        public int dashboardMenu()
        {
            int answer = -1;
            // Keep looping until we get a valid response
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

        /// <summary>
        /// Prompts the user to log in, register, or exit the application
        /// </summary>
        /// <returns>int</returns>
        public int mainMenu()
        {
            int answer = -1;
            // Keep looping until we get a valid input
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

        /// <summary>
        /// Prompts the user for a number
        /// </summary>
        /// <param name="message"></param>
        /// <returns>int</returns>
        public int promptNumber(String message)
        {
            // Keep looping until we receive a number
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

        /// <summary>
        /// Prompts user for an input
        /// </summary>
        /// <param name="message"></param>
        /// <returns>String</returns>
        public String promptString(String message)
        {
            String answer = null;
            // Keep looping until the user enters something
            do
            {
                Console.Write(message);
                answer = Console.ReadLine();
            } while (answer == String.Empty);

            return answer;
        }



    }
}
