from clipteleporter_server.extensions import db
from datetime import datetime
from sqlalchemy import func
from sqlalchemy.orm import Mapped, mapped_column


class Clip(db.Model):
    __tablename__ = "clips"
    id: Mapped[int] = mapped_column(primary_key=True)
    upload_date: Mapped[datetime] = mapped_column(insert_default=func.now())
    token: Mapped[str] = mapped_column()
    data_object: Mapped[str] = mapped_column()