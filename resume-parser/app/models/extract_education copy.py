# #from fuzzywuzzy import fuzz
# from fuzzywuzzy import process
# from date_extractor import extract_dates
# from spacy.matcher import Matcher
# from spacy.matcher import PhraseMatcher
# import re
# import json
# from datetime import datetime
# import spacy
# import os

# # load pre-trained model
# base_path = os.path.dirname(__file__)


# edu_stop_heading = "skills|experience|declaration|personal|activities|projects|objective|professional|summary|history|background|internship|technical|activities|work|exposure|achievements|career"
# nlp = spacy.load('en_core_web_sm')

# matcher = Matcher(nlp.vocab)
# degree_re = "Bachelor of Engineering|B.TECH(CSE)|M.P.C|B.TECH|B.Tech|B.E.|MCA|B.E|BACHELOR OF ENGINEERING|BACHELOR DEGREE IN ENGINEERING|BACHELOR'S DEGREE IN ENGINEERING|bachelor of engineering|Bachelor Of Engineering|Bachelor's Degree in Engineering|BACHELOR'S DEGREE IN ENGINEERING|BE|B.sc|Bachelor In Technology|Bachelor of Arts|B.A|B.A.|Bachelor Degree in Arts|Bachelor Degree in English|Bachelor Degree in Economics|Bachelor Degree in History|B.A (Hons.)|Bachelor Degree in Computer Science|Bachelor of Science|B.Sc|B.Sc.|Bachelors of Sciences|BACHELOR OF SCIENCE|BACHELOR'S OF SCIENCE|BACHELOR DEGREE IN SCIENCE|B.SC.|BSCMaster of Science|M.Sc.|MSc|Master of Sciences|Master Of Science|M. Sc.|BCA|Bachelor of Computer Application|B.C.A.|Bachelor Of Computer Application|Bachelors of Computer Applications|BACHELOR OF COMPUTER APPLICATION|BACHELORS OF COMPUTER APPLICATIONS|bca|b.c.a.|Bachelor Degree in Computer Applications|Bachelor Degree in Computer Application|Bachelor's Degree|BBA|Bachelor of Business Administration|B.B.A.|B.B.A|BACHELOR OF BUSINESS ADMINISTRATION|Bachelor Of Business Administration|Bachelor Degree in Business Administration|B. Com|Bcom|Bachelor of Commerce|Bachelor of Commerce|Bachelor Degree in Commerce|Bachelor's Degree in Commerce|b.com.|B.COM.|BACHELOR OF COMMERCE|BCOM|BACHELOR DEGREE IN COMMERCE|BACHELOR'S DEGREE IN COMMERCE|Bachelor of Education|BEd|B.Ed|BACHELOR OF EDUCATION|BACHELOR'S DEGREE IN EDUCATION|B.ED.|Bachelor's Degree in Education|bachelor of education|Bachelor Of Education|Bachelor of Dental science|BDS|B.D.S.|B.D.S|BACHELOR OF DENTAL SCIENCE|Bachelor of Paramaceutical|B.Pharm|B.PHARM|B.PHARM.|L.L.B.|C.A|C.A.|Chartered Accountant|Company Secretary|C.S|Master of Law|L.L.M.|Master in Commerce|M.Com|MCom|M.Com.|MCom.|Master in Education|M.Ed.|M. Ed|Master in Edn.|Master in Pharmaceutical|M.Pharm|MPharm|Master of Pharmaceutical|Master in Arts|M.A|M A|Master of Arts|M.A.|MBA|M.B.A|M.B.A.|Master in Business Administration|Master of Business Administration|Master in Information Technology|Masters of Information Technology|Master in Information Tech.|MCA|M.C.A|M.C.A.|Master of Computer Application|Masters in Computer Application|Master in Computer Applications|Masters of Computer Applications|M.E.|M.E|Master of Engineering|Master of Engg.|Master of Engg|Master in Engineering|Graduation BE|Bachelors of Technology|B.Tech|BTech|BTech.|Bachelor of Technology|M.Tech|m.tech|Master of Technology|M.Tech.|M.tech.|M Tech|M tech|MSIT|MS(IT)|M.S.I.T.|Master of Information Technology|MS IT|M.S. IT|Phd|P.H.D|Phd.|Bachelor Of Law|B.L.|Bachelor of Law|BACHELOR OF LAW|BACHELOR DEGREE IN LAW|BACHELOR'S DEGREE IN LAW|Bachelor Degree in Law|Bachelor's Degree in Law|MSc IT|MSc (IT)|Master of Information Technology|MSC(IT)|MScIT|Master in Information Technology|Master of IT.|Master of IT|Advance Diploma In Tourism & Travel Industry Management|Diploma Course In Labour Laws And Labour Welfare|Diploma in Advertising and Public Relations|Diploma In Analytical Instrumentation|Diploma in Business Management|Diploma In Communication|Diploma in Computer Application Technology|Diploma In Computer Management|Diploma In Computer Programming|Diploma in Computer Science|Diploma In Computerised Data Processing And Management Information System|Diploma in Electronics|Diploma in Engineering|Diploma In Financial Management|Diploma In French|Diploma In German|Diploma in Human Resource Management|Diploma in Information Technology|Diploma In Managment Studies (D.M.S)|Diploma In Marketing Management|Diploma in Mechanical|Diploma in Mechatronics|Diploma in Pharmacy|Diploma In System Management|Diploma In Tourism And Travel Industry Management. (Dip. In T.T.M.)|icwa|pgdm|M.B.B.S|MBBS|MBBS.|Post Graduate Diploma in Business Management|MD/MS|B.Ped.|B.PED.|Bachelor Of Physical Education|BACHELOR OF PHYSICAL EDUCATION|M.Ped.|M.PED.|MPED.|MPED|Mped|Mped.|M.PHIL|MASTER OF PHILOSOPHY|M.Phil.|m.phil.|Master Of Philosophy|MPHIL|M.PHIL.|M.Phil|Master of Fisheries Science|M.S.F.|MSF|M.S.F|M.S.W.|m.s.w.|Master Of Social Works|Masters of Social Works|Bachelors of Information Technology|Bachelor of Information Technology|S.S.C|10th|SSC|SSLC|S.S.L.C|X standard|CBSE XII|Pre Degree|Higher Secondary Certificate H.S.C|II PUC|H.Sc|Board of Intermediate Education|H.Sc|HSC|HSE|12th|II P.U.C|Post Graduation Diploma in Banking|P.G.D.B|Bachelor of Engineering|Bachelor of Arts|Bachelor of Science|Master of Science|Bachelor of Computer Application|Bachelor of Business Application|Bachelor of Commerce|Bachelor of Education|Bachelor of Dental science|Bachelor of Paramacetuical|Law|Chartered Accountant|Company Secretary|Master of Law|Master in Commerce|Master in Education|Master in Pharmaceutical|Master in Arts|Master in Business Administration|Master in Information technology|MCA|M.E|M.Tech|Information Technology|Bachelor of Technology|B.E|B.A|B.Sc|M.Sc|B.C.A|B.B.A|B.Com|B.Ed|B.D.S|B.Pharm|L.L.B|C.A|C.S|L.L.M|M.Com|M.Ed|M.Pharm|M.A|M.B.A|M.I.T|M.C.A|M.E|M.Tech|MSIT|Ph.D|B.L|M.Sc IT|Diploma|ICWA|PGDM|MBBS|PGDCA|MD/MS|B.PED|M.PED|M.Phil|MSF|MSW|B.IT|SSLC|H.Sc|High School or Equivalent|Bachelor's Degree|Higher Degree|Master's Degree|Doctorate|P.G.D.B|B. Tech|B. Engg.|B. Engineering|B. Sc Agriculture|B. Sc BioTechnology|B. Sc Chemistry|B. Sc Computers|B. Sc Dairy Technology|B. Sc Food Technology|B. Sc Physics|B. Sc Statistics|B. Tech Aviation|B. Tech Chemical|B. Tech Civil|B. Tech Computers|B. Tech Electrical|B. Tech Electronics|B. Tech Instrumentation|B. Tech Mechanical|B. Tech Mining|B. Tech Environment|B. Tech Production|B. Tech Agriculture|B. Tech Architecture|B. Tech Automobile|B. Tech BioChemistry|B. Tech BioTechnology|B. Tech Dairy Technology|B. Tech Food Technology|B. Tech Industrial Engineering|B. Tech IT|B. Tech Fire|B. Tech Metallurgy|B. Tech Systems|B. Tech Telecommunications|B. Tech Textile|B.Com Commerce|B.Pharm Pharmacy|Bachelor in Arts Economics|Bachelor in Arts Journalism|Bachelor in Arts Literature|Bachelor in Arts Arts|Bachelor in Arts Psychology|BCA Computers|BDS Medicine|BE Aviation|BE Chemical|BE Civil|BE Computers|BE Electrical|BE Electronics|BE Instrumentation|BE Mechanical|BE Mining|BE Environment|BE Production|BE Agriculture|BE Architecture|BE Automobile|BE BioTechnology|BE Dairy Technology|BE Fire|BE Food Technology|BE Industrial Engineering|BE IT|BE Marine|BE Metallurgy|BE Textile|BSL Labour Law|CS Company Secretary|Diploma Computers|Diploma Fashion Design|Diploma Hotel Management|Diploma Management|Diploma Electronics|Diploma Automobile|Diploma Chemical|Diploma Civil|Diploma Electrical|Diploma Food Technology|Diploma IT|Diploma Instrumentation|Diploma Labour Law|Diploma Marketing|Diploma Mechanical|Diploma Production|Diploma Software|Diploma Architecture|Diploma Environment|Diploma Fire|Diploma Metallurgy|Diploma Pharmacy|Diploma Telecommunications|Diploma Training|Diploma Textile|ICWA Accounts|LLM Labour Law|M.com Commerce|M.IT IT|M.Pharm Pharmacy|M.Sc Agriculture|M.Sc BioTechnology|M.Sc Chemistry|M.Sc Computers|M.Sc Dairy Technology|M.Sc Food Technology|M.Sc Physics|M.Sc Statistics|M.Sc.Tech IT|M.Sc.Tech Textile|M.Tech Aviation|M.Tech Chemical|M.Tech Civil|M.Tech Computers|M.Tech Electrical|M.Tech Electronics|M.Tech Instrumentation|M.Tech Mechanical|M.Tech Mining|M.Tech Environment|M.Tech Production|M.Tech Textile|MA Economics|MA Journalism|MA Literature|MA Arts|MA Psychology|MBA Fire|MBA Finance|MBA Marketing|MBA Mass Communication|MBA IR|MBA Systems|MBA Manufacturing|MBBS Medicine|MCA Computers|MD/MS Medicine|ME Aviation|ME Chemical|ME Civil|ME Computers|ME Electrical|ME Electronics|ME Instrumentation|ME Mechanical|ME Mining|ME Environment|ME Production|ME Textile|MS IT IT|PGDCA Computers|PGDM Fire|PGDM Finance|PGDM IR|PGDM Marketing|PGDM Mass Communication|PGDM Systems|Ph.D Agriculture|Ph.D Mass Communication|Ph.D Fire|Ph.D Architecture|Ph.D Aviation|Ph.D BioTechnology|Ph.D Chemistry|Ph.D Civil|Ph.D Commerce|Ph.D Computers|Ph.D Dairy Technology|Ph.D Economics|Ph.D Electrical|Ph.D Electronics|Ph.D Fashion Design|Ph.D Finance|Ph.D Food Technology|Ph.D Hotel Management|Ph.D Journalism|Ph.D Labour Law|Ph.D Literature|Ph.D Mechanical|Ph.D Medicine|Ph.D Pharmacy|Ph.D Physics|Ph.D Psychology|Ph.D Software|Ph.D Statistics|Ph.D Telecommunications|B.E|B.A|B.Sc|M.Sc|B.C.A|B.B.A|B.Com|B.Ed|B.D.S|B.Pharm|L.L.B|C.A|C.S|L.L.M|M.Com|M.Ed|M.Pharm|M.A|M.B.A|M.I.T|M.C.A|M.E|M.Tech|MSIT|Ph.D|B.L|M.Sc IT|Diploma|ICWA|PGDM|MBBS|PGDCA|MD/MS|B.PED|M.PED|M.Phil|B.IT|SSLC|ICSE|CBSE|C.B.S.E|I.S.C.|ISC|S.S.L.C|I.C.S.E."
# only_college_degree ="Bachelor of Engineering|B.TECH(CSE)|B.TECH|B.Tech|B.E.|MCA|B.E|BACHELOR OF ENGINEERING|BACHELOR DEGREE IN ENGINEERING|BACHELOR'S DEGREE IN ENGINEERING|bachelor of engineering|Bachelor Of Engineering|Bachelor's Degree in Engineering|BACHELOR'S DEGREE IN ENGINEERING|BE|B.sc|Bachelor In Technology|Bachelor of Arts|B.A|B.A.|Bachelor Degree in Arts|Bachelor Degree in English|Bachelor Degree in Economics|Bachelor Degree in History|B.A (Hons.)|Bachelor Degree in Computer Science|Bachelor of Science|B.Sc|B.Sc.|Bachelors of Sciences|BACHELOR OF SCIENCE|BACHELOR'S OF SCIENCE|BACHELOR DEGREE IN SCIENCE|B.SC.|BSCMaster of Science|M.Sc.|MSc|Master of Sciences|Master Of Science|M. Sc.|BCA|Bachelor of Computer Application|B.C.A.|Bachelor Of Computer Application|Bachelors of Computer Applications|BACHELOR OF COMPUTER APPLICATION|BACHELORS OF COMPUTER APPLICATIONS|bca|b.c.a.|Bachelor Degree in Computer Applications|Bachelor Degree in Computer Application|Bachelor's Degree|BBA|Bachelor of Business Administration|B.B.A.|B.B.A|BACHELOR OF BUSINESS ADMINISTRATION|Bachelor Of Business Administration|Bachelor Degree in Business Administration|B. Com|Bcom|Bachelor of Commerce|Bachelor of Commerce|Bachelor Degree in Commerce|Bachelor's Degree in Commerce|b.com.|B.COM.|BACHELOR OF COMMERCE|BCOM|BACHELOR DEGREE IN COMMERCE|BACHELOR'S DEGREE IN COMMERCE|Bachelor of Education|BEd|B.Ed|BACHELOR OF EDUCATION|BACHELOR'S DEGREE IN EDUCATION|B.ED.|Bachelor's Degree in Education|bachelor of education|Bachelor Of Education|Bachelor of Dental science|BDS|B.D.S.|B.D.S|BACHELOR OF DENTAL SCIENCE|Bachelor of Paramaceutical|B.Pharm|B.PHARM|B.PHARM.|L.L.B.|C.A|C.A.|Chartered Accountant|Company Secretary|C.S|Master of Law|L.L.M.|Master in Commerce|M.Com|MCom|M.Com.|MCom.|Master in Education|M.Ed.|M. Ed|Master in Edn.|Master in Pharmaceutical|M.Pharm|MPharm|Master of Pharmaceutical|Master in Arts|M.A|M A|Master of Arts|M.A.|MBA|M.B.A|M.B.A.|Master in Business Administration|Master of Business Administration|Master in Information Technology|Masters of Information Technology|Master in Information Tech.|MCA|M.C.A|M.C.A.|Master of Computer Application|Masters in Computer Application|Master in Computer Applications|Masters of Computer Applications|M.E.|M.E|Master of Engineering|Master of Engg.|Master of Engg|Master in Engineering|Graduation BE|Bachelors of Technology|B.Tech|BTech|BTech.|Bachelor of Technology|M.Tech|m.tech|Master of Technology|M.Tech.|M.tech.|M Tech|M tech|MSIT|MS(IT)|M.S.I.T.|Master of Information Technology|MS IT|M.S. IT|Phd|P.H.D|Phd.|Bachelor Of Law|B.L.|Bachelor of Law|BACHELOR OF LAW|BACHELOR DEGREE IN LAW|BACHELOR'S DEGREE IN LAW|Bachelor Degree in Law|Bachelor's Degree in Law|MSc IT|MSc (IT)|Master of Information Technology|MSC(IT)|MScIT|Master in Information Technology|Master of IT.|Master of IT|Advance Diploma In Tourism & Travel Industry Management|Diploma Course In Labour Laws And Labour Welfare|Diploma in Advertising and Public Relations|Diploma In Analytical Instrumentation|Diploma in Business Management|Diploma In Communication|Diploma in Computer Application Technology|Diploma In Computer Management|Diploma In Computer Programming|Diploma in Computer Science|Diploma In Computerised Data Processing And Management Information System|Diploma in Electronics|Diploma in Engineering|Diploma In Financial Management|Diploma In French|Diploma In German|Diploma in Human Resource Management|Diploma in Information Technology|Diploma In Managment Studies (D.M.S)|Diploma In Marketing Management|Diploma in Mechanical|Diploma in Mechatronics|Diploma in Pharmacy|Diploma In System Management|Diploma In Tourism And Travel Industry Management. (Dip. In T.T.M.)|icwa|pgdm|M.B.B.S|MBBS|MBBS.|Post Graduate Diploma in Business Management|MD/MS|B.Ped.|B.PED.|Bachelor Of Physical Education|BACHELOR OF PHYSICAL EDUCATION|M.Ped.|M.PED.|MPED.|MPED|Mped|Mped.|M.PHIL|MASTER OF PHILOSOPHY|M.Phil.|m.phil.|Master Of Philosophy|MPHIL|M.PHIL.|M.Phil|Master of Fisheries Science|M.S.F.|MSF|M.S.F|M.S.W.|m.s.w.|Master Of Social Works|Masters of Social Works|Bachelors of Information Technology|Bachelor of Information Technology|S.S.C|10th|SSC|SSLC|S.S.L.C|X standard|CBSE XII|Pre Degree|Higher Secondary Certificate H.S.C|II PUC|H.Sc|Board of Intermediate Education|H.Sc|HSC|HSE|12th|II P.U.C|Post Graduation Diploma in Banking|P.G.D.B|Bachelor of Engineering|Bachelor of Arts|Bachelor of Science|Master of Science|Bachelor of Computer Application|Bachelor of Business Application|Bachelor of Commerce|Bachelor of Education|Bachelor of Dental science|Bachelor of Paramacetuical|Law|Chartered Accountant|Company Secretary|Master of Law|Master in Commerce|Master in Education|Master in Pharmaceutical|Master in Arts|Master in Business Administration|Master in Information technology|MCA|M.E|M.Tech|Information Technology|Bachelor of Technology|B.E|B.A|B.Sc|M.Sc|B.C.A|B.B.A|B.Com|B.Ed|B.D.S|B.Pharm|L.L.B|C.A|C.S|L.L.M|M.Com|M.Ed|M.Pharm|M.A|M.B.A|M.I.T|M.C.A|M.E|M.Tech|MSIT|Ph.D|B.L|M.Sc IT|Diploma|ICWA|PGDM|MBBS|PGDCA|MD/MS|B.PED|M.PED|M.Phil|MSF|MSW|B.IT|SSLC|H.Sc|High School or Equivalent|Bachelor's Degree|Higher Degree|Master's Degree|Doctorate|P.G.D.B|B. Tech|B. Engg.|B. Engineering|B. Sc Agriculture|B. Sc BioTechnology|B. Sc Chemistry|B. Sc Computers|B. Sc Dairy Technology|B. Sc Food Technology|B. Sc Physics|B. Sc Statistics|B. Tech Aviation|B. Tech Chemical|B. Tech Civil|B. Tech Computers|B. Tech Electrical|B. Tech Electronics|B. Tech Instrumentation|B. Tech Mechanical|B. Tech Mining|B. Tech Environment|B. Tech Production|B. Tech Agriculture|B. Tech Architecture|B. Tech Automobile|B. Tech BioChemistry|B. Tech BioTechnology|B. Tech Dairy Technology|B. Tech Food Technology|B. Tech Industrial Engineering|B. Tech IT|B. Tech Fire|B. Tech Metallurgy|B. Tech Systems|B. Tech Telecommunications|B. Tech Textile|B.Com Commerce|B.Pharm Pharmacy|Bachelor in Arts Economics|Bachelor in Arts Journalism|Bachelor in Arts Literature|Bachelor in Arts Arts|Bachelor in Arts Psychology|BCA Computers|BDS Medicine|BE Aviation|BE Chemical|BE Civil|BE Computers|BE Electrical|BE Electronics|BE Instrumentation|BE Mechanical|BE Mining|BE Environment|BE Production|BE Agriculture|BE Architecture|BE Automobile|BE BioTechnology|BE Dairy Technology|BE Fire|BE Food Technology|BE Industrial Engineering|BE IT|BE Marine|BE Metallurgy|BE Textile|BSL Labour Law|CS Company Secretary|Diploma Computers|Diploma Fashion Design|Diploma Hotel Management|Diploma Management|Diploma Electronics|Diploma Automobile|Diploma Chemical|Diploma Civil|Diploma Electrical|Diploma Food Technology|Diploma IT|Diploma Instrumentation|Diploma Labour Law|Diploma Marketing|Diploma Mechanical|Diploma Production|Diploma Software|Diploma Architecture|Diploma Environment|Diploma Fire|Diploma Metallurgy|Diploma Pharmacy|Diploma Telecommunications|Diploma Training|Diploma Textile|ICWA Accounts|LLM Labour Law|M.com Commerce|M.IT IT|M.Pharm Pharmacy|M.Sc Agriculture|M.Sc BioTechnology|M.Sc Chemistry|M.Sc Computers|M.Sc Dairy Technology|M.Sc Food Technology|M.Sc Physics|M.Sc Statistics|M.Sc.Tech IT|M.Sc.Tech Textile|M.Tech Aviation|M.Tech Chemical|M.Tech Civil|M.Tech Computers|M.Tech Electrical|M.Tech Electronics|M.Tech Instrumentation|M.Tech Mechanical|M.Tech Mining|M.Tech Environment|M.Tech Production|M.Tech Textile|MA Economics|MA Journalism|MA Literature|MA Arts|MA Psychology|MBA Fire|MBA Finance|MBA Marketing|MBA Mass Communication|MBA IR|MBA Systems|MBA Manufacturing|MBBS Medicine|MCA Computers|MD/MS Medicine|ME Aviation|ME Chemical|ME Civil|ME Computers|ME Electrical|ME Electronics|ME Instrumentation|ME Mechanical|ME Mining|ME Environment|ME Production|ME Textile|MS IT IT|PGDCA Computers|PGDM Fire|PGDM Finance|PGDM IR|PGDM Marketing|PGDM Mass Communication|PGDM Systems|Ph.D Agriculture|Ph.D Mass Communication|Ph.D Fire|Ph.D Architecture|Ph.D Aviation|Ph.D BioTechnology|Ph.D Chemistry|Ph.D Civil|Ph.D Commerce|Ph.D Computers|Ph.D Dairy Technology|Ph.D Economics|Ph.D Electrical|Ph.D Electronics|Ph.D Fashion Design|Ph.D Finance|Ph.D Food Technology|Ph.D Hotel Management|Ph.D Journalism|Ph.D Labour Law|Ph.D Literature|Ph.D Mechanical|Ph.D Medicine|Ph.D Pharmacy|Ph.D Physics|Ph.D Psychology|Ph.D Software|Ph.D Statistics|Ph.D Telecommunications|B.E|B.A|B.Sc|M.Sc|B.C.A|B.B.A|B.Com|B.Ed|B.D.S|B.Pharm|L.L.B|C.A|C.S|L.L.M|M.Com|M.Ed|M.Pharm|M.A|M.B.A|M.I.T|M.C.A|M.E|M.Tech|MSIT|Ph.D|B.L|M.Sc IT|Diploma|ICWA|PGDM|MBBS|PGDCA|MD/MS|B.PED|M.PED|M.Phil|B.IT"
# file = os.path.join(base_path,"degrees.txt")
# file = open(file, "r", encoding='utf-8')
# degree = [line.strip().lower() for line in file]
# degreematcher = PhraseMatcher(nlp.vocab)
# patterns = [nlp.make_doc(text) for text in degree if len(nlp.make_doc(text)) < 10]
# degreematcher.add("Degree title", None, *patterns)


