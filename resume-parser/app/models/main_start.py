from .skills_extract import extract_skills
from .personal_info import personal_info_extract
from .extract_education import edu_section_details
from .extract_experience import exp_component_extract
from .section_headings import extract_target_headings
from .name_parser import name_extraction

from operator import itemgetter
import threading
import fitz
import _thread
import concurrent.futures
import time 
import re 

#from docx2pdf import convert
import os
import json
from multiprocessing import Process




def flags_decomposer(flags):
    """Make font flags human readable."""
    l = []

    if flags & 2 ** 4:
        l.append("bold")
    return ", ".join(l)

def line_extractor(doc):
    bold_identifier  = []
    font_sizes = []
    latex_id = 0
    exclude_re =  "Bio-Data|BIODATA|R E S U M E|P a g e|biodata|Page|PAGE|page|CURRICULUMVITAE|CURRICULAM VITAE|CURRICULUM VITAE|●|Curriculum Vitae|CV|Resume|RESUME|CONFIDENTIAL|Look forward|My CV is detailed below|Work is Worship|Not keen on sales" + "|DSP MERRILYNCH|Private & Confidential|(Private and Confidential)" +"|Private and Confidential|Please feel free to contact|Moved to" +"|a manager who is……..|Here is the resume of the applicant|trial version can convert" +"|(this message is omitted in registered version)|converted by activertf trial version" +"|planman consulting|referred|reffered|Last active|Last Modified|NOT LOOKING FOR CHANGE" +"|Timesjobs profile|Jobstreet profile|Monster profile|Naukri Profile" +"|click here to view resume in doc format|click here to unsubscribe" +"|Add Comments to Resume|BIO -  DATA|Consultants|Names will be provided on request|circulam vitae|urriculam vitae" +"|Qualification|Total Exp|Skill sets|CIRRUCULAM VITAE|C  U  R  R  I  C  U  L  U  M      V  I  T  A  E|present company" + "|total exp|circulam vitae|urriculam vitae|SAP-|Ref. By|CIRCULAM - VITAE|CIRCULAM-VITAE|job code" + "|i am writing|my objective is|i am confident|currently i am|i am enclosing|my norm is" +"|BIO - DATA|.Curriculum  Vitae|Carriculam Vitae|CIRRICULAM VITAE|CARRICULUM VITAE|urriculum Vitae|CRRICULUM VITAE|CUURICLUM VITAE|urriculum Vitae|- CIRCULLUM VITAE -|DCURRICULAM|CARICULAM VITAE" +"|RICULUM VITAE|CARRICULAM VITAE|Initial Contact - Curriculum Vitae|CIRCULUM VITAE|CIRRICULUM VITAE|comprehensive|URRICULUM  VITAE|CURRICULUM  VITAE|IRCULUM VITAE" +"|CURICURUM VITAE|CIRICULAM-REVITA|RESUMẾ|Om Sai Ram|A        R        C        H        I        T        E        C        T|Contact Address :" +"|Age :|Contact No. :|Email Id :|Nationality/Sex :|Languages known :" +"|Associate|CARRICULAM VITE|CERTIFIED|POST APPLIED|Gender|Nationality|Contact Address|MICROSOFT CERTIFIED|rRESUME|CONCISE RESUME|View Resume|(Word 2000 Format)|View Text Resume Only|Forward This Resume|Contact by Email|Print Resume|Save to Folder|Close Window|Go to:|E.CTC:|C.CTC:"
    exclude_list = ["CV", "Curriculum Vitae", "Resume","bio-data","biodata","CURRICULUM VITAE"]
    terms = []

    #print(doc.metadata['producer'])
    #print(doc.metadata['creator'])
    if "pdfTeX" in doc.metadata['producer'] or "LaTeX" in doc.metadata['creator'] or "WPS Writer" in doc.metadata['creator'] :
        latex_id = 1

    for page in doc:
        # read page text as a dictionary, suppressing extra spaces in CJK fonts
        blocks = page.getText("dict", flags=11)["blocks"]
        #print(len(blocks))
        if latex_id == 1 :
            text_block = page.getText("blocks")
            #print(len(text_block))
            #print("text-block \n \n",text_block)
            if len(text_block)== len(blocks):
                for i in range(len(blocks)):
                    temp = text_block[i]
                    text_temp = str(temp[4])
                    text_temp= text_temp.replace('\n',' ')
                    blocks[i]['lines'][0]['spans'][0]['text']=text_temp
        #print("blocks \n \n \n",blocks)
        # print(len(text_block_only))
        for b in blocks:  # iterate through the text blocks
            for l in b["lines"]:  # iterate through the text lines
                for s in l["spans"]:  # iterate through the text spans
                        term = []
                        font_properties =  (
                            #s["font"],  # font name
                            flags_decomposer(s["flags"]),  # readable font flags
                            s["size"],  # font size
                            #s["color"],  # font color
                            s["bbox"]
                        )
                        s["text"] = re.sub(exclude_re," ",s["text"])
                        if s["text"] != " " and  s["text"] not in exclude_list:
                            term.append(font_properties[0])
                            term.append(font_properties[1])
                            font_sizes.append(font_properties[1])
                            term.append(str(s["text"]))
                            term.append(font_properties[2])
                            #print(term)
                            terms.append(term)

    #print(terms)
    font_sizes.sort(reverse=True)
    #print(font_sizes)

    sizes  = list(set(font_sizes))
    sizes.sort(reverse=True)
    sizes_count= []

    for i in range(len(sizes)):
        temp = []
        temp.append(sizes[i])
        temp.append(font_sizes.count(sizes[i]))
        sizes_count.append(temp)
    sizes = sizes_count
    #print(sizes)
    #print(terms)

    
   

    return terms,sizes

