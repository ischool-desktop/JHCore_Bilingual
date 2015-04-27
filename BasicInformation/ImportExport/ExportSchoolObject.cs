using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using SmartSchool.API.PlugIn;

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
            List<StudentRecord_Ext> allrecords = helper.Select<StudentRecord_Ext>();

            wizard.ExportableFields.AddRange("英文別名", "居留證號", "入學日期", "畢業日期");

            wizard.ExportPackage += (sender, e) =>
            {
                List<StudentRecord_Ext> records = new List<StudentRecord_Ext>();

                records = allrecords.Where(x => e.List.Contains(x.RefStudentID)).ToList();

                for (int i = 0; i < records.Count; i++)
                {
                    RowData row = new RowData();
                    row.ID = records[i].RefStudentID;
                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "英文別名": row.Add(field, "" + records[i].Nickname); break;
                                case "居留證號": row.Add(field, "" + records[i].PassportNumber); break;
                                case "入學日期": row.Add(field, records[i].EntranceDate.HasValue ? records[i].EntranceDate.Value.ToShortDateString() : ""); break;
                                case "畢業日期": row.Add(field, records[i].LeavingDate.HasValue ? records[i].LeavingDate.Value.ToShortDateString() : ""); break;
                            }
                        }
                    }

                    e.Items.Add(row);
                }
            };
        }
    }
}