# edu_degree_head = ["associate", "bachelor", "master", "doctoral","B.E","Btech","BTech","B.Tech","BSc","MS","B.A.","BCA","B.D.S","L.L.B.","C.S.","M.Com.","M.Ed.","M.Pharm","M.C.A.","M.E","Phd","M.Phil.","MBBS","SSLC","PGDM","M.IT","ICWA","MCA","BCA","Bachelor of Engineering","Bachelor of Technology"]

# edu_uni_head = ["school", "university", "college" , "institute"]



# def extract_edu_section(terms, index_edu, heading_index):
#     # temp_index_edu = index_edu
#     # edu_sec_text = []
#     # try:
#     #     index_edu = heading_index.index(index_edu)
#     # except:
#     #     index_edu = 0
#     # """ for i in range(heading_index[index_edu],heading_index[index_edu+1]):
#     #     edu_sec_text.append(terms[i])"""
#     # chk = 0

#     # try: 
#     #     for i in range(temp_index_edu, temp_index_edu+3):
#     #         if i in heading_index:
#     #             chk = 1
#     # except: 
#     #     a =0 

#     # try :
#     #     leng= len(terms)
#     #     if ((temp_index_edu+1) not in heading_index) or ((temp_index_edu+2) not in  heading_index ) and chk ==0 :
#     #         for i in range(heading_index[index_edu],heading_index[index_edu+1]):
#     #             edu_sec_text.append(terms[i])
#     #     else :
#     #         try: 
#     #             for i in range(heading_index[index_edu],heading_index[index_edu+2]):
#     #                 edu_sec_text.append(terms[i])
#     #         except: 
#     #             for i in range(heading_index[index_edu],leng-2):
#     #                 edu_sec_text.append(terms[i])

