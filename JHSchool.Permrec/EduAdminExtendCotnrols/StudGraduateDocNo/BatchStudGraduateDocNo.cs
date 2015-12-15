using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using FISCA.DSAUtil;
using System.Xml;
using Framework;
using JHSchool.Feature.Legacy;
using Aspose.Cells;
using JHSchool;
using FISCA.Presentation;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.StudGraduateDocNo
{
    // 主要在批次產生畢修業字號
    public partial class BatchStudGraduateDocNo : FISCA.Presentation.Controls.BaseForm 
    {        
        public BatchStudGraduateDocNo()
        {
            InitializeComponent();
        }

        Dictionary<string,List<JHSchool.Data.JHStudentRecord >>tmpStudRecs;
        List<StudDiplomaInfoJuniorDiplomaNumber > studRecSDIs;
        List<StudDiplomaInfoJuniorDiplomaNumber> GraduateStudRecs;
        List<StudDiplomaInfoJuniorDiplomaNumber> NGraduateStudRecs;
        List<StudDiplomaInfoJuniorDiplomaNumber> StudDiplomaInfoJuniorDiplomaNumberList;

        // 畢業排序用
        Dictionary<string, string> sortGraduateDocNoDatas = new Dictionary<string, string>();
        // 修業排序用
        Dictionary<string, string> sortNGraduateDocNoDatas = new Dictionary<string, string>();

        Dictionary<string, string> tmpGraduateStatus = new Dictionary<string, string>();

        private SmartSchool.Common.EnhancedErrorProvider Errors { get; set; }
        bool chkInputData = false;
        const string  optName1 = "依學號遞增";


        // datagridView 因為需求改變，所以在顯示設成隱藏，但實作上還是保留。
        private void BatchStudGraduateDocNo_Load(object sender, EventArgs e)
        {
            // 加入年級, 先濾一般生
            tmpStudRecs = new Dictionary<string, List<JHSchool.Data.JHStudentRecord>>();

            List<JHSchool.Data.JHStudentRecord> AllStudRecList= JHSchool.Data.JHStudent.SelectAll();
            
            foreach (JHSchool.Data.JHStudentRecord studRec in AllStudRecList)
            { 
                if(studRec.Class !=null )
                    if (studRec.Class.GradeYear.HasValue && studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 )
                    { 
                        string grYear=studRec.Class.GradeYear.Value+"";
                        if (tmpStudRecs.ContainsKey(grYear))
                            tmpStudRecs[grYear].Add(studRec);                        
                        else
                        {
                            List<JHSchool.Data.JHStudentRecord > studRecList= new List<JHSchool.Data.JHStudentRecord>();
                            studRecList.Add(studRec );
                            tmpStudRecs.Add(grYear, studRecList);
                        }                    
                    }            
            }
            List<string> tmpGrYear = new List<string>();
            foreach (KeyValuePair<string, List<JHSchool.Data.JHStudentRecord>> Grade in tmpStudRecs)
                tmpGrYear.Add(Grade.Key);
            tmpGrYear.Sort();


            cboGradeYear.Items.AddRange(tmpGrYear.ToArray());

            lblHasGraduateCount.Text = "";
            lblTotalCount.Text = "";
            GraduateStudRecs = new List<StudDiplomaInfoJuniorDiplomaNumber>();
            NGraduateStudRecs = new List<StudDiplomaInfoJuniorDiplomaNumber>();

            // 加入標頭
            dgGraduateDocNoData.Columns.Add("StudentID", "StudentID");
            dgGraduateDocNoData.Columns.Add("ClassName", "班級");
            dgGraduateDocNoData.Columns.Add("SeatNo", "座號");
            dgGraduateDocNoData.Columns.Add("StudentNum", "學號");            
            dgGraduateDocNoData.Columns.Add("Name", "姓名");
            dgGraduateDocNoData.Columns.Add("Status", "離校類別");
            dgGraduateDocNoData.Columns.Add("GRDocNo", "畢業證書字號");
            dgGraduateDocNoData.Columns.Add("Sort1", "Sort1");
            cboSortBySnum.Checked = true;

            if(Errors ==null )
                Errors = new SmartSchool.Common.EnhancedErrorProvider();

            studRecSDIs = new List<StudDiplomaInfoJuniorDiplomaNumber>();
        }


        // 學號排序使用
        private int StudentNumberCompare(StudDiplomaInfoJuniorDiplomaNumber x, StudDiplomaInfoJuniorDiplomaNumber y)
        {
            return x.StudentNumber.CompareTo(y.StudentNumber);
        }

        // 座號排序使用
        private int SeatNoCompare(StudDiplomaInfoJuniorDiplomaNumber x, StudDiplomaInfoJuniorDiplomaNumber  y)
        {
            int SeatNoX=0,SeatNoY=0;

            if(x.SeatNo>0)
                SeatNoX = x.SeatNo;
            else
                SeatNoX = 100;

            if (y.SeatNo>0)
                SeatNoY = y.SeatNo;
            else
                SeatNoY = 100;

            return SeatNoX.CompareTo(SeatNoY);

        }

        // 字串轉數字排序用
        private int stringToIntCompare(string x, string y)
        {
            int VarX = 0, VarY = 0;
            int.TryParse(x, out VarX);
            int.TryParse(y, out VarY);
            return VarX.CompareTo(VarY);
        }

        // 班級座號排序
        private List<StudDiplomaInfoJuniorDiplomaNumber> SortClassSeatNo(List<StudDiplomaInfoJuniorDiplomaNumber> SDIRecList)
        {
            Dictionary<int, List<string>> tmpClassSort = new Dictionary<int, List<string>>();
            Dictionary<string, List<StudDiplomaInfoJuniorDiplomaNumber>> tmpSeatNoSort = new Dictionary<string, List<StudDiplomaInfoJuniorDiplomaNumber>>();

            List<int> tmpNoSort = new List<int>();
            List<StudDiplomaInfoJuniorDiplomaNumber> tmpStudRecSDIs = new List<StudDiplomaInfoJuniorDiplomaNumber>();


            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in SDIRecList)
            {
                int x = 0;
                int.TryParse(sdi.ClassDisplayOrder, out x);

                    if (tmpClassSort.ContainsKey(x))
                    {
                        // 當 ID 沒重覆加入
                        if (!tmpClassSort[x].Contains(sdi.ClassID ))                            
                            tmpClassSort[x].Add(sdi.ClassID );
                    }
                    else
                    {
                        List<string> tmp = new List<string>();
                        tmp.Add(sdi.ClassID);
                        tmpClassSort.Add(x, tmp);
                        tmp = null;
                    }


                    if (tmpSeatNoSort.ContainsKey(sdi.ClassID))
                        tmpSeatNoSort[sdi.ClassID ].Add(sdi);
                    else
                    {
                        List<StudDiplomaInfoJuniorDiplomaNumber> tmp = new List<StudDiplomaInfoJuniorDiplomaNumber> ();
                        tmp.Add(sdi);
                        tmpSeatNoSort.Add(sdi.ClassID, tmp);
                        tmp = null;
                    }
              }  

            tmpNoSort.Clear();
            foreach (KeyValuePair<int,List<string >> key in tmpClassSort)
                tmpNoSort.Add(key.Key);
            tmpNoSort.Sort();

            // 依班級 ID 排序
            foreach (KeyValuePair <int,List<string >> classR in tmpClassSort)
                classR.Value.Sort(new Comparison<string>(stringToIntCompare));

            // 座號排序
            foreach (KeyValuePair<string, List<StudDiplomaInfoJuniorDiplomaNumber>> studRec in tmpSeatNoSort)
                studRec.Value.Sort(new Comparison<StudDiplomaInfoJuniorDiplomaNumber>(SeatNoCompare));

            tmpStudRecs.Clear();
            // 重組 StudentRec 順序 沒有序號排最後
            foreach (int i in tmpNoSort)            
            {
                if (i > 0)
                    if (tmpClassSort.ContainsKey(i))
                        foreach (string classid in tmpClassSort[i])
                        {
                            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in tmpSeatNoSort[classid])
                                tmpStudRecSDIs.Add(sdi);
                        }
            
            }

            // 這堆是沒序號
            if (tmpClassSort.ContainsKey(0))
                foreach (string classid in tmpClassSort[0])
                {
                    foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in tmpSeatNoSort[classid])
                        tmpStudRecSDIs.Add(sdi);
                }

            return tmpStudRecSDIs;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;


            Preview();

            if (chkInputData == true)
            {
                string tmpStr = "";
                // getDataGradViewData
                Dictionary<string, string> tmpDocNo = new Dictionary<string, string>();
                foreach (DataGridViewRow dgv in dgGraduateDocNoData.Rows)
                {
                    if (dgv.IsNewRow)
                        continue;

                    if (dgv.Cells[6].Value  == null)
                        tmpStr = "";
                    else
                        tmpStr =dgv.Cells[6].Value.ToString();

                    tmpDocNo.Add(dgv.Cells[0].Value.ToString(),tmpStr);
                }

                // 批示填入證書字號
                foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in StudDiplomaInfoJuniorDiplomaNumberList)
                    if (tmpDocNo.ContainsKey(sdi.StudentID))
                        sdi.GRDocNo  = tmpDocNo[sdi.StudentID];
                
                //寫入資料庫
                btnSave.Enabled = false;
                setStudDiplomaInfoData();
                PermRecLogProcess prlp = new PermRecLogProcess();
                prlp.SaveLog("教務.產生畢修業證書字號", "新增", "批次產生畢修業證書字號.");
            }
            else
            {
                MessageBox.Show("無法產生資料!");
                btnSave.Enabled = true;
            }
           
        }

        // 依照學號排序
        private void sortByStudNumber()
        {
            // GrNoLength 畢業 ,Gr1NoLength 修業
            int GrNoLength = 0, Gr1NoLength = 0;
            // 取得人數長度
            int x = StudDiplomaInfoJuniorDiplomaNumberList.Count.ToString().Length;

            if (x > txtGRNo.Text.Length )
                GrNoLength = x;
            else
                GrNoLength = txtGRNo.Text.Length;

            if (x > txtGDNo1.Text.Length)
                Gr1NoLength = x;
            else
                Gr1NoLength = txtGDNo1.Text.Length;


            sortGraduateDocNoDatas.Clear();
            sortNGraduateDocNoDatas.Clear();

            GraduateStudRecs.Clear ();
            NGraduateStudRecs.Clear ();
            tmpGraduateStatus.Clear();

            // 取得畢修業狀態
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in StudDiplomaInfoJuniorDiplomaNumberList)
            {
                if (sdi.GRStatus == "畢業")
                    GraduateStudRecs.Add(sdi);

                if (sdi.GRStatus == "修業")
                    NGraduateStudRecs.Add(sdi);                
            }


            GraduateStudRecs.Sort(new Comparison<StudDiplomaInfoJuniorDiplomaNumber>(StudentNumberCompare));
            NGraduateStudRecs.Sort(new Comparison<StudDiplomaInfoJuniorDiplomaNumber>(StudentNumberCompare));
            string zeroStr = "";
            int noStart,noStart1;
            int.TryParse(txtGRNo.Text, out noStart);
            for (int i = 0; i < GrNoLength; i++)
                zeroStr += 0;
            // 畢業
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in GraduateStudRecs)
            {
                string tmpStr = zeroStr + noStart;
                string tmpStrDocNo = txtGRDoc.Text + "字第" + tmpStr.Substring((tmpStr.Length - GrNoLength), GrNoLength) + "號";
                sortGraduateDocNoDatas.Add(sdi.StudentID, tmpStrDocNo);                
                noStart++;
            }

            zeroStr = "";
            int.TryParse(txtGDNo1.Text, out noStart1);
            for (int i = 0; i < Gr1NoLength; i++)
                zeroStr += 0;
            // 修業
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in NGraduateStudRecs)
            {
                string tmpStr = zeroStr + noStart1;
                string tmpStrDocNo = txtGRDoc1.Text + "字第" + tmpStr.Substring((tmpStr.Length - Gr1NoLength), Gr1NoLength) + "號";
                sortGraduateDocNoDatas.Add(sdi.StudentID, tmpStrDocNo);
                noStart1++;
            }

        }

        // 依照班級座號排序
        private void sortByClassSeatNo()
        {
            // GrNoLength 畢業 ,Gr1NoLength 修業
            int GrNoLength = 0, Gr1NoLength = 0;
            // 取得人數長度
            int x = StudDiplomaInfoJuniorDiplomaNumberList.Count.ToString().Length;

            if (x > txtGRNo.Text.Length)
                GrNoLength = x;
            else
                GrNoLength = txtGRNo.Text.Length;

            if (x > txtGDNo1.Text.Length)
                Gr1NoLength = x;
            else
                Gr1NoLength = txtGDNo1.Text.Length;


            sortGraduateDocNoDatas.Clear();
            sortNGraduateDocNoDatas.Clear();

            GraduateStudRecs.Clear();
            NGraduateStudRecs.Clear();
            tmpGraduateStatus.Clear();

            // 取得畢修業狀態
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in StudDiplomaInfoJuniorDiplomaNumberList)
            {
                if (sdi.GRStatus == "畢業")
                    GraduateStudRecs.Add(sdi);

                if (sdi.GRStatus == "修業")
                    NGraduateStudRecs.Add(sdi);
            }



            sortGraduateDocNoDatas.Clear();
            sortNGraduateDocNoDatas.Clear();

            GraduateStudRecs.Clear();
            NGraduateStudRecs.Clear();
            tmpGraduateStatus.Clear();

            // 取得畢修業狀態
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in StudDiplomaInfoJuniorDiplomaNumberList)
            {
                if (sdi.GRStatus  == "畢業")
                    GraduateStudRecs.Add(sdi);

                if (sdi.GRStatus  == "修業")
                    NGraduateStudRecs.Add(sdi);
            }
            // 傳入班座排序功能後傳回
            GraduateStudRecs = SortClassSeatNo(GraduateStudRecs);
            NGraduateStudRecs = SortClassSeatNo(NGraduateStudRecs);

            string zeroStr = "";
            int noStart, noStart1;
            int.TryParse(txtGRNo.Text, out noStart);
            for (int i = 0; i < GrNoLength ; i++)
                zeroStr += 0;
            // 畢業
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in GraduateStudRecs)
            {
                if (cboSortByClassSeatNo.Checked == true)
                {
                        string tmpStr = zeroStr + noStart;
                        string tmpStrDocNo = txtGRDoc.Text + "字第" + tmpStr.Substring((tmpStr.Length - GrNoLength), GrNoLength ) + "號";
                        sortGraduateDocNoDatas.Add(sdi.StudentID, tmpStrDocNo);
                        noStart++;
                }
            }

            zeroStr = "";
            int.TryParse(txtGDNo1.Text, out noStart1);
            for (int i = 0; i < Gr1NoLength; i++)
                zeroStr += 0;
            // 修業
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdi in NGraduateStudRecs)
            {
                if (cboSortByClassSeatNo.Checked == true)
                {
                    if(sdi.SeatNo>0 )
                    {
                        string tmpStr = zeroStr + noStart1;
                        string tmpStrDocNo = txtGRDoc1.Text + "字第" + tmpStr.Substring((tmpStr.Length - Gr1NoLength), Gr1NoLength ) + "號";
                        sortGraduateDocNoDatas.Add(sdi.StudentID, tmpStrDocNo);
                        noStart1++;
                    }
                
                }
            }

        }


        // 取得畢修業狀態
        private void getGRDocNoToDG()
        {
            dgGraduateDocNoData.Rows.Clear();

            StudDiplomaInfoJuniorDiplomaNumberList = DALTransfer.GetStudentDiplomaInfoJuniorDiplomaNumberListByGradeYear(cboGradeYear.Text);
            StudDiplomaInfoJuniorDiplomaNumberList.Sort(new Comparison<StudDiplomaInfoJuniorDiplomaNumber>(StudentNumberCompare));

            // 填入 DataGridView
            int dgRowIdx = 0, hasGDDocNoCount = 0, GraduateCount = 0, NGraduateCount = 0;
            foreach (StudDiplomaInfoJuniorDiplomaNumber  sdij in StudDiplomaInfoJuniorDiplomaNumberList)
            {
                dgGraduateDocNoData.Rows.Add();
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[0].Value = sdij.StudentID;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[1].Value = sdij.ClassName;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[2].Value = sdij.SeatNo;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[3].Value = sdij.StudentNumber ;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[4].Value = sdij.Name;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[5].Value = sdij.GRStatus;
                if (!string.IsNullOrEmpty(sdij.GRDocNo ) && sdij.GRStatus  == "畢業")
                    GraduateCount++;
                if (!string.IsNullOrEmpty(sdij.GRDocNo) && sdij.GRStatus == "修業")
                    NGraduateCount++;

                if (!string.IsNullOrEmpty(sdij.GRDocNo))
                    hasGDDocNoCount++;
                dgGraduateDocNoData.Rows[dgRowIdx].Cells[6].Value = sdij.GRDocNo;
                dgRowIdx++;
            }
            lblTotalCount.Text = "總共" + dgRowIdx + "人, 畢業: " + GraduateCount + "人,修業: "+NGraduateCount +"人";            
            lblHasGraduateCount.Text="有畢修業證書字號有 "+ hasGDDocNoCount +"人";

            // set column readnoly and width
            dgGraduateDocNoData.Columns[0].Visible = false;
            dgGraduateDocNoData.Columns[1].Width = 80;
            dgGraduateDocNoData.Columns[2].Width = 40;
            dgGraduateDocNoData.Columns[3].Width = 80;
            dgGraduateDocNoData.Columns[4].Width = 80;
            dgGraduateDocNoData.Columns[5].Width = 40;
            dgGraduateDocNoData.Columns[6].Width = 160;
            
        }


        // 因架構改變目前捨棄，改呼叫 btnPreview()
        private void btnPreview_Click(object sender, EventArgs e)
        {
            chkInputData = true;
            // 檢查是否輸入
            ChkInputDocNo();

            if (chkInputData == true)
            {
                if (cboSortBySnum.Checked== true )
                    sortByStudNumber();
                foreach (DataGridViewRow row in dgGraduateDocNoData.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (sortGraduateDocNoDatas.ContainsKey(row.Cells[0].Value.ToString()))
                        row.Cells[6].Value = sortGraduateDocNoDatas[row.Cells[0].Value.ToString()];

                    if (sortNGraduateDocNoDatas.ContainsKey(row.Cells[0].Value.ToString()))
                        row.Cells[6].Value = sortNGraduateDocNoDatas[row.Cells[0].Value.ToString()];
                }
            }
        }

        public void Preview()
        {
            chkInputData = true;
            // 檢查是否輸入
            ChkInputDocNo();

            if (chkInputData == true)
            {
                if (cboSortBySnum.Checked == true)
                    sortByStudNumber();

                if (cboSortByClassSeatNo.Checked == true)
                    sortByClassSeatNo();

                foreach (DataGridViewRow row in dgGraduateDocNoData.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (sortGraduateDocNoDatas.ContainsKey(row.Cells[0].Value.ToString()))
                        row.Cells[6].Value = sortGraduateDocNoDatas[row.Cells[0].Value.ToString()];

                    if (sortNGraduateDocNoDatas.ContainsKey(row.Cells[0].Value.ToString()))
                        row.Cells[6].Value = sortNGraduateDocNoDatas[row.Cells[0].Value.ToString()];

                }                

            }
        }

        public void ChkInputDocNo()
        { 
            if (string.IsNullOrEmpty(txtGRDoc.Text)  )
            {
                Errors.SetError(txtGRDoc, "請輸入畢業字");
                chkInputData = false;
                return;
            }
            if(string.IsNullOrEmpty(txtGRNo.Text))
            {
                Errors.SetError(txtGRNo,"請輸入畢業號");
                chkInputData = false;
                return;
            }
            if (string.IsNullOrEmpty(txtGRDoc1.Text))
            {
                Errors.SetError(txtGRDoc1, "請輸入修業字");
                chkInputData = false;
                return;
            }

            if (string.IsNullOrEmpty(txtGDNo1.Text))
            {
                Errors.SetError(txtGDNo1, "請輸入修業號");
                chkInputData = false;
                return;
            }

        }

        public void setStudDiplomaInfoData()
        {
            MotherForm.SetStatusBarMessage("畢修業證書字號產生中..");
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);         
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
 
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MotherForm.SetStatusBarMessage("");
            btnExport.Enabled = true;
            btnSave.Enabled = true;
            MessageBox.Show("儲存完成");
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DALTransfer.SetStudentDiplomaInfoJuniorDiplomaNumberList(StudDiplomaInfoJuniorDiplomaNumberList);            
        }

        private void txtGRDoc_TextChanged(object sender, EventArgs e)
        {
            if (Errors == null)
                Errors = new SmartSchool.Common.EnhancedErrorProvider();
            Errors.Clear();

        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgGraduateDocNoData.Rows.Count < 1)
            {
                MessageBox.Show("沒有資料");
                return;
            }
            Workbook wb = new Workbook();
            int row = 1,co=0;

            foreach (DataGridViewColumn dgvc in dgGraduateDocNoData.Columns)
            {
                if (dgvc.Index != 0 && dgvc.Index != 7)
                {
                    wb.Worksheets[0].Cells[0, co++].PutValue(dgvc.HeaderText);
                }
            }

            // 產生前先排序
            if (cboSortBySnum.Checked == true)
            {
                // 依學號排序
                dgGraduateDocNoData.Sort(dgGraduateDocNoData.Columns[3], System.ComponentModel.ListSortDirection.Ascending);
            }

            if (cboSortByClassSeatNo.Checked == true)
            {
                int no;
                string tmp="";
                // 依班級座號排序
                foreach (DataGridViewRow drv in dgGraduateDocNoData.Rows)
                {
                    if (drv.IsNewRow)
                        continue;
                    int.TryParse(drv.Cells[2].Value.ToString (),out no);
                    if(no<10)
                        tmp="0"+drv.Cells[2].Value.ToString ();
                    else
                        tmp=drv.Cells[2].Value.ToString ();
                    drv.Cells[7].Value = drv.Cells[1].Value.ToString ()+tmp;                    
                }
                dgGraduateDocNoData.Sort(dgGraduateDocNoData.Columns[7], System.ComponentModel.ListSortDirection.Ascending);
            }

            

            // 產生到 Worksheet
            foreach (DataGridViewRow drv in dgGraduateDocNoData.Rows)
            {
                if (drv.IsNewRow)
                    continue;
                for (int col = 1; col < drv.Cells.Count; col++)
                    if(drv.Cells[col].Value != null && col !=7)
                        wb.Worksheets[0].Cells[row, col - 1].PutValue(drv.Cells[col].Value.ToString());
                row++;
            }

 

            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\畢修業證書字號.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\畢修業證書字號.xls");
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "畢修業證書字號.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd1.FileName);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }



        }

        private void cboGradeYear_TextChanged(object sender, EventArgs e)
        {
            getGRDocNoToDG();
        }
    }

}
