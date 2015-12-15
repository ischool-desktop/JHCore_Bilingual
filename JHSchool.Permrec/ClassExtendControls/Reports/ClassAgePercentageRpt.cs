using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    public partial class ClassAgePercentageRpt : BaseForm
    {
        private List<DAL.StudentEntity> _SelectStudents;
        private bool _lock1 = false;

        public ClassAgePercentageRpt()
        {
            InitializeComponent();

            foreach (string str in DefaultItems())
            lstVwAgeItems.Items.Add(str);
            _SelectStudents = new List<JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentEntity>();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 都沒選不處理
            if (lstVwAgeItems.CheckedItems.Count < 1)
                return;

            // 取得所選班級學生資料
            _SelectStudents = DAL.Transfer.GetSelectStudentEntitys(Class.Instance.SelectedList);            

            // 檢查所選擇項目
            Dictionary<int,string> intAgeSelectItems = new Dictionary<int,string> ();
            List<string> SelectItems = new List<string> ();
            int intAge=9;
            foreach (ListViewItem lvi in lstVwAgeItems.Items  )
            {
                if(lvi.Checked)
                {
                    intAgeSelectItems.Add(intAge,lvi.Text );
                    SelectItems.Add(lvi.Text );
                }
                intAge++;
            }
                
            DAL.StudentAgeCounter sac = new JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentAgeCounter();

            sac.wsItems = DefaultItems();
            sac.AddAgeCounterItems(SelectItems);

            int age=0;

            List<DAL.StudentEntity> ErrorData = new List<JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentEntity>();

            foreach (DAL.StudentEntity se in _SelectStudents)
            {
                age=se.GetAge();

                if(age <10)
                    age=9;
                
                if(age>=18)
                    age=18;

                // 符合年齡加入統計
                if(intAgeSelectItems.ContainsKey(age))
                    se.Memo =sac.AddAgeCounterItemValue(intAgeSelectItems[age],se);

                if (!string.IsNullOrEmpty (se.Memo))
                    ErrorData.Add(se);

            }

            // 訊息處理            
            
            // 呼叫報表處理
            ClassCellReportManger ccrm = new ClassCellReportManger();
            ccrm.ProcessClassStudentAgeCount("年齡統計", "年齡統計", sac,ErrorData);

        }


        // 畫面預設內容
        private List<string> DefaultItems()
        {
            List<string> items = new List<string>();
            int Year = DateTime.Now.Year-1911;
            string item0 = "未滿10歲," + (Year - 10) + "年9月2日以後出生";
            string item1 = "10至未滿11歲,"+ (Year - 11)+ "年9月2日至"+ (Year - 10) + "年9月1日";
            string item2 = "11至未滿12歲,"+ (Year - 12)+ "年9月2日至"+ (Year - 11) + "年9月1日";
            string item3 = "12至未滿13歲,"+ (Year - 13)+ "年9月2日至"+ (Year - 12) + "年9月1日";
            string item4 = "13至未滿14歲,"+ (Year - 14)+ "年9月2日至"+ (Year - 13) + "年9月1日";
            string item5 = "14至未滿15歲,"+ (Year - 15)+ "年9月2日至"+ (Year - 14) + "年9月1日";
            string item6 = "15至未滿16歲,"+ (Year - 16)+ "年9月2日至"+ (Year - 15) + "年9月1日";
            string item7 = "16至未滿17歲,"+ (Year - 17)+ "年9月2日至"+ (Year - 16) + "年9月1日";
            string item8 = "17至未滿18歲,"+ (Year - 18)+ "年9月2日至"+ (Year - 17) + "年9月1日";
            string item9 = "18歲以上," + (Year - 18) + "年9月1日以前出生";

            items.Add(item0);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            items.Add(item8);
            items.Add(item9);
            //items.Add("未滿10歲");
            //items.Add("10至未滿11歲");
            //items.Add("11至未滿12歲");
            //items.Add("12至未滿13歲");
            //items.Add("13至未滿14歲");
            //items.Add("14至未滿15歲");
            //items.Add("15至未滿16歲");
            //items.Add("16至未滿17歲");
            //items.Add("17至未滿18歲");
            //items.Add("18歲以上");
            return items;
        }

        private void lstVwAgeItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_lock1 == false)
            {
                if (lstVwAgeItems.Items.Count == lstVwAgeItems.CheckedItems.Count)
                    cbxSelectAll.Checked = true;
                else
                    cbxSelectAll.Checked = false;
            }
        }

        private void cbxSelectAll_CheckValueChanged(object sender, EventArgs e)
        {

            if (_lock1)
            {
                if (cbxSelectAll.Checked)
                    foreach (ListViewItem lvi in lstVwAgeItems.Items)
                        lvi.Checked = true;

                else
                    foreach (ListViewItem lvi in lstVwAgeItems.Items)
                        lvi.Checked = false;
            }
            _lock1 = false;
        }

        private void cbxSelectAll_Click(object sender, EventArgs e)
        {
            _lock1 = true;
        }        

    }
}
