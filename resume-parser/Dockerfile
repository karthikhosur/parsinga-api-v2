FROM tiangolo/uvicorn-gunicorn-fastapi:python3.7
# Set environment varibles

COPY ./app /app

WORKDIR /app


RUN pip3.7 install -r requirements.txt

RUN python -m nltk.downloader all