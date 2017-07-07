using BankManager_Csharp_Console.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankManager_Csharp_Console
{
    public class BankManagerCLI
    {
        public HttpClient client;

        public BankManagerCLI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080");
        }

        public void run()
        {
            int answer = mainMenu();
            String username = null;


            // User wants to log in
            if (answer == 1)
            {
                // Prompts the user for username and password
                username = promptString("Enter your username: ");
                String password = promptString("Enter your password: ");

                FormUrlEncodedContent loginForm = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("username", username),
                    new KeyValuePair<String, String>("password", password),
                });

                Task<String> response = HttpUtility.Post(client, "/Login/authorizeAPI", loginForm);

                
            }

            // User wants to create an account
            else if (answer == 2)
            {

                username = promptString("Enter your username: ");
  
                // Prompts user for password, first name, last name, and address
                String password = promptString("Enter your password: ");
                String firstName = promptString("Enter your first name: ");
                String lastName = promptString("Enter your last name: ");
                String address = promptString("Enter your address: ");


                FormUrlEncodedContent registrationForm = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("username", username),
                    new KeyValuePair<String, String>("password", password),
                    new KeyValuePair<String, String>("firstName", firstName),
                    new KeyValuePair<String, String>("lastName", lastName),
                    new KeyValuePair<String, String>("address", address),
                });

                Task<String> callback = HttpUtility.Post(client, "/Register/createAPI", registrationForm);

                AccountResponse accountResponse = JsonConvert.DeserializeObject<AccountResponse>(callback.Result);


                // Display response from BankManager
                Console.WriteLine(accountResponse.response.message);

                // Return to main menu if account creation not sucessful
                if (accountResponse.response.isSuccessful == false)
                {
                    run();
                }

            }

            // User wants to exit
            else if (answer == 3)
            {
                Console.WriteLine("Have a great day! :)");
                return;
            }

            dashboard(username);
        }

        /// <summary>
        /// Displays user information and asks the user to deposit, withdraw, show transaction history, or log out.
        /// </summary>
        /// <param name="username"></param>
        public void dashboard(String username)
        {

            String args = "?username=" + username;
            Task<String> callback = HttpUtility.Get(client, "/Dashboard/getAccountInfoAPI" + args);

            Console.WriteLine(callback.Result);
            AccountResponse accountResponse = JsonConvert.DeserializeObject<AccountResponse>(callback.Result);
            Account account = accountResponse.account;

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
            if (answer == 1 || answer == 2)
            {
                String option = "";
                if (answer == 1)
                {
                    Console.WriteLine("=== Withdraw ===");
                    option = "Withdraw";
                }
                else if (answer == 2)
                {
                    Console.WriteLine("=== Deposit ===");
                    option = "Deposit";
                }

                int amount = promptNumber("Enter a number: ");

                FormUrlEncodedContent transactionForm = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("username", username),
                    new KeyValuePair<String, String>("amount", amount.ToString()),
                    new KeyValuePair<String, String>("option", option)
                });
                callback = HttpUtility.Post(client, "/Dashboard/makeTransactionAPI", transactionForm);

                accountResponse = JsonConvert.DeserializeObject<AccountResponse>(callback.Result);

                Console.WriteLine(accountResponse.response.message);
            }

            // User wants to show transaction history
            else if (answer == 3)
            {
                List<Transaction> history = account.history;

                foreach (Transaction transaction in history)
                {
                    Console.WriteLine(transaction.prettyPrint());
                }
            }
            // User wants to exit the application.
            else if (answer == 4)
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
