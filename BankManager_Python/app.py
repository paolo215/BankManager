from flask import Flask
from flask import render_template
from flask import redirect
from flask import url_for
from flask import request
from flask import session
from argparse import ArgumentParser
from BankManager import BankManager
from gevent.wsgi import WSGIServer

# Instantiate Flask
app = Flask(__name__)
app.secret_key = "secret key!"

# Instantiate BankManager
bank_manager = BankManager()
bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My address")



@app.route("/")
def index():
    """
    Renders index.html when the user accesses main website or index.html
    :return: Renders index.html
    """
    return render_template("index.html")


@app.route("/login", methods=["GET", "POST"])
def login():
    """
    Renders the login page so the user can access their bank account
    :return: Renders login.html or redirect user to dashboard if user has been authenticated
    """

    error = None

    # Check if user has been authenticated. If so, redirect user to the dashboard
    if session.get("is_authenticated"):
        return redirect(url_for("dashboard"))

    try:
        # Obtain username and password when the user clicks the log in button
        if request.method == "POST":
            username = request.form["username"]
            password = request.form["password"]

            # Authenticate the user credentials
            response = bank_manager.authenticate_user(username, password)
            is_successful = response[0]
            error = response[1]

            # If username and password does not match, go back and post the error
            if is_successful == False:
                return render_template("login.html", error=error)

            # Store relevant information to use when user is in the dashboard
            # session allows to store information by using cookies.
            # It uses secret key above to encrypt it
            session["is_authenticated"] = True
            session["username"] = username

            # Redirect to the dashboard if login is successful
            return redirect(url_for("dashboard"))

    except Exception as e:
        error = e

    # Go back to login page if error found
    return render_template("login.html", error=error)

@app.route("/register", methods=["GET", "POST"])
def register():
    """
    Renders the registration page so the user can create an account
    :return: Renders register.html or redirect user to the dashboard
    """
    error = None

    # Check if user has been authenticated. If so, redirect user to the dashboard
    if session.get("is_authenticated"):
        return redirect(url_for("dashboard"))

    try:
        if request.method == "POST":
            username = request.form["username"]
            password = request.form["password"]
            first_name = request.form["first_name"]
            last_name = request.form["last_name"]
            address = request.form["address"]

            response = bank_manager.create_account(username, password, first_name, last_name, address)

            is_successful = response[0]
            message = response[1]

            if is_successful:
                session["is_authenticated"] = True
                session["username"] = username
                return redirect(url_for("dashboard"))
            else:
                return render_template("register.html", message=message)
    except Exception as e:
        error = e

    return render_template("register.html", error=error)

@app.route("/dashboard", methods=["GET", "POST"])
def dashboard():
    """
    Renders the dashboard. This is where the user can see their profile and be able to withdraw
    or deposit funds to their account.
    :return:
    """
    error = None
    username = session.get("username")

    # Obtain account information
    account = bank_manager.get_user_account(username)
    if account == None or session["is_authenticated"] == False:
        return redirect(url_for("index"))

    try:
        # This is true when user clicks "Complete transaction" button
        if request.method == "POST":
            # Obtain option (withdraw or deposit) and amount
            option = request.form["option"]
            amount = int(request.form["amount"])
            response = None

            if option == "Withdraw":
                # Attempts to withdraw money from the user's account
                response = bank_manager.withdraw(username, amount)
            elif option == "Deposit":
                # Attempts to deposit money to the user's account
                response = bank_manager.deposit(username, amount)

            # Obtain output message
            message = response[1]
            return render_template("dashboard.html", user_data=account.get_data(), message=message)
    except Exception as e:
        error = e

    return render_template("dashboard.html", user_data=account.get_data(), message=error)


@app.route("/logout")
def logout():
    session["is_authenticated"] = False
    session["username"] = None
    return index()


# ======== Functions for command line interface ======

