# import json
# import spacy
# from date_extractor import extract_dates
# from datetime import datetime
# from datetime import date
# import re
# import os 
# from spacy.matcher import Matcher
# from spacy.matcher import PhraseMatcher
# import datefinder
# from .skills_extract import workex_extract_skills
# #from dateparser.search import search_dates



# base_path = os.path.dirname(__file__)

# nlp = spacy.load('en_core_web_sm')

# file = os.path.join(base_path,"titles_combined.txt")
# file = open(file, "r", encoding='utf-8')
# jobtitle = [line.strip().lower() for line in file]
# jobtitlematcher = PhraseMatcher(nlp.vocab)
# patterns = [nlp.make_doc(text) for text in jobtitle if len(nlp.make_doc(text)) < 10]
# jobtitlematcher.add("Job title", None, *patterns)



# def extract_exp_section(terms):
#     exp_sec_text = ""
#     experience_headings = "Professional Experience|PROFESSIONAL EXPERIENCE|Employment Details|EMPLOYMENT DETAILS|Employment History|EMPLOYMENT HISTORY|Career Contour|CAREER CONTOUR|EXPERIENCE|Experience|ORGANIZATIONAL SCAN|Major Assignments|experience chronology|organisational scan|Total Full Time Experience (in Months)|Career Progression|LABOR EXPERIENCE|work experience|working experience|professional experience|organisational experience|Experience:|Experience : " + "|Employment History|EMPLOYMENT HISTORY|highlights of professional experience|details of experience" +"|managerial experiences|experience|previous work experience|professional summary|selected experience" +"|professional work experience|e x p e r i e n c e|occupational contour|work history" +"|Professional Experience in Brief:|Professional Experience in Brief|Employee History" +"|employment history|career history|professional experience||proffesional experience" + "|performance highlights|current assignment|assignments held|EXPERIENCE SUMMARY|Experience Summary|hardware technical expertise|software technical expertise|networking expertise|WORK SUMMARY|Work Summary|job nature"
#     for i in range(len(terms)):
#        term_text =  terms[i][2]
#        term_text =  re.sub(' +', ' ',term_text)
#        #term_text = re.sub('[^A-Za-z0-9]+', ' ', term_text)
#        if term_text.isalpha() and len(term_text.split())<4 and len(term_text)>5: 
#            if re.search(experience_headings,term_text)[0]:
#                print(re.search(experience_headings,term_text)[0])
#                for k in range(i,len(terms)-2):
#                    if len(exp_sec_text.split())<60:
#                        temp_text = re.sub(' +', ' ', terms[k][2])
#                        #temp_text = re.sub('[^A-Za-z0-9]+', ' ', temp_text)
#                        exp_sec_text = exp_sec_text + " "+ temp_text
#     #print("exp",exp_sec_text)

#     return  exp_sec_text


# def exp_component_extract(terms, index_exp, heading_index):
#     total_duration = 0
  
#     experience_dict = {"exp_text": [],"organization":[],"job_designation":[], "dates" : {"date_tags":[], "duration":[]},"workex_skills":[] }
#     exp_sec_text = extract_exp_section(terms)

#     if exp_sec_text:
        
#         print(exp_sec_text)
#         try:
#             job_designition_list =[]

#             job_designition_list = job_designition(exp_sec_text)
#             experience_dict["job_designation"] = job_designition_list
#         except:
#              experience_dict["job_designation"] = []

#         try:
#             org_names = []
#             org_names = organization_name(exp_sec_text)
#             experience_dict["organization"] = org_names
#         except: 
#             experience_dict["organization"] =[]
#         try:
#             duration_time = 0 
#             duration_time = duration_func(exp_sec_text)
#             experience_dict["dates"]["duration"].append("0")
#             experience_dict["dates"]["duration"].append(duration_time)
#         except:
#             experience_dict["dates"]["duration"].append("0")
#             experience_dict["dates"]["date_tags"].append("0")
        

#         priority_skills = workex_extract_skills(exp_sec_text)
#         experience_dict["workex_skills"].append(priority_skills)
#         experience_dict["exp_text"].append(exp_sec_text)

#         return experience_dict 



# def search_job_title(exp_sec_text):
#     jobtitles =[]
#     nlp_new = nlp(exp_sec_text)
#     matches = jobtitlematcher(nlp_new)
#     for match_id, start, end in matches:
#         span = nlp_new[start:end]
#         jobtitles.append( span.text)
#     jobtitles_set = list(set(jobtitles))
#     return jobtitles_set

# def duration_func(exp_sec_text):
#     month_full_names = ["january","february", "march","april","may","june","july","august","september","october","november","december"]
#     month_abbre_names = ["jan","feb","mar","apr","may","jun","jul","aug","sep","oct","nov","dec"]
#     year_range = ["01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19","20","21"]
#     month_range =["01","02","03","04","05","06","07","08","09","10","11","12"]
#     today_words = ["present","current","till","onwards"]


#     text_piece = re.sub('[^A-Za-z0-9 ]+', ' ', exp_sec_text)
#     text_piece = text_piece.lower()

#     res = text_piece.split()
#     only_words = []
#     years_only = []
#     months = []

#     start_month =""
#     last_month = ""

#     #extraction of year from the experience section 
#     for i in range(len(res)):
#         for j in range(len(today_words)):
#             if today_words[j] in res[i]:
#                 years_only.append("2021")
#                 last_month = "feb"
#         if res[i].isdigit() or res[i] in month_full_names:
#             only_words.append(res[i])
#             if res[i].isdigit():
#                     if int(res[i])>2000 and int(res[i])<2022:
#                         years_only.append(res[i])
#                     elif res[i-1].isalpha() and (res[i-1] in month_abbre_names or res[i-1] in month_full_names):
#                         temp = int(res[i])
#                         if temp > 0 and temp < 22:
#                             temp = 2000 + temp
#                             years_only.append(str(temp))


#     temp_years_only = years_only
#     years_only.sort()
#     duration_time = 0
#     #try :
#     if years_only:
#             start = int(years_only[0])
#             last = int(years_only[len(years_only)-1])
#             duration_time = last-start
#             if len(years_only)== 1 :
#                 duration_time = 2021 - years_only 
#                 if duration_time == 0:
#                     duration_time = 1
#     #except: 
#     #   a  = 0


#     return duration_time

# def job_designition(text):
#     job_titles = []
#     text = text.lower()
#     text =  re.sub(' +', ' ', text)
#     text = re.sub('[^A-Za-z0-9]+', ' ', text)
#     __nlp = nlp(text.lower())

#     matches = jobtitlematcher(__nlp)
#     for match_id, start, end in matches:
#         span = __nlp[start:end]
#         job_titles.append(span.text)
#     return job_titles


# def organization_name(text):
#     org_name =[]
#     comp_keywords = ["pvt","private","ltd","limited","llc","corp","industr","solutions"]
#     name = nlp(text)
#     for X in name.ents :
#         if X.label_ == 'ORG':
#             org_name.append(X.text)
#     new_org_name = []
#     for i in range(len(org_name)):
#         for j in range(len(comp_keywords)):
#             if comp_keywords[j] in org_name[i].lower():
#                 new_org_name.append(org_name[i])
#                 break


#     org_name = new_org_name

#     return org_name
