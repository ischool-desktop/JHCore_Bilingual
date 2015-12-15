using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.API.PlugIn;
using JHSchool.Data;

namespace JHSchool.Permrec.ImportExport.StudentTag
{
    class ImportStudentTag : SmartSchool.API.PlugIn.Import.Importer
    {
        // 可匯入項目
        List<string> ImportItemList = new List<string>();

        public ImportStudentTag()
        {
            this.Image = null;
            this.Text = "匯入學生類別";
            ImportItemList.Add("群組");
            ImportItemList.Add("類別名稱");
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            Dictionary<string, JHStudentRecord> students = new Dictionary<string, JHStudentRecord>();
            Dictionary<string, List<JHStudentTagRecord>> StudTagRecDic = new Dictionary<string,List<JHStudentTagRecord>> ();

            Dictionary<string, Dictionary<string,string>> StudTagNameDic = DAL.DALTransfer.GetStudentTagNameDic();

            // 取得可加入學生 TagName            
            wizard.PackageLimit = 3000;
            wizard.ImportableFields.AddRange(ImportItemList);            
            wizard.ValidateStart += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateStartEventArgs e)
            {
                // 取得學生資料
                foreach (JHStudentRecord studRec in JHStudent.SelectByIDs(e.List))
                    if (!students.ContainsKey(studRec.ID))
                    {
                        students.Add(studRec.ID, studRec);
                        StudTagRecDic.Add(studRec.ID, new List<JHStudentTagRecord>());
                    }



                // 取得學生類別
                foreach (JHStudentTagRecord studTag in JHStudentTag.SelectByStudentIDs(students.Keys))
                {                    
                    //if (!StudTagRecDic.ContainsKey(studTag.RefStudentID))
                    //{
                    //    List<JHStudentTagRecord> rec = new List<JHStudentTagRecord> ();
                    //    rec.Add(studTag );
                    //    StudTagRecDic.Add(studTag.RefStudentID,rec);                       
                    //}
                    //else
                    if(StudTagRecDic.ContainsKey(studTag.RefStudentID ))
                        StudTagRecDic[studTag.RefStudentID].Add(studTag);
                }
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

                // 驗證資料
                foreach (string field in e.SelectFields)
                {
                    string value = e.Data[field].Trim();

                    // 驗證$無法匯入
                    if (value.IndexOf('$') > -1)
                    {
                        e.ErrorFields.Add(field, "儲存格有$無法匯入.");
                        break;
                    }


                    if (field == "類別名稱")
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            e.ErrorFields.Add(field, "不允許空白");
                        }
                    }
                }
            
            };

            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                // 目前學生類別管理，沒有新增標示類別，有的就不更動跳過。

                Dictionary<string, List<RowData>> id_Rows = new Dictionary<string, List<RowData>>();
                foreach (RowData data in e.Items)
                {
                    if (!id_Rows.ContainsKey(data.ID))
                        id_Rows.Add(data.ID, new List<RowData>());
                    id_Rows[data.ID].Add(data);
                }

                List<JHStudentTagRecord> InsertList = new List<JHStudentTagRecord>();