#     # except :
#     #     a =0
#     # all_terms_text = ""
#     # for i in range(len(terms)):
#     #     all_terms_text = all_terms_text + " "+terms[i][2]
#     edu_sec_text = []
#     all_terms_text=""
#     edu_heads= "Education|EDUCATION|ACADEMIC|Academic|ACADEMIA|Academia|EDU|Edu|Academ|ACADEM"
#     for i in range(len(terms)):
#         if len(terms[i][2].split())<5 or terms[i][2].isupper() :
#             if re.search(edu_heads,terms[i][2]):
#                 j=i
#                 while len(all_terms_text.split())<80:
#                     try:
#                         temp = terms[j][2]
#                         if len(temp.split())<6 and len(temp.split())>0:
#                             temp  =temp.lower()
#                             if re.search(edu_stop_heading,temp) :
#                                 # print(terms[j][2])
#                                 break
#                         edu_sec_text.append(terms[j])
#                         all_terms_text = all_terms_text + " " +terms[j][2]
#                         j= j+1
#                     except:
#                         break


#     if len(all_terms_text.split())<5:
#         all_terms_text=""
#         edu_sec_text =[]
#         temp_index_edu = index_edu
#         edu_sec_text = []
#         try:
#             index_edu = heading_index.index(index_edu)
#         except:
#             index_edu = 0
#         """ for i in range(heading_index[index_edu],heading_index[index_edu+1]):
#             edu_sec_text.append(terms[i])"""
#         chk = 0

