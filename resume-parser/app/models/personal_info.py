from operator import pos
from streetaddress import StreetAddressFormatter, StreetAddressParser
import re
import spacy
from urlextract import URLExtract
import json
from spacy.matcher import Matcher
from spacy.matcher import PhraseMatcher
from collections import OrderedDict  
import phonenumbers 
import pgeocode
import os
import nltk




state_names=["andhra pradesh","arunachal pradesh","assam","bihar","chhattisgarh","goa","gujarat","haryana","himachal pradesh","jharkhand","karnataka","kerala","madhya pradesh","maharashtra","manipur","meghalaya","mizoram","nagaland","odisha","punjab","rajasthan","sikkim","tamil nadu","telangana","tripura","uttarakhand","uttar pradesh","west bengal","goa","daman and diu","puducherry","ladakh","lakshadweep","delhi"]

nlp = spacy.load('en_core_web_sm')
urls_list = ["linkedin","github"]
matcher = Matcher(nlp.vocab)

personal_info_headings= ["personal"]

base_path = os.path.dirname(__file__)

nlp = spacy.load('en_core_web_sm')

matcher = Matcher(nlp.vocab)



file = os.path.join(base_path,"list_cities.txt")
file = open(file, "r", encoding='utf-8')
location_nlp = [line.strip().lower() for line in file]
locationmatcher = PhraseMatcher(nlp.vocab)
patterns = [nlp.make_doc(text) for text in location_nlp if len(nlp.make_doc(text)) < 10]
locationmatcher.add("Location Address", None, *patterns)



def personal_info_extract( terms_text_full, terms, heading_index):

    backup_terms_text_full = terms_text_full

    name_extra = "Email|EMAIL|NAME|Name|name"

    exclude_list = ["CV","CURRICULUM VITAE", "RESU","BIO","BIODATA", "RESUME","DATA","SUMMARY"]
    email = []
    phone = []
    urls = []
    pincode= []
    email_id=[]
    email_index  = 0
    search_index = 0
    extractor = URLExtract()
    addr_parser = StreetAddressParser()
    personal_info_dict = {"name":[],"phone":[],"email":[],"url":[],"address":[],"passport_no":"","dob":"","gender":""}
    name_list =[]
    check = 0
    terms_text = ""
    no_more = 0

    email_terms = terms


    c = 0


    terms_text_full = backup_terms_text_full 
    phone_numbers =  phonenumbers.PhoneNumberMatcher(terms_text_full, None)
    try:
        for pno in phone_numbers:
            if c==0:
                personal_info_dict["phone"].append(pno.raw_string)
                c+=1
    except:
        personal_info_dict["phone"].append(re.findall(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full))

    if len(personal_info_dict["phone"]) ==0 and re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full) :
        if len(re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full)[0])>8 : 
            personal_info_dict["phone"].append(re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full)[0])

    if len(personal_info_dict["phone"]) ==0 and re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full) :
        if len(re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])

    if len(personal_info_dict["phone"])==0 and re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full):
        if len(re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full)[0])

    if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full) :
        if len(re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full)[0])

    if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full) :
        if len(re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full)[0])

    if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full):
        if len(re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])

    if len(personal_info_dict["phone"])==0 and re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full):
        if len(re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full)[0])>8:
            personal_info_dict["phone"].append(re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full)[0])

    if re.search("[1-9]{1}[0-9]{2}\\s{0, 1}[0-9]{3}",terms_text_full):
        personal_info_dict["address"].append(re.search("[1-9]{1}[0-9]{2}\\s{0, 1}[0-9]{3}",terms_text_full)[0])

    urls_list = []

    temp_email_text = ""
    
    for i in range(len(terms)):
        try:
            temp_line_text = terms[i][2]
            # pincode = extract_pincode(temp_line_text)
            # if len(pincode)>4:
            #     personal_info_dict["address"].append(pincode)

            if extractor.find_urls(terms[i][2]):
                urls.append(extractor.find_urls(terms[i][2]))
        except :
            a = 0

    if len(urls)>0:
        for j in range(len(urls)):
            #if "www" in urls[j] or "http" in urls[j]:
            personal_info_dict["url"].append(urls[j])
    

    pincode = extract_pincode(terms_text_full)
   
    personal_info_dict["address"].append(pincode)

    email_id = extract_email_id(email_terms,terms_text_full)
    personal_info_dict["email"].append(email_id)

    back_up_name = personal_info_dict["name"]

    try:

        birth_date = ""
        birth_date = extract_dob(terms)
        personal_info_dict["dob"]= birth_date

        pno = ""
        pno = extract_pno(terms_text_full)
        personal_info_dict["passport_no"]= pno
 
        gender = ""
        gender = extract_gender(terms_text_full)
        personal_info_dict["gender"]= gender 


    except:
        a = 0


    personal_info_dict["name"].append("")


    return personal_info_dict


