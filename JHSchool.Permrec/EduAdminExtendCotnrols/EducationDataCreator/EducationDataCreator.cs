using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator
{
    class EducationCodeCreator
    {
        // 畢業 81,非畢業 82
        public enum SelectType { 新生, 畢業 };

        private List<DAL.StudentEntity> _StudentEntityList;
        private List<DAL.StudentEntity> _ErrorData;
        private List<string> _ExportText;

        public EducationCodeCreator()
        {
            _StudentEntityList = new List<JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.DAL.StudentEntity>();
            _ErrorData = new List<JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.DAL.StudentEntity>();
            _ExportText = new List<string>();
        
        }

        /// <summary>
        /// 加入學生資料
        /// </summary>
        /// <param name="StudentEntityList"></param>
        public void AddStudentList(List<DAL.StudentEntity> StudentEntityList)
        {
            _StudentEntityList = StudentEntityList;        
        }

        /// <summary>
        /// 檢查是否有錯誤資料
        /// </summary>
        public void CheckErrorData()
        {
            _ErrorData.Clear();

            foreach (DAL.StudentEntity se in _StudentEntityList)
                if (!string.IsNullOrEmpty(se.Memo))
                    _ErrorData.Add(se);        
        }

        /// <summary>
        /// 產生文字資料
        /// </summary>
        private void ExportTextData()
        {
            _ExportText.Clear();
            // 姓名+身分證+生日+代碼+SchoolCode            
            foreach (DAL.StudentEntity se in _StudentEntityList)
            {
                if (!string.IsNullOrEmpty(se.Memo))
                {
                    string name = se.Name + "　　　　　　";
                    name = name.Substring(0, 6);
                    string strText = name + se.IDNumber + se.GetBirthdayGovStr() + se.CertCode+se.SchoolCode;
                    _ExportText.Add(strText);
                }
            }        
        }

        /// <summary>
        /// 傳入選擇狀態 是新生或畢業，畢業、修業81
        /// </summary>
        /// <param name="ST"></param>
        public void SetStatus(SelectType ST)
        {
            // 加入教育程度代碼,畢業 81，其他都82
            foreach (DAL.StudentEntity se in _StudentEntityList)
            {
                se.CheckDataError();

                // 自己檢查資料是否正確
                if (ST == SelectType.畢業)
                {
                    se.CertCode = "  ";

                    if (se.Status == "畢業" || se.Status =="修業")
                        se.CertCode = "81";
                    else
                    {
                        // 計算年齡，以當年12/31計算,一年天數365.2422天
                        DateTime dt = new DateTime(DateTime.Now.Year, 12, 31);
                        TimeSpan ts = dt - se.Birthday;
                        double age = ts.TotalDays / 365.2422;

                        // 如果學生是中輟或滿15歲,代碼82
                        if (age >= 15 || se.StudStatus == "輟學")
                            se.CertCode = "82";
                    }                        
                }
                else
                {
                    se.CertCode = "82";
                }
            }
        }

        /// <summary>
        /// 取得匯出文字
        /// </summary>
        public List<string> GetExportText()
        {
            ExportTextData();
            return _ExportText;            
        }

        /// <summary>
        /// 依學號小到大排序
        /// </summary>
        public void StudentListSortByStudentNumber()
        {
            _StudentEntityList.Sort(new Comparison<DAL.StudentEntity>(StudentEntitySorterByStudentNumber));        
        }

        // 學號排序用 string parse int 小->大
        private static int StudentEntitySorterByStudentNumber(DAL.StudentEntity se1,DAL.StudentEntity se2)
        {
            int num1, num2;
            int.TryParse(se1.StudentNumber, out num1);
            int.TryParse(se2.StudentNumber , out num2);

            return num1.CompareTo(num2);
        }

    }
}
