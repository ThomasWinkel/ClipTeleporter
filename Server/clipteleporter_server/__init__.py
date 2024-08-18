from flask import Flask, render_template
from config import Config
from clipteleporter_server.extensions import db, migrate
from clipteleporter_server.models.clipboard import Clip

def create_app(config_class=Config):
    app = Flask(__name__)
    app.config.from_object(config_class)

    # Initialize Flask extensions here
    db.init_app(app)
    migrate.init_app(app, db)

    # Register blueprints here
    from clipteleporter_server.blueprints.api import bp as api_bp
    app.register_blueprint(api_bp, url_prefix='/api')

    # Serve info page
    @app.route('/')
    def index():
        clip_counter = db.session.query(Clip).count()
        return render_template('index.html', clip_counter=clip_counter)

    return app