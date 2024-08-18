#!/usr/bin/env bash
flask db upgrade
flask --app clipteleporter_server run --host=0.0.0.0