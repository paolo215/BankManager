
class Account(object):
    def __init__(self, account_id, username, password, first_name, last_name, address):
        """
        Creates a bank account for the user

        :param account_id: Account id of the user. Generated by BankManager
        :param username:  User's username
        :param password:  User's password
        :param first_name: User's first name
        :param last_name: User's last name
        :param address:  User's home address
        """
        self.username = username
        self.account_id = account_id
        self.password = password
        self.first_name = first_name
        self.last_name = last_name
        self.address = address

        # Overall balance in the user's bank account
        self.balance = 0

        # User's transaction history
        self.history = []


    def get_username(self):
        """
        Get username for this account
        :return:
        """
        return self.username

    def get_password(self):
        """
        Get password for this account
        :return:
        """
        return self.password

    def withdraw(self, amount):
        """
        Withdraw money from this account
        :param amount: Amount of value you want to withdraw
        :return: overall valance
        """
        self.balance -= amount
        return self.get_balance()


    def deposit(self, amount):
        """
        Deposits money from this account.
        :param amount: Amount of value you want to deposit
        :return: overall balance
        """
        self.balance += amount
        return self.get_balance()

    def get_account_id(self):
        """
        Get id for this account
        :return: account id
        """
        return self.account_id

    def add_transaction(self, transaction):
        """
        Add transaction to history
        :param transaction:
        :return:
        """
        self.history.append(transaction)

    def get_balance(self):
        """
        Get current balance of this account
        :return: balance
        """
        return self.balance

    def get_data(self):
        """
        Get necessary data for the dashboard
        :return:
        """
        return {
                "username" : self.username,
                "first_name": self.first_name,
                "last_name" : self.last_name,
                "address": self.address,
                "balance": self.balance,
                "history": self.history
                }
