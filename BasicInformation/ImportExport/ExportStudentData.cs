using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using K12.Data;

namespace BasicInformation
{
    class ExportStudentData : SmartSchool.API.PlugIn.Export.Exporter
    {
        List<string> _FieldNameList;
        
        public ExportStudentData()
        {
            this.Image = null;
            this.Text = "匯出學生基本資料(2015)";
            _FieldNameList = new List<string>();
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
            _FieldNameList.Add("年級");
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

        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            wizard.ExportableFields.AddRange(_FieldNameList);
            wizard.ExportPackage += (sender, e) =>
            {
                // 取得學生資料
                List<StudentRecord> StudentRecList = K12.Data.Student.SelectByIDs(e.List);

                // 學生排序 班座排序
                StudentRecList = (from data in StudentRecList orderby data.Class != null ? data.Class.Name.PadLeft(3,'0') : "000" ascending,data.SeatNo.HasValue ? data.SeatNo.Value:999 ascending select data).ToList();

                // 取得電話
                Dictionary<string, PhoneRecord> PhoneRecordDict = new Dictionary<string, PhoneRecord>();
                List<PhoneRecord> PhoneRecordList = K12.Data.Phone.SelectByStudentIDs(e.List);
                foreach (PhoneRecord rec in PhoneRecordList)
                {
                    if (!PhoneRecordDict.ContainsKey(rec.RefStudentID))
                        PhoneRecordDict.Add(rec.RefStudentID, rec);
                }

                // 取得父母監護人資料
                Dictionary<string, ParentRecord> ParentRecordDict = new Dictionary<string, ParentRecord>();
                List<ParentRecord> ParentRecordList = K12.Data.Parent.SelectByStudentIDs(e.List);
                foreach (ParentRecord rec in ParentRecordList)
                {
                    if (!ParentRecordDict.ContainsKey(rec.RefStudentID))
                        ParentRecordDict.Add(rec.RefStudentID, rec);
                }

                // 取得地址資料
                Dictionary<string, AddressRecord> AddressRecordDict = new Dictionary<string, AddressRecord>();
                List<AddressRecord> AddressRecordList = K12.Data.Address.SelectByStudentIDs(e.List);
                foreach (AddressRecord rec in AddressRecordList)
                {
                    if (!AddressRecordDict.ContainsKey(rec.RefStudentID))
                        AddressRecordDict.Add(rec.RefStudentID, rec);
                }

                // 取得 UDT 延伸
                AccessHelper accHelper = new AccessHelper();
                string strqry = "ref_student_id in('"+string.Join("','",e.List.ToArray())+"')";
                List<StudentRecord_Ext> StudentRecord_ExtList = accHelper.Select<StudentRecord_Ext>(strqry);
                Dictionary<string, StudentRecord_Ext> StudentRecord_ExtDict = new Dictionary<string, StudentRecord_Ext>();
                foreach (StudentRecord_Ext rec in StudentRecord_ExtList)
                {
                    if (!StudentRecord_ExtDict.ContainsKey(rec.RefStudentID))
                        StudentRecord_ExtDict.Add(rec.RefStudentID, rec);
                }

                foreach(StudentRecord StudRec in StudentRecList)
                {
                    RowData row = new RowData();
                    row.ID = StudRec.ID;

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "身分證號": row.Add(field, StudRec.IDNumber); break;
                                case "國籍": row.Add(field, StudRec.Nationality); break;
                                case "出生地": row.Add(field, StudRec.BirthPlace); break;
                                case "生日": 
                                    if(StudRec.Birthday.HasValue)
                                    row.Add(field, StudRec.Birthday.Value.ToShortDateString()); break;
                                case "性別": row.Add(field, StudRec.Gender); break;
                                case "英文姓名": row.Add(field, StudRec.EnglishName); break;

                                case "英文別名": 
                                    if(StudentRecord_ExtDict.ContainsKey(StudRec.ID))
                                        row.Add(field, StudentRecord_ExtDict[StudRec.ID].Nickname);  break;
                                case "居留證號":
                                    if (StudentRecord_ExtDict.ContainsKey(StudRec.ID))
                                            row.Add(field, StudentRecord_ExtDict[StudRec.ID].PassportNumber);  break;
                                case "GivenName": 
                                    if (StudentRecord_ExtDict.ContainsKey(StudRec.ID))
                                            row.Add(field, StudentRecord_ExtDict[StudRec.ID].GivenName);  break;                                    
                                case "MiddleName":
                                    if (StudentRecord_ExtDict.ContainsKey(StudRec.ID))
                                        row.Add(field, StudentRecord_ExtDict[StudRec.ID].MiddleName); break;
                                case "FamilyName": 
                                if (StudentRecord_ExtDict.ContainsKey(StudRec.ID))
                                    row.Add(field, StudentRecord_ExtDict[StudRec.ID].FamilyName);  break;                                    
    
                                case "年級": 
                                    if(StudRec.Class !=null && StudRec.Class.GradeYear.HasValue)
                                    row.Add(field, StudRec.Class.GradeYear.Value.ToString()); break;
                                case "父親姓名": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].FatherName); break;
                                case "父親學歷": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].FatherEducationDegree); break;
                                case "父親職業": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].FatherJob); break;
                                case "父親電話": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].FatherPhone); break;
                                case "母親姓名": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].MotherName); break;
                                case "母親學歷": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].MotherEducationDegree); break;
                                case "母親職業": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].MotherJob); break;
                                case "母親電話": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, ParentRecordDict[StudRec.ID].MotherPhone); break;
                                
                                case "戶籍電話": 
                                    if(PhoneRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, PhoneRecordDict[StudRec.ID].Permanent); break;
                                case "聯絡電話":
                                    if (PhoneRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, PhoneRecordDict[StudRec.ID].Contact); break;
                                case "行動電話": 
                                    if(PhoneRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, PhoneRecordDict[StudRec.ID].Cell); break;
                                case "其它電話1": 
                                   if(PhoneRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field, PhoneRecordDict[StudRec.ID].Phone1); break;                                
                                case "監護人姓名": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,ParentRecordDict[StudRec.ID].CustodianName); break;
                                case "監護人電話": 
                                    if(ParentRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,ParentRecordDict[StudRec.ID].CustodianPhone); break;
                                case "入學日期": 
                                if (StudentRecord_ExtDict.ContainsKey(StudRec.ID) && StudentRecord_ExtDict[StudRec.ID].EntranceDate.HasValue)
                                    row.Add(field, StudentRecord_ExtDict[StudRec.ID].EntranceDate.Value.ToShortDateString());  break;
                                case "畢業日期": 
                                if (StudentRecord_ExtDict.ContainsKey(StudRec.ID) && StudentRecord_ExtDict[StudRec.ID].LeavingDate.HasValue)
                                    row.Add(field, StudentRecord_ExtDict[StudRec.ID].LeavingDate.Value.ToShortDateString());  break;
                                
                                case "登入帳號": row.Add(field, StudRec.SALoginName); break;
                                case "狀態":row.Add(field, StudRec.StatusStr); break;
                                case "戶籍:郵遞區號":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].PermanentZipCode); break;
                                case "戶籍:縣市":
                                    if (AddressRecordDict.ContainsKey(StudRec.ID))
                                        row.Add(field, AddressRecordDict[StudRec.ID].PermanentCounty); break;
                                case "戶籍:鄉鎮市區":                                    
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].PermanentTown); break;
                                case "戶籍:村里": 
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].PermanentDistrict); break;

                                case "戶籍:鄰":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].PermanentArea); break;

                                case "戶籍:其他": 
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].PermanentDetail); break;

                                case "聯絡:郵遞區號": 
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingZipCode); break;

                                case "聯絡:縣市": 
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingCounty); break;

                                case "聯絡:鄉鎮市區":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingTown); break;

                                case "聯絡:村里":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingDistrict); break;

                                case "聯絡:鄰":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingArea); break;

                                case "聯絡:其他":
                                    if(AddressRecordDict.ContainsKey(StudRec.ID))
                                    row.Add(field,AddressRecordDict[StudRec.ID].MailingDetail); break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }

            };
        }

    }
}