#         try: 
#             for i in range(temp_index_edu, temp_index_edu+3):
#                 if i in heading_index:
#                     chk = 1
#         except: 
#             a =0 

#         try :
#             leng= len(terms)
#             if ((temp_index_edu+1) not in heading_index) or ((temp_index_edu+2) not in  heading_index ) and chk ==0 :
#                 for i in range(heading_index[index_edu],heading_index[index_edu+1]):
#                     edu_sec_text.append(terms[i])
#             else :
#                 try: 
#                     for i in range(heading_index[index_edu],heading_index[index_edu+2]):
#                         edu_sec_text.append(terms[i])
#                 except: 
#                     for i in range(heading_index[index_edu],leng-2):
#                         edu_sec_text.append(terms[i])

#         except :
#             a =0
#         all_terms_text = ""
#         for i in range(len(terms)):
#             all_terms_text = all_terms_text + " "+terms[i][2]

#         #print("edutext:",all_terms_text)

#     return edu_sec_text, all_terms_text 


# def edu_section_details(terms, index_edu, heading_index):
#     sec_data =[]
#     total_duration = []
#     sec_data, all_terms_text= extract_edu_section(terms, index_edu, heading_index)
#     # print("Education data:",sec_data)
#     education_dict = {"edu_text":[],"degree":[], "university":[],"dates" : {"date_tags":[], "duration":""},"grade": []}
  

