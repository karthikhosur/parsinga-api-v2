import json
import spacy
from date_extractor import extract_dates
from datetime import datetime
from datetime import date
import re
import os 
from spacy.matcher import Matcher
from spacy.matcher import PhraseMatcher
import datefinder
from .skills_extract import workex_extract_skills
#from dateparser.search import search_dates

edu_stop_heading =  "skills|declaration|personal|education|academ|activities|projects|objective|professional|summary|background|internship|technical|activities|exposure|achievement"

base_path = os.path.dirname(__file__)

nlp = spacy.load('en_core_web_sm')

file = os.path.join(base_path,"titles_combined.txt")
file = open(file, "r", encoding='utf-8')
jobtitle = [line.strip().lower() for line in file]
jobtitlematcher = PhraseMatcher(nlp.vocab)
patterns = [nlp.make_doc(text) for text in jobtitle if len(nlp.make_doc(text)) < 10]
jobtitlematcher.add("Job title", None, *patterns)



def extract_exp_section(terms,  index_exp, heading_index):
    # temp_index_exp = index_exp
    # try:
    #     index_exp = heading_index.index(index_exp)
    # except:
    #     index_exp = 0
    # line_text = ""
    # try:
    #     if ((temp_index_exp+1) not in heading_index) or ((temp_index_exp+2) not in  heading_index ) or ((temp_index_exp+3) not in  heading_index )  :
    #         exp_sec_text = terms[heading_index[index_exp]: heading_index[index_exp+1]]
    #         for i in range(len(exp_sec_text)):
    #             line_text = line_text+" "+ exp_sec_text[i][2]
    #     else :
    #         exp_sec_text = terms[heading_index[index_exp]: heading_index[index_exp+3]]
    #         for i in range(len(exp_sec_text)):
    #             line_text = line_text+" "+ exp_sec_text[i][2]
    # except:
    #     exp_sec_text =""
    # #print("Experience data:",line_text)
    # try:
    #     if len(line_text.split())< 20:
    #         line_text = ""
    #         for i in range(heading_index[index_exp],len(terms)-2):
    #             if len(line_text.split())<60:
    #                 line_text =line_text+" "+terms[i][2]
    # except:
    #     a=0 

    # experience_headings = "Professional Experience|PROFESSIONAL EXPERIENCE|Employment Details|Experience|EMPLOYMENT DETAILS|Employment History|EMPLOYMENT HISTORY|Career Contour|CAREER CONTOUR|EXPERIENCE|Experience|ORGANIZATIONAL SCAN|Major Assignments|experience chronology|organisational scan|Total Full Time Experience (in Months)|Career Progression|LABOR EXPERIENCE|work experience|working experience|professional experience|organisational experience|experience:|experience : " + "|employment history|highlights of professional experience|details of experience" +"|managerial experiences|experience|previous work experience|professional summary|selected experience" +"|professional work experience|e x p e r i e n c e|occupational contour|work history" +"|Professional Experience in Brief:|Professional Experience in Brief|Employee History" +"|employment history|career history|professional experience||proffesional experience" + "|performance highlights|current assignment|assignments held|EXPERIENCE SUMMARY|Experience Summary|hardware technical expertise|software technical expertise|networking expertise|WORK SUMMARY|Work Summary|job nature";
    # if not re.search(experience_headings,line_text):
    #     for i in range(len(terms)):
    #         temp_text = terms[i][2]
    #         temp_text =  re.sub(' +', ' ', temp_text)

    #         if temp_text.isalpha() and len(temp_text.split())<4 and len(temp_text)>5:
    #             if re.search(experience_headings,temp_text):
    #                 line_text = ""
    #                 try:
    #                     exp_sec_text = terms[i : i+20]
    #                 except :
    #                     exp_sec_text = terms[i : len(terms)-2]
    #                 for j in range(len(exp_sec_text)):
    #                     line_text = line_text+exp_sec_text[j][2]
    


    # remove personal info
    personal_keywords = "Personal Information|PERSONAL INFORMATION|PERSONAL DETAIL|Personal Detail"
    




    # Check 1

    

    exp_sec_text=[]
    line_text=""
    work_ex_heading ="Professional|PROFESSIONAL|INTERNSHIP|Internship|WORK|Work|Employ|EMPLOY|Career|CAREER|EMPLOYMENT|Employer|EMPLOYER"
    for i in range(len(terms)):
        if len(terms[i][2].split())<5:
            if re.search(work_ex_heading,terms[i][2]):
                j=i
                while len(line_text.split())<150:
                    try:
                        temp = terms[j][2]
                        if len(temp.split())<6 and len(temp.split())>0 and len(line_text.split())>10:
                            temp  =temp.lower()
                            if re.search(edu_stop_heading,temp) :
                                # print(terms[j][2])
                                break
                        exp_sec_text.append(terms[j])
                        line_text = line_text + " " +terms[j][2]
                        j= j+1
                    except:
                        break


    # Check 2
    if len(line_text.split())<6:
         exp_sec_text=[]
    line_text=""
    work_ex_heading ="Experience|EXPERIENCE"
    for i in range(len(terms)):
        if len(terms[i][2].split())<4:
            if re.search(work_ex_heading,terms[i][2]):
                j=i
                while len(line_text.split())<150:
                    try:
                        temp = terms[j][2]
                        if len(temp.split())<6 and len(temp.split())>0 and len(line_text.split())>10:
                            temp  =temp.lower()
                            if re.search(edu_stop_heading,temp) :
                                # print(terms[j][2])
                                break
                        exp_sec_text.append(terms[j])
                        line_text = line_text + " " +terms[j][2]
                        j= j+1
                    except:
                        break
    

    # CHeck 3
    if len(line_text.split())<15:
        try:
            line_text=""
            exp_sec_text=[]

            temp_index_exp = index_exp
            try:
                index_exp = heading_index.index(index_exp)
            except:
                index_exp = 0
            line_text = ""
            try:
                if ((temp_index_exp+1) not in heading_index) or ((temp_index_exp+2) not in  heading_index ) or ((temp_index_exp+3) not in  heading_index )  :
                    exp_sec_text = terms[heading_index[index_exp]: heading_index[index_exp+1]]
                    for i in range(len(exp_sec_text)):
                        line_text = line_text+" "+ exp_sec_text[i][2]
                else :
                    exp_sec_text = terms[heading_index[index_exp]: heading_index[index_exp+3]]
                    for i in range(len(exp_sec_text)):
                        line_text = line_text+" "+ exp_sec_text[i][2]
            except:
                exp_sec_text =""
            #print("Experience data:",line_text)
            try:
                if len(line_text.split())< 20:
                    line_text = ""
                    for i in range(heading_index[index_exp],len(terms)-2):
                        if len(line_text.split())<100:
                            line_text =line_text+" "+terms[i][2]
            except:
                a=0 

            experience_headings = "Professional Experience|PROFESSIONAL EXPERIENCE|Employment Details|Experience|EMPLOYMENT DETAILS|Employment History|EMPLOYMENT HISTORY|Career Contour|CAREER CONTOUR|EXPERIENCE|Experience|ORGANIZATIONAL SCAN|Major Assignments|experience chronology|organisational scan|Total Full Time Experience (in Months)|Career Progression|LABOR EXPERIENCE|work experience|working experience|professional experience|organisational experience|experience:|experience : " + "|employment history|highlights of professional experience|details of experience" +"|managerial experiences|experience|previous work experience|professional summary|selected experience" +"|professional work experience|e x p e r i e n c e|occupational contour|work history" +"|Professional Experience in Brief:|Professional Experience in Brief|Employee History" +"|employment history|career history|professional experience||proffesional experience" + "|performance highlights|current assignment|assignments held|EXPERIENCE SUMMARY|Experience Summary|hardware technical expertise|software technical expertise|networking expertise|WORK SUMMARY|Work Summary|job nature";
            if not re.search(experience_headings,line_text):
                for i in range(len(terms)):
                    temp_text = terms[i][2]
                    temp_text =  re.sub(' +', ' ', temp_text)

                    if temp_text.isalpha() and len(temp_text.split())<4 and len(temp_text)>5:
                        if re.search(experience_headings,temp_text):
                            line_text = ""
                            try:
                                exp_sec_text = terms[i : i+20]
                            except :
                                exp_sec_text = terms[i : len(terms)-2]
                            for j in range(len(exp_sec_text)):
                                line_text = line_text+exp_sec_text[j][2]
        except:
            a = 0


    return line_text, exp_sec_text

