using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchool.API.PlugIn;
using JHSchool.Data;

namespace JHSchool.Permrec.ImportExport.GraduateInfo
{
    class ExportGraduateInfo : SmartSchool.API.PlugIn.Export.Exporter
    {
        List<string> ExportItemList = new List<string>();
        public ExportGraduateInfo()
        {
            this.Image = null;
            this.Text = "匯出畢業資訊";
            // 可匯出項目            
            ExportItemList.Add("畢業學年度");
            ExportItemList.Add("畢業資格");
            ExportItemList.Add("畢業證書字號");
            ExportItemList.Add("畢業相關訊息");
        }

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {           
            wizard.ExportableFields.AddRange(ExportItemList);

            wizard.ExportPackage += delegate(object sender, SmartSchool.API.PlugIn.Export.ExportPackageEventArgs e)
            {
                // 取得畢業相關資訊
                List<JHLeaveInfoRecord> JHLeaveInfoRecordList = JHLeaveIfno.SelectByStudentIDs(e.List);

                foreach (JHLeaveInfoRecord lir in JHLeaveInfoRecordList)
                {
                    RowData row = new RowData();
                    row.ID = lir.RefStudentID ;
                    
                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "畢業學年度":
                                    if (lir.SchoolYear.HasValue)
                                        row.Add(field, lir.SchoolYear.Value.ToString());   
                                    break;
                                case "畢業資格":
                                    row.Add(field, lir.Reason); 
                                    break;
                                case "畢業證書字號":
                                    row.Add(field, lir.DiplomaNumber);   
                                    break;
                                case "畢業相關訊息":
                                    row.Add(field, lir.Memo); 
                                    break;                           
                            }                            
                        }
                    }
                    e.Items.Add(row);
                }
                
                PermRecLogProcess prlp = new PermRecLogProcess();
                prlp.SaveLog("學生.匯出畢業資訊", "匯出", "共匯出" + K12.Presentation.NLDPanels.Student.SelectedSource.Count + "筆學生類別資料.");
            };  
        }
    }  
}
