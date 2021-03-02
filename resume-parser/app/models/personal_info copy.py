# from operator import pos
# from streetaddress import StreetAddressFormatter, StreetAddressParser
# import re
# import spacy
# from urlextract import URLExtract
# import json
# from spacy.matcher import Matcher
# from spacy.matcher import PhraseMatcher
# from collections import OrderedDict  
# import phonenumbers 
# import pgeocode
# import os
# import nltk




# state_names=["andhra pradesh","arunachal pradesh","assam","bihar","chhattisgarh","goa","gujarat","haryana","himachal pradesh","jharkhand","karnataka","kerala","madhya pradesh","maharashtra","manipur","meghalaya","mizoram","nagaland","odisha","punjab","rajasthan","sikkim","tamil nadu","telangana","tripura","uttarakhand","uttar pradesh","west bengal","goa","daman and diu","puducherry","ladakh","lakshadweep","delhi"]

# nlp = spacy.load('en_core_web_sm')
# urls_list = ["linkedin","github"]
# matcher = Matcher(nlp.vocab)

# personal_info_headings= ["personal"]

# base_path = os.path.dirname(__file__)

# nlp = spacy.load('en_core_web_sm')

# matcher = Matcher(nlp.vocab)



# file = os.path.join(base_path,"list_cities.txt")
# file = open(file, "r", encoding='utf-8')
# location_nlp = [line.strip().lower() for line in file]
# locationmatcher = PhraseMatcher(nlp.vocab)
# patterns = [nlp.make_doc(text) for text in location_nlp if len(nlp.make_doc(text)) < 10]
# locationmatcher.add("Location Address", None, *patterns)



# def personal_info_extract( terms_text_full, terms, heading_index):

#     #terms,terms_text_full = name_correction(terms,terms_text_full)


#     name_extra = "Email|EMAIL|NAME|Name|name"

#     exclude_list = ["CV","CURRICULUM VITAE", "RESU","BIO","BIODATA", "RESUME","DATA","SUMMARY"]
#     email = []
#     phone = []
#     urls = []
#     pincode= []
#     email_id=[]
#     email_index  = 0
#     search_index = 0
#     extractor = URLExtract()
#     addr_parser = StreetAddressParser()
#     personal_info_dict = {"name":[],"phone":[],"email":[],"url":[],"address":[],"passport_no":"","dob":"","gender":""}
#     name_list =[]
#     check = 0
#     terms_text = ""
#     no_more = 0

#     if no_more == 0:
#         for i in range(0, 10):
#             try:
#                 terms[i][2] = re.sub(name_extra," ",terms[i][2])
#                 terms[i][2] = re.sub(":|\.|\-"," ",terms[i][2])
#                 res = terms.split()
#                 terms[i][2] = re.sub("[0-9]","",terms[i][2])
#                 terms_text  += " " + terms[i][2]

#                 if terms[i][2] not in exclude_list:#Name Extraction
#                     if len(terms[i][2].split(" "))<4:
#                         name = nlp(terms[i][2])
#                         for token in name :
#                             if token.is_alpha:
#                                 if token.pos_ == "PROPN":
#                                     # print("Name:",name)
#                                         #personal_info_dict["name"].append(name.text)
#                                         name_list.append(name.text)
#                                         check = 1
#                                         break
#                         for X in name.ents :
#                             if X.label_ == 'PERSON':
#                                 #personal_info_dict["name"].append(name.text)
#                                 name_list.append(name.text)
#             except :
#                 a = 0

#         personal_info_dict["name"].append(name_list)
#         personal_info_dict["name"].append(extract_name(terms_text))

#         print(personal_info_dict["name"])
#         y = 0
#         try:
#             for i in range(len(name_list)):
#                 name_list[i].upper()
#                 for j in range(len(exclude_list)):
#                     if checkOrder(name_list[i],exclude_list[j]) == 1:
#                         name_list.pop(i)
#                         break
#             #print(name_list)
#             if len(name_list)>0 and name_list[0].isalpha() :
#                 personal_info_dict["name"].append(name_list[0])

