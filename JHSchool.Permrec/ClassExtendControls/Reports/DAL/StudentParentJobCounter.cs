using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class StudentParentJobCounter
    {
        public enum ParentJobType { 父親, 母親, 監護人 };

        private Dictionary<string,Dictionary<string, int>> _FatherJobCountDic;
        private Dictionary<string, Dictionary<string, int>> _MotherJobCountDic;
        private Dictionary<string, Dictionary<string, int>> _CustodianJobCountDic;
        private Dictionary<string,int> _GradeClassStudCountDic;
        private Dictionary<string, List<string>> _GradeClassName;

        public StudentParentJobCounter()
        {
            _FatherJobCountDic = new Dictionary<string, Dictionary<string, int>>();
            _MotherJobCountDic = new Dictionary<string, Dictionary<string, int>>();
            _CustodianJobCountDic = new Dictionary<string, Dictionary<string, int>>();
            _GradeClassStudCountDic = new Dictionary<string, int>();
            _GradeClassName = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// 設定班級學生人數
        /// </summary>
        /// <param name="ClassRecords"></param>
        public void SetGradeClassStudCount(List<ClassRecord> ClassRecords)
        {
            _GradeClassStudCountDic.Clear();
            string strGradeYear = "";
            string strClassName = "";
            foreach (ClassRecord cr in ClassRecords)
            {
                strGradeYear = cr.GradeYear + "_年級";

                if (_GradeClassStudCountDic.ContainsKey(strGradeYear))
                    _GradeClassStudCountDic[strGradeYear] += cr.Students.Count;
                else
                    _GradeClassStudCountDic.Add(strGradeYear, cr.Students.Count);

                if (_GradeClassStudCountDic.ContainsKey(cr.Name))
                    _GradeClassStudCountDic[cr.Name] += cr.Students.Count;
                else
                    _GradeClassStudCountDic.Add(cr.Name, cr.Students.Count);

                if (_GradeClassName.ContainsKey(cr.GradeYear))
                {
                    _GradeClassName[cr.GradeYear].Add(cr.Name );
                }
                else
                { 
                    List<string > tmpClassName = new List<string> ();
                    tmpClassName.Add(cr.Name );
                    _GradeClassName.Add(cr.GradeYear, tmpClassName);
                }
            }        
        }

        /// <summary>
        /// 取得選取年級班級名稱
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetSelectGradeClassName()
        {
            return _GradeClassName;
        }

        /// <summary>
        /// 取得年級與班級人數
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="isClassName"></param>
        /// <returns></returns>
        public int GetGradeClassStudCount(string Name, bool isGetGradeYear)
        {
            int returnValue = 0;

            if (isGetGradeYear)
                 Name += "_年級";                

            if (_GradeClassStudCountDic.ContainsKey(Name))
                returnValue = _GradeClassStudCountDic[Name];

            return returnValue;
        }

        /// <summary>
        /// 透過班級職業名稱取得人數
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="JobName"></param>
        /// <param name="PJT"></param>
        /// <param name="isGetGradeYear"></param>
        /// <returns></returns>
        public int GetParentJobCount(string ClassName, string JobName, ParentJobType PJT, bool isGetGradeYear)
        {
            int returnValue = 0;

            if (isGetGradeYear)
                ClassName += "_年級";

            if (PJT == ParentJobType.父親)
            {
                if (_FatherJobCountDic.ContainsKey(ClassName))
                    if (_FatherJobCountDic[ClassName].ContainsKey(JobName))
                        returnValue = _FatherJobCountDic[ClassName][JobName];
            }

            if (PJT == ParentJobType.母親)
            {
                if (_MotherJobCountDic.ContainsKey(ClassName))
                    if (_MotherJobCountDic[ClassName].ContainsKey(JobName))
                        returnValue = _MotherJobCountDic[ClassName][JobName];            
            }

            if (PJT == ParentJobType.監護人)
            {
                if (_CustodianJobCountDic.ContainsKey(ClassName))
                    if (_CustodianJobCountDic[ClassName].ContainsKey(JobName))
                        returnValue = _CustodianJobCountDic[ClassName][JobName];            
            }
            return returnValue;
        }

        /// <summary>
        /// 透過班級職業名稱取得人數百分比
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="JobName"></param>
        /// <param name="PJT"></param>
        /// <param name="isGetGradeYear"></param>
        /// <param name="RoundRange"></param>
        /// <returns></returns>
        public decimal GetParentJobCountPercentage(string ClassName, string JobName, ParentJobType PJT, bool isGetGradeYear, int RoundRange)
        {
            decimal returnValue = 0;

            int tmpParentJobCount = GetParentJobCount(ClassName, JobName, PJT, isGetGradeYear);
            int tmpClassCount = GetGradeClassStudCount(ClassName, isGetGradeYear);

            if (tmpClassCount > 0)
            {
                returnValue = Math.Round(((decimal)tmpParentJobCount / (decimal)tmpClassCount)*100, RoundRange, MidpointRounding.AwayFromZero);            
            }

            return returnValue;
        }


        /// <summary>
        /// 加入職業計算
        /// </summary>
        /// <param name="se"></param>
        /// <returns></returns>
        public string AddParentJob(StudentEntity se)
        {
            string Memo = "";
            string JobName = "";
            string GradeYear = "";

            if (string.IsNullOrEmpty(se.GradeYear))
                GradeYear = " ";
            else
                GradeYear = se.GradeYear;
            
            GradeYear += "_年級";

            if (string.IsNullOrEmpty(se.FatherJob))
                JobName = " ";
            else
                JobName = se.FatherJob;
            AddParentJobBaseByClassName(se.ClassName, JobName, ParentJobType.父親);
            AddParentJobBaseByClassName(GradeYear, JobName, ParentJobType.父親);

            if (string.IsNullOrEmpty(se.MotherJob))
                JobName = " ";
            else
                JobName = se.MotherJob;
            AddParentJobBaseByClassName(se.ClassName, JobName, ParentJobType.母親);
            AddParentJobBaseByClassName(GradeYear, JobName, ParentJobType.母親);

            if (string.IsNullOrEmpty(se.CustodianJob))
                JobName = " ";
            else
                JobName = se.CustodianJob;

            AddParentJobBaseByClassName(se.ClassName, JobName, ParentJobType.監護人);
            AddParentJobBaseByClassName(GradeYear, JobName, ParentJobType.監護人);   

            return Memo;
        }        

        // 計算底層
        private void AddParentJobBaseByClassName(string ClassName,string JobName, ParentJobType PJT)
        {
            if (PJT == ParentJobType.父親)
            {
                if (_FatherJobCountDic.ContainsKey(ClassName))
                {
                    if (_FatherJobCountDic[ClassName].ContainsKey(JobName))
                    {
                        _FatherJobCountDic[ClassName][JobName]++;
                    }
                    else
                    {                     
                        _FatherJobCountDic[ClassName].Add(JobName, 1);
                    }

                }
                else
                {
                    Dictionary<string, int> tmpDic = new Dictionary<string, int>();
                    tmpDic.Add(JobName, 1);
                    _FatherJobCountDic.Add(ClassName, tmpDic);
                }
            }

            if (PJT == ParentJobType.母親)
            {
                if (_MotherJobCountDic.ContainsKey(ClassName))
                {
                    if (_MotherJobCountDic[ClassName].ContainsKey(JobName))
                    {
                        _MotherJobCountDic[ClassName][JobName]++;
                    }
                    else
                    {                        
                        _MotherJobCountDic[ClassName].Add(JobName, 1);
                    }

                }
                else
                {
                    Dictionary<string, int> tmpDic = new Dictionary<string, int>();
                    tmpDic.Add(JobName, 1);
                    _MotherJobCountDic.Add(ClassName, tmpDic);
                }            
            }

            if (PJT == ParentJobType.監護人)
            {
                if (_CustodianJobCountDic.ContainsKey(ClassName))
                {
                    if (_CustodianJobCountDic[ClassName].ContainsKey(JobName))
                    {
                        _CustodianJobCountDic[ClassName][JobName]++;
                    }
                    else
                    {                        
                        _CustodianJobCountDic[ClassName].Add(JobName, 1);
                    }

                }
                else
                {
                    Dictionary<string, int> tmpDic = new Dictionary<string, int>();
                    tmpDic.Add(JobName, 1);
                    _CustodianJobCountDic.Add(ClassName, tmpDic);
                }
            }
        }
    }
}
