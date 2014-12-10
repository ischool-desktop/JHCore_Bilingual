using FISCA.Presentation.Controls;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasicInformation.Student
{
    public partial class SchoolYearEditor : BaseForm
    {
        List<string> _ids;
        List<StudentRecord_Ext> _studExtList;
        Dictionary<string, StudentRecord_Ext> _studExtDic;
        Dictionary<string, StudentRecord> _studentRecords;
        Dictionary<string, LogObj> _logDic;

        public SchoolYearEditor()
        {
            InitializeComponent();

            _studExtDic = new Dictionary<string, StudentRecord_Ext>();
            _studentRecords = new Dictionary<string, StudentRecord>();
            _logDic = new Dictionary<string, LogObj>();

            _ids = K12.Presentation.NLDPanels.Student.SelectedSource;
            lblCount.Text = _ids.Count + "";

            dtEntrance.Value = DateTime.Today;
            dtLeaving.Value = DateTime.Today;

            DataInit();
        }

        private void DataInit()
        {
            _studentRecords.Clear();
            List<StudentRecord> students = K12.Data.Student.SelectByIDs(_ids);
            foreach (StudentRecord s in students)
            {
                _studentRecords.Add(s.ID, s);
            }

            string str_id = string.Join("','", _ids);
            str_id = "'" + str_id + "'";
            _studExtList = tool._A.Select<StudentRecord_Ext>("ref_student_id in (" + str_id + ")");

            _studExtDic.Clear();
            foreach (string id in _ids)
            {
                //先全部建立一個StudentRecord_Ext
                StudentRecord_Ext record = new StudentRecord_Ext();
                record.RefStudentID = id;
                _studExtDic.Add(id, record);
            }

            foreach (StudentRecord_Ext record in _studExtList)
            {
                //有已存在的資料就覆蓋回去
                _studExtDic[record.RefStudentID] = record;
            }

            _logDic.Clear();
            foreach (string id in _studExtDic.Keys)
            {
                //紀錄更新前的資料
                LogObj obj = new LogObj(id);
                obj.BeforeEn = _studExtDic[id].EntranceDate;
                obj.BeforeLv = _studExtDic[id].LeavingDate;
                _logDic.Add(id, obj);
            }
        }

        private void btnEntrance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確認將入學日期設為 " + dtEntrance.Value.ToShortDateString() + " ?", "ischool", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (string id in _studExtDic.Keys)
                {
                    _studExtDic[id].EntranceDate = dtEntrance.Value;
                    _logDic[id].AfterEn = dtEntrance.Value;
                }

                Save(sender);
            }
        }

        private void btnGraduate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確認將畢業日期設為 " + dtLeaving.Value.ToShortDateString() + " ?", "ischool", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (string id in _studExtDic.Keys)
                {
                    _studExtDic[id].LeavingDate = dtLeaving.Value;
                    _logDic[id].AfterLv = dtLeaving.Value;
                }

                Save(sender);
            }
        }

        private void Save(object sender)
        {
            //預備insert清單
            List<StudentRecord_Ext> insert = new List<StudentRecord_Ext>();

            //抽出沒有UID的record
            foreach (string id in _studExtDic.Keys)
            {
                if (string.IsNullOrEmpty(_studExtDic[id].UID))
                {
                    insert.Add(_studExtDic[id]);
                }
            }

            tool._A.InsertValues(insert);
            tool._A.UpdateValues(_studExtList);
            WriteLog(sender);

            DataInit();
        }

        private void WriteLog(object sender)
        {
            StringBuilder sb = new StringBuilder();

            string title = "";
            if (sender == btnEntrance)
                title = "入學日期";
            else if (sender == btnGraduate)
                title = "畢業日期";

            foreach (string id in _logDic.Keys)
            {
                StudentRecord sr = _studentRecords[id];
                LogObj obj = _logDic[id];

                if (sender == btnEntrance && obj.BeforeEn != obj.AfterEn)
                {
                    string before_date = obj.BeforeEn.HasValue ? obj.BeforeEn.Value.ToShortDateString() : string.Empty;
                    string after_date = obj.AfterEn.HasValue ? obj.AfterEn.Value.ToShortDateString() : string.Empty;
                    sb.AppendLine(string.Format("學生:{0} 學號:{1} 入學日期由「{2}」改為「{3}」", sr.Name, sr.StudentNumber, before_date, after_date));
                }

                if (sender == btnGraduate && obj.BeforeLv != obj.AfterLv)
                {
                    string before_date = obj.BeforeLv.HasValue ? obj.BeforeLv.Value.ToShortDateString() : string.Empty;
                    string after_date = obj.AfterLv.HasValue ? obj.AfterLv.Value.ToShortDateString() : string.Empty;
                    sb.AppendLine(string.Format("學生:{0} 學號:{1} 畢業日期由「{2}」改為「{3}」", sr.Name, sr.StudentNumber, before_date, after_date));
                }
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
                FISCA.LogAgent.ApplicationLog.Log("批次修改入學及畢業日期", "修改" + title, sb.ToString());

            MessageBox.Show(title + " 修改完成");
        }
    }

    class LogObj
    {
        public string ID;
        public DateTime? BeforeEn;
        public DateTime? AfterEn;
        public DateTime? BeforeLv;
        public DateTime? AfterLv;

        public LogObj(string id)
        {
            this.ID = id;
        }
    }
}