#         except :
#             a =0

#         print(personal_info_dict["name"])
#         print(terms_text)
#         try:
#             if len(personal_info_dict["name"]) ==0:
#                 nlp_text = nlp(terms_text)

#                 # First name and Last name are always Proper Nouns
#                 # pattern_FML = [{'POS': 'PROPN', 'ENT_TYPE': 'PERSON', 'OP': '+'}]

#                 pattern = [{'POS': 'PROPN'}, {'POS': 'PROPN'}]
#                 matcher.add('NAME', None, pattern)

#                 matches = matcher(nlp_text)

#                 for match_id, start, end in matches:
#                     span = nlp_text[start:end]
#                     if span.text.isalpha():
#                         personal_info_dict["name"].append(span.text)
#                         break
#         except:
#             a=1

#         c = 0

#         print(personal_info_dict["name"])


#         try:
#             if len(personal_info_dict["name"])>0:
#                 for k in range(len(personal_info_dict["name"])):
#                     if personal_info_dict["name"][k] in exclude_list:
#                         personal_info_dict["name"].pop(k)
#                     if k != 0:
#                         personal_info_dict["name"].pop(k)
#         except:
#             a =2


    


#     c = 0
#     phone_numbers =  phonenumbers.PhoneNumberMatcher(terms_text_full, None)
#     try:
#         for pno in phone_numbers:
#             if c==0:
#                 personal_info_dict["phone"].append(pno.raw_string)
#                 c+=1
#     except:
#         personal_info_dict["phone"].append(re.findall(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full))

#     if len(personal_info_dict["phone"]) ==0 and re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full) :
#         if len(re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full)[0])>8 : 
#             personal_info_dict["phone"].append(re.search(r"\s*(?:\+?(\d{1,3}))?[-. (]*(\d{2,3})[-. )]*(\d{2,3})[-. ]*(\d{3,4,5})(?: *x(\d+))?\s*",terms_text_full)[0])

#     if len(personal_info_dict["phone"]) ==0 and re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full) :
#         if len(re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"\d{10}|\d{3}\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])

#     if len(personal_info_dict["phone"])==0 and re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full):
#         if len(re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"\d{3}(-)\d{3}(-)\d{4}",terms_text_full)[0])

#     if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full) :
#         if len(re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))(-)\d{3}(-)\d{4}",terms_text_full)[0])

#     if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full) :
#         if len(re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))\s{0,1}\d{3}(-)\d{4}",terms_text_full)[0])

#     if len(personal_info_dict["phone"])==0 and re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full):
#         if len(re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"(\()\d{3}(\))\s{1}\d{3}\s{1}\d{4}",terms_text_full)[0])

#     if len(personal_info_dict["phone"])==0 and re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full):
#         if len(re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full)[0])>8:
#             personal_info_dict["phone"].append(re.search(r"\d{3}(.)\d{3}(.)\d{4}",terms_text_full)[0])

#     if re.search("[1-9]{1}[0-9]{2}\\s{0, 1}[0-9]{3}",terms_text_full):
#         personal_info_dict["address"].append(re.search("[1-9]{1}[0-9]{2}\\s{0, 1}[0-9]{3}",terms_text_full)[0])

#     urls_list = []



#     temp_email_text = ""
    
#     for i in range(len(terms)):
#         try:
#             temp_line_text = terms[i][2]
#             # pincode = extract_pincode(temp_line_text)
#             # if len(pincode)>4:
#             #     personal_info_dict["address"].append(pincode)

#             if extractor.find_urls(terms[i][2]):
#                 urls.append(extractor.find_urls(terms[i][2]))
#         except :
#             a = 0

#     if len(urls)>0:
#         for j in range(len(urls)):
#             #if "www" in urls[j] or "http" in urls[j]:
#             personal_info_dict["url"].append(urls[j])
    