//                List<JHStudentTagRecord> UpdateList = new List<JHStudentTagRecord>();

                // 放需要新增的學生類別
                Dictionary<string, List<string>> NeedAddPrefixName = new Dictionary<string, List<string>>();

                // 檢查用 List
                List<string> CheckStudTagName = new List<string>();

                foreach (KeyValuePair<string,Dictionary<string,string>> data in StudTagNameDic)
                {
                    foreach (KeyValuePair<string, string> data1 in data.Value)
                        CheckStudTagName.Add(data.Key + data1.Key);
                }

                // 檢查類別是否已經存在
                foreach (string id in id_Rows.Keys)
                {
                    if (!StudTagRecDic.ContainsKey(id))
                        continue;
                    foreach (RowData data in id_Rows[id])
                    {
                        string strPrefix = string.Empty, strName = string.Empty;

                        if (data.ContainsKey("群組"))
                            strPrefix = data["群組"];

                        if (data.ContainsKey("類別名稱"))
                            strName = data["類別名稱"];

                        string FullName = strPrefix + strName;

                        // 需要新增的,
                        if (!CheckStudTagName.Contains(FullName))
                        {
                            CheckStudTagName.Add(FullName);
                            if((NeedAddPrefixName.ContainsKey(strPrefix )))
                                NeedAddPrefixName[strPrefix].Add(strName );
                            else
                            {
                                List<string> Names = new List<string>();
                                Names.Add(strName);
                                NeedAddPrefixName.Add(strPrefix, Names);
                            }
                        }
                    }
                }
                                
                // 新增至學生類別管理
                List<JHTagConfigRecord> Recs = new List<JHTagConfigRecord>();
                foreach (KeyValuePair<string, List<string>> data in NeedAddPrefixName)
                {
                    foreach (string data1 in data.Value)
                    {
                        JHTagConfigRecord rec = new JHTagConfigRecord();
                        rec.Category = "Student";
                        rec.Prefix = data.Key;
                        rec.Name = data1;
                        rec.Color = System.Drawing.Color.White;
                        Recs.Add(rec);
                    }
                }
                JHTagConfig.Insert(Recs);

                StudTagNameDic.Clear();

                // 重新取得
                StudTagNameDic = DAL.DALTransfer.GetStudentTagNameDic();

                foreach (string id in id_Rows.Keys)
                {
                    if (!StudTagRecDic.ContainsKey(id))
                        continue;
                    foreach (RowData data in id_Rows[id])
                    {
                        string strPrefix = string.Empty, strName = string.Empty;

                        if (data.ContainsKey("群組"))
                            strPrefix = data["群組"];                        

                        if(data.ContainsKey("類別名稱"))
                            strName = data["類別名稱"];

  
                            // 欄位有在 Tag Prefix 內
                                      bool isInsert = true;

                                    foreach (JHStudentTagRecord rec in StudTagRecDic[id])
                                    {
                                        if (rec.Prefix == strPrefix && rec.Name == strName)
                                        {
                                            isInsert = false;
                                            break;
                                        }
                                    }

                                    if (isInsert)
                                    {
                                        // 學生類別管理名稱對照
                                        if (StudTagNameDic.ContainsKey(strPrefix))
                                            if (StudTagNameDic[strPrefix].ContainsKey(strName))
                                            {                                                
                                                JHStudentTagRecord StudTag = new JHStudentTagRecord();
                                                StudTag.RefEntityID = id;
                                                StudTag.RefTagID = StudTagNameDic[strPrefix][strName];
                                                InsertList.Add(StudTag);
                                            }                                        
                                    }                        
                    }
                }

                try 
                {
                    if (InsertList.Count > 0)
                        Insert(InsertList);

                    //if (UpdateList.Count > 0)
                    //    Update(UpdateList);

                    JHSchool.PermRecLogProcess prlp = new JHSchool.PermRecLogProcess();
                    prlp.SaveLog("學生.匯入類別", "匯入學生類別", "匯入學生類別：共新增" + InsertList.Count + "筆資料");
                    JHSchool.Student.Instance.SyncAllBackground();
                    JHSchool.StudentTag.Instance.SyncAllBackground();
                    JHSchool.Data.JHStudent.RemoveAll();
                    JHSchool.Data.JHStudent.SelectAll();

                }
                catch(Exception ex){}
            
            };
        }


        // 更新
        private void Update(object item)
        {
            try
            {
                List<JHStudentTagRecord> UpdatePackage = (List<JHStudentTagRecord>)item;
                JHStudentTag.Update(UpdatePackage);
            }
            catch (Exception ex) { }
        }

        // 新增
        private void Insert(object item)
        {
            try
            {
                List<JHStudentTagRecord> InsertPackage = (List<JHStudentTagRecord>)item;
                JHStudentTag.Insert(InsertPackage);
            }
            catch(Exception ex) { }
        }
    }
}