def dashboard_cli(username):
    """
    Main menu for command line interface dashboard
    :param username:
    :return:
    """

    # Obtain user info
    account = bank_manager.get_user_account(username)
    account_info = account.get_data()

    # Show user info
    print("==== Dashboard ====")
    print("Username: " + account_info["username"])
    print("First name: " + account_info["first_name"])
    print("Last name: " + account_info["last_name"])
    print("Address: " + account_info["address"])
    print("Balance: " + str(account_info["balance"]))

    # Show menu and let user decide what to do (withdraw, deposit, or exit)
    answer = dashboard_cli_menu()

    # User wants to create a transaction. (1 for withdraw and 2 for deposit)
    if answer == 1 or answer == 2:
        if answer == 1:
            print("\n==== Withdraw ====")
        elif answer == 2:
            print("\n==== Deposit ====")
        amount = required_number_cli("Enter a number: ")

        response = None
        if answer == 1:
            response = bank_manager.withdraw(username, amount)
        elif answer == 2:
            response = bank_manager.deposit(username, amount)

        message = response[1]
        print(message)

    # User wants to show transaction history
    elif answer == 3:
        history = account_info["history"]
        print("\n\n ==== Transaction history ====\n")
        for transaction in history:
            print(transaction)

        print("\n")
    # User wants to log out
    elif answer == 4:
        print("Logging out. Have a nice day! :)")
        return None

    return dashboard_cli(username)

def dashboard_cli_menu():
    """
    Show dashboard menu and handles user input
    :return:
    """
    answer = None
    while not answer and (answer <= 0 or answer > 4):
        print("""Choose what to do:
        1.) Withdraw
        2.) Deposit
        3.) See history
        4.) Exit
        """)
        try:
            answer = int(input("Enter a number: "))
        except TypeError as e:
            print("Incorrect option. Try again.")
    return answer

def run_cli():
    """
    Run command line interface for bank management
    :return:
    """

    # Show main menu and handle user input
    answer = main_menu_cli()
    username = None

    # User wants to create an account
    if answer == 1:
        # Prompts the user to enter username, password, first name, last name
        # and home address
        username = required_input_cli("Enter your username: ")

        # Prompts the user to enter a different username if username
        # already exists
        while bank_manager.check_account_exists(username):
            print("Account already exists. Please choose a different username.")
            username = required_input_cli("Enter your username: ")

        password = required_input_cli("Enter your password: ")
        first_name = required_input_cli("Enter your first name: ")
        last_name = required_input_cli("Enter your last name: ")
        address = required_input_cli("Enter your address: ")

        # Attempts to create a bank account
        response = bank_manager.create_account(username, password, first_name, last_name, address)
        is_successful = response[0]
        message = response[1]

        # Print message
        print(message + "\n")

        # If account creation is not successful, call this function again.
        if not is_successful:
            return run_cli()
    # User wants to log in
    elif answer == 2:
        # Prompts the user to enter their username and password to log in
        username = required_input_cli("Enter your username: ")
        password = required_input_cli("Enter your password: ")

        # Authenticate credentials
        response = bank_manager.authenticate_user(username, password)
        is_successful = response[0]
        message = response[1]

        print(message + "\n")

        # Call this function again, if log in is unsuccessful
        if not is_successful:
            return run_cli()

    # User wants to exit the application
    elif answer == 3:
        print("Have a great day! :)")
        return None

    return dashboard_cli(username)

def required_input_cli(message):
    """
    Asks the user for input
    :param message:
    :return:
    """
    user_input = None
    while not user_input:
        user_input = raw_input(message)

    return user_input

def required_number_cli(message):
    """
    Asks the user to enter a valid number
    :param message:
    :return:
    """
    user_input = ""
    while not user_input.isdigit():
        user_input = raw_input(message)

    return float(user_input)

def main_menu_cli():
    """
    Main menu for command line interface
    :return:
    """
    answer = None
    while not answer or answer <= 0 or answer > 3:
        print("""Choose what to do:
        1.) Register
        2.) Log in
        3.) Exit
        """)
        answer = int(input("Enter a number: "))

    return answer


# ==== Main =====

def main():
    option_parser = ArgumentParser()
    option_parser.add_argument("-g", "--gui", action="store_true")

    args = option_parser.parse_args()

    if args.gui:
        print("Starting up GUI. Navigate to: localhost:5000")
        server = WSGIServer(("", 5000), app)
        server.serve_forever()
    else:
        print("Starting up CLI.")
        run_cli()


if "__main__" == __name__:
    main()
