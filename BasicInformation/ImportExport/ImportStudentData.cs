using System.Collections.Generic;
using System.Windows.Forms;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using System;
using System.Data;
using K12.Data;
using System.Text;

namespace BasicInformation
{
    class ImportStudentData : SmartSchool.API.PlugIn.Import.Importer
    {
        List<string> _FieldNameList=new List<string> ();
        List<string> _Keys = new List<string>();
        public ImportStudentData()
        {
            this.Image = null;
            this.Text = "匯入學生基本資料(測試版)";            
            _FieldNameList.Add("姓名");
            _FieldNameList.Add("學號");
            _FieldNameList.Add("身分證號");
            _FieldNameList.Add("國籍");
            _FieldNameList.Add("出生地");
            _FieldNameList.Add("生日");
            _FieldNameList.Add("性別");
            _FieldNameList.Add("英文姓名");
            _FieldNameList.Add("英文別名");
            _FieldNameList.Add("居留證號");
            _FieldNameList.Add("GivenName");
            _FieldNameList.Add("MiddleName");
            _FieldNameList.Add("FamilyName");
            _FieldNameList.Add("班級");
            _FieldNameList.Add("年級");
            _FieldNameList.Add("座號");
            _FieldNameList.Add("父親姓名");
            _FieldNameList.Add("父親學歷");
            _FieldNameList.Add("父親職業");
            _FieldNameList.Add("父親電話");
            _FieldNameList.Add("母親姓名");
            _FieldNameList.Add("母親學歷");
            _FieldNameList.Add("母親職業");
            _FieldNameList.Add("母親電話");
            _FieldNameList.Add("戶籍電話");
            _FieldNameList.Add("聯絡電話");
            _FieldNameList.Add("行動電話");
            _FieldNameList.Add("其它電話1");
            _FieldNameList.Add("監護人姓名");
            _FieldNameList.Add("監護人電話");
            _FieldNameList.Add("入學日期");
            _FieldNameList.Add("畢業日期");
            _FieldNameList.Add("登入帳號");
            _FieldNameList.Add("狀態");
            _FieldNameList.Add("戶籍:郵遞區號");
            _FieldNameList.Add("戶籍:縣市");
            _FieldNameList.Add("戶籍:鄉鎮市區");
            _FieldNameList.Add("戶籍:村里");
            _FieldNameList.Add("戶籍:鄰");
            _FieldNameList.Add("戶籍:其他");
            _FieldNameList.Add("聯絡:郵遞區號");
            _FieldNameList.Add("聯絡:縣市");
            _FieldNameList.Add("聯絡:鄉鎮市區");
            _FieldNameList.Add("聯絡:村里");
            _FieldNameList.Add("聯絡:鄰");
            _FieldNameList.Add("聯絡:其他");
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            wizard.PackageLimit = 3000;
            //必需要有的欄位
            wizard.RequiredFields.AddRange("學號");
            //可匯入的欄位
            wizard.ImportableFields.AddRange(_FieldNameList);

            //驗證每行資料的事件
            wizard.ValidateRow += wizard_ValidateRow;

            //實際匯入資料的事件
            wizard.ImportPackage += wizard_ImportPackage;

            //匯入完成
            wizard.ImportComplete += (sender, e) => MessageBox.Show("匯入完成!");
        }

        void wizard_ImportPackage(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
        {
            
        }

        void wizard_ValidateRow(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
        {
            #region 驗各欄位填寫格式
            
            DateTime dt;
            foreach (string field in e.SelectFields)
            {
                string value = e.Data[field];
                switch (field)
                {
                    default:
                        break;

                    case "學號": break;
                    case "生日": break;
                    case "性別": break;
                    case "年級": break;
                    case "座號": break;
                    case "入學日期": break;
                    case "畢業日期": break;
                    case "狀態": break;
                    case "戶籍:郵遞區號": break;
                    case "聯絡:郵遞區號": break;
                    
                    
                    //case "學號":
                    //    if (value == "")
                    //        e.ErrorFields.Add(field, "此欄為必填欄位。");
                    //    break;


                    //case "入學日期":
                    //    if (value != "")
                    //    {
                    //        if (!DateTime.TryParse(value, out dt))
                    //            e.ErrorFields.Add(field, "資料必須為日期格式。");
                    //    }
                    //    break;
                    //case "畢業日期":
                    //    if (value != "")
                    //    {
                    //        if (!DateTime.TryParse(value, out dt))
                    //            e.ErrorFields.Add(field, "資料必須為日期格式。");
                    //    }
                    //    break;
                }
            }
            #endregion
            #region 驗證主鍵
           
            string Key = e.Data.ID;
            string errorMessage = string.Empty;

            if (_Keys.Contains(Key))
                errorMessage = "";
            else
                _Keys.Add(Key);

            e.ErrorMessage = errorMessage;

            #endregion
        }
    }
}
