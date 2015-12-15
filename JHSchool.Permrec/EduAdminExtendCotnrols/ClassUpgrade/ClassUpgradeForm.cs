using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using System.Drawing.Drawing2D;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.ClassUpgrade
{
    public partial class ClassUpgradeForm : BaseForm
    {
        Dictionary<string, int> selCot = new Dictionary<string, int>();
        Dictionary <string ,int> setUpgradeCot = new Dictionary<string,int> ();
        Dictionary<string, int> setGraduateCot = new Dictionary<string, int>();
        List<ClassItem> ClassItems;
        BackgroundWorker bkWork;
        BackgroundWorker bkWorkUpgrd;

        public ClassUpgradeForm()
        {
            InitializeComponent();
        }
        private enum UpgradeType { 升級, 畢業,恢復};
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 載入班級項目
        private void LoadClassItems()
        {
            bkWork = new BackgroundWorker();
            bkWork.DoWork += new DoWorkEventHandler(bkWork_DoWork);
            bkWork.RunWorkerAsync();
            bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWork_RunWorkerCompleted);
           
        }

        void bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgClassUpgrade.Rows.Clear();

            int row = 0;
            foreach (ClassItem ci in ClassItems)
            {
                dgClassUpgrade.Rows.Add();
                dgClassUpgrade.Rows[row].Cells[0].Value = ci.ClassID;
                dgClassUpgrade.Rows[row].Cells[1].Value = ci.GradeYear;
                dgClassUpgrade.Rows[row].Cells[2].Value = ci.ClassName;
                dgClassUpgrade.Rows[row].Cells[3].Value = ci.newGradeYear;
                dgClassUpgrade.Rows[row].Cells[4].Value = ci.newClassName;
                dgClassUpgrade.Rows[row].Cells[6].Value = ci.NamingRule;
                dgClassUpgrade.Rows[row].Cells[7].Value = ci;
                row++;
            }
        }

        void bkWork_DoWork(object sender, DoWorkEventArgs e)
        {
            ClassItems = DAL.UpgradeClassDAL.getClassItems();
        }

        // 設定班級項目
        private void SetClassItems()
        { 
        
        }

        private void LoadCots()
        {
            // 放入統計目前選取與設定初值
            setGraduateCot.Clear();
            setUpgradeCot.Clear();
            selCot.Clear();
            foreach (ClassItem ci in ClassItems)
            {
                if (!selCot.ContainsKey(ci.GradeYear))
                {
                    selCot.Add(ci.GradeYear, 0);
                    setGraduateCot.Add(ci.GradeYear, 0);
                    setUpgradeCot.Add(ci.GradeYear, 0);
                }
            }
        
        }

        private void ClassUpgradeForm_Load(object sender, EventArgs e)
        {
            //載入預設學年度
            txtSchoolYear.Text = School.DefaultSchoolYear.ToString ();
            
            
            // 載入班級項目
            LoadClassItems();
        }        

        private void dgClassUpgrade_SelectionChanged(object sender, EventArgs e)
        {
            LoadCots();
            labelX1.Text = "";

            foreach (DataGridViewRow dgv in dgClassUpgrade.SelectedRows )
            {
                string str = "" + dgv.Cells[1].Value;
                if(selCot.ContainsKey(str))
                    selCot[str]++;
            }   

            foreach (KeyValuePair<string,int> var in selCot)
                labelX1.Text += "原"+ var.Key+ "年級選取" + selCot[var.Key] + "班\n";
            

            getUpgardeGraguateCot();
        }

        private void getUserSelectCot()
        {
            foreach (KeyValuePair<string, int> var in selCot)
                selCot[var.Key] = 0;
            
        }

        private void getUpgardeGraguateCot()
        {
            List<string> items = new List<string>();

            // 清空累計
            foreach (string str in setGraduateCot.Keys)
                items.Add(str);

            foreach (string str in items)
                setGraduateCot[str] = 0;

            items.Clear();

            foreach (string str in setUpgradeCot.Keys)
                items.Add(str);

            foreach (string str in items)
                setUpgradeCot[str] = 0;
                

            foreach (DataGridViewRow dgv in dgClassUpgrade.Rows)
            {
                if (dgv.Cells[5].Value != null)
                {
                    if (dgv.Cells[5].Value.ToString() == "升級")
                        setUpgradeCot[dgv.Cells[1].Value.ToString()]++;
                    if (dgv.Cells[5].Value.ToString() == "畢業")
                        setGraduateCot[dgv.Cells[1].Value.ToString()]++;
                        
                }
            }

            lblMsg.Text = "";
            foreach (KeyValuePair<string, int> var in setGraduateCot)
                lblMsg.Text += "原"+var.Key +"年級升級班級數:" + setUpgradeCot[var.Key] + " ,原"+var.Key +"年級畢業班級數:" + setGraduateCot[var.Key]+"\n";
        }
        
        // 班級升級
        private void UpgradeDGClassItem(UpgradeType ut)
        {
            // 對所選擇的 Row 判斷
            foreach (DataGridViewRow dgv in dgClassUpgrade.SelectedRows)
            {
                int defGrYear,upgradeGrYear,defClassYear;

                string ClassName="",newClassName="",ClassNamingRule="";
                ClassName = dgv.Cells[2].Value.ToString();
                ClassNamingRule = "" + dgv.Cells[6].Value;

                if (ut==UpgradeType.升級 )
                {
                    bool checkNew = true;

                    if (dgv.Cells[5].Value != null)
                    {
                        if (dgv.Cells[5].Value.ToString() == "升級")
                        {
                            dgv.Cells[3].Value = "";
                            dgv.Cells[4].Value = "";
                            dgv.Cells[5].Value = "";
                            foreach (DataGridViewCell dd in dgv.Cells)
                                dd.Style.BackColor = Color.White;
                            checkNew = false;
                        }                        
                    }

                    if (checkNew == true)
                    {
                        int.TryParse(dgv.Cells[1].Value.ToString(), out defGrYear);
                        string strUpgradeGrYear;
                        upgradeGrYear = defGrYear + 1;
                        dgv.Cells[3].Value = strUpgradeGrYear=upgradeGrYear.ToString();
                        
                        // 檢查是否有命名規則
                        bool checkClassNamingRule = DAL.UpgradeClassDAL.ValidateNamingRule(ClassNamingRule);

                        if (checkClassNamingRule == true)
                        {
                            // 使用預設命名規則                        
                            newClassName = DAL.UpgradeClassDAL.ParseClassName(ClassNamingRule, upgradeGrYear);
                            dgv.Cells[4].Value = newClassName;

                        }
                        else
                        {
                            // 沒有命名規則
                            newClassName = ClassName;
                            if (upgradeGrYear > 10)
                            {
                                    newClassName = strUpgradeGrYear + ClassName.Substring(2, (ClassName.Length - 2));
                            }
                            else
                                newClassName = strUpgradeGrYear + ClassName.Substring(1, (ClassName.Length - 1));

                            dgv.Cells[4].Value = newClassName;

                        }
                        dgv.Cells[5].Value = "升級";
                        foreach (DataGridViewCell dd in dgv.Cells)
                            dd.Style.BackColor = Color.Yellow;
                    }                    
                }
                
                if(ut==UpgradeType.畢業 )
                {
                    bool checkNew = true;

                    if (dgv.Cells[5].Value != null)
                        if(dgv.Cells[5].Value.ToString ()=="畢業")
                        {
                            dgv.Cells[3].Value = "";
                            dgv.Cells[4].Value = "";
                            dgv.Cells[5].Value = "";
                            foreach (DataGridViewCell dd in dgv.Cells)
                                dd.Style.BackColor = Color.White;
                            checkNew = false;

                        }

                    if (checkNew == true)
                    {
                        dgv.Cells[3].Value = "";
                        dgv.Cells[4].Value = txtSchoolYear.Text + ClassName;
                        dgv.Cells[5].Value = "畢業";
                        foreach (DataGridViewCell dd in dgv.Cells)
                            dd.Style.BackColor = Color.LightCyan;

                    }

                }

                if (ut == UpgradeType.恢復)
                {
                    dgv.Cells[3].Value = "";
                    dgv.Cells[4].Value = "";
                    dgv.Cells[5].Value = "";
                    foreach (DataGridViewCell dd in dgv.Cells)
                        dd.Style.BackColor = Color.White;
                }
            }        
        }

        // 儲存升級班級項目
        private void SaveClassItems(List<ClassItem> ClassItems)
        { 
            DAL.UpgradeClassDAL.UpdateClassNameGradeYear(ClassItems);
        }

        // 更改學生狀態
        private void UpdateStudentStatus(List<ClassItem> ClassItems, string oldStatus,string newStatus)
        {
            Dictionary<string, List<StudentItem>> StudentItems = new Dictionary<string, List<StudentItem>>();
            List<StudentItem> GraduateStudentItems = new List<StudentItem>();
            
           
            StudentItems = DAL.UpgradeClassDAL.getStudentItems(ClassItems, oldStatus);
           foreach (KeyValuePair<string ,List<StudentItem >> si in StudentItems )
           {
            foreach (StudentItem sii in si.Value )
            {
                sii.Status = newStatus;
                GraduateStudentItems.Add(sii);
            }
           }
           DAL.UpgradeClassDAL.setStudentStatus(GraduateStudentItems);

        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            UpgradeDGClassItem(UpgradeType.升級);
            getUpgardeGraguateCot();
        }

        // 檢查畫面上升級後班級名稱是否重覆
        private bool checkNewClassName()
        {
            bool checksameClassName = false;
            string strName = "", classname="" ;
            foreach (DataGridViewRow drv in dgClassUpgrade.Rows)
            {
                if (drv.Cells[4].Value != null)
                {
                    classname =drv.Cells[4].Value.ToString();
                    if (!string.IsNullOrEmpty(classname))
                    {
                        if (strName == classname)
                        {
                            checksameClassName = true;
                            drv.Cells[4].ErrorText = "調整後班級名稱重複!";
                        }
                        strName = classname;
                    }                    
                }
            }
            return checksameClassName;
        }

        // 清空 Datagridviewrow cell error text
        private void ClearDGRowCellErrorText(DataGridViewRowCollection dgRows,int RowCellIdx)        
        {
            foreach (DataGridViewRow drv in dgRows)
                drv.Cells[RowCellIdx].ErrorText = "";        
        }


        List<ClassItem> GraduateClassItems;
        List<ClassItem> UpgradeClassItems;
        List<ClassItem> checkClassItems;

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            GraduateClassItems = new List<ClassItem>();
            UpgradeClassItems = new List<ClassItem>();
            checkClassItems = new List<ClassItem>();

            bool checkSaveGrauate = true;
            bool checkSaveUpgrade = true;
            // 調整後班級
            ClearDGRowCellErrorText(dgClassUpgrade.Rows ,4);

            if (checkNewClassName() == true)
                return;

            foreach (DataGridViewRow dgv in dgClassUpgrade.Rows)
            {
                if (dgv.Cells[4].Value != null)
                {
                    ClassItem ciClassItem = dgv.Cells[7].Value as ClassItem;
                    ciClassItem.newGradeYear = dgv.Cells[3].Value.ToString();
                    ciClassItem.newClassName = dgv.Cells[4].Value.ToString();

                    if (dgv.Cells[5].Value.ToString() == "畢業")
                        GraduateClassItems.Add(ciClassItem);

                    if (dgv.Cells[5].Value.ToString() == "升級")
                        UpgradeClassItems.Add(ciClassItem);
                }
            
            }


            
            
            checkClassItems = DAL.UpgradeClassDAL.checkUpdateClassName(GraduateClassItems);
            if (checkClassItems.Count > 0)
            {
                checkSaveGrauate = false;
                foreach (ClassItem ci in checkClassItems)
                    foreach (DataGridViewRow drv in dgClassUpgrade.Rows)
                        if (drv.Cells[0].Value.ToString() == ci.ClassID)
                        {
                            drv.Cells[4].ErrorText = "畢業班級名稱與系統內有重覆!";
                            break;
                        }
                
            }

            checkClassItems.Clear();            
            checkClassItems = DAL.UpgradeClassDAL.checkUpdateClassName(UpgradeClassItems);
            if (checkClassItems.Count > 0)
            {
                List<string> classname = new List<string>();
                foreach (ClassItem ci in GraduateClassItems)
                    classname.Add(ci.ClassName);
                foreach (ClassItem ci in UpgradeClassItems)
                    classname.Add(ci.ClassName);                    

                List<ClassItem> rmClassItems = new List<ClassItem>();
                
                    foreach (ClassItem cic in checkClassItems)
                        if(classname.Contains(cic.newClassName ))
                            rmClassItems.Add(cic);
                foreach (ClassItem ci in rmClassItems)
                    checkClassItems.Remove(ci);
            }                            

                if (checkClassItems.Count > 0)
                {
                    checkSaveUpgrade = false;
                    foreach (ClassItem ci in checkClassItems)
                        foreach (DataGridViewRow drv in dgClassUpgrade.Rows)
                            if (drv.Cells[0].Value.ToString() == ci.ClassID)
                            {
                                drv.Cells[4].ErrorText = "調整後班級名稱與系統內有重覆!";
                                break;
                            }
                }



                if (GraduateClassItems.Count == 0 && UpgradeClassItems.Count == 0)
                {
                    MessageBox.Show("請先設定升級或畢業");
                    return;
                
                }


                if (checkSaveGrauate == true && checkSaveUpgrade == true)
                {
                    // 使用 bk

                    bkWorkUpgrd = new BackgroundWorker();
                    bkWorkUpgrd.DoWork += new DoWorkEventHandler(bkWorkUpgrd_DoWork);
                    bkWorkUpgrd.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWorkUpgrd_RunWorkerCompleted);
                    bkWorkUpgrd.RunWorkerAsync();
                }
                else
                    btnSave.Enabled = true;
        }

        void bkWorkUpgrd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PermRecLogProcess prlp = new PermRecLogProcess();

            foreach (DataGridViewRow dgv in dgClassUpgrade.Rows)
            {
                if (dgv.Cells[5].Value != null)
                {
                    prlp.SetBeforeSaveText(dgv.Cells[2].Value.ToString() + "班級名稱", dgv.Cells[2].Value.ToString());
                    prlp.SetAfterSaveText(dgv.Cells[2].Value.ToString() + "班級名稱", dgv.Cells[4].Value.ToString());
                    prlp.SetBeforeSaveText(dgv.Cells[2].Value.ToString() + "班級年級", dgv.Cells[1].Value.ToString());
                    prlp.SetAfterSaveText(dgv.Cells[2].Value.ToString() + "班級年級", dgv.Cells[3].Value.ToString());

                }
            }
            prlp.SetAction("學籍.班級升級");
            prlp.SetActionBy("學籍系統", "班級升級或畢業");
            prlp.SetDescTitle("班級調整：");
            prlp.SaveLog("", "", "", "");

            MessageBox.Show("完成");
            this.Close();

        }

        void bkWorkUpgrd_DoWork(object sender, DoWorkEventArgs e)
        {
            // 畢業
            if (GraduateClassItems.Count > 0)
            {
                SaveClassItems(GraduateClassItems);
                UpdateStudentStatus(GraduateClassItems, "一般", "畢業或離校");
            }

            //// 升級
            if (UpgradeClassItems.Count > 0)
            {
                SaveClassItems(UpgradeClassItems);
            }
        }

        private void btnGraduate_Click(object sender, EventArgs e)
        {            
            UpgradeDGClassItem(UpgradeType.畢業);
            getUpgardeGraguateCot();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpgradeDGClassItem(UpgradeType.恢復);
            getUpgardeGraguateCot();
        }
    }
}
