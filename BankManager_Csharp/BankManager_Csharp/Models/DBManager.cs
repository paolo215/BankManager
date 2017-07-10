using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class DBManager
    {
        private BankManagerDBEntities db;

        public DBManager()
        {
            db = new BankManagerDBEntities();
        }

        public void addAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void addTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public void updateFunds(Account account)
        {
            db.Accounts.Attach(account);
            db.Entry(account).Property(u => u.balance).IsModified = true;
            db.SaveChanges();
        }

        public Account getAccount(String username)
        {
            return db.Accounts.Where(u => u.username.Equals(username)).First();
        }

        public Transaction getTransaction(int transactionId)
        {
            return db.Transactions.Where(u => u.transactionId == transactionId).First();
        }

        public bool checkAccountExists(String username)
        {
            return db.Accounts.Any(u => u.username.Equals(username));
        }

        public List<Transaction> getTransactions(int accountId)
        {
            return db.Transactions.Where(u => u.accountId == accountId).ToList();
        }
    }
}