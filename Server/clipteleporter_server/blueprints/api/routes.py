from flask import request, jsonify
from clipteleporter_server.blueprints.api import bp
from clipteleporter_server.extensions import db
from clipteleporter_server.models.clipboard import Clip
from clipteleporter_server.utilities import generate_token
import logging


@bp.route('/get_clip/<token>')
def get_clip(token):
    clip: Clip = Clip.query.filter_by(token=token).first()
    return clip.data_object if clip else ''


@bp.route('/add_clip', methods=['POST'])
def add_clip():
    try:
        data = request.get_json()

        new_clip: Clip = Clip(
            data_object = data['data_object'],
            token = data['token']
        )

        db.session.add(new_clip)
        db.session.commit()

        return jsonify({'message': 'Ok'}), 201

    except Exception as e:
        logging.exception('Error adding clip.')
        return jsonify({'message': ''}), 500