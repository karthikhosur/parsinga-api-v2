import string 
from fuzzywuzzy import fuzz 
from fuzzywuzzy import process 
import re 

education_hds = ["educatio","employme"]
other_education_hds = ["academic qualificat"]
exclude_list = ["declaration","description"]
heading_possible_names = ["educational","academics","academic","education","qualification","work","career","experience","work experience","professional","leadership","profession","volunteer","objective","summary","goal","personal","employment","publications","activities","skills","accomplishment"]
experience_hds = ["work experience","employment","professional synopsis"]
other_experience_hds = ["experience","professional experience","professional"]
heading_index = []
remove_chars = ["=",":","-"]

def section_heading_ranking(line_text,term,sizes ):
    count = 0
    marker = 10
    if  term[0] == "bold":
        count += marker**1

    if line_text.isupper():
        count +=  marker**2

    if term[1] in sizes :
        count +=  marker**3


    return count



def sec_heading_titles(terms,sizes):
    check = 0
    counts = []
    heading_index = []
    heading_name = []
    sizes_len = int((len(sizes)/2)+1)
    temp_sizes = sizes[0:sizes_len] 
    for i in range(len(temp_sizes)):
        temp_sizes[i] = temp_sizes[i][0]
    # print(temp_sizes)

    for i in range(len(terms)):
                line_text = "".join(terms[i][2].split())
                temp = terms[i][2]
                line_text = ''.join((filter(lambda i: i not in remove_chars, line_text)))
                if line_text.isalpha():
                    if len(terms[i][2].split(" ")) < 4 and  len(terms[i][2])>4 and len(terms[i][2])<25 :
                        line_text=temp
                        count = section_heading_ranking(line_text,terms[i] ,temp_sizes )
                        if count >= 0 :
                            counts.append(count)
                            # print(count,line_text )
                            heading_index.append(i)
                            heading_name.append(terms[i][2])
   
    temp_exp =""
    temp_edu=""
    score = 0
    edu_sec = 0
    try:
        for i in range(len(heading_name)):
            for j in range(len(exclude_list)):
                if exclude_list[j] in heading_name[i].lower():
                    heading_name.pop(i)
                    heading_index.pop(i)
    except:
        a =2

    for i in range(len(heading_name)):
        line_text = heading_name[i]
        if fuzz.token_sort_ratio(line_text, education_hds) > score:
            edu_sec = i
            temp_edu =line_text
            score = fuzz.token_sort_ratio(line_text, education_hds)
    if score < 50:
        for i in range(len(heading_name)):
            line_text = heading_name[i]
            if fuzz.token_sort_ratio(line_text, other_education_hds) > score:
                edu_sec = i
                temp_edu =line_text
                score = fuzz.token_sort_ratio(line_text, education_hds)
    try:
        count_edu_sec =counts[edu_sec]
    except:
        count_edu_sec = 0
        edu_sec=0
    score = 0
    exp_sec = 0
    try:
        for i in range(len(heading_name)):
            line_text = heading_name[i]
            if fuzz.token_sort_ratio(line_text, experience_hds) > score:
                exp_sec = i
                temp_exp =line_text
                score = fuzz.token_sort_ratio(line_text, experience_hds)
        if score < 50:
            for i in range(len(heading_name)):
                line_text = heading_name[i]
                if fuzz.token_sort_ratio(line_text, other_experience_hds) > score:
                    exp_sec = i
                    temp_exp =line_text
                    score = fuzz.token_sort_ratio(line_text, experience_hds)

    except:
        a =5
    try:
        count_exp_sec =counts[exp_sec]
    except:
        count_exp_sec=0
        exp_sec=0

    new_heading_id = []
    new_heading_name = []
    try:
        for i in range(len(counts)):
            if (counts[i] == count_edu_sec) or (counts[i] == count_exp_sec):
                new_heading_id.append(heading_index[i])
                new_heading_name.append(heading_name[i])
        heading_index = new_heading_id
        heading_name = new_heading_name
    
    except:
        a =6

    upper_flag =0
    try:
        for i in range(len(heading_name)):
            if temp_edu in heading_name[i]:
                edu_sec = i
            if temp_exp in heading_name[i]:
                exp_sec = i
    except:
        a =7

    new_heading_id = []
    new_heading_name = []

    upper_len = 0 

    for i in range(len(heading_name)):
        if heading_name[i].isupper():
            upper_len+=1


    if upper_len>=7:
        new_heading_id = []
        new_heading_name = []
        for i in range(len(heading_name)):
                if  heading_name[i].isupper():
                    new_heading_id.append(heading_index[i])
                    new_heading_name.append(heading_name[i])
        heading_name = new_heading_name
        heading_index= new_heading_id
    else:
        new_heading_id = []
        new_heading_name = []

        if len(heading_name) > 9:
            for i in range(len(heading_name)):
                for j in range(len(heading_possible_names)):
                    if heading_possible_names[j] in heading_name[i].lower():
                        new_heading_name.append(heading_name[i])
                        new_heading_id.append(heading_index[i])

            heading_name=new_heading_name
            heading_index=new_heading_id
    new_heading_name = []
    new_heading_id=[] 
    for i in range(len(heading_name)): 
        if heading_name[i] not in new_heading_name: 
            new_heading_name.append(heading_name[i])
            new_heading_id.append(heading_index[i])
    heading_name=new_heading_name
    heading_index = new_heading_id
    return heading_name, heading_index


def extract_target_headings(terms,sizes):
    heading_name, heading_index = sec_heading_titles(terms,sizes)

    index_edu = education_heading_extract(heading_name,heading_index)
    index_exp = experience_heading_extract(heading_name, heading_index)
    return index_edu,index_exp, heading_index

def education_heading_extract(heading_name, heading_index):
    try:
        score = 0
        edu_sec = 0
        mark =0
        temp_edu_sec =0


        for i in range(len(heading_name)):
            temp_educ_name = heading_name[i]
            temp_educ_name.lower()
            line_text = heading_name[i]
            #print(temp_educ_name)
            if "educ" in temp_educ_name or "acade" in temp_educ_name:
            #if re.findall( "educ", temp_educ_name ) or re.findall("acade", temp_educ_name ): 
                mark=1
                #print("True")
                temp_edu_sec = i

            if fuzz.token_sort_ratio(line_text, education_hds) > score:
                edu_sec = i
                score = fuzz.token_sort_ratio(line_text, education_hds)

        if score < 50:
            for i in range(len(heading_name)):
                line_text = heading_name[i]
                if fuzz.token_sort_ratio(line_text, other_education_hds) > score:
                    edu_sec = i
                    score = fuzz.token_sort_ratio(line_text, education_hds)

        if mark==1 and temp_edu_sec != edu_sec :
            edu_sec= temp_edu_sec

        return heading_index[edu_sec]
    except:
        return 0


def experience_heading_extract(heading_name, heading_index):
    try:   
        score = 0 
        exp_sec=0

        for i in range(len(heading_name)):
            line_text = heading_name[i]
            if fuzz.token_sort_ratio(line_text, experience_hds) > score:
                exp_sec = i
                score = fuzz.token_sort_ratio(line_text, experience_hds)
        if score < 50:
            for i in range(len(heading_name)):
                line_text = heading_name[i]
                if fuzz.token_sort_ratio(line_text, other_experience_hds) > score:
                    exp_sec = i
                    score = fuzz.token_sort_ratio(line_text, experience_hds)
        return heading_index[exp_sec]
    except:
        return 0