def main_in_file(doc):

    resume_json = {"personal_info":[],"education":[],"experience":[],"total_experience":"","priority_skills":[],"all_skills":[],"other_sections":[],"text":[]}
    terms,sizes = line_extractor(doc)
    length_terms = len(terms)-2
    resume_text = ""
    # Remove duplicate terms
    try :
        for i in range(length_terms):
            terms[i][2] = re.sub("[a-zA-Z;,]\s [0-9]", "[a-zA-Z;,][0-9]", str(terms[i][2]))
            terms[i][2] = re.sub("[0-9]\s [a-zA-Z;,]", "[0-9][a-zA-Z;,]", str(terms[i][2]))
            resume_text = resume_text +" "+(str(terms[i][2]))
            if terms[i][2] == terms[i+1][2]:
                terms.pop((i+1))
    except:
            resume_text=""
            for i in range(len(terms)):
                terms[i][2] = re.sub("[a-zA-Z;,]\s [0-9]", "[a-zA-Z;,][0-9]", str(terms[i][2]))
                terms[i][2] = re.sub("[0-9]\s [a-zA-Z;,]", "[0-9][a-zA-Z;,]", str(terms[i][2]))
                resume_text = resume_text  + " " +(str(terms[i][2]))

    index_edu,index_exp,heading_index = extract_target_headings(terms, sizes)
    #print(heading_index)

    #for i in heading_index:
        #print(terms[i])
    last_index = int(len(terms)-1)
    try:
        heading_index.append(last_index)
    except:
        a = 0
    resume_text = re.sub(" +", ' ', resume_text)
    #' '.join(resume_text.split())
    
    resume_text=' '.join(filter(None,resume_text.split(' ')))
    #Extract if skill region if any__________________________________________________________
    skill_text = ""
    try: 
        for i in range(len(heading_index)):
            #print(terms[heading_index[i]])
            if "skill" in terms[heading_index[i]][2].lower():
                new_list = terms[i:len(terms)-2]
                for m in range(len(new_list)):
                    skill_text = skill_text+new_list[m][2]
                break
    except:
        a=0

    terms_backup=terms 
    priority_skills=[]
    all_skills=[]

    with concurrent.futures.ThreadPoolExecutor() as executor:
        future1 = executor.submit(extract_skills, resume_text,skill_text)
        future2 = executor.submit(personal_info_extract,resume_text, terms,heading_index)
        future3 = executor.submit(edu_section_details, terms, index_edu, heading_index)
        future4 = executor.submit(exp_component_extract, terms, index_exp, heading_index)
        future5 = executor.submit(name_extraction,terms_backup,resume_text)
        all_skills, priority_skills = future1.result()
        personal_info_json = future2.result()
        edu_info_json = future3.result()
        exp_info_json = future4.result()
        altered_name = future5.result()

    heading_names = []

    #revaluate name
    if len(altered_name)>6:
        personal_info_json["name"]= []
        personal_info_json["name"].append(altered_name)
    else :
        personal_info_json["name"]= []
        personal_info_json["name"].append(alter_name(resume_text))
    #Revaluate The experience duration---------------------------------------------------
    try:
        text_piece = re.sub('[^A-Za-z0-9. ]+', ' ', resume_text)
        text_piece = re.sub("\. | \.",".",text_piece)
        text_piece= text_piece.lower()
        res = text_piece.split()
        list_dur = []
        temp_dur = 0
        last = ""
        for i in range(len(res)):
                if re.search("year|years",res[i]) :
                    if  "experience" in res[i+1].lower() or "experience" in res[i+2].lower() or "experience" in res[i+3].lower() or  "experience" in res[i+4].lower() or "experience" in res[i+5].lower() or "experience" in res[i-1].lower() or "experience" in res[i-2].lower() or "experience" in res[i-3].lower() :
                        #print("yes i am in")
                        if "." in res[i-1] or  res[i-1].isdigit():
                            temp1 = float(res[i-1])
                            if temp1 not in list_dur:
                                temp_dur =temp_dur+temp1
                                #print(temp1)
                                list_dur.append(temp_dur)
                        elif "." in res[i-2] or res[i-2].isdigit() :
                            temp2 = float(res[i-2])
                            if temp2 not in list_dur:
                                temp_dur =temp_dur+temp2
                                list_dur.append(temp_dur)
           
        if len(exp_info_json["dates"]["duration"])>0:
            if temp_dur>0 and float(exp_info_json["dates"]["duration"][0]) < temp_dur:
                exp_info_json["dates"]["duration"] = str(temp_dur)



    except:
        a=0

  
    try:
        if len(edu_info_json["dates"]["date_tags"])>0:
                max_edu = max(edu_info_json["dates"]["date_tags"])
                print(max_edu)
                print(int(2021-int(max_edu)))
                if int(exp_info_json["dates"]["duration"][0]) >= int(2020-int(max_edu)) and int(2020-int(max_edu))>0:
                    print("true")
                    exp_info_json["dates"]["duration"][0] = str(2020-int(max_edu))
    except:
        a =0
    #RevaluatThe experience duration---------------------------------------------------


    #print(exp_info_json)
    
    for i in range(len(heading_index)-1):
        heading_names.append(terms[heading_index[i]][2])


    resume_json["personal_info"].append(personal_info_json)
    resume_json["education"].append(edu_info_json)
    resume_json["experience"].append(exp_info_json)
    resume_json["priority_skills"].append(priority_skills)
    try : 
        resume_json["total_experience"]=str(exp_info_json["dates"]["duration"][0])
    except :
        resume_json["total_experience"]= "0"

    resume_json["all_skills"].append(all_skills)
    resume_json["other_sections"].append(heading_names)
    resume_json["text"].append(resume_text)
    tem_json = resume_json
    
    alter_results(tem_json)
    

    return (resume_json)



