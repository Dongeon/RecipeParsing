using RecipeParsing.DBConn;
using SevenZip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace RecipeParsing
{
    class KNS
    {
        StreamWriter sw;
        public List<RecipeConfig> KNSPARSE(string filePath, string recipeName)
        {
            //string BODY_MGR_CODE = DateTime.Now.ToString("yyyyMMddHHmmss") + SequenceGeneratorGlobal.GetInstance().Next().ToString("0000");
            string sDirPath = string.Empty;
            System.IO.DirectoryInfo DI = null;
            string fileExtension = string.Empty;
            string fileRead = string.Empty;
            string fileName = string.Empty;
            string groupid = string.Empty;
            string wireMaster = string.Empty;
            string masterxValue = string.Empty;
            string masteryValue = string.Empty;
            string masteryUnit = string.Empty;
            string units = string.Empty;

            string[] fileSplit;

            RecipeConfig recipeConfig = null;

            ArrayList REFList = new ArrayList();
            ArrayList groupIdList = new ArrayList();
            List<RecipeConfig> RecipeParaList = new List<RecipeConfig>();
            ArrayList RecipeWireList = new ArrayList();

            //-- 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)

            DI = new System.IO.DirectoryInfo(filePath);
            string tgzFileName = filePath + recipeName + ".tgz";
            foreach (var item in DI.GetFiles())
            {
                if (item.Name.Equals(recipeName))
                {
                    System.IO.File.Copy(filePath + recipeName, tgzFileName, true);
                    break;
                }
            }

            sDirPath = filePath + "untgz_" + recipeName + "\\";
            DI = new DirectoryInfo(sDirPath);
            if (DI.Exists == false)
            {
                DI.Create();
            }
            FIleCompact.UnTgz(tgzFileName, sDirPath);

//            Directory.Delete(tgzFileName, true);

            fileExtension = string.Empty;
            //-- 3. Parameter(.PRM) Group ID = .WIR


            //DI = null;
            //DI = new System.IO.DirectoryInfo(sDirPath);

            foreach (var item in DI.GetFiles())
            {
                if (item.Extension == ".WIR")
                //if (item.Extension == ".PHY")
                    {
                    try
                    {
                        fileSplit = null;
                        groupIdList = new ArrayList();
                        fileExtension = item.Name;
                        fileRead = File.ReadAllText(sDirPath + fileExtension);
                        fileSplit = fileRead.Replace("\r", "").Split('\n');
                        for (int i = 0 ; i < fileSplit.Length ; i++)
                        {
                            if (fileSplit[i].Contains(".PRM"))
                            {
                                string[] groupIdSplit = fileSplit[i].Split(' ');
                                if (groupIdSplit[0].Equals("group") && groupIdSplit[1].Contains(".PRM"))
                                    groupIdList.Add(groupIdSplit[0] + groupIdSplit[3] + ":" + groupIdSplit[1]);
                            }
                        }
                        fileRead = null;
                        fileSplit = null;
                    }
                    catch (Exception ex)
                    {
                        fileRead = null;
                        fileSplit = null;
                    }
                }
                if (item.Extension == ".BND")
                {
                    try
                    {
                        fileSplit = null;
                        fileExtension = item.Name;
                        fileRead = File.ReadAllText(sDirPath + item.Name);
                        fileSplit = fileRead.Split('\n');
                        for (int i = 0 ; i < fileSplit.Length ; i++)
                        {
                            if (fileSplit[i].Contains("ref"))
                            {
                                string[] paraSplit = fileSplit[i].ToString().Replace("\t", " ").Split(' ');
                                if (paraSplit[1].Contains(".REF"))
                                    REFList.Add(paraSplit[1]);
                            }
                        }
                        fileRead = null;
                        fileSplit = null;
                    }
                    catch (Exception ex)
                    {
                        fileRead = null;
                        fileSplit = null;
                    }
                }
            }

            for (int i = 0 ; i < groupIdList.Count ; i++)
            //Get parameter
            {
                fileSplit = groupIdList[i].ToString().Split(':');
                fileName = fileSplit[1];
                groupid = fileSplit[0];
                try
                {
                    fileRead = File.ReadAllText(sDirPath + fileSplit[1]);
                    fileSplit = fileRead.Split('\n');
                    for (int j = 15 ; j < fileSplit.Length ; j++)
                    {
                        recipeConfig = new RecipeConfig();
                        recipeConfig.FILE_NAME = fileName;
                        recipeConfig.GROUP_ID = groupid;
                        recipeConfig.WB_TYPE = "P";
                        if (fileSplit[j].Contains("="))
                        {
                            string[] paraSplit = fileSplit[j].ToString().Replace(" ", ",").Split(',');
                            List<string> paraRefactoring = new List<string>();
                            //paraRefactoring.Add(group_id);
                            foreach (string str in paraSplit)
                            {
                                if (!string.IsNullOrEmpty(str) && !str.Equals("="))
                                {
                                    paraRefactoring.Add(str);
                                }
                            }
                            //# symbol          = value    units    sys_type  parm_type    class   min     max     default
                            //recipeConfig.ITEM_ID = paraRefactoring[0];
                            //recipeConfig.PARA_VALUE = paraRefactoring[1];

                            //recipeConfig.PARA_MIN = paraRefactoring[6];
                            //recipeConfig.PARA_MAX = paraRefactoring[7];
                            //recipeConfig.PARA_DEF = paraRefactoring[8];

                            recipeConfig.ITEM_ID = paraRefactoring[0];
                            recipeConfig.PARA_VALUE = paraRefactoring[1];

                            if (paraRefactoring[2].Equals("no_units"))
                                recipeConfig.ITEM_UNIT = "";
                            else
                                recipeConfig.ITEM_UNIT = paraRefactoring[2];

                            recipeConfig.PARA_sys_type = paraRefactoring[3];
                            recipeConfig.PARA_param_type = paraRefactoring[4];
                            recipeConfig.PARA_CLASS = paraRefactoring[5];
                            recipeConfig.PARA_MIN = paraRefactoring[6];
                            recipeConfig.PARA_MAX = paraRefactoring[7];
                            recipeConfig.PARA_DEF = paraRefactoring[8];

                            RecipeParaList.Add(recipeConfig);
                        }
                    }
                    fileSplit = null;
                    fileRead = null;
                    recipeConfig = null;
                }
                catch (Exception ex)
                {
                    fileSplit = null;
                    fileRead = null;
                    recipeConfig = null;
                }
            }

            groupIdList = null;
            string b = null;

            foreach (var item in DI.GetFiles())
            //Get wiremap data
            {
                List<string> wireRefactoring = new List<string>();
                try
                {
                    if (item.Extension.Equals(".REF"))
                    {
                        fileRead = File.ReadAllText(sDirPath + item.Name);
                        fileName = item.Name;
                        fileSplit = fileRead.Replace("\r", "").Split('\n');
                        wireMaster = null;
                        for (int j = 0 ; j < fileSplit.Length ; j++)
                        {
                            recipeConfig = new RecipeConfig();
                            if (fileSplit[j].Contains("name"))
                            {
                                groupid = fileSplit[j].Split('\t')[1];
                                //recipeConfig.GROUP_ID = fileSplit[j].Split('\t')[1];
                            }
                            else if (fileSplit[j].Contains("opp") && !fileSplit[j].Contains("_"))
                            {
                                string a = null;

                                if (wireMaster == null && fileSplit[j + 1].Contains("loc"))
                                {
                                    wireMaster = fileSplit[j].Split(' ')[1];
                                    masterxValue = fileSplit[j + 1].Split('\t')[4];
                                    masteryValue = fileSplit[j + 1].Split('\t')[5];
                                    masteryUnit = fileSplit[j + 1].Split('\t')[6];
                                    recipeConfig.FILE_NAME = fileName;
                                    recipeConfig.GROUP_ID = groupid;
                                    recipeConfig.ITEM_ID = wireMaster;
                                    recipeConfig.ITEM_UNIT = masteryUnit;
                                    recipeConfig.MASTER_ID = wireMaster;
                                    recipeConfig.MASTER_X_VALUE = masterxValue;
                                    recipeConfig.MASTER_Y_VALUE = masteryValue;
                                    recipeConfig.COMPARE_YN = "N";
                                    recipeConfig.WB_TYPE = "W";
                                    recipeConfig.REVISION = null;
                                    RecipeParaList.Add(recipeConfig);
                                }
                                recipeConfig = null;
                            }
                            else if (fileSplit[j].Contains("site") && fileSplit[j].Contains("{"))
                            {
                                string a = null;
                                recipeConfig.ITEM_ID = fileSplit[j].Split(' ')[0] + fileSplit[j].Split(' ')[1];
                                if (fileSplit[j + 1].Contains("loc"))
                                {
                                    recipeConfig.GROUP_ID = groupid;
                                    recipeConfig.WIRE_X_VALUE = fileSplit[j + 1].Split('\t')[4];
                                    recipeConfig.WIRE_Y_VALUE = fileSplit[j + 1].Split('\t')[5];
                                    recipeConfig.ITEM_UNIT = fileSplit[j + 1].Split('\t')[6];
                                    recipeConfig.MASTER_ID = wireMaster;
                                    recipeConfig.MASTER_X_VALUE = masterxValue;
                                    recipeConfig.MASTER_Y_VALUE = masteryValue;
                                    recipeConfig.COMPARE_YN = "Y";
                                    recipeConfig.WB_TYPE = "W";
                                    recipeConfig.FILE_NAME = fileName;
                                    recipeConfig.REVISION = null;
                                }
                                RecipeParaList.Add(recipeConfig);
                            }
                            
                        }
                        //RecipeWireList.Add(recipeConfig);
                        
                    }
                }
                catch (Exception ex)
                {
                    fileSplit = null;
                    fileRead = null;
                }
            }
            return RecipeParaList;
        }

        //-- 이름, 확장자 변환, 압축 해제
        public string Rename(string filePath, string recipeName)
        {
            string sDirPath = string.Empty;
            System.IO.DirectoryInfo DI = null;

            //-- 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)
            DI = new System.IO.DirectoryInfo(filePath);
            string tgzFileName = filePath + recipeName + ".tgz";
            foreach (var item in DI.GetFiles())
            {
                if (item.Name.Equals(recipeName))
                {
                    System.IO.File.Copy(filePath + recipeName, tgzFileName, true);
                    break;
                }
            }

            sDirPath = filePath + "untgz_" + recipeName + "\\";
            DI = new DirectoryInfo(sDirPath);
            if (DI.Exists == false)
            {
                DI.Create();
            }

            if (FIleCompact.UnTgz(tgzFileName, sDirPath))
                return sDirPath;
            else
                return null;

        }

        //-- PRM Parsing
        public List<RecipeConfigKnsPrm> KNSParaPARSE(string sDirPath, string recipeName)
        {
            //string BODY_MGR_CODE = DateTime.Now.ToString("yyyyMMddHHmmss") + SequenceGeneratorGlobal.GetInstance().Next().ToString("0000");
            //string sDirPath = string.Empty;
            System.IO.DirectoryInfo DI = null;
            string fileExtension = string.Empty;
            string fileRead = string.Empty;
            string fileName = string.Empty;
            string groupid = string.Empty;
            string wireMaster = string.Empty;
            string masterxValue = string.Empty;
            string masteryValue = string.Empty;
            string masteryUnit = string.Empty;
            string units = string.Empty;

            string[] fileSplit;

            RecipeConfigKnsPrm recipeConfig = null;

            ArrayList REFList = new ArrayList();
            ArrayList groupIdList = new ArrayList();
            List<RecipeConfigKnsPrm> RecipeParaList = new List<RecipeConfigKnsPrm>();
            ArrayList RecipeWireList = new ArrayList();

            #region
            //-- 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)

            //DI = new System.IO.DirectoryInfo(filePath);
            //string tgzFileName = filePath + recipeName + ".tgz";
            //foreach (var item in DI.GetFiles())
            //{
            //    if (item.Name.Equals(recipeName))
            //    {
            //        System.IO.File.Copy(filePath + recipeName, tgzFileName, true);
            //        break;
            //    }
            //}

            //sDirPath = filePath + "untgz_" + recipeName + "\\";
            //DI = new DirectoryInfo(sDirPath);
            //if (DI.Exists == false)
            //{
            //    DI.Create();
            //}
            //FIleCompact.UnTgz(tgzFileName, sDirPath);

            //            Directory.Delete(tgzFileName, true);

            //fileExtension = string.Empty;
            //-- 3. Parameter(.PRM) Group ID = .WIR


            //DI = null;
            #endregion
            DI = new System.IO.DirectoryInfo(sDirPath);

            foreach (var item in DI.GetFiles())
            {
                if (item.Extension == ".WIR")
                //if (item.Extension == ".PHY")
                {
                    try
                    {
                        fileSplit = null;
                        groupIdList = new ArrayList();
                        fileExtension = item.Name;
                        fileRead = File.ReadAllText(sDirPath + fileExtension);
                        fileSplit = fileRead.Replace("\r", "").Split('\n');
                        for (int i = 0 ; i < fileSplit.Length ; i++)
                        {
                            if (fileSplit[i].Contains(".PRM"))
                            {
                                string[] groupIdSplit = fileSplit[i].Split(' ');
                                if (groupIdSplit[0].Equals("group") && groupIdSplit[1].Contains(".PRM"))
                                    groupIdList.Add(groupIdSplit[0] + groupIdSplit[3] + ":" + groupIdSplit[1]);
                            }
                        }
                        fileRead = null;
                        fileSplit = null;
                    }
                    catch (Exception ex)
                    {
                        fileRead = null;
                        fileSplit = null;
                    }
                }
            }

            for (int i = 0 ; i < groupIdList.Count ; i++)
            //Get parameter
            {
                fileSplit = groupIdList[i].ToString().Split(':');
                fileName = fileSplit[1];
                groupid = fileSplit[0];
                try
                {
                    fileRead = File.ReadAllText(sDirPath + fileSplit[1]);
                    fileSplit = fileRead.Split('\n');
                    for (int j = 15 ; j < fileSplit.Length ; j++)
                    {
                        recipeConfig = new RecipeConfigKnsPrm();
                        recipeConfig.FILE_NAME = fileName;
                        recipeConfig.GROUP_ID = groupid;
                        recipeConfig.WB_TYPE = "P";
                        if (fileSplit[j].Contains("="))
                        {
                            string[] paraSplit = fileSplit[j].ToString().Replace(" ", ",").Split(',');
                            List<string> paraRefactoring = new List<string>();
                            //paraRefactoring.Add(group_id);
                            foreach (string str in paraSplit)
                            {
                                if (!string.IsNullOrEmpty(str) && !str.Equals("="))
                                {
                                    paraRefactoring.Add(str);
                                }
                            }
                            //# symbol          = value    units    sys_type  parm_type    class   min     max     default
                            //recipeConfig.ITEM_ID = paraRefactoring[0];
                            //recipeConfig.PARA_VALUE = paraRefactoring[1];

                            //recipeConfig.PARA_MIN = paraRefactoring[6];
                            //recipeConfig.PARA_MAX = paraRefactoring[7];
                            //recipeConfig.PARA_DEF = paraRefactoring[8];

                            recipeConfig.ITEM_ID = paraRefactoring[0];
                            recipeConfig.PARA_VALUE = paraRefactoring[1];

                            if (paraRefactoring[2].Equals("no_units"))
                                recipeConfig.ITEM_UNIT = "";
                            else
                                recipeConfig.ITEM_UNIT = paraRefactoring[2];

                            recipeConfig.PARA_sys_type = paraRefactoring[3];
                            recipeConfig.PARA_param_type = paraRefactoring[4];
                            recipeConfig.PARA_CLASS = paraRefactoring[5];
                            recipeConfig.PARA_MIN = paraRefactoring[6];
                            recipeConfig.PARA_MAX = paraRefactoring[7];
                            recipeConfig.PARA_DEF = paraRefactoring[8];

                            RecipeParaList.Add(recipeConfig);
                        }
                    }
                    fileSplit = null;
                    fileRead = null;
                    recipeConfig = null;
                }
                catch (Exception ex)
                {
                    fileSplit = null;
                    fileRead = null;
                    recipeConfig = null;
                }
            }
            return RecipeParaList;
        }

        public List<RecipeConfigKnsWm> KNSWirePARSE(string sDirPath, string recipeName)
        {
            //string BODY_MGR_CODE = DateTime.Now.ToString("yyyyMMddHHmmss") + SequenceGeneratorGlobal.GetInstance().Next().ToString("0000");
            //string sDirPath = string.Empty;
            System.IO.DirectoryInfo DI = null;
            string fileExtension = string.Empty;
            string fileRead = string.Empty;
            string fileName = string.Empty;
            string groupid = string.Empty;
            string wireMaster = string.Empty;
            string masterxValue = string.Empty;
            string masteryValue = string.Empty;
            string masteryUnit = string.Empty;
            string units = string.Empty;
            string RESULT_X_VALUE = string.Empty;
            string RESULT_Y_VALUE = string.Empty;
            string X_LSL = string.Empty;
            string X_USL = string.Empty;
            string Y_LSL = string.Empty;
            string Y_USL = string.Empty;


            string[] fileSplit;

            RecipeConfigKnsWm recipeConfig = null;

            ArrayList REFList = new ArrayList();
            ArrayList groupIdList = new ArrayList();
            List<RecipeConfigKnsWm> RecipeParaList = new List<RecipeConfigKnsWm>();
            ArrayList RecipeWireList = new ArrayList();
            #region
            //-- 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)

            //DI = new System.IO.DirectoryInfo(filePath);
            //string tgzFileName = filePath + recipeName + ".tgz";
            //foreach (var item in DI.GetFiles())
            //{
            //    if (item.Name.Equals(recipeName))
            //    {
            //        System.IO.File.Copy(filePath + recipeName, tgzFileName, true);
            //        break;
            //    }
            //}

            //sDirPath = filePath + "untgz_" + recipeName + "\\";
            //DI = new DirectoryInfo(sDirPath);
            //if (DI.Exists == false)
            //{
            //    DI.Create();
            //}
            //FIleCompact.UnTgz(tgzFileName, sDirPath);

            //            Directory.Delete(tgzFileName, true);

            //fileExtension = string.Empty;
            //-- 3. Parameter(.PRM) Group ID = .WIR


            //DI = null;
            #endregion
            DI = new System.IO.DirectoryInfo(sDirPath);

            foreach (var item in DI.GetFiles())
            {
                if (item.Extension == ".BND")
                {
                    try
                    {
                        fileSplit = null;
                        fileExtension = item.Name;
                        fileRead = File.ReadAllText(sDirPath + item.Name);
                        fileSplit = fileRead.Split('\n');
                        for (int i = 0 ; i < fileSplit.Length ; i++)
                        {
                            if (fileSplit[i].Contains("ref"))
                            {
                                string[] paraSplit = fileSplit[i].ToString().Replace("\t", " ").Split(' ');
                                if (paraSplit[1].Contains(".REF"))
                                    REFList.Add(paraSplit[1]);
                            }
                        }
                        fileRead = null;
                        fileSplit = null;
                    }
                    catch (Exception ex)
                    {
                        fileRead = null;
                        fileSplit = null;
                    }
                }
            }

            groupIdList = null;
            string b = null;

            foreach (var item in DI.GetFiles())
            //Get wiremap data
            {
                List<string> wireRefactoring = new List<string>();
                try
                {
                    if (item.Extension.Equals(".REF"))
                    {
                        fileRead = File.ReadAllText(sDirPath + item.Name);
                        fileName = item.Name;
                        fileSplit = fileRead.Replace("\r", "").Split('\n');
                        wireMaster = null;
                        for (int j = 0 ; j < fileSplit.Length ; j++)
                        {
                            recipeConfig = new RecipeConfigKnsWm();
                            if (fileSplit[j].Contains("name"))
                            {
                                groupid = fileSplit[j].Split('\t')[1];
                                //recipeConfig.GROUP_ID = fileSplit[j].Split('\t')[1];
                            }
                            else if (fileSplit[j].Contains("opp") && !fileSplit[j].Contains("_"))
                            {
                                string a = null;

                                if (wireMaster == null && fileSplit[j + 1].Contains("loc"))
                                {
                                    wireMaster = fileSplit[j].Split(' ')[1];
                                    masterxValue = fileSplit[j + 1].Split('\t')[4];
                                    masteryValue = fileSplit[j + 1].Split('\t')[5];
                                    masteryUnit = fileSplit[j + 1].Split('\t')[6];
                                    recipeConfig.FILE_NAME = fileName;
                                    recipeConfig.GROUP_ID = groupid;
                                    recipeConfig.ITEM_ID = wireMaster;
                                    recipeConfig.ITEM_UNIT = masteryUnit;
                                    recipeConfig.MASTER_ID = wireMaster;
                                    recipeConfig.MASTER_X_VALUE = masterxValue;
                                    recipeConfig.MASTER_Y_VALUE = masteryValue;
                                    recipeConfig.COMPARE_YN = "N";
                                    recipeConfig.FILE_NAME = fileName;
                                    recipeConfig.REVISION = null;
                                    RecipeParaList.Add(recipeConfig);
                                }
                                recipeConfig = null;
                            }
                            else if (fileSplit[j].Contains("site") && fileSplit[j].Contains("{"))
                            {
                                string a = null;
                                string itemasd = fileSplit[j].ToString();
                                recipeConfig.ITEM_ID = fileSplit[j].Split(' ')[0] +" "+ fileSplit[j].Split(' ')[1];
                                if (fileSplit[j + 1].Contains("loc"))
                                {

                                    recipeConfig.GROUP_ID = groupid;
                                    recipeConfig.WIRE_X_VALUE = fileSplit[j + 1].Split('\t')[4];
                                    recipeConfig.WIRE_Y_VALUE = fileSplit[j + 1].Split('\t')[5];
                                    recipeConfig.ITEM_UNIT = fileSplit[j + 1].Split('\t')[6];
                                    recipeConfig.MASTER_ID = wireMaster;
                                    recipeConfig.MASTER_X_VALUE = masterxValue;
                                    recipeConfig.MASTER_Y_VALUE = masteryValue;
                                    recipeConfig.COMPARE_YN = "Y";
                                    recipeConfig.FILE_NAME = fileName;
                                    recipeConfig.WB_VALUE = "10";
                                    recipeConfig.RESULT_X_VALUE = calculationResult(recipeConfig.MASTER_X_VALUE, recipeConfig.WIRE_X_VALUE);
                                    recipeConfig.RESULT_Y_VALUE = calculationResult(recipeConfig.MASTER_Y_VALUE, recipeConfig.WIRE_Y_VALUE);
                                    recipeConfig.X_LSL = calculationLsl(recipeConfig.RESULT_X_VALUE, recipeConfig.WB_VALUE);
                                    recipeConfig.X_USL = calculationUsl(recipeConfig.RESULT_X_VALUE, recipeConfig.WB_VALUE);
                                    recipeConfig.Y_LSL = calculationLsl(recipeConfig.RESULT_Y_VALUE, recipeConfig.WB_VALUE);
                                    recipeConfig.Y_USL = calculationUsl(recipeConfig.RESULT_Y_VALUE, recipeConfig.WB_VALUE);
                                    recipeConfig.VALID = valid(recipeConfig.RESULT_X_VALUE, recipeConfig.X_LSL, recipeConfig.X_USL, recipeConfig.RESULT_Y_VALUE, recipeConfig.Y_LSL, recipeConfig.Y_USL).ToString();

                                    recipeConfig.REVISION = null;

                                }
                                RecipeParaList.Add(recipeConfig);
                            }

                        }
                        //RecipeWireList.Add(recipeConfig);

                    }
                }
                catch (Exception ex)
                {
                    fileSplit = null;
                    fileRead = null;
                }

            }
            return RecipeParaList;
        }

        public string calculationResult(string master, string wireValue)
        {
            string result = (float.Parse(master) - float.Parse(wireValue)).ToString();
            return result;
        }

        public string calculationLsl(string result, string wbValue)
        {
            return (float.Parse(result) - float.Parse(wbValue)).ToString();
        }
        public string calculationUsl(string result, string wbValue)
        {
            return (float.Parse(result) + float.Parse(wbValue)).ToString();
        }

        public bool valid(string x, string x_lsl, string x_usl, string y, string y_lsl, string y_usl)
        {
            if (float.Parse(x_lsl) < float.Parse(x) || float.Parse(x) < float.Parse(x_usl)
             || float.Parse(y_lsl) < float.Parse(y) || float.Parse(y) < float.Parse(y_usl))
            {
                return true;
            }
            else
                return false;
            
        }

        public void RecipeSave()
        {

        }
    
        #region
        /*
        2019 Winpac Project

        Recipe_Files(KNS)
                1. 이름변경 'Recipename.tgz'
                2. TGZ 압축해제
                3. Parameter(.PRM) Group ID = .WIR

                    ex)
                    group EC90CA41.PRM 2 2 2 2 2 2 

                    group EC90CA42.PRM 3 3 3 3 3 3 

                    group EC90CA2A.PRM 1 1 1 1 1 1 

                    group EC90CA43.PRM 4 4 4 4 4 4 
                4. Parameter 기준 :  symbol          = value    units sys_type  parm_type    class min     max     default 
                5. WireMap Master Data : 각.REF 파일의 opp 중 의 첫번째(OP0, OP1)

                    ex)
                    opp OP0
                {		(이것만 사용)
                    loc		=	195.91861	-78.662544	mils
                    mag		= 6.0
                    v_ill_p
                    {
                        red = 0.0

                        blue = 8.0

                    }
                    o_ill_p
                    {
                        red = 0.0

                        blue = 0.0

                    }
                    ill_optimization_mode	= CONTRAST
                    ltol		= 0.5
                    opp_window	=	5.0	5.0	mils
                    opp_win_angle	= 0.0	deg
                    snapshot_img	=	CV908A25.JPG
                }
                opp OP1
                {
                    loc		=	-195.87097	97.659302	mils
                    mag		= 6.0
                        ill_optimization_mode	= CONTRAST
                    snapshot_img	=	CV908A28.JPG
                }
                6. WireMap Spec Data : ex 참고


                    ex)
                    site 2 {	(para id)
                        loc		=	219.51819	-80.809013	mils(X value, Y value, 단위)

                        auto_frdg	= 1.0
                        compliance	= 0.0
                    }

                    Group ID : 	
                    ex)
                    name DIE_1(Group Id)

                    boundary	{
                        loc	=	-198.677	-100.9798	mils
                    }	
                    boundary	{
                        loc	=	198.36142	101.72874	mils
                    }	
                    boundary	{
                        loc	=	198.6272	-100.29877	mils
                    }	
                    boundary	{
                        loc	=	-198.67636	100.82072	mils
                    }	
                7. Compare 기준 : 
                    7-1 : Result = Master X - Spec X  && Master Y - Spec Y
                    7-2 : Spec LSL, Spec USL = Result +- WireMapValue
                    7-3 : lsl, usl in out 에 따른 valid, invalid

                8. .BND file 에서 읽어올 .ref 파일 읽어옴
            */
        #endregion

        /// <summary>
        /// 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)
        /// </summary>
        /// <param name="path"></param>

        public void RenameUntgzRecipeFile(string filePath, string recipeName)
        {
            string sDirPath = string.Empty;
            System.IO.DirectoryInfo DI = null;

            //-- 1. 이름변경 'Recipename.tgz', 2. TGZ 압축해제(untgz_RECIPENAME)

            DI = new System.IO.DirectoryInfo(filePath);
            string tgzFileName = filePath + recipeName + ".tgz";
            foreach (var item in DI.GetFiles())
            {
                if (item.Name.Equals(recipeName))
                {
                    System.IO.File.Copy(filePath+recipeName, tgzFileName, true);
                }
            }
            
            sDirPath = filePath + "untgz_" + recipeName + "\\";
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }
            FIleCompact.UnTgz(tgzFileName, sDirPath);
        }

        public ArrayList GetRef(string path)
        {
            try
            {
                Dictionary<string, string> Data = new Dictionary<string, string>();

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                FileSystemInfo[] fsi = di.GetFileSystemInfos();
                System.IO.DirectoryInfo[] folder = di.GetDirectories();

                ArrayList al = new ArrayList();

                int fileCnt = fsi.Count();

                string fileExtension = "";

                string[] oper = path.Split('\\');

                int cnt = 0;

                foreach (var item in di.GetFiles())
                {
                    fileExtension = item.Extension;

                    if (fileExtension == ".BND")
                    {
                        fileExtension = item.Name;

                        string fileRead = File.ReadAllText(path);
                        string[] filesplit = fileRead.Split('\n');

                        al.Add("ref");
                    }
                }
                return al;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 3. Parameter(.PRM) Group ID = .WIR
        /// </summary>
        /// <param name="path"></param>
        public List<string> GetGroupID(string path)
        {
            Dictionary<string, string> Data = new Dictionary<string, string>();
            List<string> groupIdList = null;
            System.IO.DirectoryInfo DI = new System.IO.DirectoryInfo(path);

            string fileExtension = string.Empty;

            foreach (var item in DI.GetFiles())
            {
                if (item.Extension == ".WIR")
                {
                    groupIdList = new List<string>();
                    fileExtension = item.Name;
                    string fileRead = File.ReadAllText(path + fileExtension);
                    string[] fileSplit = fileRead.Replace("\r", "").Split('\n');
                    for (int i = 0 ; i < fileSplit.Length ; i++)
                    {
                        if (fileSplit[i].Contains(".PRM"))
                        {

                            string[] groupIdSplit = fileSplit[i].Split(' ');
                            if(groupIdSplit[0].Equals("group") && groupIdSplit[1].Contains(".PRM"))
                            {
                                groupIdList.Add(groupIdSplit[0] + groupIdSplit[3] + ":" + groupIdSplit[1]);
                            }
                        }
                    }
                    break;
                }
            }
            return groupIdList;
        }

        public void ParaParsing(List<string> groupIdList)
        {
            foreach (string str in groupIdList)
            {
                string[] fileSplit = str.Split(':');

            }
        }

        public List<KNS> KNSRecipeParse()
        {

            return null;
        }



        //-- save
        public void knsParameterChangeSave()
        {
            string filename = string.Empty;
            string paraId = string.Empty;

            #region -- Parameter Change Save
            if (Global.changeKnsParaList.Count != 0)
            {
                int counter = Global.changeKnsParaList.Count;
                int indexin = 0;

                foreach (RecipeConfigKnsPrm para in Global.changeKnsParaList)
                {
                    filename = para.FILE_NAME;
                    paraId = para.ITEM_ID;

                    string[] lines = File.ReadAllLines(Global.FilePath + filename);
                    sw = new StreamWriter(Global.FilePath + filename, false);
                    for (int i = 0 ; i < lines.Length ; i++)
                    {
                        //# symbol          = value    units    sys_type  parm_type    class   min     max     default
                        string index = "";
                        //string hellyeah = para.ITEM_ID + "\r=\r" + para.PARA_VALUE + "\r" + para.ITEM_UNIT + "\r" + para.PARA_sys_type + "\r" + para.PARA_param_type + "\r" + para.PARA_CLASS
                        //+ "\r" + para.PARA_MIN + "\r" + para.PARA_MAX + "\r" + para.PARA_DEF + "\r" + para.PARA_MIN + "\r" + para.PARA_MAX;

                        //-- unit 처리
                        if (string.IsNullOrEmpty(para.ITEM_UNIT))
                        {
                            para.ITEM_UNIT = "no_units";
                        }

                        //-- value 값 min max 범위 내 설정
                        int value = Convert.ToInt32(para.PARA_VALUE);
                        int min = Convert.ToInt32(para.PARA_MIN);
                        int max = Convert.ToInt32(para.PARA_MAX);


                        string hellyeah = para.ITEM_ID + " = " + para.PARA_VALUE + "       " + para.ITEM_UNIT + "  " + para.PARA_sys_type + "     " + para.PARA_param_type + " " + para.PARA_CLASS + " " + para.PARA_MIN + " " + para.PARA_MAX + " " + para.PARA_DEF + "           " + para.PARA_MIN + "        " + para.PARA_MAX;
                        if (lines[i].Contains(paraId))
                        {
                            if (i == 0)
                            {
                                for (int j = 1 ; j < lines.Length ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                sw.Close();
                            }
                            else
                            {
                                for (int j = 0 ; j < i ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                sw.WriteLine(hellyeah);
                                for (int j = i + 1 ; j < lines.Length ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                sw.Close();
                            }
                            //-- 해당 라인 참조
                            
                            //sw.WriteLine(hellyeah);

                            indexin++;
                            if (counter == indexin)
                                break;
                        }
                    }
                }
                Global.changeKnsParaList.RemoveRange(0,counter);
                if (Global.changeKnsParaList.Count == 0)
                    return;
                else
                {
                    Global.changeKnsParaList = null;
                } 
            }
            #endregion
        }
        public void knsWiremapChangeSave()
        {
            string filename = string.Empty;
            string paraId = string.Empty;
            string groupId = string.Empty;

            #region -- Parameter Change Save
            if (Global.changeKnsWireList.Count != 0)
            {
                int counter = Global.changeKnsWireList.Count;
                int indexin = 0;

                foreach (RecipeConfigKnsWm wire in Global.changeKnsWireList)
                {
                    filename = wire.FILE_NAME;
                    paraId = wire.ITEM_ID;

                    string[] lines = File.ReadAllLines(Global.FilePath + filename);
                    sw = new StreamWriter(Global.FilePath + filename, false);
                    for (int i = 0 ; i < lines.Length ; i++)
                    {
                        //# symbol          = value    units    sys_type  parm_type    class   min     max     default
                        string index = "";
                        //string hellyeah = para.ITEM_ID + "\r=\r" + para.PARA_VALUE + "\r" + para.ITEM_UNIT + "\r" + para.PARA_sys_type + "\r" + para.PARA_param_type + "\r" + para.PARA_CLASS
                        //+ "\r" + para.PARA_MIN + "\r" + para.PARA_MAX + "\r" + para.PARA_DEF + "\r" + para.PARA_MIN + "\r" + para.PARA_MAX;

                        //-- unit 처리
                        if (string.IsNullOrEmpty(wire.ITEM_UNIT))
                        {
                            wire.ITEM_UNIT = "no_units";
                        }

                        //-- value 값 min max 범위 내 설정
                        //int value = Convert.ToInt32(wire.PARA_VALUE);
                        //int min = Convert.ToInt32(wire.PARA_MIN);
                        //int max = Convert.ToInt32(wire.PARA_MAX);


                        //string hellyeah = wire.ITEM_ID + " = " + wire.PARA_VALUE + "       " + wire.ITEM_UNIT + "  " + wire.PARA_sys_type + "     " + wire.PARA_param_type + " " + wire.PARA_CLASS + " " + wire.PARA_MIN + " " + wire.PARA_MAX + " " + wire.PARA_DEF + "           " + wire.PARA_MIN + "        " + wire.PARA_MAX;
                        if (lines[i].Contains(paraId))
                        {
                            if (i == 0)
                            {
                                for (int j = 1 ; j < lines.Length ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                sw.Close();
                            }
                            else
                            {
                                for (int j = 0 ; j < i ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                //sw.WriteLine(hellyeah);
                                for (int j = i + 1 ; j < lines.Length ; j++)
                                {
                                    sw.WriteLine(lines[j]);
                                }
                                sw.Close();
                            }
                            //-- 해당 라인 참조

                            //sw.WriteLine(hellyeah);

                            indexin++;
                            if (counter == indexin)
                                break;
                        }
                    }
                }
                Global.changeKnsWireList.RemoveRange(0, counter);
                if (Global.changeKnsWireList.Count == 0)
                    return;
                else
                {
                    Global.changeKnsWireList = null;
                }
            }
            #endregion
        }

    }
}
