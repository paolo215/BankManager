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

  


    }
}
