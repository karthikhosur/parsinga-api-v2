import spacy
import re
from spacy.matcher import Matcher
from spacy.matcher import PhraseMatcher
from collections import OrderedDict 
from nltk.corpus import words
import nltk


nlp = spacy.load('en_core_web_sm')
matcher = Matcher(nlp.vocab)


exclude_list = ["CV","CURRICULUM VITAE", "RESU","BIO","BIODATA", "RESUME","DATA","SUMMARY"]
name_extra = "Email|EMAIL|NAME|Name|name|EMail|EMailID"
name_not  = "profession|strateg|areas|manager|designer|father|developer|internship|internship|work|work|employ|www|http|employ|career|career|employment|employer|employer|project|summary|technical|skill|engineer|place|ltd|company|skills|experience|declaration|personal|activities|projects|objective|professional|summary|history|personal|@|email|background|internship|technical|activities|work|exposure|achievements|career|pvt|private|ltd|limited|llc|corp|industr|solutions|school|college|university|bachelor|master"


def name_extraction(terms,terms_text_full):
    #Check if there is a name and copy that
    name_text =""
    new_len = len(terms)
    if new_len >22:
        new_len =20
    for i in range(0,new_len):
        terms[i][2]= re.sub("[\(\[].*?[\)\]]", "", terms[i][2])
       
        terms[i][2] = re.sub(" +"," ",terms[i][2])
        if name_text =="":
            if len(terms[i][2].split())<5 and len(terms[i][2])>4:
                if not re.search(name_not,terms[i][2].lower()):
                    if not re.search("[0-9]",terms[i][2]):
                        
                        if not re.search("@|,",terms[i][2]):
                            terms[i][2] = re.sub(":|\.|\-|\/"," ",terms[i][2])
                            name_text = re.sub(" +"," ",terms[i][2])
                            doc = nlp(name_text)
                            for token in doc:
                                if token.pos_ == "PROPN":
                                    if not token.text in name_text:
                                        name_text= name_text + " "+token.text
                        
                            res=[]
                            res = name_text.split()
                            print(name_text)
                            for j in range(len(res)):
                                if res[j] in words.words() or len(res[j])>20:
                                    if not len(res[j])>0 and len(res[j])<3:
                                        name_text=""
                    


    personal_info_dict={"name":[]}
    personal_info_dict["name"].append(name_text)
    print("My name is: ",personal_info_dict["name"])

    return personal_info_dict["name"][0]










































































































































#     #first way of parsing
#     personal_info_dict={"name":[]}
#     terms_text =""

#     terms_text_full = re.sub(name_extra," ",terms_text_full)
#     terms_text_full = re.sub(":|\.|\-"," ",terms_text_full)
#     terms_text_full = re.sub("[0-9]","",terms_text_full)
#     terms_text_full = re.sub(" +"," ",terms_text_full)
#     terms_text  += " " + terms_text_full


#     doc = nlp(terms_text)
#     only_noun= ""
#     cnt= 0
#     for token in doc:
#         cnt+=1
#         if (token.pos_ == "PROPN" and cnt<4) or (token.text.isupper() and cnt<4):
#             only_noun= only_noun+" "+token.text

#     personal_info_dict["name"].append(only_noun)

#     #second way of parsing
#     need_more_name = 0
#     if len(personal_info_dict["name"]) == 0:
#         need_more_name = 1
#     if name_not in personal_info_dict["name"][0].lower():
#         need_more_name =1

#     name_list= []
#     if need_more_name ==1:

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

#         try:
#             if len(personal_info_dict["name"])>0:
#                 for k in range(len(personal_info_dict["name"])):
#                     if personal_info_dict["name"][k] in exclude_list:
#                         personal_info_dict["name"].pop(k)
#                     if k != 0:
#                         personal_info_dict["name"].pop(k)
#         except:
#             a =2


#     print("Name is :",personal_info_dict["name"])


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
# return 0

