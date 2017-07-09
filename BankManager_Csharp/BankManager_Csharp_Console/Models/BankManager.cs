using System;
using System.Collections.Generic;

namespace BankManager_Csharp_Console.Models
{
    public class BankManager
    {
        /// <summary>
        /// Used to store bank accounts.
        /// Key = Username (String)
        /// Value = Bank account (Account)
        /// </summary>
        public Dictionary<String, Account> accountData;

        /// <summary>
        /// Used to store completed transactions.
        /// Key = Transaction id (int)
        /// Value = Transaction information (Transaction)
        /// </summary>
        public Dictionary<int, Transaction> transactionData;


        /// <summary>
        /// Instantiates BankManager. This handles account creation, showing transaction history of the user, deposits, and withdraws.
        /// </summary>
        public BankManager()
        {
            accountData = new Dictionary<String, Account>();
            transactionData = new Dictionary<int, Transaction>();
        }

        /// <summary>
        /// Attempts to create an account for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <returns>AccountResponse</returns>
        public AccountResponse createAccount(String username, String password, 
            String firstName, String lastName, String address)
        {

            Account account = null;

            // Check if username exists in accountData
            if (checkAccountExists(username) == false)
            {
                // Creates an account for the user, since we didn't find an existing account with that username.
                int accountId = accountData.Count + 1;
                account = new Account(accountId, username, password, firstName, lastName, address);
                accountData[username] = account;
                

                return new AccountResponse(account, new Response(true, "Success! A new account has been created."));

            }

            // Username already exists. 
            account = new Account(-1, username, null, firstName, lastName, address);
            return new AccountResponse(account, new Response(false, "Username already exists. Try again."));
        }

        /// <summary>
        /// Authenticates username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Response</returns>
        public Response authenticateUser(String username, String password)
        {
            // Check if username exists
            if(checkAccountExists(username) == false)
            {
                return new Response(false, "Incorrect username or password. Try again.");
            }

            Account account = accountData[username];
            // Check if the user input password and stored password are the same
            if(account.password.Equals(password))
            {
                return new Response(true, "Success!");
            }

            return new Response(false, "Incorrect username or password. Try again.");
        }

        public Account getUserAccount(String username)
        {
            // Check if username exists. 
            // Returns null if username not found. Otherwise, return the account
            if(checkAccountExists(username) == false)
            {
                return null;
            }
            return accountData[username];
        }

        /// <summary>
        /// Checks if username already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        public bool checkAccountExists(String username)
        {
            if(accountData.ContainsKey(username) == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates a transaction and store transaction information in user's account history.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Transaction createTransaction(Account account, int amount, String status)
        {
            float balance = account.balance;
            int accountId = account.accountId;
            DateTime date = DateTime.Now;

            int transactionId = transactionData.Count + 1;
            // Creates transaction
            Transaction transaction = new Transaction(transactionId,
                accountId, balance, amount, status, date);


            // Record transaction
            transactionData[transactionId] = transaction;
            account.addTransaction(transaction);

            return transaction;
        }

        /// <summary>
        /// Attempts to withdraw funds from user's account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <returns>Response</returns>
        public Response withdraw(Account account, int amount)
        {
            float balance = account.balance;

            // Validates amount of funds the user wants to withdraw.
            // Rules: 
            //    Balance should be greater than or equal to amount of funds the user wants to withdraw.
            //    User should not be able to withdraw 0 funds.
            if (balance >= amount && amount > 0)
            {
                // Withdraws money from the account and records transaction.
                account.withdraw(amount);
                createTransaction(account, amount, "WITHDRAW");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Insufficient amount of founds. Your balance is currently " + account.balance);
        }

        /// <summary>
        /// Attempts to deposit funds to user's bank account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <returns>Response</returns>
        public Response deposit(Account account, int amount)
        {
            float balance = account.balance;
            // Validates the amount of money the user wants to deposit
            // Rule:
            //    User should not be able to depist 0 funds.
            if (amount > 0)
            {
                // Deposits funds to user's account and records transaction.
                account.deposit(amount);
                createTransaction(account, amount, "DEPOSIT");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Invalid amount of funds. Try again.");
        }

    }
}