#     pincode = extract_pincode(terms_text_full)
   
#     personal_info_dict["address"].append(pincode)

#     email_id = extract_email_id(terms)
#     personal_info_dict["email"].append(email_id)

#     back_up_name = personal_info_dict["name"]

#     try:
#         if len(personal_info_dict["email"]) > 0 :
#                 temp_email = personal_info_dict["email"][0]
#                 temp_email = re.sub('[@]', ' ', temp_email)
#                 res = temp_email.split()
#                 temp_name = res[0]
#                 #"." in temp_name
#                 if  temp_name.count(".")==1  :
#                     personal_info_dict["name"] =[]
#                     res = temp_name.split(".")
#                     for i in range(len(terms)):
#                         for j in range(len(res)):
#                             text1 =  res[j].lower()
#                             text2 = terms[i][2]
#                             text2 =text2.lower()
#                             text2_len = len(text2.split())
#                             if text1 in text2 and "@" not in text2 and text2_len>0:
#                                 #print(text2_len)
#                                 if text2_len >1 and text2_len <4 and text2.isalpha() :
#                                     add_string  = ' '.join(terms[i][2].split())
#                                     personal_info_dict["name"].append(add_string)
#                 else:
#                     personal_info_dict["name"] =[]
#                     temp_name = re.sub('[^A-Za-z]+', '', temp_name)
#                     #print(temp_name)
                    
#                     for i in range(len(terms)):
#                         text1 =  temp_name.lower()
#                         text2 = terms[i][2]
#                         text2 =text2.lower()
#                         text2_len = len(text2.split())
#                         if text2_len>0 and text2_len<4 and "@" not in text2 :
#                             chk = 0
#                             #print(text2)
#                             for c in text1:
#                                 if c not in text2:
#                                     chk=1 
#                             if chk ==0:
#                                 #print(terms[i][2])
#                                 add_string  = ' '.join(terms[i][2].split())
#                                 if len(text2)> 12:
#                                     if " " in text2 : 
#                                         add_string = re.sub('[^A-Za-z ]+', '', add_string)
#                                         personal_info_dict["name"].append(add_string)
#         birth_date = ""
#         birth_date = extract_dob(terms)
#         personal_info_dict["dob"]= birth_date

#         pno = ""
#         pno = extract_pno(terms_text_full)
#         personal_info_dict["passport_no"]= pno

#         gender = ""
#         gender = extract_gender(terms_text_full)
#         personal_info_dict["gender"]= gender 



#         if len(personal_info_dict["name"]) ==0:


#             terms_text_full = re.sub(name_extra,"",terms_text_full)
#             terms_text_full = re.sub(":|\-","",terms_text_full)
#             terms_text_full = re.sub("[0-9]","",terms_text_full)

#             two_words_split = terms_text_full.split()

#             two_words= two_words_split[0]+" "+two_words_split[1]
#             two_check = 0
#             name = nlp(two_words)
#             two_check=0
#             #print(two_words)
#             for token in name :
#                 if token.pos_ == 'PROPN':
#                         two_check = 1

#             #print(two_check)
#             if two_check == 1:
#                 personal_info_dict["name"].append(two_words_split[0]+" "+two_words_split[1])
#     except:
#         personal_info_dict["name"] = []

#     if len(personal_info_dict["name"])>1:
#         temp = personal_info_dict["name"][0]
#         personal_info_dict["name"] = [temp]



#     if len(personal_info_dict["name"])>0:
#         temp = personal_info_dict["name"][0]
#         temp = re.sub("\.|\-|:|<|>"," ",temp)
#         temp = re.sub(name_extra,"",temp)
#         res = temp.split()
#         print(len(res))
#         if len(res)>3:
#             temp=""
#             for i in range(0,3):
    
#                 temp = temp +" "+res[i]

