from flask import Blueprint

bp = Blueprint('visio', __name__)


from clipteleporter_server.blueprints.api import routes
