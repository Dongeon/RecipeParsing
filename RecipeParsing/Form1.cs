using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RecipeParsing;

namespace RecipeParsing
{
    public partial class Form1 : Form
    {
        //List<RecipeConfig> ParaList = null;



        public Form1()
        {
            InitializeComponent();

            //-- gitHub Test
            //-- xps github test
            
            KNS kns = new KNS();
            Global gl = new Global();
            string sDirPath = kns.Rename(System.AppDomain.CurrentDomain.BaseDirectory + "RECIPE\\KNS\\WBK01\\UPLOAD\\", "FDFB162080061");

            Global.FilePath = sDirPath;
            Global.RecipeName = "FDFB162080061";

            Global.KnsParaList = kns.KNSParaPARSE(Global.FilePath, Global.RecipeName);
            Global.KnsWireList = kns.KNSWirePARSE(Global.FilePath, Global.RecipeName);
            
            this.Text = Global.RecipeName;
            dataGridViewPara.DataSource = Global.KnsParaList;
            dataGridViewWM.DataSource = Global.KnsWireList;


            ////-- OPEN
            //string RCP_MGR_CODE = string.Empty;

            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "OPEN RECIPE";
            //ofd.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "RECIPE\\";

            ////파일 오픈창 로드
            //DialogResult dr = ofd.ShowDialog();

            ////OK버튼 클릭시
            //if (dr == DialogResult.OK)
            //{
            //    //File명과 확장자를 가지고 온다.
            //    string[] res = Open(ofd);

            //    if (res[0].Contains("KNS"))
            //    {
            //        KNS kns = new KNS();
            //        //List<RecipeConfig> ParaList = new List<RecipeConfig>();
            //        Global.changeParaList = null;
            //        string sDirPath = kns.Rename(res[0], res[1]);
            //        Global.KnsParaList = kns.KNSParaPARSE(sDirPath, res[1]);
            //        Global.KnsWireList = kns.KNSWirePARSE(sDirPath, res[1]);

            //        this.Text = Global.RecipeName;
            //        dataGridViewPara.DataSource = Global.KnsParaList;
            //        dataGridViewWM.DataSource = Global.KnsWireList;
            //    }
            //    else if (res[0].Contains("SKW"))
            //    {

            //    }
            //    else if (res[0].Contains("FCB"))
            //    {

            //    }
            //}
            ////취소버튼 클릭시 또는 ESC키로 파일창을 종료 했을경우
            //else if (dr == DialogResult.Cancel)
            //{
            //    this.Close();
            //}
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            //--

            if (string.IsNullOrEmpty(lblStatus.Text))
                MessageBox.Show("종료하시겠습니까?");
            else
                MessageBox.Show("저장하시겠습니까?");


            //foreach(RecipeConfig rep in ParaList)
            //{
            //    if (string.IsNullOrEmpty(rep.REVISION))
            //        resRevision = false;
            //    else
            //    {
            //        resRevision = true;
            //        break;
            //    }
            //}
            //if (resRevision)

            //else


        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //-- OPEN
            string RCP_MGR_CODE = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "OPEN RECIPE";
            ofd.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "RECIPE\\";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string[] res = Open(ofd);
                

                if (res[0].Contains("KNS"))
                {
                    KNS kns = new KNS();
                    string sDirPath = kns.Rename(res[0], res[1]);
                    //List<RecipeConfig> ParaList = new List<RecipeConfig>();
                    Global.changeParaList = null;
                    Global.KnsParaList = kns.KNSParaPARSE(sDirPath, res[1]);
                    Global.KnsWireList = kns.KNSWirePARSE(sDirPath, res[1]);

                    this.Text = Global.RecipeName;
                    dataGridViewPara.DataSource = Global.KnsParaList;
                    dataGridViewWM.DataSource = Global.KnsWireList;

                    lblStatus.Text = null;
                }
                else if (res[0].Contains("SKW"))
                {

                }
                else if (res[0].Contains("FCB"))
                {

                }
            }
            //취소버튼 클릭시 또는 ESC키로 파일창을 종료 했을경우
            else if (dr == DialogResult.Cancel)
            {
                string[] res = null;
            }
        }

        public string[] Open(OpenFileDialog ofd)
        {
            string fileName = null;
            string fileFullName = null;
            string filePath = null;

            //File명과 확장자를 가지고 온다.
            fileName = ofd.SafeFileName;

            //File경로와 File명을 모두 가지고 온다.
            fileFullName = ofd.FileName;

            //File경로만 가지고 온다.
            filePath = fileFullName.Replace(fileName, "");

            Global.RecipeName = fileName;
            Global.FilePath = filePath;

            string[] result = { Global.FilePath, Global.RecipeName };

            return result;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //-- SAVE
            if (!string.IsNullOrEmpty(lblStatus.Text))
            {
                KNS kns = new KNS();
                kns.knsParameterChangeSave();
                kns.knsWiremapChangeSave();

                Global.KnsParaList = kns.KNSParaPARSE(Global.FilePath, Global.RecipeName);
                Global.KnsWireList = kns.KNSWirePARSE(Global.FilePath, Global.RecipeName);

                this.Text = Global.RecipeName;
                dataGridViewPara.DataSource = Global.KnsParaList;
                dataGridViewWM.DataSource = Global.KnsWireList;

                lblStatus.Text = null;
            }
            else
            {

            }
        }


