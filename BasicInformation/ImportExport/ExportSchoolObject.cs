using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using K12.Data;

namespace BasicInformation
{
    class ExportSchoolObject : SmartSchool.API.PlugIn.Export.Exporter
    {
        private AccessHelper helper = new AccessHelper();

        //建構子
        public ExportSchoolObject()
        {
            this.Image = null;
            this.Text = "匯出學生基本資料(雙語部)";
        }

        //覆寫
        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            wizard.ExportableFields.AddRange("英文別名", "居留證號", "入學日期", "畢業日期");

            wizard.ExportPackage += (sender, e) =>
            {
                List<StudentRecord_Ext> records = tool._A.Select<StudentRecord_Ext>(string.Format("ref_student_id in ('{0}')", string.Join("','", e.List)));
                Dictionary<string, StudentRecord_Ext> recordsDic = new Dictionary<string, StudentRecord_Ext>();
                foreach (StudentRecord_Ext each in records)
                {
                    if (!recordsDic.ContainsKey(each.RefStudentID))
                    {
                        recordsDic.Add(each.RefStudentID, each);
                    }
                }

                List<StudentRecord> StudList = K12.Data.Student.SelectByIDs(e.List);
                StudList.Sort(SortStudent);

                foreach (StudentRecord stud in StudList)
                {
                    RowData row = new RowData();
                    row.ID = stud.ID;

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "英文別名": row.Add(field, "" + recordsDic[stud.ID].Nickname); break;
                                case "居留證號": row.Add(field, "" + recordsDic[stud.ID].PassportNumber); break;
                                case "入學日期": row.Add(field, recordsDic[stud.ID].EntranceDate.HasValue ? recordsDic[stud.ID].EntranceDate.Value.ToShortDateString() : ""); break;
                                case "畢業日期": row.Add(field, recordsDic[stud.ID].LeavingDate.HasValue ? recordsDic[stud.ID].LeavingDate.Value.ToShortDateString() : ""); break;
                            }
                        }
                    }

                    e.Items.Add(row);
                }
            };
        }

        private int SortStudent(StudentRecord sr1, StudentRecord sr2)
        {
            string sr1A = sr1.Class != null ? sr1.Class.Name.PadLeft(20, '0') : "";
            string sr2A = sr2.Class != null ? sr2.Class.Name.PadLeft(20, '0') : "";

            sr1A += sr1.SeatNo.HasValue ? sr1.SeatNo.Value.ToString().PadLeft(3, '0') : "000";
            sr2A += sr2.SeatNo.HasValue ? sr2.SeatNo.Value.ToString().PadLeft(3, '0') : "000";

            sr1A += sr1.Name.PadLeft(20, '0');
            sr2A += sr2.Name.PadLeft(20, '0');

            return sr1A.CompareTo(sr2A);
        }
    }
}
