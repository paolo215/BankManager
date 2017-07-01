from Account import Account
from Transaction import Transaction
import datetime

class BankManager(object):
    def __init__(self):
        """
        Instantiates a BankManager which manages creation of accounts and any transactions
        """
        self.account_data = {}
        self.transaction_data = {}
        self.next_account_id = 1
        self.next_transaction_id = 1

    def create_account(self, username, password, first_name, last_name, address):
        """
        Creates a bank account for the user
        :param username: username
        :param password: password
        :param first_name: first name
        :param last_name: last name
        :param address: home address
        :return: Tuple that contains boolean variable that determines if account creation is
         successful or not and a message
        """

        # Check if the username does not exist
        if not username in self.account_data:

            # If the account doesn't exist, then we can create a bank account with this username
            self.account_data[username] = Account(self.next_account_id, username, password, first_name, last_name, address)

            # Increate account id
            self.next_account_id += 1
            return (True, "Account has been successfully created")

        # Username already exists. User must enter a different username
        return (False, "Username already exists. Please enter a different username.")

    def authenticate_user(self, username, password):
        """
        Authenticates user credentials
        :param username: username
        :param password: password
        :return:
        """

        # Check if username doesn't exist
        if not username in self.account_data:
            return (False, "Incorrect credentials. Try again.")


        # Obtain this account information
        account = self.account_data[username]

        # Check if credentials matches what we have
        if account.username == username and account.password == password:
            return (True, "Success!")


        # Reaching this point means that the user enters incorrect credentials.
        return (False, "Incorrect credentials. Try again.")


    def get_user_account(self, username):
        """
        Obtain user account
        :param username: username
        :return:
        """
        if not username in self.account_data:
            return None
        return self.account_data[username]

    def check_account_exists(self, username):
        if not username in self.account_data:
            return False
        return True

    def create_transaction(self, account, amount, status):
        """
        Creates a safe transaction (creates a transaction after validating the
        amount of money withdrawn/added and balance
        :param account: Bank account information
        :param amount: Amount of money being withdrawn/added
        :param status: WITHDRAW or DEPOSIT
        :return:
        """

        # Obtain this account's balance and account id
        balance = account.get_balance()
        account_id = account.get_account_id()

        # Obtain datetime information so that we know when this transaction happened
        datetime_now = datetime.datetime.now()

        # Create Transaction object
        transaction = Transaction(self.next_transaction_id, account_id, balance, amount, status, datetime_now)

        # Record transaction
        self.transaction_data[self.next_transaction_id] = transaction

        # Add transaction to account's history
        account.add_transaction(transaction)

        # Increment next transaction id
        self.next_transaction_id += 1


    def withdraw(self, username, amount):
        """
        Attempts to withdraw money from account
        :param username:
        :param amount:
        :return:
        """

        # Obtain account information and balance
        account = self.account_data[username]
        balance = account.get_balance()

        # Check if there are more money in the account than the amount
        # of money the user is withdraw.  The amount of money should be greater than 0 to
        # avoid unnecessary transactions.
        if balance >= amount and amount > 0:

            # Withdraw and record transaction
            account.withdraw(amount)
            self.create_transaction(account, account.get_balance(), "WITHDRAW")

            # Transaction complete.
            return (True, "Success! Your balance is now: %s" % str(account.get_balance()))

        return (False, "Insufficient amount of funds. Your balance is currently %s" % str(balance))


    def deposit(self, username, amount):
        """
        Attempts to deposit money to the account
        :param username:
        :param amount:
        :return:
        """

        # Obtain account information and balance
        account = self.account_data[username]
        balance = account.get_balance()

        # Check if the amount of money being added is greater than 0.
        if amount > 0:
            account.deposit(amount)
            self.create_transaction(account, account.get_balance(), "DEPOSIT")
            return (True, "Success! Your balance is now %s" % str(balance))

        return (False, "Invalid amount of funds. Try again.")