#         personal_info_dict["name"][0] =temp
#         if re.search("S\/o|s\/o|S\/O",personal_info_dict["name"][0]):
#             find =  re.search("S\/o|s\/o|S\/O",personal_info_dict["name"][0]).start()
#             personal_info_dict["name"][0]  =personal_info_dict["name"][0][0:find]

    

#     return personal_info_dict

# def checkOrder(input, pattern):  

#     # create empty OrderedDict  
#     # output will be like {'a': None,'b': None, 'c': None}  
#     dict = OrderedDict.fromkeys(input)  

#     # traverse generated OrderedDict parallel with  
#     # pattern string to check if order of characters  
#     # are same or not  
#     ptrlen = 0
#     for key,value in dict.items():  
#         if (key == pattern[ptrlen]):  
#             ptrlen = ptrlen + 1

#         # check if we have traverse complete  
#         # pattern string  
#         if (ptrlen == (len(pattern))):  
#             return 1
  
#     # if we come out from for loop that means  
#     # order was mismatched  
#     return 0

# # def address_extract():
# #     addr_parser = StreetAddressParser()
# #     addr_text = ""
# #     addr = addr_parser.parse(addr_text) 
# #     print(addr )


# def extract_name(resume_text):
#         nlp_text = nlp(resume_text)

#         # First name and Last name are always Proper Nouns
#         # pattern_FML = [{'POS': 'PROPN', 'ENT_TYPE': 'PERSON', 'OP': '+'}]

#         pattern = [{'POS': 'PROPN'}, {'POS': 'PROPN'}]
#         matcher.add('NAME', None, pattern)

#         matches = matcher(nlp_text)

#         for match_id, start, end in matches:
#             span = nlp_text[start:end]
#             return span.text
#         return ""

# def extract_pincode(temp_line_text):
#     address={"postal_code":" ","place_name":" ","state_name":" ","city_name":" ","area_name":" ","longitude":" ","latitude":""}
#     pincode = ""

#     if re.search(r"\b\d{3}\s{0,1}\d{3}\b",temp_line_text):
#         ind_pincode =str(re.search(r"\b\d{3}\s{0,1}\d{3}\b",temp_line_text)[0])
#         if int(ind_pincode[0])>0 and int(ind_pincode[0])<9:
#             pincode = ind_pincode

#     if re.search(r"\b\d{5}\b",temp_line_text):
#         us_zipcode = re.search(r"\b\d{5}\b",temp_line_text)[0]
#         pincode =us_zipcode

#     if pincode:
#         if len(str(pincode))==6:
#             nomi = pgeocode.Nominatim('in')
#             #print(nomi.query_postal_code(str(pincode))["place_name"])
#             address["postal_code"]= nomi.query_postal_code(str(pincode))["postal_code"]
#             address["place_name"]= nomi.query_postal_code(str(pincode))["place_name"]
#             address["state_name"]= nomi.query_postal_code(str(pincode))["state_name"]
#             address["city_name"]= nomi.query_postal_code(str(pincode))["county_name"]
#             address["area_name"]= nomi.query_postal_code(str(pincode))["community_name"]
#             address["longitude"]= str(nomi.query_postal_code(str(pincode))["longitude"])
#             address["latitude"]= str(nomi.query_postal_code(str(pincode))["latitude"])

#     if address["postal_code"] == " ":
#         location_extra=[]
#         #print(skill_text)
#         try:
#             nlp_new = nlp(temp_line_text.lower())
#             matches = locationmatcher(nlp_new)
#             for match_id, start, end in matches:
#                 span = nlp_new[start:end]
#                 location_extra.append(span.text)
#                 address["place_name"] = span.text
#                 address["city_name"] = span.text

#         except:
#             location_extra = []
    


#     return address



