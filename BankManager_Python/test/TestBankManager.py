import unittest
from Account import Account
from Transaction import Transaction
from BankManager import BankManager
import datetime

class TestBankManager(unittest.TestCase):
    def setUp(self):
        self.bank_manager = BankManager()

    def test_creating_an_account(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        is_successful = response[0]
        
        self.assertTrue(is_successful)
        does_exists = self.bank_manager.check_account_exists("admin") 
        self.assertTrue(does_exists)
        self.assertTrue(self.bank_manager.next_account_id == 2)


    def test_creating_an_account_that_already_exists(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        is_successful = response[0]
        
        self.assertFalse(is_successful)
        

    def test_user_enters_wrong_password(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        response = self.bank_manager.authenticate_user("admin", "entering incorrect password")

        is_successful = response[0]
        self.assertFalse(is_successful)

    def test_user_enters_username_that_does_not_exists(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        response = self.bank_manager.authenticate_user("test", "password")
         
        is_successful = response[0]
        self.assertFalse(is_successful)
    

    def test_user_enters_correct_credentials(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        response = self.bank_manager.authenticate_user("admin", "password")
         
        is_successful = response[0]
        self.assertTrue(is_successful)
        

    def test_create_transaction_deposit_and_withdraw(self):
        response = self.bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My Address")
        account = self.bank_manager.get_user_account("admin")
        self.bank_manager.deposit("admin", 100)
        
        added_transaction = account.history[0]
        self.assertTrue(added_transaction.account_id == account.account_id)
        self.assertTrue(added_transaction.balance == 100)
        self.assertTrue(added_transaction.amount == 100)
        self.assertTrue(added_transaction.status == "DEPOSIT")
        

        self.bank_manager.withdraw("admin", 100)
        added_transaction = account.history[1]
        self.assertTrue(added_transaction.account_id == account.account_id)
        self.assertTrue(added_transaction.balance == 0)
        self.assertTrue(added_transaction.amount == 100)
        self.assertTrue(added_transaction.status == "WITHDRAW")
 


if "__main__" == __name__:
    unittest.main()

