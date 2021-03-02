      string[] tDataLines = ResumeConvertedText.Split(cSETASCII, StringSplitOptions.RemoveEmptyEntries);
                SubDataLines = ResumeText.Split(cSETASCII, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                for (int i = tDataLines.GetLowerBound(0); i <= tDataLines.GetUpperBound(0); i++)
                {

                    if (tDataLines[i].Trim() != "" && tDataLines[i].Trim() != "_" && tDataLines[i].Trim() != "ü")
                        count += 1;

                }
                DataEmployer = ResumeConvertedText.Split(chrArrayASCII);
                for (int cnt = 0; cnt < DataEmployer.Length; cnt++)
                    DataEmployer[cnt] = DataEmployer[cnt].Replace("*", " ").Replace(Convert.ToChar(10).ToString(), " ").Trim();

                DataLines = new string[count];
                count = 0;
                for (int i = 0; i <= tDataLines.GetUpperBound(0); i++)
                {
                    if (tDataLines[i].Trim() != "" && tDataLines[i].Trim() != "_" && tDataLines[i].Trim() != "ü")
                    {
                        if (tDataLines[i].Trim().IndexOf("@") >= 0)
                            DataLines[count] = tDataLines[i].Replace("\a", "").Replace("ü", "").Replace("Ø", "").Replace(Convert.ToChar(160).ToString(), Convert.ToChar(32).ToString()).ToString();
                        else
                            DataLines[count] = tDataLines[i].Replace("_", "").Replace("\a", "").Replace("ü", "").Replace("Ø", "").Replace(Convert.ToChar(160).ToString(), Convert.ToChar(32).ToString()).ToString().Replace(Convert.ToChar(128).ToString(), Convert.ToChar(32).ToString()).ToString().Replace(Convert.ToChar(147).ToString(), Convert.ToChar(32).ToString()).ToString();
                        if (DataLines[count].ToLower().StartsWith("|") || DataLines[count].ToLower().StartsWith("[") || DataLines[count].ToLower().StartsWith("*"))
                            DataLines[count] = DataLines[count].Remove(0, 1);
                        count += 1;
                    }
                }


private void GetName()
        {
 
            fname = "";
            string[] TempDataLines = null;
            Boolean tNameSearchFrmLast = false;
            string tNameFrmFirst = "";
            string[] strArrayName = new string[DataLines.Length];
            for (int cnt = 0; cnt < DataLines.Length; cnt++)
                strArrayName[cnt] = DataLines[cnt];

            Regex rgxWordsExclude = new Regex("[a-z]{2,}", RegexOptions.IgnoreCase);
            string[] strArraySplit = null;
            char[] _strSplitSpace = { '?', Convert.ToChar(9) };
            Regex rgxCon = new Regex(@"\s{4,}:");
            ArrayList strArrayList = new ArrayList();
            for (int cnt = 0; cnt < DataLines.Length; cnt++)
            {
                if (rgxCon.IsMatch(DataLines[cnt]))
                    strArrayName[cnt] = strArrayName[cnt].Replace(rgxCon.Match(DataLines[cnt]).ToString(), ":");
                strArrayName[cnt] = strArrayName[cnt].Replace("              ", "?").Replace("             ", "?").Replace(Convert.ToChar(8221).ToString(), "?");

                //Array Splitting Based on Hosrizontal Tab Order ... 01 / Feb / 2013 
                strArraySplit = strArrayName[cnt].Split(_strSplitSpace);
                for (int cnt1 = 0; cnt1 < strArraySplit.Length; cnt1++)
                    if (strArraySplit[cnt1].Replace("?", "").Replace("_", "").Replace(":", "").Replace("-", "").Trim().Length > 1)
                        strArrayList.Add(strArraySplit[cnt1].Trim().ToString());
            }
            int _count = 0;
            TempDataLines = new string[strArrayList.Count];
            foreach (string str in strArrayList)
            {
                string tData = str;
                if (tData.ToLower().Trim() == "age")
                    tData = "";
                if (tData.ToLower().Trim().IndexOf(" age ") > 0 || tData.ToLower().Trim().IndexOf("(age: ") > 0)
                {
                    string[] sp = { " age ", " Age ", ",", " AGE ", "(age:", "(Age:", "(AGE:" };
                    string[] sp1 = str.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                    if (sp1.Length > 0)
                        tData = sp1[0];
                }
                if (tData.ToLower().Trim().IndexOf("(mrs)") >= 0 || tData.ToLower().Trim().IndexOf("(mr.)") >= 0)
                    tData = tData.Replace("(Mrs)", "").Replace("(mrs)", "").Replace("(Mr.)", "").Replace("(mr.)", "").Replace("(MR.)", "");
                if (tData.ToLower().Trim().StartsWith("|") || tData.ToLower().Trim().StartsWith("[") || tData.ToLower().Trim().StartsWith("*"))
                    tData = tData.Remove(0, 1);
                if (tData.ToLower().Trim().EndsWith("]") || tData.ToLower().Trim().EndsWith("|") || tData.ToLower().Trim().EndsWith("*"))
                    tData = tData.Substring(0, tData.Length - 1);
                TempDataLines[_count] = tData.Replace("ï»¿", "").Trim();
                _count++;
            }
            for (int nCntTemp = 0; nCntTemp < TempDataLines.Length; nCntTemp++)
            {
                for (int iNameCnt = 0; iNameCnt < appSet.Length; iNameCnt++)
                {
                    if (TempDataLines[nCntTemp].ToLower().Trim().StartsWith(appSet[iNameCnt].ToLower().Trim()))
                        TempDataLines[nCntTemp] = "";//TempDataLines[nCntTemp].ToLower().Trim().Replace((strArrNameBk[iNameCnt].ToLower().Trim()),"");

                }
                if (TempDataLines[nCntTemp].ToLower().Trim().StartsWith("|") || TempDataLines[nCntTemp].ToLower().Trim().StartsWith("[") || TempDataLines[nCntTemp].ToLower().Trim().StartsWith("*"))
                    TempDataLines[nCntTemp] = TempDataLines[nCntTemp].Remove(0, 1);
                if (TempDataLines[nCntTemp].ToLower().Trim().EndsWith("]") || TempDataLines[nCntTemp].ToLower().Trim().EndsWith("|") || TempDataLines[nCntTemp].ToLower().Trim().EndsWith("*"))
                    TempDataLines[nCntTemp] = TempDataLines[nCntTemp].Substring(0, TempDataLines[nCntTemp].Length - 1);
            }


            int nCntTempRem = 0;
            if (TempDataLines[nCntTempRem].ToLower().Trim().StartsWith("remarks :") || TempDataLines[nCntTempRem].ToLower().Trim().StartsWith("i will look on"))
                TempDataLines[nCntTempRem] = "";

            int iJK = 0;
            for (int iNN = 0; iNN < TempDataLines.Length - 1; iNN++)
            {
                if (TempDataLines[iNN].Trim() != "")
                {
                    TempDataLines[iJK] = TempDataLines[iNN];
                    iJK++;
                }
            }
            string tName = "";
            string[] add;

            //LABEL STARTS FROM HERE ******************************
            //LABEL---------01   Done

            //LABEL---------01   Done
            #region "Check by Candidate Name /Name"
            try
            {
                Match mtchCandidateName;
                StringBuilder _strbName = null;
                Regex rgxLastName = new Regex(@"^.{0,2}\s{0,4}\b(Last\s{0,3}Name|Sir\s{0,3}Name|Family\s{0,3}Name|Middle\s{0,3}Name)\b", RegexOptions.IgnoreCase);
                Regex rgxCvOf = new Regex(@"^((Curriculum|Curricullum|Curruculum|Currucullum|Curicullum|Curucullum|Curiculum|Curuculum|DOSSIER)\s+(Vitie|Vitae) Of|(Profile|Resume|RÉSUMÉ|RESUMÉ|DOSSIER) Of|CV Of|Resume for|C\.V\. Of|Resume\s*[:\-\–][:\-\–]?)(\b|\s+)[“]?[a-z\.\s]{3,30}\b", RegexOptions.IgnoreCase);
                Regex rgxResumeOf = new Regex(@"^((Curriculum|Curricullum|Curruculum|Currucullum|Curicullum|Curucullum|Curiculum|Curuculum|DOSSIER)\s+(Vitie|Vitae) Of|(Profile|Resume|RÉSUMÉ|RESUMÉ|DOSSIER) Of|CV Of|Resume for|C\.V\. Of|Resume\s*[:\-\–][:\-\–]?)(\b|\s+)[“]?", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                Regex rgxCandidateName = new Regex(@"[0-9\.\)\s]{0,3}\s*((Name|Full Name)\s(of the applicant|of applicant|of the candidate|of candidate)|My Full Name|(Candidate|Applicant|Consultant|Employee)\s?[']?\s?(Name|Full Name)|First Name)[']?[s]?[\s\t]*([,\-]|\s{5}|:)", RegexOptions.IgnoreCase);
                for (int cnt = 0; cnt < TempDataLines.Length && tName.Trim().Length < 2; cnt++)
                {
                    TempDataLines[cnt] = TempDataLines[cnt].Replace("*", " ");
                    if (rgxCvOf.IsMatch(TempDataLines[cnt].Trim().ToString()) && cnt < 10 && TempDataLines[cnt].Trim().Length < 70)
                    {
                        _strbName = new StringBuilder();
                        _strbName.Append(rgxCvOf.Match(TempDataLines[cnt].Trim().ToString()).ToString());
                        if (rgxResumeOf.IsMatch(_strbName.ToString()))
                            _strbName.Remove(0, rgxResumeOf.Match(_strbName.ToString()).Index + rgxResumeOf.Match(_strbName.ToString()).Length);
                        if (_strbName.Length > 2)
                            tName = _strbName.Replace(",", " ").ToString().Trim();
                        break;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(TempDataLines[cnt].Trim().ToString(), @"^\d[.]\sFirst Name\s–") && System.Text.RegularExpressions.Regex.IsMatch(TempDataLines[cnt + 1].Trim().ToString(), @"^\d[.]\sLast Name\s–"))
                    {
                        _strbName = new StringBuilder();
                        _strbName.Append(TempDataLines[cnt].Trim().ToString().Replace(System.Text.RegularExpressions.Regex.Match(TempDataLines[cnt].Trim().ToString(), @"^\d[.]\sFirst Name\s–").Value, ""));
                        _strbName.Append(" " + TempDataLines[cnt + 1].Trim().ToString().Replace(System.Text.RegularExpressions.Regex.Match(TempDataLines[cnt + 1].Trim().ToString(), @"^\d[.]\sLast Name\s–").Value, ""));
                        if (_strbName.Length > 2)
                            tName = _strbName.Replace(",", " ").ToString().Trim();
                        _strbName = null;
                        break;
                    }
                    if (TempDataLines[cnt].Replace(" ", "").Trim().Length > 50) continue;
                    mtchCandidateName = rgxCandidateName.Match(TempDataLines[cnt].Trim().ToString() + "     ");
                    if (mtchCandidateName.Index != 0 || mtchCandidateName.ToString().Trim().Length < 5) continue;
                    _strbName = new StringBuilder();
                    if (mtchCandidateName.ToString().ToLower().IndexOf("first name") >= 0)
                    {
                        for (int cnt1 = cnt - 2; cnt1 < cnt + 3 && cnt + 3 < TempDataLines.Length; cnt1++)
                        {
                            if (cnt1 < 0) continue;
                            if (rgxLastName.IsMatch(TempDataLines[cnt1].Trim()) == false) continue;
                            if (TempDataLines[cnt].Replace(mtchCandidateName.ToString().Trim(), "").Length > 1)
                                _strbName.Append(TempDataLines[cnt].Replace(mtchCandidateName.ToString().Trim(), "").Trim());
                            else
                                if (TempDataLines[cnt + 1].Trim().Length < 75)
                                    _strbName.Append(TempDataLines[cnt + 1].Trim().ToString());
                            if (TempDataLines[cnt1].Trim().Replace(rgxLastName.Match(TempDataLines[cnt1].Trim()).ToString(), " ").Trim().Length > 1)
                                _strbName.Append(" " + TempDataLines[cnt1].Trim().Replace(rgxLastName.Match(TempDataLines[cnt1].Trim()).ToString(), " ").Trim());
                            else
                                if (TempDataLines[cnt1 + 1].Trim().Length < 75)
                                    _strbName.Append(" " + TempDataLines[cnt1 + 1].Trim().ToString());

                        }
                    }
                    else
                    {
                        if (TempDataLines[cnt].Replace(mtchCandidateName.ToString().Trim(), "").Trim().Length > 1)
                            _strbName.Append(TempDataLines[cnt].Replace(mtchCandidateName.ToString().Trim(), "").Trim());
                        if (_strbName.ToString().Trim().Length < 2 && cnt + 1 < TempDataLines.Length && TempDataLines[cnt + 1].Trim().Length < 75)
                            _strbName.Append(TempDataLines[cnt + 1].Trim().ToString());

                    }
                    ReplaceSpecialCharacters(ref _strbName);
                    ReplaceFromName(ref _strbName, _strNameRemove);
                    if (_strbName.ToString().Trim().Length > 1)
                        tName = _strbName.ToString().Trim().Replace(":", "").Replace(",", "");
                }
            }
            catch { }
            #endregion

            //LABEL ----------02
            #region  "Check Full Name in top 15 lines or before Career profile etc......"
            try
            {
                if (tName.Trim().Length < 2)
                {
                    Regex rgxNotMatch = new Regex("Organisation|Organization|Company|University|College|Education|Degree|Course|Employer|Institute", RegexOptions.IgnoreCase);
                    Regex rgxMyName = new Regex(@"^[1a]?\s?[\.\)]?\s*(CANDIDATE DOSSIER|FullName|Full Name|My Name|Name)\s*[:\-]|\s(My Name is|name as in NRIC|name in full|name (full name)|name as appears in passport|name as appearing in passport|name as in passport|name in passport|Name as on passport)\s", RegexOptions.IgnoreCase);
                    Regex regNotName = null;
                    regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|address|male|female|base info|thread|safe|Class|Green Card|Message|AREAS OF INTEREST|Subject)[:\s]?[:\)\}\-\s]?", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                    for (int cnt = 0; cnt < 15 & cnt < TempDataLines.Length; cnt++)
                    {
                        if (rgxBreak.IsMatch(" " + TempDataLines[cnt].ToString() + " ")) break;
                        if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString() + " ") && TempDataLines[cnt].Replace(" ", "").Length < 20) break;
                        if (TempDataLines[cnt].Replace(" ", "").Length > 80) break;
                        if (regNotName.IsMatch(" " + TempDataLines[cnt].Trim().ToLower()))
                            continue;
                        if (rgxMyName.IsMatch(TempDataLines[cnt].Trim().ToString() + "     ") == false) continue;
                        if (cnt - 1 > 0 && rgxNotMatch.IsMatch(TempDataLines[cnt - 1])) continue;
                        string strName = TempDataLines[cnt].Remove(0, rgxMyName.Match(TempDataLines[cnt].Trim().ToString() + "     ").ToString().Trim().Length);
                        if (strName.StartsWith("-")) strName = strName.Remove(0, 1);
                        if (strName.Trim().Length > 1 && isNumberExists(strName.Trim().ToCharArray()))
                            tName = strName.Trim().Replace(":", "").Replace(",", "");
                        else
                            if (cnt + 1 < TempDataLines.Length && isNumberExists(TempDataLines[cnt + 1].Trim().ToCharArray()) && TempDataLines[cnt + 1].Trim().Length < 75 && TempDataLines[cnt + 1].Trim().Contains("曾荣江") == false)
                                tName = TempDataLines[cnt + 1].Trim().ToString().Replace(":", "").Replace(",", "");
                            else
                                continue;
                        if (rgxContinue.IsMatch(" " + tName + " ")) continue;
                        else
                            break;
                    }
                }
            }
            catch { }
            #endregion

            //LABEL---------04
            #region "Check for Covering letter"
            for (int z = 0; z < TempDataLines.Length && (tName.Trim().Length == 0); z++)
            {
                if (TempDataLines[z].Trim().ToLower().IndexOf("covering letter") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("dear sir") == 0 || TempDataLines[z].Trim().ToLower().IndexOf("dear manager") == 0 || TempDataLines[z].Trim().ToLower().IndexOf("respected sir") == 0 || TempDataLines[z].Trim().ToLower().IndexOf("cover letter") == 0)
                {
                    for (int innZ = z; innZ < TempDataLines.Length && (tName.Trim().Length == 0); innZ++)
                    {
                        if (TempDataLines[innZ].Trim().ToLower().IndexOf("with regards") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("regards") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("yours") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("sincerely,") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("sincerely") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("thanks and best regards") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("very truly yours") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("truly yours") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("with best regards") == 0 || TempDataLines[innZ].Trim().ToLower().IndexOf("respectfully your") == 0)
                        {
                            if (((innZ + 1) < TempDataLines.Length) && (TempDataLines[innZ + 1].Trim().ToLower().IndexOf("(") > 0 && TempDataLines[innZ + 1].Trim().ToLower().IndexOf("(") > 0))
                            {
                                add = TempDataLines[innZ + 1].Trim().Split('(', ')');
                                if (add.Length >= 1)
                                {
                                    if (isNumberExists(add[0].ToCharArray()))
                                    {
                                        tName = add[0];
                                    }
                                }
                            }
                            else
                            {
                                if (TempDataLines[innZ + 1].Trim().ToLower().IndexOf("you’re sincerely") == 0)
                                {
                                    if ((innZ + 2) < TempDataLines.Length && isNumberExists(TempDataLines[innZ + 2].Trim().ToCharArray()))
                                        tName = TempDataLines[innZ + 2].Replace(".", " ").Trim();
                                }
                                else
                                    if (isNumberExists(TempDataLines[innZ + 1].Trim().ToCharArray()))
                                        tName = TempDataLines[innZ + 1].Replace(".", " ").Trim();
                            }
                        }
                    }
                }
            }
            #endregion
            //LABEL ----------05
            //LABEL ----------05
            #region "Name from Last Line when Place , Date/Name will be mentioned."
            try
            {
                StringBuilder _strbName = null;
                Regex rgxEndName = new Regex(@"^(place name|location|place|Date)\s*(:|\-|\s{5}).{0,15}$", RegexOptions.IgnoreCase);
                Regex rgxLocnName = new Regex(@"^(place name|location|place)\s*(:|\-)?$", RegexOptions.IgnoreCase);
                for (int cnt = TempDataLines.Length - 1; cnt > TempDataLines.Length - 5 && cnt > 0 && tName.Trim().Length < 2; cnt--)
                {
                    if (TempDataLines[cnt].Trim().Length > 25) break;
                    if (rgxEndName.IsMatch(TempDataLines[cnt].Trim().ToString() + "     ") == false) continue;
                    if (rgxLocnName.IsMatch(TempDataLines[TempDataLines.Length - 2].Trim())) continue;
                    if (cnt < TempDataLines.Length - 1 && isNumberExists(TempDataLines[TempDataLines.Length - 1].Trim().ToCharArray()))
                        _strbName = new StringBuilder();
                    _strbName.Append(TempDataLines[TempDataLines.Length - 1].Trim().ToString());
                    ReplaceSpecialCharacters(ref _strbName);
                    ReplaceFromName(ref _strbName, _strNameRemove);
                    if (_strbName.ToString().Trim().Length > 1)
                        tName = _strbName.ToString().Trim().Replace(":", "").Replace(",", "");
                    if (tName.Length > 2 && rgxContinue.IsMatch(" " + tName.Trim() + " ") == true)
                    {
                        tName = "";
                        continue;
                    }
                }
            }
            catch { }
            #endregion
            //LABEL---------06
            #region "checking for text resume"
            if (tName.Trim().Length == 0)
            {
                try
                {
                    for (int z = 0; z < TempDataLines.Length && tName.Trim().Length == 0; z++)
                    {
                        if (TempDataLines[z].ToLower().Trim().IndexOf("text resume") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("candidate text resume") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("my text resume") == 0)
                        {
                            for (int y1 = z; (y1 < z + 3) && tName.Trim().Length < 2; y1++)
                            {
                                if (TempDataLines[y1].ToLower().IndexOf("reffered") >= 0 || TempDataLines[y1].ToLower().IndexOf("referred") >= 0 || TempDataLines[y1].ToLower().IndexOf("referr") >= 0 || TempDataLines[y1].ToLower().IndexOf("reffer") >= 0 || TempDataLines[y1].ToLower().IndexOf("text resume") >= 0 || TempDataLines[y1].ToLower().IndexOf("resume") >= 0) continue;
                                add = TempDataLines[y1].Trim().Split('(');
                                if (isNumberExists1(add[0].Trim().ToCharArray()) && !(stringExist(add[0])))
                                    tName = add[0].Trim();
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            #endregion


            #region "Checking on personal details..."

            if (tName.Trim().Length == 0)
            {
                try
                {
                    int cnt = 0;
                    int i = TempDataLines.Length;
                    string tPersonal = "PERSONAL DETAILS|personal details|profile:|personel details|p e r  s o n a l   d e t a i l s|personal detail|personel :|personal :|personel detail|personal details’|personal profile|personel profile|personal information|personel information|personal info|personel info|personel Particulars|personal Particulars|Personel Details: -|Personal Details: -|personal data:|personal data|personel data:|personel data|personal history|personal  history|My Performa : -|candidate assessment|Full Name|my name is|PERSONAL BACKGROUND:";
                    string ttPersonalBk = @"date and place of birth:|dateofbirth|data of birth|date of  birth|birthdate|date of birth/age:|birth date|born|date of birthage|b\\'date|b’date|date  of  birth|date of birth|dob|date & place of birth|d\.o\.b|date of birth|date   of   birth|current company|current location|work |position held|view their|TECHNICAL EXPERTISE|Reference|BORN:|Health|References|Nickname|Marital Status";
                    Regex regNotName = null;
                    regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|male|female|base info|reference|references|References|Nickname|Marital Status|Relationship|Green Card|Message|AREAS OF INTEREST|Board/University)[:\s]?[:\)\}\-\s]?", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                    Boolean bPersonal = false;
                    int iPersonal = 0;
                    Regex rPersonal = null;
                    Regex rPersonal1 = null;
                    Regex rNameBreak = null;
                    Regex rNameBreak1 = null;
                    try
                    {
                        rPersonal = new Regex(@"[\s,\(\:](" + tPersonal + @")[,\s]?[\s]?\b", RegexOptions.IgnoreCase);
                        rPersonal1 = new Regex(@"[\s,\(\:](" + tPersonal + @")[,\s]?[\s]?$", RegexOptions.IgnoreCase);
                        rNameBreak = new Regex(@"[\s,\(\:](" + ttPersonalBk + @")[,\s]?[\s]?\b", RegexOptions.IgnoreCase);
                        rNameBreak1 = new Regex(@"[\s,\(\:](" + ttPersonalBk + @")[,\s]?[\s]?$", RegexOptions.IgnoreCase);
                        for (iPersonal = 0; iPersonal < TempDataLines.Length; iPersonal++)
                        {
                            if (rPersonal.IsMatch(" " + TempDataLines[iPersonal].ToLower().Trim()))
                            {
                                bPersonal = true;
                                break;
                            }
                            if (rPersonal1.IsMatch(" " + TempDataLines[iPersonal].ToLower().Trim()))
                            {
                                bPersonal = true;
                                break;
                            }
                        }
                        for (int iName = iPersonal; iName < TempDataLines.Length && bPersonal == true && tName.Trim().Length == 0; iName++)
                        {
                            if (TempDataLines[iName].ToLower().Trim().IndexOf("name of examination") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name of organization") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("father's name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name of wife") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name of children") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("father’s name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("fathers name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("mothers name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("mother's name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("mother’s name") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("father") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("mother") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name of the company") >= 0 | TempDataLines[iName].ToLower().Trim().IndexOf("name of course") >= 0 | TempDataLines[iName].ToLower().Trim().IndexOf("name of inst") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name of degree") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("surname") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("sex") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("gender") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("nationality") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("language") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("permanent residency") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("i' m from") >= 0 || TempDataLines[iName].ToLower().Trim().IndexOf("marital status") >= 0) continue; //Marital Status
                            if (rNameBreak.IsMatch(" " + TempDataLines[iName].Trim()))
                                break;
                            if (rNameBreak1.IsMatch(" " + TempDataLines[iName].Trim()))
                                break;

                            if ((TempDataLines[iName - 1].ToLower().Trim() == "names" || TempDataLines[iName].ToLower().Trim().IndexOf("name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("a. name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name:") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("name:-") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("1.  full name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("1. name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("full name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("candidate name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("full name") == 0 || TempDataLines[iName].ToLower().Trim().IndexOf("my name is") == 0) && tName.Trim().Length <= 2)
                            {
                                StringBuilder tNameValue = new StringBuilder();
                                if (TempDataLines[iName].Replace("Name and Address:", "").Trim() == "")

                                    tNameValue.Append(TempDataLines[iName + 1].Trim());
                                else
                                    tNameValue.Append(TempDataLines[iName].Trim());

                                //opReplaceTextFromNameText(ref tNameValue);
                                if (rNameBreak.IsMatch(tNameValue.ToString()) == true) break;
                                if (regNotName.IsMatch(" " + TempDataLines[iName].Trim()))
                                    continue;

                                tNameValue = tNameValue.Replace("A. Name", "").Replace("a. name", "").Replace("a. Name", "").Replace("name as in NRIC", "").Replace("Name as in NRIC", "").Replace("name as in nric", "").Replace("as in NRIC", "").Replace("as in nric", "").Replace("my name is", "").Replace("candidate name", "").Replace("1. name", "").Replace("1.  name in full", "").Replace("name in full", "").Replace("name-", "").Replace("name:-", "").Replace("name:", "").Replace("nurrent ctc", "").Replace("expected ctc", "").Replace("expected ctc", "").Replace("notice period", "").Replace("date of birth", "").Replace("name (full name)", "").Replace("name as appears in passport", "").Replace("name as appearing in passport", "").Replace("name as in passport", "").Replace("name as in passport", "").Replace("name in passport", "").Replace("name in passport", "").Replace("Surname", "").Replace("name", "").Replace("full", "").Replace("call", "").Replace("=", "").Replace("contact", "").Replace("address", "").Replace("telephone", "").Replace("phone", "").Replace("designation", "").Replace("company", "").Replace("organization", "").Replace("organisation", "").Replace("organizations", "").Replace("organisations", "").Replace("tel", "").Replace("manager", "").Replace("from", "").Replace("(mr.)", "").Replace(": -", "").Replace("(", "").Replace(")", "");//"Name (Mr.)".Replace("name", "")
                                tNameValue = tNameValue.Replace("My Name is", "").Replace("Candidate Name", "").Replace("1. Name", "").Replace("1.  NAME IN FULL", "").Replace("NAME IN FULL", "").Replace("NAME-", "").Replace("NAME:-", "").Replace("NAME:", "").Replace("NURRENT CTC", "").Replace("EXPECTED CTC", "").Replace("EXPECTED CTC", "").Replace("NOTICE PERIOD", "").Replace("DATE OF BIRTH", "").Replace("NAME (FULL NAME)", "").Replace("NAME AS APPEARS IN PASSPORT", "").Replace("NAME AS APPEARING IN PASSPORT", "").Replace("NAME AS IN PASSPORT", "").Replace("NAME AS IN PASSPORT", "").Replace("NAME IN PASSPORT", "").Replace("NAME IN PASSPORT", "").Replace("NAME", "").Replace("NAME", "").Replace("FULL", "").Replace("CALL", "").Replace("NAME", "").Replace("=", "").Replace("CONTACT", "").Replace("ADDRESS", "").Replace("TELEPHONE", "").Replace("PHONE", "").Replace("DESIGNATION", "").Replace("COMPANY", "").Replace("ORGANIZATION", "").Replace("ORGANISATION", "").Replace("ORGANIZATIONS", "").Replace("ORGANISATIONS", "").Replace("TEL", "").Replace("MANAGER", "").Replace("FROM", "").Replace("(MR.)", "");//"Name (Mr.)"
                                tNameValue = tNameValue.Replace("MY NAME IS", "").Replace("CANDIDATE NAME", "").Replace("1. NAME", "").Replace("1.  Name In Full", "").Replace("1.  Name in Full", "").Replace("Name In Full", "").Replace("Name-", "").Replace("Name:-", "").Replace("Name:", "").Replace("Nurrent Ctc", "").Replace("Expected Ctc", "").Replace("Expected Ctc", "").Replace("Notice Period", "").Replace("Date Of Birth", "").Replace("Name (Full Name)", "").Replace("Name As Appears In Passport", "").Replace("Name As Appearing In Passport", "").Replace("Name As In Passport", "").Replace("Name As In Passport", "").Replace("Name In Passport", "").Replace("Name In Passport", "");
                                tNameValue = tNameValue.Replace("Name as appearing in Passport", "").Replace("Name as appearing in passport", "").Replace("name as appearing in passport", "").Replace("NAME AS APPEARING IN PASSPORT", "").Replace("name as appears in passport", "").Replace("Name as Appears in Passport", "").Replace("Name As Appears In Passport", "").Replace("Name as appears in passport", "").Replace("NAME AS APPEARS IN PASSPORT", "").Replace("name as appearing in passport", "").Replace("Name as Appearing in Passport", "").Replace("Name As Appearing In Passport", "").Replace("Name as appearing passport", "").Replace("NAME AS APPEARINGN PASSPORT", "");
                                tNameValue = tNameValue.Replace("name as in passport", "").Replace("Name as in Passport", "").Replace("Name As In Passport", "").Replace("Name as in passport", "").Replace("NAME AS IN PASSPORT", "").Replace("as in passport", "").Replace("as in Passport", "").Replace("As In Passport", "").Replace("as in passport", "").Replace("AS IN PASSPORT", "").Replace("AS APPEARING IN PASSPORT", "").Replace("as appearing in passport", "").Replace("as appearing in Passport", "").Replace("Relationship", "");
                                tNameValue = tNameValue.Replace("name as in the passport", "").Replace("Name as in the Passport", "").Replace("Name As In The Passport", "").Replace("Name as in passport", "").Replace("NAME AS IN THE PASSPORT", "").Replace("as in the passport", "").Replace("as in the Passport", "").Replace("As In The Passport", "").Replace("as in the passport", "").Replace("passport", "");
                                tNameValue = tNameValue.Replace("name as on passport", "").Replace("Name as on Passport", "").Replace("Name As On Passport", "").Replace("Name as on passport", "").Replace("NAME AS ON PASSPORT", "").Replace("as on passport", "").Replace("as on Passport", "").Replace("As On Passport", "").Replace("as on passport", "").Replace("AS ON PASSPORT", "").Replace("AS APPEARING ON PASSPORT", "").Replace("sex", "").Replace("gender", "");
                                tNameValue = tNameValue.Replace("Name", "").Replace("Name", "").Replace("Full", "").Replace("Call", "").Replace("Name", "").Replace("=", "").Replace("Contact", "").Replace("Address", "").Replace("Telephone", "").Replace("Phone", "").Replace("Designation", "").Replace("Company", "").Replace("Organization", "").Replace("Organisation", "").Replace("Organizations", "").Replace("Organisations", "").Replace("Tel", "").Replace("Manager", "").Replace("From", "").Replace("(Mr.)", "").Replace(", ", " ");
                                try
                                {
                                    if (tNameValue.Replace(":", "").ToString().Trim().Length == 0)
                                        tNameValue.Append(TempDataLines[iName + 1].Replace(":", "").Trim());
                                    if (tNameValue.Replace(":", "").ToString().Trim().Length == 0)
                                        tNameValue.Append(TempDataLines[iName + 2].Trim());
                                }
                                catch { }
                                if (tNameValue.ToString().ToLower().Trim().IndexOf("fathers name") >= 0 || tNameValue.ToString().ToLower().Trim().IndexOf("father's") >= 0 || tNameValue.ToString().ToLower().Trim().IndexOf("father’s") >= 0 || tNameValue.ToString().ToLower().Trim().IndexOf("mother's") >= 0 || tNameValue.ToString().ToLower().Trim().IndexOf("mother’s") >= 0) continue;
                                //opReplaceTextFromNameText(ref tNameValue);
                                tNameValue = tNameValue.Replace("as in NRIC", "").Replace("as in nric", "").Replace("name as in NRIC", "").Replace("Name as in NRIC", "").Replace("name as in nric", "").Replace("1.  name in full", "").Replace("name in full", "").Replace("name-", "").Replace("name:-", "").Replace("nurrent ctc", "").Replace("expected ctc", "").Replace("expected ctc", "").Replace("notice period", "").Replace("date of birth", "").Replace("name (full name)", "").Replace("name as appears in passport", "").Replace("name as appearing in passport", "").Replace("name as in passport", "").Replace("name as in passport", "").Replace("name in passport", "").Replace("name in passport", "").Replace("name", "").Replace("name", "").Replace("full", "").Replace("call", "").Replace("name", "").Replace("=", "").Replace("contact", "").Replace("email", "").Replace("date of birth", "").Replace("address", "").Replace("telephone", "").Replace("phone", "").Replace("designation", "").Replace("company", "").Replace("organization", "").Replace("organisation", "").Replace("organizations", "").Replace("organisations", "").Replace(" tel", "").Replace("tel ", "").Replace("manager", "").Replace("from", "").Replace(":-", ":").Replace(": -", ":").Replace("(", "").Replace(")", ""); ;
                                tNameValue = tNameValue.Replace("1.  NAME IN FULL", "").Replace("NAME IN FULL", "").Replace("NAME-", "").Replace("NAME:-", "").Replace("NURRENT CTC", "").Replace("EXPECTED CTC", "").Replace("EXPECTED CTC", "").Replace("NOTICE PERIOD", "").Replace("DATE OF BIRTH", "").Replace("NAME (FULL NAME)", "").Replace("NAME AS APPEARS IN PASSPORT", "").Replace("NAME AS APPEARING IN PASSPORT", "").Replace("NAME AS IN PASSPORT", "").Replace("NAME AS IN PASSPORT", "").Replace("NAME IN PASSPORT", "").Replace("NAME IN PASSPORT", "").Replace("NAME", "").Replace("NAME", "").Replace("FULL", "").Replace("CALL", "").Replace("NAME", "").Replace("=", "").Replace("CONTACT", "").Replace("EMAIL", "").Replace("DATE OF BIRTH", "").Replace("ADDRESS", "").Replace("TELEPHONE", "").Replace("PHONE", "").Replace("DESIGNATION", "").Replace("COMPANY", "").Replace("ORGANIZATION", "").Replace("ORGANISATION", "").Replace("ORGANIZATIONS", "").Replace("ORGANISATIONS", "").Replace(" TEL", "").Replace("MANAGER", "").Replace("FROM", "").Replace(":-", ":");
                                tNameValue = tNameValue.Replace("1.  Name In Full", "").Replace("1.  Name in Full", "").Replace("Name In Full", "").Replace("Name-", "").Replace("Name:-", "").Replace("Nurrent Ctc", "").Replace("Expected Ctc", "").Replace("Expected Ctc", "").Replace("Notice Period", "").Replace("Date Of Birth", "").Replace("Name (Full Name)", "").Replace("Name As Appears In Passport", "").Replace("Name As Appearing In Passport", "").Replace("Name As In Passport", "").Replace("Name As In Passport", "").Replace("Name In Passport", "").Replace("Name In Passport", "").Replace("Name", "").Replace("Name", "").Replace("Full", "").Replace("Call", "").Replace("Name", "").Replace("=", "").Replace("Contact", "").Replace("Email", "").Replace("Date Of Birth", "").Replace("Address", "").Replace("Telephone", "").Replace("Phone", "").Replace("Designation", "").Replace("Company", "").Replace("Organization", "").Replace("Organisation", "").Replace("Organizations", "").Replace("Organisations", "").Replace("Tel", "").Replace("Manager", "").Replace("From", "").Replace(":-", ":").Replace("PROFFESIONAL", "").Replace("MEMBERSHIPS", "");
                                tNameValue = tNameValue.Replace("PASSPORT", "").Replace("Number", "").Replace("NUMBER", "").Replace("number", "").Replace("Passport", "").Replace("passport", "").Replace(" As in ", "").Replace("as on", "").Replace("Relationship", "");
                                add = tNameValue.Replace(":-", ":").Replace("–", "").ToString().Split(':');
                                for (int z = 0; z < add.Length; z++)
                                {
                                    if (add[z].IndexOf("Hobbies") >= 0) continue;
                                    if (add[z].Trim().Length <= 1) continue;
                                    if (isNumberExists(add[z].Trim().ToCharArray()))
                                        tName = add[z].Trim();
                                }
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        //rNameBreak = null;
                        rNameBreak1 = null;
                    }

                    if (tName.Trim().Length <= 2)
                    {
                        int Flag = 0, iFlag = 0;
                        int count = cnt;
                        for (cnt = count; cnt < TempDataLines.Length; cnt++)
                        {
                            TempDataLines[cnt] = TempDataLines[cnt].Replace(Convert.ToChar(45), Convert.ToChar(32)).Trim();
                            ///modified on 1-3-06
                            ///TempDataLines[cnt].ToLower().Trim().IndexOf("personal detail") == 0 
                            if ((cnt > 2 && TempDataLines[cnt - 1].ToLower().Trim().IndexOf("references") >= 0) || TempDataLines[cnt].ToLower().Trim().IndexOf("reference") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("references") == 0) { Flag = -1; break; }
                            if ((TempDataLines[cnt].ToLower().Trim().IndexOf("name") == 0 && (TempDataLines[cnt].ToLower().Trim().IndexOf("named") < 0)) | TempDataLines[cnt].ToLower().Trim().IndexOf("name:") == 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("name:-") == 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("personal details") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("personal detail") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("personal details’") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("personal profile") == 0 || TempDataLines[cnt].ToLower().IndexOf("personal information") == 0 || TempDataLines[cnt].ToLower().IndexOf("personal info") == 0 || TempDataLines[cnt].ToLower().IndexOf("personal Particulars") == 0)
                            {
                                Flag = 2;//setting flag as 2  means we found a personal details ,personal profile etc
                                break;
                            }
                        }
                        count = cnt;
                        for (cnt = count; cnt < TempDataLines.Length && Flag == 2; cnt++)
                        {
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("father's name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of exam") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name difference") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of wife") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of children") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("fathers name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mothers name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother's name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the company") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("name of course") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of inst") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of consultant") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of employer") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of degree") >= 0) continue;
                            ///modified on 8
                            ///|TempDataLines[cnt].ToLower().Trim().IndexOf("name of degree/ qualification") >= 0Institute/ University
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("educational") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("education") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("qualification") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("university") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("institute") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("organisation") >= 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("organisations") >= 0)
                            {
                                cnt++;
                                break;
                            }
                            ///modified 3-3-6
                            ///TempDataLines[cnt].ToLower().Trim().IndexOf("projects") >= 0
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("references") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("reference") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("educational qualification") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("passport details") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("projects") >= 0)
                            {
                                Flag = 1;
                                cnt++;
                                break;
                            }
                            ///modified on 21-02-06
                            /// check "Call Name" is exists....
                            /// check for "name as in passport"
                            /// check  "name (full name)" to null
                            /// modified 2-3-6
                            /// check for name in passport
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("name of the") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of exam") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of organization") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the organization") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the firm") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of firm") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of school") == 0 | TempDataLines[cnt].ToLower().Trim().IndexOf("name of the school") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of institute") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the institute") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of exam") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of consultant") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of degree") >= 0)
                                continue;
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("name as appearing in Passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name as in passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name in passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name (full name)") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("1. name") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name") == 0 || TempDataLines[cnt].ToLower().IndexOf("full name") >= 0 || TempDataLines[cnt].ToLower().IndexOf("call name") == 0)
                            {

                                int tempNameCheck = cnt - 5;
                                if (tempNameCheck < 0)
                                    tempNameCheck = 0;
                                bool bNameCheck = true;
                                count = tempNameCheck;
                                for (tempNameCheck = count; tempNameCheck < TempDataLines.Length & tempNameCheck < cnt + 1; tempNameCheck++)
                                    if (TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("detailed career profile") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("version no") >= 0 || TempDataLines[tempNameCheck].ToUpper().Trim().IndexOf("CERTIFICATIONS & TRAININGS") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("server") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("college/school") >= 0 || TempDataLines[tempNameCheck].ToUpper().Trim().IndexOf("DIALOG PROGRAMMING") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("work") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("designation") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("experience") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("institution") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("project") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("organization") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("company") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("application description") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("current product") >= 0)
                                    {
                                        bNameCheck = false;
                                        break;
                                    }
                                if (bNameCheck)
                                    iFlag = 1;
                                else
                                    iFlag = 0;
                                break;
                            }
                        }
                        //second attempt to find Personal detail name
                        count = cnt;
                        for (cnt = count; cnt < TempDataLines.Length && Flag == 2; cnt++)
                        {
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("father's name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of exam") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name difference") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of wife") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of children") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("fathers name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mothers name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother's name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother’s name") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("father") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("mother") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the company") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of consultant") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of degree") >= 0) continue;
                            ///modified on 8
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("educational") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("education") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("university") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("institute") >= 0) break;
                            ///modified 3-3-6
                            ///TempDataLines[cnt].ToLower().Trim().IndexOf("projects") >= 0 References
                            if ((cnt > 2 && TempDataLines[cnt - 1].ToLower().Trim().IndexOf("references") >= 0) || TempDataLines[cnt].ToLower().Trim().IndexOf("references") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("educational qualification") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("passport details") >= 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("projects") >= 0)
                            {
                                Flag = 1; break;
                            }
                            ///modified on 21-02-06
                            /// check "Call Name" is exists....
                            /// check for "name as in passport"
                            /// check  "name (full name)" to null
                            /// modified 2-3-6
                            /// check for name in passport
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("name of the") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of the organization") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of exam") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of school") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of institute") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of consultant") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name of degree") >= 0)
                                continue;
                            if (TempDataLines[cnt].ToLower().Trim().IndexOf("name as appearing in passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name as in passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name in passport") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name (full name)") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("1. name") == 0 || TempDataLines[cnt].ToLower().Trim().IndexOf("name") == 0 || TempDataLines[cnt].ToLower().IndexOf("full name") >= 0 || TempDataLines[cnt].ToLower().IndexOf("call name") == 0)
                            {
                                int tempNameCheck = cnt - 5;
                                if (tempNameCheck < 0)
                                    tempNameCheck = 0;
                                bool bNameCheck = true;
                                count = tempNameCheck;
                                for (tempNameCheck = count; tempNameCheck < TempDataLines.Length & tempNameCheck < cnt + 1; tempNameCheck++)
                                    if (TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("detailed career profile") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("version no") >= 0 || TempDataLines[tempNameCheck].ToUpper().Trim().IndexOf("CERTIFICATIONS & TRAININGS") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("server") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("college/school") >= 0 || TempDataLines[tempNameCheck].ToUpper().Trim().IndexOf("DIALOG PROGRAMMING") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("work") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("designation") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("experience") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("institution") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("project") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("organization") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("company") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("application description") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("current product") >= 0 || TempDataLines[tempNameCheck].ToLower().Trim().IndexOf("address of employer") >= 0)
                                    {
                                        bNameCheck = false;
                                        break;
                                    }
                                if (bNameCheck)
                                    iFlag = 1;
                                else
                                    iFlag = 0;
                                break;
                            }
                        }


                        if (cnt < TempDataLines.Length && Flag == 0)
                        {
                            add = TempDataLines[cnt].ToLower().Replace("name in full", "").Replace("1. name", "").Replace("name-", "").Replace("name", "").Replace("full", "").Replace("(mr.)", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("contact:", "").Replace("details", "").Replace("name", "").Split(':');
                            for (int cnt1 = 0; cnt1 < add.Length; cnt1++)
                            {
                                //if(add[cnt1].ToLower().Trim().IndexOf("name")>=0)continue;
                                if (add[cnt1].Trim().Length > 1)
                                {
                                    if (isNumberExists(add[cnt1].Trim().ToCharArray()))
                                        tName = add[cnt1];
                                }
                            }
                        }
                        if (iFlag == 1 && tName.Trim().Length == 0 && cnt < TempDataLines.Length)
                        {
                            for (int k = 0; k < 2 && (cnt + k) < TempDataLines.Length; k++)
                            {
                                StringBuilder tNameValue = new StringBuilder();

                                tNameValue.Append(TempDataLines[cnt + k].Trim());
                                if (tNameValue.Length > 2)
                                    opGetNameReplace(ref tNameValue);
                                //opReplaceTextFromNameText(ref tNameValue);
                                if (rNameBreak.IsMatch(tNameValue.ToString()) == true) break;
                                if (regNotName.IsMatch(" " + tNameValue.ToString()) == true) break;

                                tNameValue.Replace("name in full", "").Replace("Name in full", "").Replace("Name in Full", "").Replace("Name In Full", "").Replace("name as in nric", "").Replace("of candidate", "").Replace("of Candidate", "").Replace("OF CANDIDATE", "");
                                tNameValue.Replace("Name as in NRIC", "").Replace("contact by", "").Replace("Contact By", "").Replace("Contact by", "").Replace("CONTACT BY", "");
                                tNameValue.Replace("name as in NRIC", "").Replace("emailgeneral", "").Replace("Emailgeneral", "").Replace("EmailGeneral", "").Replace("EMAILGENERAL", "");
                                tNameValue.Replace("as in NRIC", "").Replace("as in nric", "").Replace("email id", "").Replace("Email ID", "").Replace("Email Id", "").Replace("EMAIL ID", "");
                                tNameValue.Replace("email", "").Replace("Email", "").Replace("EMAIL", "");
                                tNameValue.Replace("general", "").Replace("General", "").Replace("GENERAL", "").Replace("information", "").Replace("Information", "").Replace("INFORMATION", "").Replace("Position", "");
                                tNameValue.Replace("mobile no.", "").Replace("Mobile No.", "").Replace("Mobile no.", "").Replace("MOBILE NO.", "");
                                tNameValue.Replace("(mobile)", "").Replace("(Mobile)", "").Replace("(MOBILE)", "").Replace("mobile", "").Replace("Mobile", "").Replace("MOBILE", "");
                                tNameValue.Replace("mob", "").Replace("Mob", "").Replace("MOB", "").Replace("(r)", "").Replace("(R)", "");
                                tNameValue.Replace("emergency contact no.:", "").Replace("Emergency Contact No.:", "").Replace("Emergency contact no.:", "").Replace("EMERGENCY CONTACT NO.:", "");
                                tNameValue.Replace("emergency", "").Replace("Emergency", "").Replace("EMERGENCY", "");
                                tNameValue.Replace("contact no.:", "").Replace("Contact No.:", "").Replace("Contact no.:", "").Replace("CONTACT NO.:", "").Replace("contact no.", "").Replace("Contact No.", "").Replace("Contact no.", "").Replace("CONTACT NO.", "").Replace("contact no", "").Replace("Contact No", "").Replace("Contact no", "").Replace("CONTACT NO", "").Replace("contact", "").Replace("Contact", "").Replace("CONTACT", "");
                                tNameValue.Replace("number", "").Replace("Number", "").Replace("NUMBER", "");
                                tNameValue.Replace("organizations", "").Replace("Organizations", "").Replace("ORGANIZATIONS", "").Replace("organization", "").Replace("Organization", "").Replace("ORGANIZATION", "").Replace("organisations", "").Replace("Organidations", "").Replace("ORGANISATIONS", "").Replace("organisation", "").Replace("Organidation", "").Replace("ORGANISATION", "");
                                tNameValue.Replace("phone no.", "").Replace("Phone No.", "").Replace("Phone no.", "").Replace("PHONE NO.", "");
                                tNameValue.Replace("telephone", "").Replace("Telephone", "").Replace("TELEPHONE", "").Replace("phone", "").Replace("Phone", "").Replace("PHONE", "").Replace("ph:", "").Replace("Ph:", "").Replace("PH:", "");
                                tNameValue.Replace("tel no. res", "").Replace("Tel No. Res", "").Replace("Tel No. res", "").Replace("TEL NO. RES", "");
                                tNameValue.Replace("cell no", "").Replace("Cell No", "").Replace("Cell No", "").Replace("CELL NO", "").Replace("cell", "").Replace("Cell", "").Replace("CELL", "").Replace("English", "");
                                tNameValue.Replace("date of birth", "").Replace("Date Of Birth", "").Replace("Date of birth", "").Replace("DATE OF BIRTH", "");
                                tNameValue.Replace("name as appears in passport", "").Replace("Name as Appears in Passport", "").Replace("Name As Appears In Passport", "").Replace("Name as appears in passport", "").Replace("NAME AS APPEARS IN PASSPORT", "").Replace("name as appearing in passport", "").Replace("Name as Appearing in Passport", "").Replace("Name As Appearing In Passport", "").Replace("Name as appearing passport", "").Replace("NAME AS APPEARINGN PASSPORT", "");
                                tNameValue.Replace("name as in passport", "").Replace("Name as in passport", "").Replace("Name As In Passport", "").Replace("Name as in passport", "").Replace("NAME AS IN PASSPORT", "");
                                tNameValue.Replace("name (full name)", "").Replace("Name (Full Name)", "").Replace("Name (full name)", "").Replace("NAME (FULL NAME)", "").Replace("name in passport", "").Replace("Name In Passport", "").Replace("Name in passport", "").Replace("NAME IN PASSPORT", "").Replace("name in full", "").Replace("Name In Full", "").Replace("Name in Full", "").Replace("NAME IN FULL", "").Replace("name-", "").Replace("Name-", "").Replace("NAME-", "").Replace("name:-", "").Replace("Name:-", "").Replace("NAME:-", "").Replace("name", "").Replace("Name", "").Replace("NAME", "").Replace("contact:", "").Replace("Contact:", "").Replace("CONTACT:", "").Replace("details", "").Replace("Details", "").Replace("DETAILS", "");
                                tNameValue.Replace("ADDRESS", "").Replace("Address", "").Replace("address", "").Replace("CALL", "").Replace("Call", "").Replace("call", "").Replace("FULL", "").Replace("Full", "").Replace("full", "").Replace("MANAGER", "").Replace("Manager", "").Replace("manager", "").Replace("COMPNAY", "").Replace("Company", "").Replace("company", "").Replace("DESIGNATION", "").Replace("Designation", "").Replace("designation", "").Replace("NOTICE PERIOD", "").Replace("Notice Period", "").Replace("notice period", "").Replace("expected ctc", "").Replace("Expected Ctc", "").Replace("EXPECTED CTC", "").Replace("Expected CTC", "").Replace("nurrent ctc", "").Replace("Nurrent CTC", "").Replace("Nurrent Ctc", "").Replace("NURRENT CTC", "").Replace("City", "");
                                tNameValue.Replace("Name as appearing in Passport", "").Replace("Name as appearing in passport", "").Replace("name as appearing in passport", "").Replace("NAME AS APPEARING IN PASSPORT", "").Replace("name as appears in passport", "").Replace("Name as Appears in Passport", "").Replace("Name As Appears In Passport", "").Replace("Name as appears in passport", "").Replace("NAME AS APPEARS IN PASSPORT", "").Replace("name as appearing in passport", "").Replace("Name as Appearing in Passport", "").Replace("Name As Appearing In Passport", "").Replace("Name as appearing passport", "").Replace("NAME AS APPEARINGN PASSPORT", "");
                                tNameValue.Replace("name as in passport", "").Replace("Name as in Passport", "").Replace("Name As In Passport", "").Replace("Name as in passport", "").Replace("NAME AS IN PASSPORT", "").Replace("as in passport", "").Replace("as in Passport", "").Replace("As In Passport", "").Replace("as in passport", "").Replace("AS IN PASSPORT", "").Replace("AS APPEARING IN PASSPORT", "").Replace("as appearing in passport", "").Replace("as appearing in Passport", "");
                                tNameValue.Replace("name as in the passport", "").Replace("Name as in the Passport", "").Replace("Name As In The Passport", "").Replace("Name as in passport", "").Replace("NAME AS IN THE PASSPORT", "").Replace("as in the passport", "").Replace("as in the Passport", "").Replace("As In The Passport", "").Replace("as in the passport", "");
                                tNameValue.Replace("name as on passport", "").Replace("Name as on Passport", "").Replace("Name As On Passport", "").Replace("Name as on passport", "").Replace("NAME AS ON PASSPORT", "").Replace("as on passport", "").Replace("as on Passport", "").Replace("As On Passport", "").Replace("as on passport", "").Replace("AS ON PASSPORT", "").Replace("AS APPEARING ON PASSPORT", "").Replace("passport", "").Replace("Passport", "").Replace("PASSPORT", "");
                                tNameValue.Replace("(MR.)", "").Replace("(Mr.)", "").Replace("(mr.)", "").Replace("=", "").Replace("FROM", "").Replace("From", "").Replace("from", "").Replace(" tel", "").Replace("tel ", "").Replace(" Tel", "").Replace(" TEL", "").Replace("Tel ", "").Replace("TEL ", "");
                                tNameValue.Replace("Date of Issue", "").Replace("date of issue", "").Replace("DATE OF ISSUE", "").Replace("Expiry Date", "").Replace("EXPIRY DATE", "").Replace("Expiry Date", "").Replace("PLACE OF ISSUE", "").Replace("place of issue", "").Replace("Place of Issue", "").Replace("             ", ":");
                                if (tNameValue.ToString().ToLower().IndexOf("place") >= 0 || tNameValue.ToString().ToLower().IndexOf("permanent") >= 0 || tNameValue.ToString().ToLower().IndexOf("fathers") >= 0 || tNameValue.ToString().ToLower().IndexOf("father's name") >= 0 || tNameValue.ToString().ToLower().IndexOf("father’s name") >= 0 | tNameValue.ToString().ToLower().IndexOf("fathers name") >= 0 | tNameValue.ToString().ToLower().IndexOf("mothers name") >= 0 | tNameValue.ToString().ToLower().IndexOf("mother's name") >= 0 | tNameValue.ToString().ToLower().IndexOf("mother’s name") >= 0 | tNameValue.ToString().ToLower().IndexOf("father") >= 0 | tNameValue.ToString().ToLower().IndexOf("mother") >= 0 | tNameValue.ToString().ToLower().IndexOf("name of the company") >= 0 | tNameValue.ToString().ToLower().IndexOf("name of course") >= 0 || tNameValue.ToString().ToLower().IndexOf("name of inst") >= 0 || tNameValue.ToString().ToLower().IndexOf("name of consultant") >= 0 || tNameValue.ToString().ToLower().Trim().IndexOf("name of degree") >= 0) continue;

                                if (stringExist(tNameValue.ToString()) == true) continue; ;
                                add = tNameValue.Replace(":-", ":").ToString().Replace("-", ":").ToString().Split(':');
                                for (int z = 0; z < add.Length && tName.Trim().Length <= 20; z++)
                                {
                                    if (add[z].IndexOf("Hobbies") >= 0 || add[z].ToLower().IndexOf("d o b") >= 0 || add[z].IndexOf("Age") >= 0) continue;
                                    if (add[z].Trim().Length <= 1) continue;
                                    if (isNumberExists(add[z].Trim().ToCharArray()))
                                        tName += ' ' + add[z].Trim();
                                }
                            }
                        }
                    }

                }
                catch
                { }
            }
            #endregion

            //LABEL ----------03  // Logic Fire after Full Name Checking .... added Saravana
            #region "Checking last line is contain ()"
            try
            {
                //(C.V Continued)
                string sLastLine = "";
                if (TempDataLines.Length >= 1 && tName.Trim().Length == 0)
                {
                    int iCLast2Cnt = 0;
                    do
                    {
                        sLastLine = TempDataLines[TempDataLines.Length - iCLast2Cnt - 1].Replace("Place:", "").Replace("Place", "").Trim();
                        if ((sLastLine.IndexOf("(") == 0) && (sLastLine.IndexOf(")") == (sLastLine.Length - 1)) || (sLastLine.IndexOf("[") == 0) && (sLastLine.IndexOf("]") == (sLastLine.Length - 1)) || (sLastLine.IndexOf("{") == 0) && (sLastLine.IndexOf("}") == (sLastLine.Length - 1)) || sLastLine.ToLower().Trim().IndexOf("curriculum vitae of") == 0)
                        {
                            StringBuilder rmBrkStr = new StringBuilder();
                            rmBrkStr.Append(sLastLine);
                            rmBrkStr.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("curriculum vitae of", "").Replace("Curriculum Vitae of", "").Replace("CURRICULAM VITAE OF", "").Replace(":", " ").Replace("-", " ").Replace("Applicant", "").Replace("Candidate", "");
                            if (isNumberExists(rmBrkStr.ToString().Trim().ToCharArray()) && (rmBrkStr.ToString().ToLower().IndexOf("affiliated") < 0 && rmBrkStr.ToString().ToLower().IndexOf("university") < 0 & rmBrkStr.ToString().ToLower().IndexOf("unviersity") < 0 && rmBrkStr.ToString().ToLower().IndexOf("signature") < 0 && rmBrkStr.ToString().ToLower().IndexOf("sponsored") < 0 && rmBrkStr.ToString().ToLower().IndexOf("seminar") < 0 && rmBrkStr.ToString().ToLower().IndexOf("exhibition") < 0 && rmBrkStr.ToString().ToLower().IndexOf("reference") < 0 & rmBrkStr.ToString().ToLower().IndexOf("exhibition") < 0 && rmBrkStr.ToString().ToLower().IndexOf("functional") < 0 && rmBrkStr.ToString().ToLower().IndexOf("application") < 0 && rmBrkStr.ToString().ToLower().IndexOf("faithfully") < 0 && rmBrkStr.ToString().ToLower().IndexOf("yours") < 0 && rmBrkStr.ToString().ToLower().IndexOf("your name") < 0 && rmBrkStr.ToString().ToLower().IndexOf("available on your request") < 0 && rmBrkStr.ToString().ToLower().IndexOf("father") < 0 && rmBrkStr.ToString().ToLower().IndexOf("c.v continued") < 0 && rmBrkStr.ToString().ToLower().IndexOf("will be provided on request ") < 0) && rmBrkStr.ToString().ToLower().IndexOf("placed") < 0)
                            {
                                Regex rgxCandidate = new Regex(@"curriculum vitae of|(Candidate|Applicant)\s?[']?\s?(Name|Full Name)|Full Name", RegexOptions.IgnoreCase);
                                if (rgxCandidate.IsMatch(rmBrkStr.ToString()))
                                    rmBrkStr.Replace(rgxCandidate.Match(rmBrkStr.ToString()).ToString(), " ");
                                tName = rmBrkStr.ToString().Trim();
                                if (tName.IndexOf("@") > 0)
                                    tName = "";
                            }
                        }
                        iCLast2Cnt++;
                    } while (iCLast2Cnt < 2 && tName.Trim().Length <= 2);
                    if (tName.Length > 2)
                    {
                        tNameSearchFrmLast = true;
                        tNameFrmFirst = tName;
                    }
                }

                if (rgxContinue.Match(" " + tName + " ").Length > 2)
                {
                    tName = "";
                    tNameFrmFirst = "";
                    tNameSearchFrmLast = false;
                }
                if (tName.Trim().Length < 2)
                {
                    if ((TempDataLines[TempDataLines.Length - 1].ToLower().Trim().IndexOf("(") == 0 && TempDataLines[TempDataLines.Length - 1].ToLower().Trim().IndexOf(")") >= 0) && Regex.Matches(TempDataLines[TempDataLines.Length - 1].Trim(), @"\s+").Count < 3)
                    {
                        tName = TempDataLines[TempDataLines.Length - 1].Trim();
                        tNameSearchFrmLast = true;
                        tNameFrmFirst = tName;
                    }
                }
            }
            catch { }

            #endregion


            //Label------------07
            #region ""
            try
            {
                int cnt = 0;
                bool flagName = false;
                Regex rgxPersonal = new Regex(@"(PERSONAL DETAILS|p e r  s o n a l   d e t a i l s|(personal|Personel)\s*(profile|information|info|Particulars|Detail|history|data)|Profile|My Performa|candidate assessment|Address|Father['’]?\s?Name|Mother['’]?\s?Name)['’]?[s]?\s*([:\-]|\s{5})", RegexOptions.IgnoreCase);
                Regex rgxDob = new Regex(@"date and place of birth\s*:|dateofbirth|data of birth|date of  birth|birthdate|date of birth/age:|birth date|born|date of birthage|b\\'date|b’date|date  of  birth|date of birth|dob|date & place of birth|d\.o\.b|date of birth|date   of   birth|BORN:", RegexOptions.IgnoreCase);
                Regex rgxMyName = new Regex(@"^[1a]?\s?[\.\)]?\s*(FullName|Full Name|My Name)[\s\t]*([,\-]|\s{5}|:)|Name[\s\t]*[:\-]|Name[\s\t]*[:\-]?$|\s(My Name is|name as in NRIC|name in full|name (full name)|name as appears in passport|name as appearing in passport|name as in passport|name in passport|Name as on passport)\s", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                Regex regNotName = null;
                regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|male|female|base info|reference|references|nickname|thread|safe|Class|Green Card|Message|AREAS OF INTEREST)[:\s]?[:\)\}\-\s]?", RegexOptions.IgnoreCase | RegexOptions.IgnoreCase);
                for (cnt = 0; cnt < TempDataLines.Length && tName.Trim().Length < 2; cnt++)
                {
                    if (TempDataLines[cnt].Replace(" ", "").Length > 75) continue;
                    flagName = false;
                    if (rgxPersonal.IsMatch(TempDataLines[cnt].Trim().ToString() + "     ") == false || rgxPersonal.Match(TempDataLines[cnt].Trim().ToString() + "     ").Index > 10)
                        if (rgxDob.IsMatch(" " + TempDataLines[cnt].Trim().ToString() + " ") == false || TempDataLines[cnt].Replace(" ", "").Length > 30)
                            continue;
                    int ct = 0;
                    for (ct = cnt; ct < cnt + 5 && ct < TempDataLines.Length; ct++)
                    {
                        if (TempDataLines[ct].Replace(" ", "").Length > 75 || ((ct > 2 && regNotName.IsMatch(TempDataLines[ct - 1].ToString().ToLower()) == true) || regNotName.IsMatch(" " + TempDataLines[ct].ToString().ToLower()) == true)) break;
                        if (rgxMyName.IsMatch(TempDataLines[ct].Trim().ToString() + "     ") == false) continue;
                        if (rgxMyName.Match(TempDataLines[ct].Trim().ToString() + "     ").ToString().Replace(" ", "").Length + 25 < TempDataLines[ct].Replace(" ", "").Trim().Length) continue;
                        flagName = true;
                        break;
                    }
                    if (flagName == false)
                        for (ct = cnt; ct > cnt - 4 && ct > 0; ct--)
                        {
                            if (TempDataLines[ct].Replace(" ", "").Length > 75 || ((ct > 2 && regNotName.IsMatch(TempDataLines[ct - 1].Trim().ToString().ToLower()) == true) || regNotName.IsMatch(" " + TempDataLines[ct].Trim().ToString().ToLower()) == true)) break;
                            if (rgxMyName.IsMatch(TempDataLines[ct].Trim().ToString() + "     ") == false) continue;
                            if (rgxMyName.Match(TempDataLines[ct].Trim().ToString() + "     ").ToString().Replace(" ", "").Length + 25 < TempDataLines[ct].Replace(" ", "").Trim().Length) continue;
                            flagName = true;
                            break;
                        }
                    if (flagName == false) continue;
                    TempDataLines[ct] = TempDataLines[ct].Remove(0, rgxMyName.Match(TempDataLines[ct].Trim().ToString() + "     ").ToString().Trim().Length);
                    if (rgxContinue.IsMatch(" " + TempDataLines[ct].ToString().Trim() + " ") || rgxWordsExclude.Matches(TempDataLines[ct].Trim()).Count > 5 || (TempDataLines[ct].Trim().Length > 1 && isNumberExists(TempDataLines[ct].Trim().ToCharArray()) == false)) continue;

                    if (TempDataLines[ct].Trim().Length > 1)
                        tName = TempDataLines[ct].Trim().ToString().Replace(":", "").Replace(",", "");
                    else
                        if (ct + 1 < TempDataLines.Length && isNumberExists(TempDataLines[ct + 1].Trim().ToCharArray()))
                            tName = TempDataLines[ct + 1].Trim().ToString().Replace(":", "").Replace(",", "");
                        else if (ct + 1 < TempDataLines.Length && isNumberExists(TempDataLines[ct + 1].Trim().Remove(0, 1).ToCharArray()))
                            tName = TempDataLines[ct + 1].Trim().ToString().Replace(":", "").Replace(",", "");
                    if (rgxContinue.IsMatch(" " + tName + " "))
                    {
                        tName = "";
                        continue;
                    }
                    else
                        break;
                }
            }
            catch { }
            #endregion


            //LABEL---------08
            #region "Name from Top"

            try
            {
                Regex rgxCheckNextLine = new Regex(@"Consultant|Full Time|FulTime|Consulting|Details|Profile|Page|\sInc[\.\.\s]|Functional|Certified|Certification|curicullum|curricullum|vitae|vitie|resume|curriculum|currucullum|C U R R I C U L U M|V I TA E", RegexOptions.IgnoreCase);
                bool flag = false;
                int count = 10;
                Regex regNotName = null;
                regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|address|assetas|male|female|language|passport|base info|thread|safe|Class|Green Card|Message|AREAS OF INTEREST|American Citizen)[:\s]?[:\)\}\-\s]?", RegexOptions.IgnoreCase | RegexOptions.IgnoreCase);
                //for (int cnt = 0; cnt < count && cnt < TempDataLines.Length && (tName.Length < 2 || tNameSearchFrmLast == true); cnt++)
                for (int cnt = 0; cnt < count && cnt < TempDataLines.Length && (tName.Length < 2); cnt++)
                {
                    if (rgxBreak.IsMatch(TempDataLines[cnt].ToString().Trim() + "     ") && rgxBreak.Match(TempDataLines[cnt].ToString().Trim() + "     ").Index < 5) break;
                    if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") || rgxBreaks.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Index > 5) break;
                    if (rgxBreaksLength.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length + 15 > TempDataLines[cnt].Trim().Length) break;
                    if (regNotName.IsMatch(" " + TempDataLines[cnt].Trim().ToLower()))
                        continue;
                    if (count > 0 && TempDataLines[cnt].Trim().Length < 2)
                    {
                        count++;
                        continue;
                    }
                    if (rgxCheckNextLine.IsMatch(" " + TempDataLines[cnt].ToString() + " ") && TempDataLines[cnt].Replace(" ", "").Trim().Length < 50 && TempDataLines[cnt].ToString().ToLower().IndexOf("professional profile of") < 0)
                    {
                        count = cnt + 2;
                        flag = true;
                        continue;
                    }
                    TempDataLines[cnt] = TempDataLines[cnt].Replace("STRICTLY CONFIDENTIAL", "");
                    if (TempDataLines[cnt].Replace(" ", "").Trim().Length > 100) break;

                    if (rgxContinue.IsMatch(" " + TempDataLines[cnt] + " ") && TempDataLines[cnt].ToString().ToLower().IndexOf("professional profile of") < 0) continue;
                    if (TempDataLines[cnt].Trim().EndsWith(","))
                        TempDataLines[cnt] = TempDataLines[cnt].Trim().Remove(TempDataLines[cnt].Trim().Length - 1, 1);
                    if (isNumberExists(TempDataLines[cnt].Trim().ToCharArray()) && (cnt == 0 || flag == true))
                    {
                        tName = TempDataLines[cnt].ToString().Trim().Replace("Professional Profile of", "");
                    }
                    if (tName.Length > 1 && rgxContinue.IsMatch(" " + tName.Trim() + " ") == true)
                    {
                        tName = "";
                        continue;
                    }
                }
                if (tNameSearchFrmLast == true && tName == "")
                    if (tName == "")
                        tName = tNameFrmFirst;


            }
            catch { }

            #endregion
            //LABEL---------09
            #region
            try
            {
                if (tName.Trim().Length < 2)
                {
                    bool FlagName = false;
                    bool FlagFullName = false;
                    Regex rgxMyName = new Regex(@"[1a]?\s?[\.\)]?\s*(My FullName|My Full Name|FullName|Full Name|My Name)\s*([,\-]|\s{5}|:)|\s(My Name is|name as in NRIC|name in full|name (full name)|name as appears in passport|name as appearing in passp|CANDIDATE DOSSIER|curriculum vitae of|Curriculum of)\s", RegexOptions.IgnoreCase);
                    Regex rgxMyName1 = new Regex(@"[1a]?\s?[\.\)]?\s*Name\s*([,\-]|\s{5}|:)", RegexOptions.IgnoreCase);
                    for (int cnt = 0; cnt < TempDataLines.Length; cnt++)
                    {
                        FlagFullName = false;
                        //FlagName = false;
                        if (TempDataLines[cnt].Replace(" ", "").Length > 100 || cnt > 15)
                            FlagName = true;
                        if (TempDataLines[cnt].Replace(" ", "").Length > 40) continue;
                        if (rgxMyName.Match((TempDataLines[cnt].Trim().ToString() + "     ")).Length > 4 && rgxMyName.Match((TempDataLines[cnt].Trim().ToString() + "     ")).Index < 3)
                            FlagFullName = true;
                        if (FlagFullName == false && FlagName == false)
                            if (rgxMyName1.Match((TempDataLines[cnt].Trim().ToString() + "     ")).Length < 4 || rgxMyName1.Match((TempDataLines[cnt].Trim().ToString() + "     ")).Index > 2)
                                continue;
                        if (FlagFullName == false && FlagName == true) continue;
                        if (FlagFullName == true)
                            TempDataLines[cnt] = TempDataLines[cnt].Remove(0, rgxMyName.Match(TempDataLines[cnt].Trim().ToString() + "     ").ToString().Trim().Length);
                        else
                            TempDataLines[cnt] = TempDataLines[cnt].Remove(0, rgxMyName1.Match(TempDataLines[cnt].Trim().ToString() + "     ").ToString().Trim().Length);
                        if (TempDataLines[cnt].Trim().Length > 1 && isNumberExists(TempDataLines[cnt].Trim().ToCharArray()))
                            tName = TempDataLines[cnt].Trim().ToString();
                        else
                            if (cnt + 1 < TempDataLines.Length && isNumberExists(TempDataLines[cnt + 1].Replace(":", "").Trim().ToCharArray()))
                                tName = TempDataLines[cnt + 1].Replace(":", "").Trim().ToString();
                        if (rgxContinue.IsMatch(" " + tName + " ") || tName.Length > 25)
                        {
                            tName = "";
                            continue;
                        }
                        else
                            break;
                    }
                }
            }
            catch { }
            #endregion

            //    ///checking NAME comes under address....
            //    ///

            #region checking NAME comes under address....
            //if (string.Compare(tName, "position", true) == 0)
            //    tName = "";
            //int iAddressCount = 0, iEnforceTopAssessmentSheet = 0;
            //string tDobSearch = "BORN:|Born on|Birth Date|Birth :|d. o. b|d o b|d  o  b|date and place of birth:|date and place of birth|date and country of birth|dateofbirth|data of birth|date of  birth|birthdate|date of birth/age:|date of birth/age|date of birthage|b\\'date|b’date|date  of  birth|date of birth|date ofbirth|dob|date & place of birth|d.o.b|date of birth|date   of   birth|aspiration|DATE OF BIRTH:";
            //Regex exDobSearch = new Regex(@"[\s,\(\:](" + tDobSearch.ToString() + @")[,\s]?[\s]?\b", RegexOptions.IgnoreCase);
            //Regex exDobSearch1 = new Regex(@"[\s,\(\:](" + tDobSearch.ToString() + @")[,\s]?[\s]?$", RegexOptions.IgnoreCase);

            //for (int z = 0; (z < 20 && z < TempDataLines.Length) && tName.Trim().Length <= 2; z++)
            //{
            //    if (TempDataLines[z].Trim().Length == 0 || TempDataLines[z].ToLower().Trim().IndexOf("personal data") >= 0) continue;//Personal Data
            //    if (TempDataLines[z].ToLower().Trim().IndexOf("towers") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("sector ") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("phase") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf(" po") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("phone2") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("address") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("enclave") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("floor") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("pin no") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("road") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("hostel") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("p.o") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("apartment") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("quarters") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("nagar") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("street") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("colony") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("pin -") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("main road") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("post box") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("p.o box") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("p.o") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("nilaya") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("villa") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("house") >= 0)
            //        iAddressCount++;
            //    if (TempDataLines[z].ToLower().Trim().IndexOf("position") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("nationality") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("current location") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("notice period") >= 0)
            //        iEnforceTopAssessmentSheet++;
            //    if (TempDataLines[z].ToLower().Trim().IndexOf("professional details") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("consultancy in") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("industry") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("information") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("extensive delivery") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("target job") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("work experience") >= 0 || TempDataLines[z].ToLower().Trim().IndexOf("language known") >= 0) break;
            //    if ((TempDataLines[z].ToUpper().Trim().IndexOf("BRIEF OVERVIEW") == 0 || TempDataLines[z].ToUpper().Trim().IndexOf("WORK") == 0 || TempDataLines[z].ToLower().Trim().IndexOf(":: objectives") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("job objective") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("career objective") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("professional") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("experience summary") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("personal information") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("objective") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("profile") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("summary") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("personal") == 0) && (iAddressCount > 0 || iEnforceTopAssessmentSheet >= 3) && (z - 1 >= 0))
            //    {
            //        if (TempDataLines[z - 1].ToLower().Trim().IndexOf("seeking") >= 0 || TempDataLines[z - 1].ToLower().Trim().IndexOf("career") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("objective") == 0) break;
            //        string checkStr = TempDataLines[z - 1].Trim().Replace("SAP FI/CO Consultant", "").Replace("Candidate Name", "").Replace("FIRST NAME", "").Replace("first name", "").Replace("First Name", "").Replace("Candidate Name:", "").Replace(".Full Name", "").Replace("1.Full Name", "").Replace("1.  Full Name:", "").Replace("Full Name", "").Replace("full name", "").Replace("1.  NAME", "").Replace("Name:", "").Replace("Name –", "").Replace("NAME", "").Replace("Name", "").Replace("name", "").Replace("career", "").Replace(":", "").Replace(",", "").Replace("Project Details", "").Replace("(Current Proj)", "").Replace("Gender", "").Replace("R e s u m e", "").Replace("R  E  S  E  U  M  E", "").Replace("CURRICULUM VITAE", "").Replace("Personnel Information", "").Replace("B IO DATA", "").Replace("(mr.)", "").Replace("(Mr.)", "").Replace("(MR.)", "").Replace("Telephone", "").Replace("Date of Birth", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("name", "").Replace("Resume", "").Replace("RESUME", "");
            //        if (checkStr.Trim().IndexOf("(") > 0 && (checkStr.Trim().IndexOf("total") < 0 || checkStr.Trim().IndexOf("years") < 0 || checkStr.Trim().IndexOf("year") < 0 || checkStr.Trim().IndexOf("seeking") < 0))
            //        {
            //            string[] acheckStr = checkStr.Trim().Split('(');
            //            if (isNumberExists(acheckStr[0].ToCharArray()) && tName.Trim().Length == 0 && acheckStr[0].Trim().Length > 3 && stringExist(acheckStr[0]) == false)
            //                tName = acheckStr[0].Trim();
            //        }
            //        else if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3)
            //            tName = checkStr.Trim();
            //        else if (checkStr.Trim().IndexOf("         ") > 0)
            //        {
            //            checkStr = checkStr.Substring(0, checkStr.Trim().IndexOf("         "));
            //            if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3)
            //                tName = checkStr.Trim();
            //        }

            //        else if ((z - 2 >= 0))
            //        {
            //            checkStr = TempDataLines[z - 2].Trim().Replace("SAP FI/CO Consultant", "").Replace("Candidate Name", "").Replace("FIRST NAME", "").Replace("first name", "").Replace("First Name", "").Replace("Candidate Name:", "").Replace(".Full Name", "").Replace("1.Full Name", "").Replace("1.  Full Name:", "").Replace("Full Name", "").Replace("full name", "").Replace("1.  NAME", "").Replace("Name:", "").Replace("Name –", "").Replace("NAME", "").Replace("Name", "").Replace("name", "").Replace("career", "").Replace(":", "").Replace(",", "").Replace("Project Details", "").Replace("(Current Proj)", "").Replace("Gender", "").Replace("R e s u m e", "").Replace("R  E  S  E  U  M  E", "").Replace("CURRICULUM VITAE", "").Replace("Personnel Information", "").Replace("B IO DATA", "").Replace("(mr.)", "").Replace("(Mr.)", "").Replace("(MR.)", "").Replace("Telephone", "").Replace("Date of Birth", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("name", "");
            //            if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3 && stringExist(checkStr) == false)
            //                tName = checkStr.Trim();
            //            else if ((z - 3) >= 0)
            //            {
            //                checkStr = TempDataLines[z - 3].Trim().Replace("SAP FI/CO Consultant", "").Replace("Candidate Name", "").Replace("FIRST NAME", "").Replace("first name", "").Replace("First Name", "").Replace("Candidate Name:", "").Replace(".Full Name", "").Replace("1.Full Name", "").Replace("1.  Full Name:", "").Replace("Full Name", "").Replace("full name", "").Replace("1.  NAME", "").Replace("Name:", "").Replace("Name –", "").Replace("NAME", "").Replace("Name", "").Replace("name", "").Replace("career", "").Replace(":", "").Replace(",", "").Replace("Project Details", "").Replace("(Current Proj)", "").Replace("Gender", "").Replace("R e s u m e", "").Replace("R  E  S  E  U  M  E", "").Replace("CURRICULUM VITAE", "").Replace("Personnel Information", "").Replace("B IO DATA", "").Replace("(mr.)", "").Replace("(Mr.)", "").Replace("(MR.)", "").Replace("Telephone", "").Replace("Date of Birth", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("name", "");
            //                if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3 && stringExist(checkStr) == false)
            //                    tName = checkStr.Trim();
            //            }
            //        }
            //        if (stringExist(tName) == true && tName.Trim().Length > 0) tName = "";
            //        if (tName.Trim().Length <= 2) tName = "";
            //        break;
            //    }
            //    if ((TempDataLines[z].ToLower().Trim() == "job objective") || (TempDataLines[z].ToLower().Trim() == "educational background") || TempDataLines[z].ToUpper().Trim().IndexOf("CAREER  OBJECTIVE") >= 0 || (TempDataLines[z].ToLower().Trim().IndexOf("educational qualification") >= 0) || (TempDataLines[z].ToLower().Trim().IndexOf("educational qual.:") >= 0) || (TempDataLines[z].ToLower().Trim().IndexOf("education qualification") >= 0) || (TempDataLines[z].ToLower().Trim() == "experience") || (TempDataLines[z].ToLower().Trim() == "brief summary") || (TempDataLines[z].ToLower().Trim() == "education") || (TempDataLines[z].ToLower().Trim() == "employment history") || (TempDataLines[z].ToLower().Trim() == "professional objective") || TempDataLines[z].ToLower().Trim().IndexOf("career objective:") == 0 || TempDataLines[z].ToLower().Trim().IndexOf("summary") == 0 || (TempDataLines[z].ToLower().Trim().IndexOf("summary:") == 0) || (TempDataLines[z].ToLower().Trim().IndexOf("objective") == 0) || (TempDataLines[z].ToLower().Trim().IndexOf("d.o.b.") == 0))
            //        break;
            //    if ((exDobSearch.IsMatch(" " + TempDataLines[z].Trim()) == true || exDobSearch1.IsMatch(" " + TempDataLines[z].Trim()) == true)) break;
            //}
            //exDobSearch1 = null; exDobSearch = null;


            #endregion

            //LABEL---------10
            #region "Name from Top"
            Regex rgxCheckNextLine1 = new Regex(@"Consultant|Fultime|Page|\sInc[\.\.\s]|Functional|Certified|Certification|curicullum|curricullum|vitae|vitie|resume|curriculum|currucullum", RegexOptions.IgnoreCase);
            bool flag2 = false;
            Regex rgxNumberMail = new Regex(@"\d{5,}|\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*|India|www\.[a-z]+\.", RegexOptions.IgnoreCase);
            //Regex rgxNumberMail = new Regex(@"\d{5,}|\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*|www\.[a-z]+\.", RegexOptions.IgnoreCase);
            try
            {
                Regex regNotName = null;
                regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|address|assetas|male|female|base info|client|thread|safe|Class|Green Card|Message|American Citizen)[:\s]?[:\)\}\-\s]?", RegexOptions.IgnoreCase | RegexOptions.IgnoreCase);
                int count = 10;
                for (int cnt = 0; cnt < count && cnt < TempDataLines.Length && tName.Length < 2; cnt++)
                {
                    if (rgxBreak.IsMatch(TempDataLines[cnt].ToString().Trim() + "     ") && rgxBreak.Match(TempDataLines[cnt].ToString().Trim() + "     ").Index < 5) break;
                    if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") || rgxBreaks.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Index > 5) break;
                    if (rgxBreaksLength.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length + 15 > TempDataLines[cnt].Trim().Length) break;
                    if (regNotName.IsMatch(" " + TempDataLines[cnt].Trim().ToLower()))
                        continue;
                    if ((rgxNumberMail.IsMatch(TempDataLines[cnt]) == false && flag2 == false)) continue;
                    flag2 = true;
                    if (rgxNumberMail.IsMatch(TempDataLines[cnt]))
                    {
                        count = cnt + 3;
                        continue;
                    }

                    if (rgxBreaks.IsMatch(TempDataLines[cnt]) && rgxCheckNextLine1.IsMatch(" " + TempDataLines[cnt].ToString() + " ") == false) break;

                    if (TempDataLines[cnt].Trim().Length > 75) break;
                    if (TempDataLines[cnt].Trim().Length < 2) continue;

                    if (rgxContinue.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ")) continue;
                    if (rgxContinue.IsMatch(" " + TempDataLines[cnt] + " ")) continue;
                    if (TempDataLines[cnt].Trim().EndsWith(","))
                        TempDataLines[cnt] = TempDataLines[cnt].Trim().Remove(TempDataLines[cnt].Trim().Length - 1, 1);
                    if (isNumberExists(TempDataLines[cnt].Trim().ToCharArray()) && TempDataLines[cnt].Trim().Length < 75)
                        tName = TempDataLines[cnt].ToString().Trim();
                    if (tName.Length > 2 && rgxContinue.IsMatch(" " + tName.Trim() + " ") == true)
                    {
                        tName = "";
                        continue;
                    }
                }
            }
            catch { }
            #endregion
            //LABEL-----11
            #region "Checking for regards"
            Regex rgxRegrads = new Regex(@"^\s?(Thanks and Regard|Your Sincerely|Sincerely|Regard|Yours Faithfully|Rgd)[s]?\s?.{0,1}$", RegexOptions.IgnoreCase);
            for (int innZ = 0; innZ < TempDataLines.Length && (tName.Trim().Length == 0); innZ++)
            {
                if (rgxName.IsMatch(TempDataLines[innZ].ToString().Trim() + " "))
                {
                    tName = rgxName.Match(" " + TempDataLines[innZ].ToString() + " ").ToString().Replace(":", " ").Replace("-", " ").Replace(",", " ").Trim();
                    break;
                }
                if (rgxRegrads.IsMatch(TempDataLines[innZ].Trim()))
                {

                    if (((innZ + 1) < TempDataLines.Length) && (TempDataLines[innZ + 1].Trim().ToLower().IndexOf("(") > 0 && TempDataLines[innZ + 1].Trim().ToLower().IndexOf("(") > 0))
                    {
                        add = TempDataLines[innZ + 1].Trim().Split('(', ')');
                        if (add.Length >= 1)
                        {
                            if (isNumberExists(add[0].ToCharArray()))
                            {
                                tName = add[0];
                            }
                        }
                    }
                    else
                    {
                        if (isNumberExists(TempDataLines[innZ + 1].Trim().ToCharArray()))
                        {
                            tName = TempDataLines[innZ + 1].Replace("(", "").Replace(")", "").Trim();
                        }
                    }
                }
                if (tName.Trim().Length > 2)
                {
                    //if()
                    StringBuilder strbTopName = new StringBuilder();
                    strbTopName.Append(tName.Trim());
                    strbTopName.Replace("'", " ").Replace(",", " ").Replace(".", " ");
                    ReplaceFromName(ref strbTopName, _strNameRemove);
                    tName = strbTopName.ToString().Trim();
                    //break;
                }
            }
            #endregion
            tName = tName.Trim().Replace("R E S U M E", "").Replace("R e s u m e", "").Replace("Contact Information", "").Replace("contact information", "").Replace("(", "").Replace(")", "").Replace("CURRICULUM VITAE", "").Replace("curriculum vitae", "").Replace("Date of Birth", "").Replace("E mail", "").Replace("Contact no", "").Replace("Addresses", "").Replace("Home ", "").Replace(" Home ", "").Replace("Mr ", "").Replace("NAME:", "");

            //LABEL------------12  
            #region "Name from Top"

            bool flag22 = false;
            try
            {
                int count = 15;
                Regex regNotName = null;
                regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|address|cource|assetas|male|female|base info|client|rbs|position|thread|safe|Class|AREAS OF INTEREST|Message|green Card|American Citizen)[:\s]?[:\)\}\-\s]?", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                for (int cnt = 0; cnt < count && cnt < TempDataLines.Length && tName.Length < 2; cnt++)
                {
                    if (rgxBreak.IsMatch(TempDataLines[cnt].ToString().Trim() + "     ") && rgxBreak.Match(TempDataLines[cnt].ToString().Trim() + "     ").Index < 5) break;
                    if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") || rgxBreaks.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Index > 5) break;
                    if (rgxBreaksLength.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length + 15 > TempDataLines[cnt].Trim().Length) break;
                    if (regNotName.IsMatch(" " + TempDataLines[cnt].Trim().ToLower()))
                        continue;
                    if ((rgxNumberMail.IsMatch(TempDataLines[cnt]) == false && (cnt + 1 < TempDataLines.Length - 1 && rgxNumberMail.IsMatch(TempDataLines[cnt + 1]) == false)) && flag22 == false || (TempDataLines[cnt].ToString().ToLower().Trim().IndexOf("technology") >= 0 || TempDataLines[cnt].ToString().ToLower().Trim().IndexOf("systems") >= 0 || TempDataLines[cnt].ToString().ToLower().Trim().IndexOf("position") >= 0)) continue; //TempDataLines
                    flag22 = true;
                    if (rgxNumberMail.IsMatch(TempDataLines[cnt]))
                    {
                        count = cnt + 6;
                        continue;
                    }
                    if ((rgxBreaks.IsMatch(TempDataLines[cnt]) && rgxCheckNextLine1.IsMatch(" " + TempDataLines[cnt].ToString() + " ") == false)) continue;//) || (FCommon.regLocationText != null && FCommon.regLocationText.IsMatch(" " + TempDataLines[cnt].ToString() + " "))

                    //if (TempDataLines.Length > cnt && cnt > 0)
                    //{
                    //    if (TempDataLines[cnt - 1].IndexOf("Address") >= 0) break; //
                    //    if (TempDataLines[cnt - 1].IndexOf("candicate referal sheet") >= 0) break;

                    //}
                    if (TempDataLines[cnt].Trim().Length > 75)
                    {
                        flag22 = false;
                        continue;
                    }
                    if (TempDataLines[cnt].Trim().Length < 2) continue;
                    if (rgxContinue.IsMatch(" " + TempDataLines[cnt] + " ")) continue;
                    if (TempDataLines[cnt].Trim().EndsWith(","))
                        TempDataLines[cnt] = TempDataLines[cnt].Trim().Remove(TempDataLines[cnt].Trim().Length - 1, 1);
                    if (isNumberExists(TempDataLines[cnt].Trim().ToCharArray()) && TempDataLines[cnt].Trim().Length < 75)
                        tName = TempDataLines[cnt].ToString().Trim();
                    if (tName.Length > 2 && rgxContinue.IsMatch(" " + tName.Trim() + " ") == true)
                    {
                        tName = "";
                        continue;
                    }
                }
            }
            catch { }
            #endregion

            //Label - 13
            #region "Name from Top Name will be like Name-other word, Name,Other word....."
            try
            {
                Regex rgxStartingName = new Regex(@"^([A-Za-z]{2,10}\s?[-]\s?[A-Za-z]{3,15}\s?|[A-Za-z][A-Za-z\.\s]{3,30})([:,\(\-\–\[\–]|\s{4})");
                Regex re = new Regex("[0-9]");
                Regex regNotName = null;
                regNotName = new Regex(@"[\|\s,\{\(\:](name of examination|name of organization|name of wife|name of children|fathers name|mothers name|father|mother|name of the company|name of course|name of inst|name of degree|sex|gender|location|address|professional|profile|cource|assetas|male|female|program|director|dealer|industrial|direct sales|base info|skills|pressures|explorer|basic information|native|nickname|educational|client|position|candidate referal sheet|client|date cv sent|thread|safe|Class|Green Card|Message|AREAS OF INTEREST|American Citizen)[:\s]?[:\)\}\-\s]?", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                for (int cnt = 0; cnt < 5 && cnt < TempDataLines.Length && tName.Trim().Length < 3; cnt++)
                {
                    if (rgxBreak.IsMatch(TempDataLines[cnt].ToString().Trim() + "     ") && rgxBreak.Match(TempDataLines[cnt].ToString().Trim() + "     ").Index < 5) break;
                    if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaks.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length > 10) break;
                    if (rgxBreaksLength.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length + 15 > TempDataLines[cnt].Trim().Length && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length > 10) break;
                    if (regNotName.IsMatch(" " + TempDataLines[cnt].Trim().ToLower()))
                        continue;
                    if (cnt > 2)
                    {
                        if (rgxBreaks.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") || rgxBreaks.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Index > 5) break;
                        if (rgxBreaksLength.IsMatch(" " + TempDataLines[cnt].ToString().Trim() + " ") && rgxBreaksLength.Match(" " + TempDataLines[cnt].ToString().Trim() + " ").Length + 15 > TempDataLines[cnt].Trim().Length) break;
                    }
                    if (re.IsMatch(TempDataLines[cnt].Trim() + "    ") && (cnt <= 5) && TempDataLines[cnt].ToString().Trim().Contains("(") == false) continue;
                    if (rgxStartingName.IsMatch(TempDataLines[cnt].Trim() + "    ") && (cnt <= 11))
                    {
                        tName = rgxStartingName.Match(TempDataLines[cnt].Trim().Replace(",", "")).ToString().Replace(",", "").Replace("(", "").Replace("-", " ").Replace("–", " ").Replace("[", " ").Replace(":", "");
                    }
                    else
                        if (rgxContinue.IsMatch(" " + TempDataLines[cnt].Replace("'", " ").Replace("Curriculum Vitae   :", " ").Replace("’", " ").ToString().Trim() + " ") && TempDataLines[cnt].ToLower().IndexOf("name") < 0)
                            continue;
                    if (tName.Trim().Length <= 4 && TempDataLines[cnt].Replace("Curriculum Vitae   :", " ").Trim().Length < 30 && isNumberExists(TempDataLines[cnt].Replace("Curriculum Vitae   :", " ").Trim().ToCharArray()))
                        tName = TempDataLines[cnt].Replace("Curriculum Vitae   :", " ").Replace("Name", "").Replace(":", "").Trim().ToString();
                    if (tName.Length > 2 && rgxContinue.IsMatch(" " + tName.Trim() + " ") == true)
                    {
                        tName = "";
                        continue;
                    }
                }
            }
            catch { }
            #endregion

            if (tName.Trim().Length > 1)
            {
                StringBuilder strbtName = new StringBuilder();
                strbtName.Append(tName.ToString().Trim());
                strbtName.Replace(".", " ");
                ReplaceFromName(ref strbtName, _strNameRemove);
                tName = strbtName.ToString().Trim();
            }
            tName = " " + tName + " ";
            if (rgxMrs.IsMatch(" " + tName + " "))
                tName = tName.Replace(rgxMrs.Match(" " + tName + " ").ToString(), " ");
            if (tName.Length > 0)
                tName = tName.Replace("   ", " ").Replace("  ", " ").ToString().Trim();


            #region "20 lines"

            int tEA = 0;
            //						if ( TempDataLines[0].ToLower().Trim().IndexOf("contact details")==0)
            //						{
            //							tEA=38;
            //						}
            //						
            for (int z = tEA; (z < tEA + 20 && z < TempDataLines.Length) && tName.Trim().Length == 0; z++)
            {

                ///Check the professional summary TempDataLines[z].ToLower().Trim().IndexOf("professional summary") == 0 
                if (TempDataLines[z].Trim().Length == 0) continue;
                if (TempDataLines[z].ToLower().Trim().IndexOf("educational qualification") >= 0 | TempDataLines[z].ToLower().Trim().IndexOf("educational qual.:") >= 0 | TempDataLines[z].ToLower().Trim().IndexOf("education qualification") >= 0 | TempDataLines[z].ToLower().Trim() == "experience" | TempDataLines[z].ToLower().Trim() == "brief summary" | TempDataLines[z].ToLower().Trim() == "education" | TempDataLines[z].ToLower().Trim() == "employment history" | TempDataLines[z].ToLower().Trim() == "professional objective" | TempDataLines[z].ToLower().Trim().IndexOf("career objective:") == 0 | TempDataLines[z].ToLower().Trim().IndexOf("educational") == 0 | TempDataLines[z].ToLower().Trim().IndexOf("candidate referal") == 0) break;
                int iNameBk = 0;
                for (int iNameCnt = 0; iNameCnt < strArrNameBk.Length; iNameCnt++)
                {
                    if (TempDataLines[z].ToLower().Trim().IndexOf(strArrNameBk[iNameCnt]) == 0)
                    {
                        iNameBk = 1;
                        break;
                    }
                }
                if (iNameBk == 1) break;


                ///if email is exists with name..as one string
                ///
                if (TempDataLines[z].Trim().ToLower().IndexOf("@") >= 0 && TempDataLines[z].Trim().ToLower().IndexOf("d.o.b") < 0)
                {
                    string tEmalWName = TempDataLines[z].Trim().ToLower();
                    if (tEmalWName.IndexOf("ph") == 0 | tEmalWName.IndexOf("e mail") == 0 | tEmalWName.IndexOf("email") == 0 || tEmalWName.IndexOf("e-mail") == 0 | tEmalWName.IndexOf("tel no. res") == 0 || tEmalWName.IndexOf("american citizen") == 0) continue;
                    //if (tEmalWName.IndexOf("ph") == 0 | tEmalWName.IndexOf("e mail") == 0  || tEmalWName.IndexOf("e-mail") == 0 | tEmalWName.IndexOf("tel no. res") == 0) continue;
                    add = tEmalWName.Replace("contact by", "").Replace("emailgeneral", "").Replace("email id", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("email", "").Replace("cell", "").Replace("contact number", "").Replace("contact", "").Replace("name", "").Replace("cel no", "").Replace("e mail", "").Replace("e mail", "").Replace("e mail", "").Replace("e mail:", "").Replace("e-mail", "").Replace("e  mail:", "").Replace("email", "").Replace("e- mail:", "").Replace("mail id", "").Replace("mail id", "").Replace("mail id", "").Replace("mail id", "").Replace("ph", "").Replace("mail", "").Replace("e-mail", "").Replace("mail", "").Replace("or ", "").Replace(" or ", "").Replace("(m)", "").Replace("(res)", "").Replace(")", "").Replace(" and", "").Replace(" and ", "").Replace("envelopeback", "").Replace("sex", "").Replace("gender", "").Replace("address", "").Replace(",", "").Replace("old", "").Split(' ');
                    int iSpaceCnt = 0;
                    for (int y = 0; y < add.Length; y++)
                    {
                        if (add[y].Length >= 1)
                        {
                            if (iSpaceCnt > 2) break;
                            if (isNumberExists(add[y].Trim().ToCharArray()) && !(stringExist(add[y].Trim())))
                            {
                                if (tName == "")
                                    tName = tName + " " + add[y].Trim();
                                else
                                    tName = tName + " " + add[y].Trim();
                            }
                        }
                        else if (tName.Trim().Length > 1)
                        {
                            iSpaceCnt++;
                        }
                    }
                    if (stringExist(tName) == true && tName.Trim().Length > 0 || tName.Trim().Length == 1) tName = "";
                }
                if ((TempDataLines[z].Trim().ToLower().IndexOf("mobile") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("cell no") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("contact") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("contact no") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("mob") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("(mobile)") >= 0 || TempDataLines[z].Trim().ToLower().IndexOf("mobile no") >= 0) && tName.Trim().Length == 0)
                {
                    string tContact = TempDataLines[z].ToLower();
                    add = tContact.Replace("contact by", "").Replace("emailgeneral", "").Replace("email id", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("(res)", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("e- mail:", "").Replace("name", "").Replace("sex", "").Replace("gender", "").Replace("address", "").Split(' ');
                    for (int y = 0; y < add.Length; y++)
                    {
                        if (add[y].Length >= 1)
                        {
                            if (isNumberExists(add[y].Trim().ToCharArray()) && !(stringExist(add[y])))
                                tName = tName + " " + add[y].Trim().Replace("(", "").Replace(")", "").Trim();
                        }
                    }

                }
                int iNameNot = 0;
                for (int iNameCnt = 0; iNameCnt < strArrNameNot.Length; iNameCnt++)
                {
                    if (TempDataLines[z].ToLower().ToString().Trim().ToLower().Replace("address to correspond", "").IndexOf(" " + strArrNameNot[iNameCnt] + " ") >= 0)
                    {
                        iNameNot = 1;
                        break;
                    }
                }
                if (iNameNot == 1) continue;
                if (regExNameNot.IsMatch(" " + TempDataLines[z].ToString().Trim().ToLower().Replace("address to correspond", "")) | regExNameNot1.IsMatch(" " + TempDataLines[z].ToString().Trim().ToLower().Replace("address to correspond", "")))
                    continue;


                string checkStr = TempDataLines[z].Trim().Replace("SAP FI/CO Consultant", "").Replace("Candidate Name", "").Replace("FIRST NAME", "").Replace("first name", "").Replace("First Name", "").Replace("Candidate Name:", "").Replace(".Full Name", "").Replace("1.Full Name", "").Replace("1.  Full Name:", "").Replace("Full Name", "").Replace("full name", "").Replace("1.  NAME", "").Replace("Name:", "").Replace("Name –", "").Replace("NAME", "").Replace("Name", "").Replace("name", "").Replace("career", "").Replace(":", "").Replace(",", "").Replace("Project Details", "").Replace("(Current Proj)", "").Replace("Gender", "").Replace("R e s u m e", "").Replace("R  E  S  E  U  M  E", "").Replace("CURRICULUM VITAE", "").Replace("Personnel Information", "").Replace("B IO DATA", "").Replace("(mr.)", "").Replace("(Mr.)", "").Replace("(MR.)", "").Replace("Telephone", "").Replace("Date of Birth", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("Sex", "").Replace("name", "").Replace("}", "").Replace("{", "").Replace("sex", "").Replace("Address to Correspond", "").Replace("Address", "");
                if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3)
                {
                    /// replace 1.  Full Name:
                    tName = checkStr.Trim();
                    try
                    {
                        if (TempDataLines[z + 1].ToLower().Trim().IndexOf("last name") == 0)
                        {
                            checkStr = TempDataLines[z + 2].Trim().Replace("SAP FI/CO Consultant", "").Replace("Candidate Name", "").Replace("FIRST NAME", "").Replace("first name", "").Replace("First Name", "").Replace("Candidate Name:", "").Replace(".Full Name", "").Replace("1.Full Name", "").Replace("1.  Full Name:", "").Replace("Full Name", "").Replace("full name", "").Replace("1.  NAME", "").Replace("Name:", "").Replace("Name –", "").Replace("NAME", "").Replace("Name", "").Replace("name", "").Replace("career", "").Replace(":", "").Replace(",", "").Replace("Project Details", "").Replace("(Current Proj)", "").Replace("Gender", "").Replace("Sex", "").Replace("R e s u m e", "").Replace("R  E  S  E  U  M  E", "").Replace("CURRICULUM VITAE", "").Replace("Personnel Information", "").Replace("B IO DATA", "").Replace("(mr.)", "").Replace("(Mr.)", "").Replace("sex", "").Replace("(MR.)", "").Replace("Telephone", "").Replace("Date of Birth", "").Replace("contact by", "").Replace("emailgeneral", "").Replace("email", "").Replace("general", "").Replace("information", "").Replace("mobile no.", "").Replace("mobile no", "").Replace("(mobile)", "").Replace("emailgeneral", "").Replace("information", "").Replace("mobile", "").Replace("mobile", "").Replace("mob", "").Replace("mob", "").Replace("emergency contact no.:", "").Replace("emergency contact no.:", "").Replace("emergency", "").Replace("contact no.:", "").Replace("contact no.", "").Replace("contact no", "").Replace("contact no", "").Replace("contact", "").Replace("contact", "").Replace("number", "").Replace("contact", "").Replace("mob", "").Replace("(mobile)", "").Replace("(mobile)", "").Replace("(r)", "").Replace("(r)", "").Replace("telephone", "").Replace("phone", "").Replace("phone", "").Replace("ph:", "").Replace("ph:", "").Replace("tel no. res", "").Replace("cell no", "").Replace("cell no", "").Replace("name", "").Replace("contact:", "").Replace("details", "").Replace("name", "").Replace("program", "").Replace("Directory", "");
                            if (isNumberExists(checkStr.ToCharArray()) && checkStr.Trim().Length > 3)
                                tName += " " + checkStr.Trim();
                        }
                    }
                    catch { }
                }
                if (checkStr.IndexOf("    ") >= 0 && tName.Trim().Length == 0)
                {
                    string tTEmp = checkStr;
                    checkStr = checkStr.Substring(0, checkStr.IndexOf("    "));
                    if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3)//&& !(stringExist(TempDataLines[nCnt])))
                        tName = checkStr.Trim();
                    else
                    {
                        checkStr = tTEmp.Substring(tTEmp.IndexOf("    "));
                        if (isNumberExists(checkStr.ToCharArray()) && tName.Trim().Length == 0 && checkStr.Trim().Length > 3)//&& !(stringExist(TempDataLines[nCnt])))
                            tName = checkStr.Trim();
                    }
                }
                if (stringExist(tName) == true && tName.Trim().Length > 0) tName = "";
                if (tName.Trim().Length <= 2) tName = "";
            }
            #endregion
            //LABEL------------14 
            #region "taking name from email"
            try
            {
                Regex regxEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (tName.Replace(".", "").Trim().Length < 2)
                {
                    Regex rgxNameLast = new Regex(@"^([A-Z][A-Za-z]{1,10}([\.\s]\s?[A-Z][A-Za-z]{1,10})?([\.\s]\s?[A-Z][A-Za-z]{1,10})?|[A-Z][\.\s]\s?([A-Z][\.\s]\s?)?([A-Z][\.\s]\s?)?[A-Z][A-Za-z]{1,10}([\.\s]\s?[A-Z][A-Za-z]{1,10})?).{0,1}$");
                    if (rgxNameLast.IsMatch(TempDataLines[TempDataLines.Length - 1].Trim()) && rgxContinue.IsMatch(TempDataLines[TempDataLines.Length - 1].Trim()) == false)
                        tName = TempDataLines[TempDataLines.Length - 1].Trim();
                }
                if (tName.Replace(".", "").Trim().Length < 2)
                {
                    int RowCount = 20;
                    Regex rgxNameTop = new Regex(@"^([A-Z][A-Za-z]{2,14}\s\s?[A-Z][A-Za-z]{2,14}(\s\s?[A-Z][A-Za-z]{2,14})?|[A-Z][\.\s]\s?(\s?[A-Z][\.\s]\s?)?(\s?[A-Z][\.\s]\s?)?\s?[A-Z][A-Za-z]{2,14}|[A-Z][A-Za-z]{2,14}\s\s?[A-Z][\.]?(\s?\s[A-Z][\.]?)?(\s?\s[A-Z][\.]?)?)$");
                    for (int cnt = 0; cnt < RowCount && cnt < TempDataLines.Length && tName.Replace(".", "").Trim().Length < 2; cnt++)
                    {
                        if (regxEmail.IsMatch(TempDataLines[cnt]))
                            TempDataLines[cnt] = TempDataLines[cnt].Replace(regxEmail.Match(TempDataLines[cnt]).ToString(), " ").Trim();
                        if (TempDataLines[cnt].Replace(" ", "").Length < 15 && RowCount < 15) RowCount++;
                        if (rgxNameTop.IsMatch(TempDataLines[cnt].Trim()) && rgxContinue.IsMatch(TempDataLines[cnt].Trim().Replace("Address to Correspond", "")) == false)
                            tName = TempDataLines[cnt].Trim();
                        String Name = TempDataLines[cnt].Replace("Email:", "");
                        if (rgxNameTop.IsMatch(Name.Trim()) == true && TempDataLines[cnt].ToString() != "")
                            tName = TempDataLines[cnt].Replace("Email:", "").Trim();
                    }
                }
                if (tName.Replace(".", "").Trim().Length < 2)
                {
                    int RowCount = 0;
                    Regex rgxNameTop = new Regex(@"^[\(\{\[]?\s?([A-Z][A-Za-z]{2,14}\s\s?[A-Z][A-Za-z]{2,14}(\s\s?[A-Z][A-Za-z]{2,14})?|[A-Z][\.\s](\s?[A-Z][\.\s]\s?)?(\s?[A-Z][\.\s]\s?)?\s?[A-Z][A-Za-z]{2,14}|[A-Z][A-Za-z]{2,14}\s\s?[A-Z][\.]?(\s?\s[A-Z][\.]?)?(\s?\s[A-Z][\.]?)?)\s?[\)\}\]]?$");
                    for (int cnt = TempDataLines.Length - 1; cnt > 0 && RowCount < 10 && tName.Replace(".", "").Trim().Length < 2; cnt--)
                    {
                        RowCount++;
                        if (rgxNameTop.IsMatch(TempDataLines[cnt].Trim()) && rgxContinue.IsMatch(TempDataLines[cnt].Trim()) == false)
                            tName = TempDataLines[cnt].Trim();
                    }
                    if (tName.Trim().Length > 0 && (tName.Trim().StartsWith("(") | tName.Trim().StartsWith("{") | tName.Trim().StartsWith("[")))
                        tName = tName.Trim().Remove(0, 1).Trim();
                }
            }
            catch { }
            #endregion

            if (tName.Trim().Length > 1)
            {
                StringBuilder strbtName = new StringBuilder();
                strbtName.Append(tName.ToString().Trim());
                strbtName.Replace(".", " ");
                ReplaceFromName(ref strbtName, _strNameRemove);
                tName = strbtName.ToString().Trim();
            }
            tName = " " + tName + " ";
            if (rgxMrs.IsMatch(" " + tName + " "))
                tName = tName.Replace(rgxMrs.Match(" " + tName + " ").ToString(), " ");
            if (tName.Length > 0)
                fname = tName.Replace("   ", " ").Replace("  ", " ").ToString().Trim();
            tName = "";
            fname = opGetname(fname).ToString().Replace(":", "");
        }



 private string opGetname(string strName)
        {
            string sName = "";
            try
            {
                string tName = "";
                System.Text.RegularExpressions.Regex rgxSpace = new System.Text.RegularExpressions.Regex("\\s{2,}");
                tName = strName.Trim();
                if (rgxSpace.Matches(strName.Trim()).Count > 0)
                {
                    int count = rgxSpace.Matches(tName).Count;
                    for (int icnt = 0; icnt <= count - 1; icnt++)
                    {
                        if (rgxSpace.IsMatch(tName) == true)
                        {
                            tName = tName.Replace(rgxSpace.Match(tName).ToString(), " ");
                        }
                        else
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                bool flag = true;
                for (int ct = 0; ct <= tName.Length - 1; ct++)
                {

                    if (flag == true)
                    {
                        sName = sName + tName[ct].ToString().ToUpper();
                    }
                    else
                    {
                        sName = sName + tName[ct].ToString().ToLower();
                    }

                    if (tName[ct].ToString() == " ")
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                return sName;
            }
            catch
            {
                return "";
            }

        }
