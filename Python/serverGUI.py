from flask import Flask
from flask import render_template
from gevent.wsgi import WSGIServer


app = Flask(__name__)



@app.route("/")
def index():
    return render_template("index.html")


def main():
    server = WSGIServer(("", 5000), app)
    server.serve_forever()


if "__main__" == __name__:
    main()