#     degree_names=[]
#     # degree_names = extract_degree_name(all_terms_text)
#     # education_dict["degree"].append(degree_names)
#     if sec_data:
#         for i in range(len(sec_data)):
#             if len(sec_data[i][2].split(" ")) > 2 :
#                 try:
#                     exclude_degree_names=["CAS","ISSECT","BIES","BRA","BJEC","MMA","CLA","CSS","bca","BILI","BOA","CDA"]
#                     text = re.sub('[^A-Za-z0-9. ]+', ' ', sec_data[i][2])
#                     if re.search(degree_re,sec_data[i][2]):
#                         if re.search(degree_re,sec_data[i][2])[0] not in exclude_degree_names:
#                             education_dict['degree'].append (re.search(degree_re,sec_data[i][2])[0])
#                 except : 
#                     a = 0
#     if sec_data:
#         edu_text = ""
#         for i in range(len(sec_data)):
#             edu_text = edu_text +" "+ sec_data[i][2]
#             if len(sec_data[i][2].split(" ")) > 2 :
#                 try:
#                     #Extract Degree Name
#                     # score = process.extractOne(sec_data[i][2], edu_degree_head)[1]
#                     # if score>80:
#                     #     for k in range(len(edu_degree_head)):
#                     #         if edu_degree_head[k] in sec_data[i][2]:
#                     #             education_dict["degree"].append(sec_data[i][2])
#                     #Extract University Name
#                     score = process.extractOne(sec_data[i][2], edu_uni_head)[1]
#                     if score>61:
#                         education_dict["university"].append(sec_data[i][2])
#                 except:
#                     a = 0

