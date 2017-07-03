using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class BankManager
    {
        public Dictionary<String, Account> accountData;
        public Dictionary<int, Transaction> transactionData;

        public BankManager()
        {
            accountData = new Dictionary<String, Account>();
            transactionData = new Dictionary<int, Transaction>();
        }

        public AccountResponse createAccount(String username, String password, 
            String firstName, String lastName, String address)
        {
            int accountId = accountData.Count + 1;
            Account account = new Account(accountId, username, password, firstName, lastName, address);

            if (accountData.ContainsKey(username) == false)
            {
                accountData[username] = account;


                return new AccountResponse(account, new Response(true, "Success! A new account has been created."));

            }
            return new AccountResponse(account, new Response(false, "Username already exists. Try again."));
        }

        public Response authenticateUser(String username, String password)
        {
            if(accountData.ContainsKey(username) == false)
            {
                return new Response(false, "Incorrect credentials. Try again.");
            }

            Account account = accountData[username];

            if(account.password.Equals(password))
            {
                return new Response(true, "Success!");
            }

            return new Response(false, "Incorrect credentials. Try again.");
        }

        public Account getUserAccount(String username)
        {
            if(accountData.ContainsKey(username) == false)
            {
                return null;
            }
            return accountData[username];
        }


        public Transaction createTransaction(Account account, int amount, String status)
        {
            int balance = account.balance;
            int accountId = account.accountId;
            DateTime date = DateTime.Now;

            int transactionId = transactionData.Count + 1;
            Transaction transaction = new Transaction(transactionId,
                accountId, balance, amount, status, date);

            transactionData[transactionId] = transaction;
            account.addTransaction(transaction);

            return transaction;
        }

        public Response withdraw(Account account, int amount)
        {
            int balance = account.balance;

            if (balance >= amount && amount > 0)
            {
                account.withdraw(amount);
                createTransaction(account, amount, "WITHDRAW");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Insufficient amount of founds. Your balance is currently " + account.balance);
        }


        public Response deposit(Account account, int amount)
        {
            int balance = account.balance;

            if (amount > 0)
            {
                account.deposit(amount);
                createTransaction(account, amount, "DEPOSIT");

                return new Response(true, "Success! Your balance is now: " + account.balance);
            }

            return new Response(false, "Invalid amount of funds. Try again.");
        }

    }
}