using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    // 學生類別統計
    class StudentTagCounter
    {
        // 男生統計
        private Dictionary<string, Dictionary<string, int>> _BoyCount;
        // 女生統計
        private Dictionary<string, Dictionary<string, int>> _GirlCount;
        // 全部統計
        private Dictionary<string, Dictionary<string, int>> _TotalCount;

        // 所選擇班級名稱
        private List<ClassRecord> _SelectedClassRecordList;

        // 所選擇項目名稱
        private List<string> _SelectedItems;

        public enum GenderType { 男生, 女生, 全部 };

        public StudentTagCounter()
        {
            _BoyCount = new Dictionary<string, Dictionary<string, int>>();
            _GirlCount = new Dictionary<string, Dictionary<string, int>>();
            _TotalCount = new Dictionary<string, Dictionary<string, int>>();
            _SelectedItems = new List<string>();
            _SelectedClassRecordList = new List<ClassRecord>();
        }

        /// <summary>
        /// 所選擇項目
        /// </summary>
        /// <returns></returns> 
        public List<string> GetSelectedItems()
        {
            return _SelectedItems;
        }

        /// <summary>
        /// 所選擇班級
        /// </summary>
        /// <returns></returns>
        public List<ClassRecord> GetSelectedClassRecordList()
        {
            return _SelectedClassRecordList;
        }

        /// <summary>
        /// 所選擇班級的年級名稱+班名
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetSelectedGradeClassNames()
        {
            Dictionary<string, List<string>> returnValue = new Dictionary<string, List<string>>();

            foreach (ClassRecord cr in _SelectedClassRecordList)
            {
                string GradeYear = "";
                if(cr.GradeYear ==null)
                    GradeYear =" ";
                else
                    GradeYear =cr.GradeYear ;

                if (returnValue.ContainsKey(GradeYear))
                    returnValue[GradeYear].Add(cr.Name);
                else
                {
                    List<string> name = new List<string>();
                    name.Add(cr.Name);
                    returnValue.Add(GradeYear, name);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 新增學生統計
        /// </summary>
        /// <param name="studEntity"></param>
        /// <param name="isGradeYear"></param>
        /// <returns></returns>
        public void AddCount(StudentEntity studEntity,bool isGradeYear)
        {
            string className;
            if (isGradeYear)
                className = studEntity.GradeYear + "_年級";
            else
            {
                if (string.IsNullOrEmpty(studEntity.ClassName))
                    className = " ";
                else
                    className = studEntity.ClassName;
            }

            if(studEntity.StudentTagFullName!=null)
            foreach (string str in studEntity.StudentTagFullName)
            {
                if (studEntity.Gender == "男")
                    if (_BoyCount.ContainsKey(className))
                        if (_BoyCount[className].ContainsKey(str))
                            _BoyCount[className][str]++;

                if (studEntity.Gender == "女")
                    if (_GirlCount.ContainsKey(className))
                        if (_GirlCount[className].ContainsKey(str))
                            _GirlCount[className][str]++;

                if (_TotalCount.ContainsKey(className))
                    if (_TotalCount[className].ContainsKey(str))
                        _TotalCount[className][str]++;
            }
        }

        /// <summary>
        /// 設定統計內容與初始化
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="ClassRecordList"></param>
        public void SetStudentTagCountItem(List<string> Items,List<ClassRecord > ClassRecordList)
        {
            ClearCountItems();
            _SelectedClassRecordList = ClassRecordList;
            _SelectedItems = Items;
            List<string> ClassNameList = new List<string>();

            foreach (ClassRecord cr in ClassRecordList)
            {
                if (cr != null)
                {                    
                    if (!ClassNameList.Contains(cr.Name))
                        ClassNameList.Add(cr.Name);

                    string classYear = cr.GradeYear+"_年級";
                    if (!ClassNameList.Contains(classYear))
                        ClassNameList.Add(classYear);
                
                }

            }
            foreach (string ClassName in ClassNameList)
            {
                if (!_TotalCount.ContainsKey(ClassName))
                {
                    _TotalCount.Add(ClassName, new Dictionary<string, int>());
                    _BoyCount.Add(ClassName, new Dictionary<string, int>());
                    _GirlCount.Add(ClassName, new Dictionary<string, int>());

                    foreach (string ItemName in Items)
                    {
                        _TotalCount[ClassName].Add(ItemName, 0);
                        _BoyCount[ClassName].Add(ItemName, 0);
                        _GirlCount[ClassName].Add(ItemName, 0);
                    }
                }
            }
        }

        
        /// <summary>
        /// 清空計算內值
        /// </summary>
        public void ClearCountItems()
        {
            _SelectedItems.Clear();
            _TotalCount.Clear();
            _BoyCount.Clear();
            _GirlCount.Clear();
            _SelectedClassRecordList.Clear();            
        
        }

        /// <summary>
        /// 取得內容
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="SelectItemName"></param>
        /// <param name="isGradeYear"></param>
        /// <param name="gt"></param>
        /// <returns></returns>
        public int GetStudentTagCount(string ClassName, string SelectItemName, bool isGradeYear, GenderType gt)
        {
            int returnValue = 0;
            if (isGradeYear)
                ClassName += "_年級";

            if (gt == GenderType.男生)
            {
                if (_BoyCount.ContainsKey(ClassName))
                    if (_BoyCount[ClassName].ContainsKey(SelectItemName))
                        returnValue = _BoyCount[ClassName][SelectItemName];
            }

            if (gt == GenderType.女生)            
            {
                if (_GirlCount.ContainsKey(ClassName))
                    if (_GirlCount[ClassName].ContainsKey(SelectItemName))
                        returnValue = _GirlCount[ClassName][SelectItemName];
            }

            if (gt == GenderType.全部)
            {
                if (_TotalCount.ContainsKey(ClassName))
                    if (_TotalCount[ClassName].ContainsKey(SelectItemName))
                        returnValue = _TotalCount[ClassName][SelectItemName];            
            }

            return returnValue;
        }

    }
}