#                 try:
#                     #Extract CGPA or Percentage
#                     if re.search(r'([0-9][.][0-9]*|[0-9]{2}[.][0-9]*|[0-9]{2}%)', sec_data[i][2], flags=0):
#                         match = re.search(r'([0-9][.][0-9]*|[0-9]{2}[.][0-9]*|[0-9]{2}%)', sec_data[i][2], flags=0).group(1)
#                         education_dict["grade"].append(match)
#                 except:
#                     a = 0

#         try:
#             total_duration,dates_years = duration_func(all_terms_text)
#             education_dict["dates"]["duration"] = str(total_duration)
#             education_dict["dates"]["date_tags"] = (dates_years)
#         except:
#             education_dict["dates"]["duration"] = ""
#             education_dict["dates"]["date_tags"] = []

#         education_dict['edu_text'].append(all_terms_text)


#         #print("______________________________-end of Edu________________________")
#         return education_dict


# def extract_degree_name(text):
#     degrees=[]

#     text = re.sub('[^A-Za-z0-9. ]+', ' ', text)
#     nlp_new = nlp(text)
#     matches = degreematcher(nlp_new)

#     for match_id, start, end in matches:
#         span = nlp_new[start:end]
#         degrees.append(span.text)

#     degrees = list(set(degrees))
#     return degrees


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
#     for i in range(len(res)):
#         for j in range(len(today_words)):
#             if today_words[j] in res[i]:
#                 years_only.append("2021")
#         if res[i].isdigit() or res[i] in month_full_names:
#             only_words.append(res[i])
#             #print(res[i])
#             if res[i].isdigit():
#                 if int(res[i])>2000 and int(res[i])<2022:
#                     years_only.append(res[i])
#                 elif res[i-1].isalpha() and (res[i-1] in month_abbre_names or res[i-1] in month_full_names):
#                     temp = int(res[i])
#                     if temp > 0 and temp < 22:
#                         temp = 2000+ temp
#                         years_only.append(str(temp))
#     new_years_only=[]
#     for i in range(len(years_only)):
#         if int(years_only[i])>2005:
#             new_years_only.append(years_only[i])


#     years_only = new_years_only
#     years_only =list(set(years_only))
#     temp_years_only =years_only
#     years_only.sort()
#     try :
#         if years_only:
#             start = int(years_only[0])
#             last = int(years_only[len(years_only)-1])
#             duration = last -start
#             if last==start:
#                 duration = 1
#             return duration,temp_years_only
#     except:
#         return 1,temp_years_only