# def extract_email_id(terms):
#     total_text=""
#     email_id =""
#     email_escape_chars = [':',',','-','=']
#     for i in range(len(terms)):
#         temp_line_text = terms[i][2]
#         for k in email_escape_chars:
#             if k in temp_line_text:
#                 temp_line_text = temp_line_text.replace(k," ")
#         if temp_line_text.find(" @"):
#             temp_line_text = temp_line_text.replace(" @","@")
#         if temp_line_text.find("@ "):
#             temp_line_text = temp_line_text.replace("@ ","@")
#         if temp_line_text.find(". "):
#             temp_line_text = temp_line_text.replace(". ",".")
#         if temp_line_text.find(" ."):
#             temp_line_text = temp_line_text.replace(" .",".")

#         total_text = total_text + " "+temp_line_text
#     if  re.search(r"([^@|\s]+@[^@]+\.[^@|\s]+)", total_text):
#         email_id = re.search(r"([^@|\s]+@[^@]+\.[^@|\s]+)", total_text)[0]
#         if len(email_id.split())>2:
#             res =email_id.split()
#             email_id = res[0]
#     return email_id


# def extract_dob(terms):
#     birth_date = ""
#     match_dob=""
#     dob_words = "Born on|Date of birth|Personal Details|PERSONAL DETAILS|P ERSONAL D ETAILS|DOB :|DOB|DOB:|DATE OF BIRTH|Birth Date|Birth :|D.O.B|d. o. b|d o b|d  o  b|date and place of birth:|date and place of birth|date and country of birth|dateofbirth|data of birth|date of  birth|birthdate|date of birth/age:|date of birth/age|date of birthage|b\\'date|b’date|date  of  birth|date of birth|date ofbirth|dob|date & place of birth|d.o.b|date of birth|date-of-birth|date   of   birth|BORN:"
#     for i in range(len(terms)):
#         if re.search(dob_words,terms[i][2]):
#             birth_date= terms[i][2]
#             match_dob=re.search(dob_words,terms[i][2])[0]

#             print(terms[i][2])
#             print(terms[i+1][2])
#             print(terms[i+2][2])
#             print(terms[i+3][2])
#             if re.search('\d', terms[i][2]):
#                 birth_date = terms[i][2]
#                 print(birth_date)
#                 #print(terms[i][2])
#             else:
#                 if re.search('\d', terms[i+1][2]):
#                     birth_date = terms[i+1][2]
#                     print("2",terms[i+1][2])
#                 elif re.search('\d', terms[i+2][2]):
#                     birth_date = terms[i+2][2]
#                     print("2",terms[i+2][2])
#                 elif re.search('\d', terms[i+3][2]):
#                     birth_date = terms[i+3][2]
#                     print("2",terms[i+3][2])

#     if birth_date=="":
#         personal_keywords = "Personal Information|PERSONAL INFORMATION|PERSONAL DETAIL|Personal Detail"
#         for i in range(len(terms)):
#             if re.search(personal_keywords,terms[i][2]):
#                 if re.search("\d",terms[i][2]):
#                     birth_date =terms[i][2]
#                 elif re.search("\d",terms[i+1][2]):
#                     birth_date =terms[i+1][2]
#                 elif re.search("\d",terms[i+2][2]):
#                     birth_date =terms[i+2][2]


#     if re.search(match_dob,birth_date):
#         birth_date = re.sub(match_dob,"",birth_date)
#         birth_date = re.sub(":","",birth_date)
#         birth_date = re.sub(" \s+","",birth_date)

#     return birth_date 

# def extract_pno(terms_text_full):
#     pno=""
#     passport_regex = "[A-PR-WYa-pr-wy][1-9]\\d" +\
#             "\\s?\\d{4}[1-9]"
#     if re.search(passport_regex,terms_text_full):
#         pno =  re.search(passport_regex,terms_text_full)[0]

#     return pno 


# def extract_gender(text):
#     gender =""
#     re_gender = "Male|MALE|FEMALE|Female|female|male"

#     if re.search(re_gender,text):
#         gender =re.search(re_gender,text)[0]

#     return gender