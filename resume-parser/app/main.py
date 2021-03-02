import pathlib
import urllib.request
from fastapi.param_functions import Path
import models.main_start
from base64 import b64decode,b64encode
import codecs
import random
import json
import time
from pathlib import Path
import shutil
import requests
from typing import Optional
from fastapi import Depends, FastAPI, HTTPException, File, UploadFile
from pydantic import BaseModel
import fitz
from subprocess import  Popen
import os 
import sys
from fastapi.security.base import SecurityBase
import binascii
import mimetypes
from tempfile import SpooledTemporaryFile



class Item(BaseModel):
    base64file: str
    file_name: str

app = FastAPI()

@app.get("/")
async def root():
    return {"message": "Hello World"}

@app.post("/resumesbase64")
async def create_item(item: Item):
    #return {"message": "Hello World"}
    file_data = item.base64file
    filename = item.file_name
    temp_filename = filename
    filename = (filename.partition('.'))
    filetype = filename[2]
    filename_name = filename[0]
    file_title = filename_name
    just_mime_filetype = mimetypes.guess_type(temp_filename)[0]
    #print(just_mime_filetype)
    mime_filetype= "\""+just_mime_filetype+"\""
    size = 100000

    if filetype =="docx" or filetype =="doc" or filetype =="DOCX" or filetype =="DOC":
        try:
            url = 'https://ats.tallint.com/api/api/Document/Converter?file_path=&lng_id=1'
            payload= "[{\"file_title\":\""+temp_filename+"\",\"mime_type\":"+mime_filetype+",\"content\":\"data:"+just_mime_filetype+";base64,"+file_data+"\",\"extension\":\""+filetype+"\",\"status\":1,\"attachment_id\":23,\"size\":1000000,\"content_type\":1,\"file_path\":null,\"is_doc\":1}]"
            #print(payload)
            headers = {
                    'Content-Type': 'application/json'
                    }
            response = requests.request("POST", url, headers=headers, data=payload)
            y = response.json()		
            #print(y)
            y_text = y["data"]["base64String"]
            bytes = b64decode(y_text, validate=True)
            doc = fitz.open("pdf", bytes)
            res = models.main_start.main_in_file(doc)
            doc.close()
            #return res
            json_object = json.dumps(res)
            return json_object
        except:
            return {"res": "filetype Error"}
   
    elif filetype  == "pdf" or filetype  == "PDF"  :
        try:
            bytes = b64decode(file_data, validate=True)
            doc = fitz.open("pdf",bytes)
            res = models.main_start.main_in_file(doc)
            doc.close()
            #return res
            json_object = json.dumps(res)
            return json_object


        except:
            return {"res": "filetype Error"}

    return {"res": "filetype Error"}
 
@app.post("/fileupload")
async def fileupload(image: UploadFile = File(...)):
    #try:
        filename = str(image.filename)
        temp_filename = filename
        
        filename = (filename.partition('.'))
        filetype = filename[2]
        filename_name = filename[0]
        pdf_filename_name = filename_name+'.pdf'
        just_mime_filetype = mimetypes.guess_type(temp_filename)[0]
        mime_filetype= "\""+just_mime_filetype+"\""
        file_data_content = image.file.read()
        size = len(file_data_content)
        file_data = str(b64encode(file_data_content))
        #print(file_data)
        file_data = file_data[2:]
        file_data = file_data[:-1]
        file_title = "\""+temp_filename+"\""

        size = str(len(image.file.read()))
        #attachment_id = 12
        
        if filetype =="docx" or filetype =="doc" or filetype =="DOCX" or filetype =="DOC":
            url = 'https://ats.tallint.com/api/api/Document/Converter?file_path=&lng_id=1'
            payload= "[{\"file_title\":"+file_title+",\"mime_type\":"+mime_filetype+",\"content\":\"data:"+just_mime_filetype+";base64,"+file_data+"\",\"extension\":\""+filetype+"\",\"status\":1,\"attachment_id\":23,\"size\":"+size+",\"content_type\":1,\"file_path\":null,\"is_doc\":1}]"
            headers = {
                    'Content-Type': 'application/json'
                    }
            response = requests.request("POST", url, headers=headers, data=payload)
            y = response.json()		
            y_text = y["data"]["base64String"]
            bytes = b64decode(y_text, validate=True)
            doc = fitz.open("pdf", bytes)
            res = models.main_start.main_in_file(doc)
            doc.close()
            #return res
            json_object = json.dumps(res)
            return json_object

        elif filetype  == "pdf" or filetype  == "PDF"  :
            doc = fitz.open("pdf",file_data_content)
            res = models.main_start.main_in_file(doc)
            doc.close()
            
            #return res
            json_object = json.dumps(res)
            return json_object

        image.file.close()
        return {"res": "filetype Error"}

    #except:
        #return {"res": "filetype Error"}

if __name__ == "__main__":
    app = FastAPI()