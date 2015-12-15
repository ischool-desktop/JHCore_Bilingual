using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.API.PlugIn;
using JHSchool.Data;

namespace JHSchool.Permrec.ImportExport.GraduateInfo
{
    class ImportGraduateInfo : SmartSchool.API.PlugIn.Import.Importer
    {
        public ImportGraduateInfo()
        {
            this.Image = null;
            this.Text = "匯入畢業資訊";        
        }       

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            // 學生
            Dictionary<string, JHStudentRecord> students = new Dictionary<string, JHStudentRecord>();
            
            // 畢業資訊
            Dictionary<string, JHLeaveInfoRecord> StudJHLeaveInfoRecordDic = new Dictionary<string,JHLeaveInfoRecord>();                


            // 可匯入項目
            List<string> ImportItemList = new List<string>();
            ImportItemList.Add("畢業學年度");
            ImportItemList.Add("畢業資格");
            ImportItemList.Add("畢業證書字號");
            ImportItemList.Add("畢業相關訊息");

            // 取得可加入學生 TagName            
            wizard.PackageLimit = 3000;
            wizard.ImportableFields.AddRange(ImportItemList);
            wizard.RequiredFields.AddRange();
            wizard.ValidateStart += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateStartEventArgs e)
            {
                // 取得學生資料
                students = JHStudent.SelectByIDs(e.List).ToDictionary(x => x.ID);
                // 取得畢業資訊
                StudJHLeaveInfoRecordDic = JHLeaveIfno.SelectByStudentIDs(e.List).ToDictionary(x => x.RefStudentID);

            };

            wizard.ValidateRow += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
            {
                int i = 0;

                // 檢查學生是否存在
                JHStudentRecord studRec = null;
                if (students.ContainsKey(e.Data.ID))
                    studRec = students[e.Data.ID];
                else
                {
                    e.ErrorMessage = "沒有這位學生" + e.Data.ID;
                    return;
                }

            };

            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                Dictionary<string, List<RowData>> id_Rows = new Dictionary<string, List<RowData>>();
                foreach (RowData data in e.Items)
                {
                    if (!id_Rows.ContainsKey(data.ID))
                        id_Rows.Add(data.ID, new List<RowData>());
                    id_Rows[data.ID].Add(data);
                }

                List<JHLeaveInfoRecord> UpdateList = new List<JHLeaveInfoRecord>();

                foreach (string id in id_Rows.Keys)
                {
                    if (StudJHLeaveInfoRecordDic.ContainsKey(id))
                    {
                        foreach (RowData data in id_Rows[id])
                        {
                            foreach (string field in e.ImportFields)
                            {
                                if (field == "畢業學年度")
                                {
                                    int SchoolYear;
                                    if (int.TryParse(data[field], out SchoolYear))
                                        StudJHLeaveInfoRecordDic[id].SchoolYear = SchoolYear;
                                    else
                                        StudJHLeaveInfoRecordDic[id].SchoolYear = null;
                                }

                                if (field == "畢業資格")
                                    StudJHLeaveInfoRecordDic[id].Reason = data[field];

                                if(field =="畢業證書字號")
                                    StudJHLeaveInfoRecordDic[id].DiplomaNumber= data[field];

                                if(field =="畢業相關訊息")
                                    StudJHLeaveInfoRecordDic[id].Memo = data[field];
                            }
                        }
                    }
                }

                UpdateList = StudJHLeaveInfoRecordDic.Values.ToList();

                try
                {
                    if (UpdateList.Count > 0)
                        Update(UpdateList);

                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學生.匯入畢業資訊", "匯入學生畢業資訊", "匯入學生畢業資訊：共更新:" + UpdateList.Count + "筆資料");
                    JHSchool.Student.Instance.SyncAllBackground();
                    JHSchool.StudentTag.Instance.SyncAllBackground();
                    JHSchool.Data.JHStudent.RemoveAll();
                    JHSchool.Data.JHStudent.SelectAll();

                }
                catch (Exception ex) { }

            };
        }


        // 更新
        private void Update(object item)
        {
            try
            {
                List<JHLeaveInfoRecord> UpdatePackage = (List<JHLeaveInfoRecord>)item;
                JHLeaveIfno.Update(UpdatePackage);
            }
            catch (Exception ex) 
            {
                
            }        
        }
    }
}
