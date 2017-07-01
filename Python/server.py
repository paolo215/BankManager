from flask import Flask
from flask import render_template
from flask import redirect
from flask import url_for
from flask import request
from BankManager import BankManager
from gevent.wsgi import WSGIServer


app = Flask(__name__)
bank_manager = BankManager()
bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My address")

@app.route("/")
def index():
    return render_template("index.html")


@app.route("/login", methods=["GET", "POST"])
def login():
    error = None
    try:
        if request.method == "POST":
            username = request.form["username"]
            password = request.form["password"]

            response = bank_manager.authenticate_user(username, password)
            is_successful = response[0]
            error = response[1]

            if is_successful == False:
                return render_template("login.html", error=error)


            return redirect(url_for("dashboard", username=username))

    except Exception as e:
        error = e

    return render_template("login.html", error=error)

@app.route("/dashboard", methods=["GET", "POST"])
def dashboard():
    error = None
    username = request.args["username"]
    account = bank_manager.get_user_account(username)

    try:
        if request.method == "POST":
            print("post")
            option = request.form["option"]
            amount = int(request.form["amount"])
            print(option)
            print(amount)
            response = None
            if option == "Withdraw":
                response = bank_manager.withdraw(username, amount)
            elif option == "Deposit":
                response = bank_manager.deposit(username, amount)

            message = response[1]

            return render_template("dashboard.html", user_data=account.get_data(), message=message)


    except Exception as e:
        print(e)
        error = e


    return render_template("dashboard.html", user_data=account.get_data(), message=error)


def main():
    server = WSGIServer(("", 5000), app)
    server.serve_forever()


if "__main__" == __name__:
    main()
