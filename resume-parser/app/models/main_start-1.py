# from .skills_extract import extract_skills
# from .personal_info import personal_info_extract
# from .extract_education import edu_section_details
# from .extract_experience import exp_component_extract
# from .section_headings import extract_target_headings
# from operator import itemgetter
# import fitz
# import _thread
# #from docx2pdf import convert
# import os
# import json
# from multiprocessing import Process


# def flags_decomposer(flags):
#     """Make font flags human readable."""
#     l = []

#     if flags & 2 ** 4:
#         l.append("bold")
#     return ", ".join(l)

# def line_extractor(doc):
#     bold_identifier  = []
#     font_sizes = []
#     latex_id = 0
#     exclude_list = ["CV", "Curriculum Vitae", "Resume","bio-data","biodata","CURRICULUM VITAE"]
#     terms = []
#     if "pdfTeX" in doc.metadata['producer'] or "LaTeX" in doc.metadata['creator']:
#         latex_id = 1
#     for page in doc:
#         # read page text as a dictionary, suppressing extra spaces in CJK fonts
#         blocks = page.getText("dict", flags=11)["blocks"]
#         #print(len(blocks))
#         if latex_id == 1 :
#             text_block = page.getText("blocks")
#             #print(len(text_block))
#             #print("text-block \n \n",text_block)
#             if len(text_block)== len(blocks):
#                 for i in range(len(blocks)):
#                     temp = text_block[i]
#                     text_temp = str(temp[4])
                    
#                     blocks[i]['lines'][0]['spans'][0]['text']=temp[4]
#         #print("blocks \n \n \n",blocks)
#         # print(len(text_block_only))
#         for b in blocks:  # iterate through the text blocks
#             for l in b["lines"]:  # iterate through the text lines
#                 for s in l["spans"]:  # iterate through the text spans
                    
#                         term = []
#                         font_properties =  (
#                             #s["font"],  # font name
#                             flags_decomposer(s["flags"]),  # readable font flags
#                             s["size"],  # font size
#                             #s["color"],  # font color
#                             s["bbox"]
#                         )
#                         if s["text"] != " " and  s["text"] not in exclude_list:

#                             #print("Text: '%s'" % s["text"])  # simple print of text
#                             #print(font_properties)
#                             term.append(font_properties[0])
#                             term.append(font_properties[1])
#                             font_sizes.append(font_properties[1])
#                             term.append(str(s["text"]))
#                             term.append(font_properties[2])
#                             #print(term)
#                             terms.append(term)

#     #print(terms)
#     font_sizes.sort(reverse=True)
#     #print(font_sizes)

#     sizes  = list(set(font_sizes))
#     sizes.sort(reverse=True)
#     sizes_count= []

#     for i in range(len(sizes)):
#         temp = []
#         temp.append(sizes[i])
#         temp.append(font_sizes.count(sizes[i]))
#         sizes_count.append(temp)
#     sizes = sizes_count
#     #print(sizes)
#     #print(terms)
#     return terms,sizes

# def main_in_file(doc):
#     # filename = "/Users/karthik/Rp-Parser_complete/RP-VScod3V3/mg.pdf"
#     # stream = open(filename, "rb").read()
#     # doc = fitz.open("pdf", stream)
#     # ,"document_structure":[],"text": []

#     resume_json = {"personal_info":[],"education":[],"experience":[],"skills":[],"other_sections":[]}    
#     terms,sizes = line_extractor(doc)
#     length_terms = len(terms)-2
#     resume_text = ""
#     # Remove duplicate terms
#     for i in range(length_terms):
#         resume_text += (str(terms[i][2]))
#         if terms[i][2] == terms[i+1][2]:
#             terms.pop((i+1))

    
#     index_edu,index_exp,heading_index = extract_target_headings(terms, sizes)
#     #print(heading_index)

#     #for i in heading_index:
#         #print(terms[i])
#     last_index = int(len(terms)-1)
#     heading_index.append(last_index)


#     personal_info_json = personal_info_extract(terms,heading_index)
#     edu_info_json = edu_section_details(terms, index_edu, heading_index)
#     exp_info_json = exp_component_extract(terms, index_exp, heading_index)
#     heading_names = []
#     section_details = {}
#     for i in range(len(heading_index)):
#         heading_names.append(terms[heading_index[i]][2])
#         # if i != index_edu or i != index_exp:
#         #     section_text  =  terms[heading_index[i]: [heading_index[i+1]]

#     skills_json = extract_skills(resume_text)

#     resume_json["personal_info"].append(personal_info_json)
#     resume_json["education"].append(edu_info_json)
#     resume_json["experience"].append(exp_info_json)
#     resume_json["skills"].append(skills_json)
#     resume_json["other_sections"].append(heading_names)
#     #resume_json["text"].append(resume_text)
#     #resume_json["document_structure"].append(terms)

#     #print(resume_json)
#     return resume_json