def extract_pincode(temp_line_text):
    address={"postal_code":" ","place_name":" ","state_name":" ","city_name":" ","area_name":" ","longitude":" ","latitude":""}
    pincode = ""

    if re.search(r"\b\d{3}\s{0,1}\d{3}\b",temp_line_text):
        ind_pincode =str(re.search(r"\b\d{3}\s{0,1}\d{3}\b",temp_line_text)[0])
        if int(ind_pincode[0])>0 and int(ind_pincode[0])<9:
            pincode = ind_pincode

    if re.search(r"\b\d{5}\b",temp_line_text):
        us_zipcode = re.search(r"\b\d{5}\b",temp_line_text)[0]
        pincode =us_zipcode

    if pincode:
        if len(str(pincode))==6:
            nomi = pgeocode.Nominatim('in')
            #print(nomi.query_postal_code(str(pincode))["place_name"])
            address["postal_code"]= nomi.query_postal_code(str(pincode))["postal_code"]
            address["place_name"]= nomi.query_postal_code(str(pincode))["place_name"]
            address["state_name"]= nomi.query_postal_code(str(pincode))["state_name"]
            address["city_name"]= nomi.query_postal_code(str(pincode))["county_name"]
            address["area_name"]= nomi.query_postal_code(str(pincode))["community_name"]
            address["longitude"]= str(nomi.query_postal_code(str(pincode))["longitude"])
            address["latitude"]= str(nomi.query_postal_code(str(pincode))["latitude"])

    if address["postal_code"] == " ":
        location_extra=[]
        #print(skill_text)
        try:
            nlp_new = nlp(temp_line_text.lower())
            matches = locationmatcher(nlp_new)
            for match_id, start, end in matches:
                span = nlp_new[start:end]
                location_extra.append(span.text)
                address["place_name"] = span.text
                address["city_name"] = span.text

        except:
            location_extra = []
    


    return address



def extract_email_id(email_terms,terms_text_full):
    total_text=""
    email_id =""
    email_escape_chars =":|,|\-|=|EMAIL|Email"
    terms_text_full =re.sub(email_escape_chars," ",terms_text_full)
    for i in range(len(email_terms)):
        if re.search("gmai",email_terms[i][2]):
            print(email_terms[i][2])
        email_terms[i][2] =re.sub(email_escape_chars," ",email_terms[i][2])
        if re.search("\s@",email_terms[i][2]):

            email_terms[i][2] =re.sub("\s@","@",email_terms[i][2])
        if re.search("@\s",email_terms[i][2]):
            email_terms[i][2] =re.sub("@\s","@",email_terms[i][2])
        if re.search(".\s",email_terms[i][2]):
            email_terms[i][2] =re.sub(".\s",".",email_terms[i][2])
        if re.search("\s.",email_terms[i][2]):
            email_terms[i][2] =re.sub("\s.",".",email_terms[i][2])
        total_text = total_text + " "+ email_terms[i][2]
        

    if  re.search(r"([^@|\s]+@[^@]+\.[^@|\s]+)", terms_text_full):
        email_id = re.search(r"([^@|\s]+@[^@]+\.[^@|\s]+)", terms_text_full)[0]
        if len(email_id.split())>2:
            res =email_id.split()
            email_id = res[0]
    return email_id


def extract_dob(terms):
    birth_date = ""
    match_dob=""
    dob_words = "Born on|Date of birth|Personal Details|PERSONAL DETAILS|P ERSONAL D ETAILS|DOB :|DOB|DOB:|DATE OF BIRTH|Birth Date|Birth :|D.O.B|d. o. b|d o b|d  o  b|date and place of birth:|date and place of birth|date and country of birth|dateofbirth|data of birth|date of  birth|birthdate|date of birth/age:|date of birth/age|date of birthage|b\\'date|b’date|date  of  birth|date of birth|date ofbirth|dob|date & place of birth|d.o.b|date of birth|date-of-birth|date   of   birth|BORN:"
    for i in range(len(terms)):
        if re.search(dob_words,terms[i][2]):
            birth_date= terms[i][2]
            match_dob=re.search(dob_words,terms[i][2])[0]

            print(terms[i][2])
            print(terms[i+1][2])
            print(terms[i+2][2])
            print(terms[i+3][2])
            if re.search('\d', terms[i][2]):
                birth_date = terms[i][2]
                print(birth_date)
                #print(terms[i][2])
            else:
                if re.search('\d', terms[i+1][2]):
                    birth_date = terms[i+1][2]
                    print("2",terms[i+1][2])
                elif re.search('\d', terms[i+2][2]):
                    birth_date = terms[i+2][2]
                    print("2",terms[i+2][2])
                elif re.search('\d', terms[i+3][2]):
                    birth_date = terms[i+3][2]
                    print("2",terms[i+3][2])

    if birth_date=="":
        personal_keywords = "Personal Information|PERSONAL INFORMATION|PERSONAL DETAIL|Personal Detail"
        for i in range(len(terms)):
            if re.search(personal_keywords,terms[i][2]):
                if re.search("\d",terms[i][2]):
                    birth_date =terms[i][2]
                elif re.search("\d",terms[i+1][2]):
                    birth_date =terms[i+1][2]
                elif re.search("\d",terms[i+2][2]):
                    birth_date =terms[i+2][2]


    if re.search(match_dob,birth_date):
        birth_date = re.sub(match_dob,"",birth_date)
        birth_date = re.sub(":","",birth_date)
        birth_date = re.sub(" \s+","",birth_date)

    return birth_date 

def extract_pno(terms_text_full):
    pno=""
    passport_regex = "[A-PR-WYa-pr-wy][1-9]\\d" +\
            "\\s?\\d{4}[1-9]"
    if re.search(passport_regex,terms_text_full):
        pno =  re.search(passport_regex,terms_text_full)[0]

    return pno 


def extract_gender(text):
    gender =""
    re_gender = "Male|MALE|FEMALE|Female|female|male"

    if re.search(re_gender,text):
        gender =re.search(re_gender,text)[0]

    return gender