import spacy
from spacy.matcher import Matcher
from spacy.matcher import PhraseMatcher
import os 
import re 
from datetime import date
import pickle

# load pre-trained model
base_path = os.path.dirname(__file__)

nlp = spacy.load('en_core_web_sm')

matcher = Matcher(nlp.vocab)

file = os.path.join(base_path,"skills_2.txt")
file = open(file, "r", encoding='utf-8')
skill = [line.strip().lower() for line in file]
skillsmatcher = PhraseMatcher(nlp.vocab)
patterns = [nlp.make_doc(text) for text in skill if len(nlp.make_doc(text)) < 10]
skillsmatcher.add("Job title", None, *patterns)




def extract_skills(text, skill_text):
    skills = []
    converted_list=[]
    converted_text=""
    __nlp = nlp(text)
        # Only run nlp.make_doc to speed things up

    for token in __nlp:
            if not token.is_stop and not token.is_punct:
                if token.pos_ == "NOUN" or token.pos_ == "PROPN" :
                    if token.text  not in converted_list:

                        converted_list.append(token.text)
                        converted_text += " "+token.text
    extra_skills =[]
    nlp_new = nlp(converted_text.lower())
    matches = skillsmatcher(nlp_new)
    for match_id, start, end in matches:
        span = nlp_new[start:end]
        skills.append( span.text)
    
    priority_skills = []
    #print(skill_text)
    try:
        nlp_new = nlp(skill_text.lower())
        matches = skillsmatcher(nlp_new)
        for match_id, start, end in matches:
            span = nlp_new[start:end]
            priority_skills.append( span.text)
    except:
        priority_skills =[]
    

    priority_skills = list(set(priority_skills))
    skills = list(set(skills))
    return skills,priority_skills


def extract_section_skills(text):
    text = re.sub('[^A-Za-z0-9]+', ' ', text)
    res = text.split()
    return res 


def workex_extract_skills(text):
    skills = []
    converted_list=[]
    converted_text=""
    __nlp = nlp(text)
        # Only run nlp.make_doc to speed things up

    for token in __nlp:
            if not token.is_stop and not token.is_punct:
                if token.pos_ == "NOUN" or token.pos_ == "PROPN" :
                    if token.text  not in converted_list:

                        converted_list.append(token.text)
                        converted_text += " "+token.text
    extra_skills =[]
    nlp_new = nlp(converted_text.lower())
    matches = skillsmatcher(nlp_new)
    for match_id, start, end in matches:
        span = nlp_new[start:end]
        skills.append( span.text)
    
    skills = list(set(skills))
    return skills