def exp_component_extract(terms, index_exp, heading_index):
    total_duration = 0
    experience_dict = {"exp_text": [],"organization":[],"job_designation":[], "dates" : {"date_tags":[], "duration":[]},"workex_skills":[] }
    exp_sec_text, sec_terms = extract_exp_section(terms, index_exp, heading_index)
    if exp_sec_text:
        for i in range(len(sec_terms)):
            if len(sec_terms[i][2].split(" ")) > 2:
                try:
                    converted_text = ""
                    org_len = len(sec_terms[i][2].split())
                    doc =nlp(sec_terms[i][2])
                    for token in doc:
                            if not token.is_stop and not token.is_punct:
                                if token.pos_ == "PROPN" :
                                    if token.text not in converted_text:
                                        #print(token," ",token.text, token.pos_)
                                        converted_text += " "+token.text
                    #print(converted_text)
                    new_len = len(converted_text.split())
                    #if new_len == org_len   or (org_len-new_len)< new_len: #EXTRACTION OF COMPANY NAME AND JOB ROLE
                        #print("Company and job title:",converted_text) 
                        #experience_dict["job_title"].append(converted_text)
                except :
                        a=0

                     #experience_dict["job_title"].append("None")
        try:
            job_designition_list =[]

            job_designition_list = job_designition(exp_sec_text)
            experience_dict["job_designation"] = job_designition_list
        except:
             experience_dict["job_designation"] = []

        try:
            org_names = []
            org_names = organization_name(exp_sec_text)
            experience_dict["organization"] = org_names
        except: 
            experience_dict["organization"] =[]
        try:
            years_only=[]
            time_dur,years_only= duration_func(exp_sec_text)
            experience_dict["dates"]["date_tags"].append(years_only)
            experience_dict["dates"]["duration"].append(time_dur)
        except:
            experience_dict["dates"]["duration"].append("0")
            experience_dict["dates"]["date_tags"].append("0")

        priority_skills = workex_extract_skills(exp_sec_text)
        experience_dict["workex_skills"].append(priority_skills)
        experience_dict["exp_text"].append(exp_sec_text)

        if len(experience_dict["dates"]["duration"]) ==0 and len(experience_dict["job_designation"]) == 0 : 
            experience_dict = extract_experience_from_text(terms)

        return experience_dict 



