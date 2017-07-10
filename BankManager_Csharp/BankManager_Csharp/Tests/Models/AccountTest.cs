using System;
using NUnit.Framework;
using BankManager_Csharp.Models;
using System.Collections.Generic;

namespace BankManager_Csharp.Tests.Models
{
    [TestFixture]
    public class AccountTest
    {
        public Account account;


        [SetUp]
        public void init()
        {
            account = new Account("admin", "password", "Paolo", "Villanueva", "My address");
        }

        [Test]
        public void withdrawingMoney()
        {
            account.balance = 100;
            account.withdraw(100);

            Assert.AreEqual(0, account.balance);
        }

        [Test]
        public void depositMoney()
        {
            account.balance = 0;
            account.deposit(100);

            Assert.AreEqual(100, account.balance);
        }

        [Test]
        public void addTransaction()
        {
            DateTime now = DateTime.Now;

            Transaction transaction = new Transaction(account.accountId, 100, 100, "WITHDRAW", now);

            account.addTransaction(transaction);

            Transaction addedTransaction = account.history[0];

            Assert.IsTrue(addedTransaction.transactionId == 1);
            Assert.IsTrue(addedTransaction.accountId == 1);
            Assert.IsTrue(addedTransaction.balance == 100);
            Assert.IsTrue(addedTransaction.amount == 100);
            Assert.IsTrue(addedTransaction.status.Equals("WITHDRAW"));
            Assert.IsTrue(addedTransaction.date.Equals(now));

        }

        [Test]
        public void addTwoTransactions()
        {
            DateTime now = DateTime.Now;

            Transaction transaction1 = new Transaction(account.accountId, 100, 100, "WITHDRAW", now);

            account.addTransaction(transaction1);

            Transaction addedTransaction1 = account.history[0];

            Assert.IsTrue(addedTransaction1.transactionId == 1);
            Assert.IsTrue(addedTransaction1.accountId == 1);
            Assert.IsTrue(addedTransaction1.balance == 100);
            Assert.IsTrue(addedTransaction1.amount == 100);
            Assert.IsTrue(addedTransaction1.status.Equals("WITHDRAW"));
            Assert.IsTrue(addedTransaction1.date.Equals(now));


            Transaction transaction2 = new Transaction(account.accountId, 100, 100, "DEPOSIT", now);
            account.addTransaction(transaction2);

            Transaction addedTransaction2 = account.history[1];
            Assert.IsTrue(addedTransaction2.transactionId == 2);
            Assert.IsTrue(addedTransaction2.accountId == 1);
            Assert.IsTrue(addedTransaction2.balance == 100);
            Assert.IsTrue(addedTransaction2.amount == 100);
            Assert.IsTrue(addedTransaction2.status.Equals("DEPOSIT"));
            Assert.IsTrue(addedTransaction2.date.Equals(now));


        }

    }
}