def alter_results(res):
    try:
        edu_res = res["education"]
        count_date_tags = len(edu_res[0]["dates"]["date_tags"])
        count_university =len(edu_res[0]["university"])
        count_degree = len((edu_res[0]["degree"]))
        count_grade = len(edu_res[0]["grade"])
        max_copies= max(count_date_tags, count_university, count_degree,count_grade)
        # copy to the new format of education
        new_edu_dict = {"edu_text":"","edu_history":[]}
        temp_new_edu_mini = {"id":"","degree":"","university":"","grade":"","graduation_year":""}
        new_edu_dict["edu_text"] = str(edu_res[0]["edu_text"][0])
        if max_copies>0:
            for i in range(0,max_copies):
                #print(i)
                new_edu_mini = temp_new_edu_mini
                new_edu_mini["id"]=str(i)
                #print(new_edu_mini)
                new_edu_dict["edu_history"].append(new_edu_mini.copy())


        #print("c date",count_date_tags)
        if count_date_tags>0:
            for i in range(0,count_date_tags):
                #print(new_edu_dict["edu_history"][i])
                new_edu_dict["edu_history"][i]["graduation_year"]= str(edu_res[0]["dates"]["date_tags"][i])
        #print("c uni",count_university)
        if count_university>0:
            for i in range(0,count_university):
                new_edu_dict["edu_history"][i]["university"] = str(edu_res[0]["university"][i])
        #print("c deg",count_degree)
        if count_degree>0:
            for i in range(0,count_degree):
                new_edu_dict["edu_history"][i]["degree"] = str(edu_res[0]["degree"][i])

        if count_grade>0:
            for i in range(0,count_grade):
                new_edu_dict["edu_history"][i]["grade"] = str(edu_res[0]["grade"][i])
        res["education"] = new_edu_dict
    except:
        new_edu_dict={"edu_text":"","edu_history":[{"id":"","degree":"","university":"","grade":"","graduation_year":""}]}
        res["education"] = new_edu_dict
    

    # Experience modify
    try: 
        exp_res = res["experience"]
        count_date_tags = len(exp_res[0]["dates"]["date_tags"][0])
        count_organization =len(exp_res[0]["organization"])
        count_job_designation = len((exp_res[0]["job_designation"]))
        count_workex_skills = len(exp_res[0]["workex_skills"][0])
        duration = len(exp_res[0]["dates"]["duration"])
        max_copies= max(count_date_tags, count_organization, count_job_designation)

        new_exp_dict = {"exp_text":"","exp_history":[],"workex_skills":[]}
        temp_new_exp_mini = {"id":"","organization":"","job_designation":"","start_date":"","end_date":""}
        new_exp_dict["exp_text"] = str(exp_res[0]["exp_text"][0])
        if max_copies>0:
            for i in range(0,max_copies):
                #print(i)
                new_exp_mini = temp_new_exp_mini
                new_exp_mini["id"]=str(i)
                #print(new_exp_mini)
                new_exp_dict["exp_history"].append(new_exp_mini.copy())

        #print("c date",count_date_tags)
        if count_date_tags>0:
            for i in range(0,count_date_tags):
                #print(new_exp_dict["exp_history"][i])
                new_exp_dict["exp_history"][i]["end_date"]= str(exp_res[0]["dates"]["date_tags"][0][i])

        #print("c uni",count_organization)
        if count_organization>0:
            for i in range(0,count_organization):
                new_exp_dict["exp_history"][i]["organization"] = str(exp_res[0]["organization"][i])

        if count_job_designation>0:
            for i in range(0,count_job_designation):
                new_exp_dict["exp_history"][i]["job_designation"] = str(exp_res[0]["job_designation"][i])

        if count_workex_skills>0:
            new_exp_dict["workex_skills"] = exp_res[0]["workex_skills"][0]
        
        res["experience"] = new_exp_dict
    except:
        new_exp_dict = {"exp_text":"","exp_history":[{"id":"","organization":"","job_designation":"","start_date":"","end_date":""}],"workex_skills":[]}
        res["experience"] = new_exp_dict

    return res


def alter_name(name_resume_text):

    new_name = ""
    name_resume_text =re.sub("[0-9]","",name_resume_text) 
    name_resume_text= re.sub(" +"," ",name_resume_text)

    res = name_resume_text.split()


    for i in range(len(0,2)):
        new_name=" "+new_name







    return new_name