def search_job_title(exp_sec_text):
    jobtitles =[]
    nlp_new = nlp(exp_sec_text)
    matches = jobtitlematcher(nlp_new)
    for match_id, start, end in matches:
        span = nlp_new[start:end]
        jobtitles.append( span.text)
    jobtitles_set = list(set(jobtitles))
    return jobtitles_set

def duration_func(exp_sec_text):

    month_full_names = ["january","february", "march","april","may","june","july","august","september","october","november","december"]
    month_abbre_names = ["jan","feb","mar","apr","may","jun","jul","aug","sep","oct","nov","dec"]
    year_range = ["01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19","20","21"]
    month_range =["01","02","03","04","05","06","07","08","09","10","11","12"]
    today_words = ["present","current","till","onwards"]

    text_piece = re.sub('[^A-Za-z0-9 ]+', ' ', exp_sec_text)
    text_piece = text_piece.lower()

    res = text_piece.split()
    only_words = []
    years_only = []
    for i in range(len(res)):
        for j in range(len(today_words)):
            if today_words[j] in res[i]:
                years_only.append("2021")
        if res[i].isdigit() or res[i] in month_full_names:
            only_words.append(res[i])
            #print(res[i])
            if res[i].isdigit():
                if int(res[i])>2000 and int(res[i])<2022:
                    years_only.append(res[i])
                elif res[i-1].isalpha() and (res[i-1] in month_abbre_names or res[i-1] in month_full_names):
                    temp = int(res[i])
                    if temp > 0 and temp < 22:
                        temp = 2000+ temp
                        years_only.append(str(temp))
    new_years_only=[]
    for i in range(len(years_only)):
        if int(years_only[i])>2005:
            new_years_only.append(years_only[i])


    years_only = new_years_only
    temp_years_only =years_only
    years_only.sort()
    try :
        if years_only:
            start = int(years_only[0])
            last = int(years_only[len(years_only)-1])
            duration = last -start
            if last==start:
                duration = 1
            return duration,temp_years_only
    except:
        return 0, years_only


    return 0,years_only

