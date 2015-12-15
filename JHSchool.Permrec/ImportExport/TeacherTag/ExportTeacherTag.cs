using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.API.PlugIn;

namespace JHSchool.Permrec.ImportExport.TeacherTag
{
    class ExportTeacherTag : SmartSchool.API.PlugIn.Export.Exporter
    {
        public ExportTeacherTag()
        {
            this.Image = null;
            this.Text = "匯出教師類別";        
        }

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {

            wizard.ExportableFields.AddRange(DAL.DALTransfer.GetTeacherTagPrefixList(Teacher.Instance.SelectedKeys));

            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                List<DAL.TeacherTagEntity> TeacherTagList = DAL.DALTransfer.GetTeacherTagList(e.List);
                foreach (DAL.TeacherTagEntity  ste in TeacherTagList)
                {
                    RowData row = new RowData();
                    row.ID = ste.TeacherID;
                    foreach (string field in e.ExportFields)
                    {

                        if (wizard.ExportableFields.Contains(field))
                        {
                            if (ste.PrefixNameDic.ContainsKey(field))
                            {
                                string str = "";
                                foreach (string strItem in ste.PrefixNameDic[field])
                                    str += strItem + "、";
                                str = str.Substring(0, str.Length - 1);
                                row.Add(field, str);
                            }
                        }
                    }
                    e.Items.Add(row);
                }

                PermRecLogProcess prlp = new PermRecLogProcess();
                prlp.SaveLog("教師.匯出類別", "匯出", "共匯出" + TeacherTagList.Count + "筆教師類別資料.");
            };  
        }
    }
}