        private void sAVEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //-- SAVE   
            if (!string.IsNullOrEmpty(lblStatus.Text))
            {
                KNS kns = new KNS();
                kns.knsParameterChangeSave();
                lblStatus.Text = null;
            }
            else
            {

            }
        }


        private void sAVEASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //-- 저장, SaveAs
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oPENDIRECTORYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //--
            string path1 = Global.FilePath + "untgz_" + Global.RecipeName;
            //string filepath = @path;
            System.Diagnostics.Process.Start(path1);
        }

        private void dataGridViewPara_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //-- Revision row Data
            if (Global.FilePath.Contains("KNS"))
            {
                int colindex = e.ColumnIndex;
                RecipeConfigKnsPrm revision = Global.KnsParaList[e.RowIndex];


                //-- 범위 검사
                if(e.ColumnIndex == 4)
                {
                    if(Convert.ToInt32(revision.PARA_VALUE) > Convert.ToInt32(revision.PARA_MAX) || Convert.ToInt32(revision.PARA_VALUE) < Convert.ToInt32(revision.PARA_MIN))
                    {
                        MessageBox.Show("out of range.");
                        return;
                    }   
                }

                //-- changeKnsParaList 의 중복 검사
                if (Global.changeKnsParaList.Count == 0)
                {
                    Global.changeKnsParaList.Add(revision);
                }
                else
                {
                    List<int> dblIndx = new List<int>();

                    //-- set check list
                    for(int i = 0 ; i < Global.changeKnsParaList.Count ; i++)
                    {
                        string checkGroupItem = Global.changeKnsParaList[i].GROUP_ID + "/" + Global.changeKnsParaList[i].ITEM_ID;
                        string revisionGroupItem = revision.GROUP_ID + "/" + revision.ITEM_ID;
                        if(checkGroupItem == revisionGroupItem)
                        {
                            dblIndx.Add(i);
                        }
                    }

                    if(dblIndx.Count == 0)
                    {
                        Global.changeKnsParaList.Add(revision);
                    }
                    else
                    {
                        foreach(int removeIndex in dblIndx)
                        {
                            Global.changeKnsParaList.RemoveAt(removeIndex);
                            Global.changeKnsParaList.Add(revision);
                        }
                        
                    }
                    dblIndx = null;
                }
                revision = null;
                //-- Revision flag
                if (Global.changeKnsParaList.Count != 0 || Global.changeKnsWireList.Count != 0)
                    lblStatus.Text = "Revision";
            }
            else if (Global.FilePath.Contains("SKW"))
            {

            }
        }

        private void dataGridViewWM_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //-- Revision row Data
            if (Global.FilePath.Contains("KNS"))
            {
                RecipeConfigKnsWm revision = Global.KnsWireList[e.RowIndex];
                KNS kns = new KNS();
                //-- 범위검사

                if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    revision.RESULT_X_VALUE = kns.calculationResult(revision.MASTER_X_VALUE, revision.WIRE_X_VALUE);
                    revision.RESULT_Y_VALUE = kns.calculationResult(revision.MASTER_Y_VALUE, revision.WIRE_Y_VALUE);

                    revision.X_LSL = kns.calculationLsl(revision.RESULT_X_VALUE, revision.WB_VALUE);
                    revision.X_USL = kns.calculationUsl(revision.RESULT_X_VALUE, revision.WB_VALUE);
                    revision.Y_LSL = kns.calculationLsl(revision.RESULT_Y_VALUE, revision.WB_VALUE);
                    revision.Y_USL = kns.calculationUsl(revision.RESULT_Y_VALUE, revision.WB_VALUE);

                    if(kns.valid(revision.RESULT_X_VALUE, revision.X_LSL, revision.X_USL, revision.RESULT_Y_VALUE, revision.Y_LSL, revision.Y_USL))
                    {
                        MessageBox.Show("out of range.");
                        return;
                    }
                    //if (Convert.ToInt32(revision.PARA_VALUE) > Convert.ToInt32(revision.PARA_MAX) || Convert.ToInt32(revision.PARA_VALUE) < Convert.ToInt32(revision.PARA_MIN))
                    //{
                    //    MessageBox.Show("out of range.");
                    //    return;
                    //}
                }
                else
                {
                    MessageBox.Show("바꿀수 없는 항목입니다.");
                    //-- roll back
                    return;
                }

                //-- changeKnsParaList 의 중복 검사

                if (Global.changeKnsWireList.Count == 0)
                    Global.changeKnsWireList.Add(revision);
                else
                {
                    List<int> dblIndx = new List<int>();

                    //-- set check list
                    for (int i = 0 ; i < Global.changeKnsWireList.Count ; i++)
                    {
                        string checkGroupItem = Global.changeKnsParaList[i].GROUP_ID + "/" + Global.changeKnsWireList[i].ITEM_ID;
                        string revisionGroupItem = revision.GROUP_ID + "/" + revision.ITEM_ID;
                        if (checkGroupItem == revisionGroupItem)
                        {
                            dblIndx.Add(i);
                        }
                    }

                    if (dblIndx.Count == 0)
                    {
                        Global.changeKnsWireList.Add(revision);
                    }
                    else
                    {
                        foreach (int removeIndex in dblIndx)
                        {
                            Global.changeKnsWireList.RemoveAt(removeIndex);
                            Global.changeKnsWireList.Add(revision);
                        }

                    }
                    dblIndx = null;
                    //-- Revision flag
                    if (Global.changeKnsParaList.Count != 0 || Global.changeKnsWireList.Count != 0)
                        lblStatus.Text = "Revision";
                }

                Global.changeKnsWireList.Add(revision);
                revision = null;
            }
            else if (Global.FilePath.Contains("SKW"))
            {

            }


        }
    }
}