def job_designition(text):
    job_titles = []
    text = text.lower()
    text =  re.sub(' +', ' ', text)
    text = re.sub('[^A-Za-z0-9]+', ' ', text)
    __nlp = nlp(text.lower())

    matches = jobtitlematcher(__nlp)
    for match_id, start, end in matches:
        span = __nlp[start:end]
        job_titles.append(span.text)

    job_titles =list(set(job_titles))
    return job_titles


def organization_name(text):
    org_name =[]
    comp_keywords = ["pvt","private","ltd","limited","llc","corp","industr","solutions"]
    name = nlp(text)
    for X in name.ents :
        if X.label_ == 'ORG':
            org_name.append(X.text)
    new_org_name = []
    for i in range(len(org_name)):
        for j in range(len(comp_keywords)):
            if comp_keywords[j] in org_name[i].lower():
                new_org_name.append(org_name[i])
                break


    org_name = new_org_name

    return org_name

def extract_experience_from_text(terms):
    no_of_chars  = 0
    experience_dict = {"exp_text": [],"organization":[],"job_designation":[], "dates" : {"date_tags":[], "duration":[]},"workex_skills":[] }

    experience_headings = "Professional Experience|PROFESSIONAL EXPERIENCE|Employment Details|EMPLOYMENT DETAILS|Employment History|EMPLOYMENT HISTORY|Career Contour|CAREER CONTOUR|EXPERIENCE|Experience|ORGANIZATIONAL SCAN|Major Assignments|experience chronology|organisational scan|Total Full Time Experience (in Months)|Career Progression|LABOR EXPERIENCE|work experience|working experience|professional experience|organisational experience|experience:|experience : " + "|employment history|highlights of professional experience|details of experience" +"|managerial experiences|experience|previous work experience|professional summary|selected experience" +"|professional work experience|e x p e r i e n c e|occupational contour|work history" +"|Professional Experience in Brief:|Professional Experience in Brief|Employee History" +"|employment history|career history|professional experience||proffesional experience" + "|performance highlights|current assignment|assignments held|EXPERIENCE SUMMARY|Experience Summary|hardware technical expertise|software technical expertise|networking expertise|WORK SUMMARY|Work Summary|job nature";
    exp_text = ""
    for i in range(len(terms)):
        line_text = terms[i][2]
        exp_text = ""
        if len(line_text.split())>0 and len(line_text.split())< 4 and len(line_text)>5 :
            if re.search(experience_headings,line_text.lower()):
                #print(line_text)
                for j in range(i,len(terms)-1 ):
                    if len(exp_text.split())<50:
                        exp_text =exp_text + " " +terms[j][2]
                        exp_text =  re.sub(' +', ' ', exp_text)
                        exp_text = re.sub('[^A-Za-z0-9 ]+', ' ', exp_text)
                    else :
                        break

                #print(exp_text)
                job_titles = job_designition(exp_text)
                #print("Job Titles :",job_titles)
                org_names  = organization_name(exp_text)
                #print("Organisation Name :",org_names)
                time_dur,years_only= duration_func(exp_text)
                #print("Years :",years_only)
                #print("Duration :",time_dur)
                priority_skills = workex_extract_skills(exp_text)
                if len(job_titles)>0 or len(org_names)>0 or len(years_only)>0:
                    experience_dict["exp_text"].append(exp_text)
                    experience_dict["organization"].append(org_names)
                    experience_dict["job_designation"].append(job_titles)
                    experience_dict["dates"]["date_tags"].append(years_only)
                    experience_dict["dates"]["duration"].append(time_dur)
                    experience_dict["workex_skills"].append(priority_skills)



    return experience_dict