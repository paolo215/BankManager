using System;
using NUnit.Framework;
using BankManager_Csharp.Models;

namespace BankManager_Csharp_Test.Models
{
    [TestFixture]
    public class BankManagerTest
    {
        public BankManager bankManager;


        [SetUp]
        public void init()
        {
            bankManager = new BankManager();
        }
        [Test]
        public void createAnAccount()
        {
            AccountResponse accountResponse = bankManager.createAccount("Person1", "password", "first", "person", "address");
            Console.WriteLine(accountResponse.response.message);

            Response response = accountResponse.response;
            Account account = accountResponse.account;

            Assert.IsTrue(response.isSuccessful);
            Assert.IsTrue(bankManager.accountData.ContainsKey("Person1"));
            Assert.IsTrue(account.accountId == 1);


        }

        [Test]
        public void createTwoAccounts()
        {
            AccountResponse accountResponse = bankManager.createAccount("Person1", "password", "first", "person", "address");
            accountResponse = bankManager.createAccount("Person2", "password", "first", "person", "address");
            Response response = accountResponse.response;
            Account account = accountResponse.account;

            Assert.IsTrue(response.isSuccessful);
            Assert.IsTrue(bankManager.accountData.ContainsKey("Person2"));
            Assert.IsTrue(account.accountId == 2);
        }

        [Test]
        public void authenticateExistingUserButWithIncorrectPassword()
        {
            AccountResponse accountResponse = bankManager.createAccount("Person1", "password", "first", "person", "address");
            Response response = bankManager.authenticateUser("Person1", "a");
            Assert.IsFalse(response.isSuccessful);

        }

        [Test]
        public void authenticateNonExistingUser()
        {
            Response response = bankManager.authenticateUser("Tester", "password");
            Assert.IsFalse(response.isSuccessful);
        }

        [Test]
        public void authenticateExistingUserWithCorrectPassword()
        {
            AccountResponse accountResponse = bankManager.createAccount("Person1", "password", "first", "person", "address");
            Response response = bankManager.authenticateUser("Person1", "password");
            Assert.IsTrue(response.isSuccessful);
        }

        [Test]
        public void createTransactions()
        {
            AccountResponse accountResponse = bankManager.createAccount("Person1", "password", "first", "person", "address");
            Account account = bankManager.getUserAccount("Person1");

            bankManager.deposit(account, 100);
            Transaction addedTransaction = account.history[0];

            Assert.IsTrue(addedTransaction.transactionId == 1);
            Assert.IsTrue(addedTransaction.status.Equals("DEPOSIT"));

            bankManager.withdraw(account, 100);
            addedTransaction = account.history[1];

            Assert.IsTrue(addedTransaction.transactionId == 2);
            Assert.IsTrue(addedTransaction.status.Equals("WITHDRAW"));

        }

    }
}
