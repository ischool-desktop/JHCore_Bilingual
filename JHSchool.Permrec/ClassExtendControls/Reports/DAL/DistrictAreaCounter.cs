using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class DistrictAreaCounter
    {
        // 里 總人數統計
        Dictionary<string, int> _DistrictTotalCount;
        // 里 男生人數統計
        Dictionary<string, int> _DistrictBoyCount;
        // 里 女生人數統計
        Dictionary<string, int> _DistrictGirlCount;
        // 里 未分性別人數統計
        Dictionary<string, int> _DistrictNoGenderCount;



        List<StudentEntity> StudentEntityList;

        // 里鄰表
        Dictionary<string, List<string>> _DistrictAreName;

        // 里鄰人數計算
        private List<DistrictAreaCounterBase> _DistrictAreaCounterList;

        public DistrictAreaCounter()
        {
            //            StudentEntityList = _StudentEntityList;
            _DistrictBoyCount = new Dictionary<string, int>();
            _DistrictGirlCount = new Dictionary<string, int>();
            _DistrictTotalCount = new Dictionary<string, int>();
            _DistrictNoGenderCount = new Dictionary<string, int>();
            _DistrictAreaCounterList = new List<DistrictAreaCounterBase>();
            _DistrictAreName = new Dictionary<string, List<string>>();
            // 計算
            //            CalDistrictAreaCount();
        }

        /// <summary>
        /// 取得里男生人數統計
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetDistrictBoyCount()
        {
            _DistrictBoyCount.Clear();

            foreach (DistrictAreaCounterBase dacb in _DistrictAreaCounterList)
            {
                string key = dacb.GetDistrict();
                if (_DistrictBoyCount.ContainsKey(key))
                    _DistrictBoyCount[key] += dacb.GetBoyCount();
                else
                    _DistrictBoyCount.Add(key, dacb.GetBoyCount());
            }
            return _DistrictBoyCount;
        }

        /// <summary>
        /// 取得里 女生人數統計
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetDistrictGirlCount()
        {
            _DistrictGirlCount.Clear();

            foreach (DistrictAreaCounterBase dacb in _DistrictAreaCounterList)
            {
                string key = dacb.GetDistrict();
                if (_DistrictGirlCount.ContainsKey(key))
                    _DistrictGirlCount[key] += dacb.GetGirlCount();
                else
                    _DistrictGirlCount.Add(key, dacb.GetGirlCount());
            }

            return _DistrictGirlCount;
        }

        /// <summary>
        /// 取得里 未分性別人數
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetDistrictNoGenderCount()
        {
            _DistrictNoGenderCount.Clear();

            foreach (DistrictAreaCounterBase dacb in _DistrictAreaCounterList)
            {
                string key = dacb.GetDistrict();
                if (_DistrictNoGenderCount.ContainsKey(key))
                    _DistrictNoGenderCount[key] += dacb.GetNoGenderCount();
                else
                    _DistrictNoGenderCount.Add(key, dacb.GetNoGenderCount());
            }

            return _DistrictNoGenderCount;
        }

        /// <summary>
        /// 取得里 總人數統計
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetDistrictTotalCount()
        {
            _DistrictTotalCount.Clear();
            foreach (DistrictAreaCounterBase dacb in _DistrictAreaCounterList)
            {
                string key = dacb.GetDistrict();
                if (_DistrictTotalCount.ContainsKey(key))
                    _DistrictTotalCount[key] += dacb.GetTotalCount();
                else
                    _DistrictTotalCount.Add(key, dacb.GetTotalCount());
            }

            return _DistrictTotalCount;
        }

        /// <summary>
        /// 取得里鄰統計
        /// </summary>
        /// <param name="District"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public DistrictAreaCounterBase GetDistricAreaCount(string District, string Area)
        {
            DistrictAreaCounterBase dacbEntity = new DistrictAreaCounterBase();
            foreach (DistrictAreaCounterBase dacb in _DistrictAreaCounterList)
            {
                if (dacb.GetDistrict() == District && dacb.GetArea() == Area)
                {
                    dacbEntity = dacb;
                    break;
                }

            }
            return dacbEntity;
        }


        /// <summary>
        /// 取得里鄰名稱
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetDistrictAreName()
        {
            _DistrictAreName.Clear();

            Dictionary<string, DistrictAreaCounterBase> tmpDic = new Dictionary<string, DistrictAreaCounterBase>();
            List<string> tmpStr = new List<string>();
            foreach (DistrictAreaCounterBase sacb in _DistrictAreaCounterList)
                tmpDic.Add(sacb.GetDistrict() + "_" + sacb.GetArea(), sacb);

            foreach (string str in tmpDic.Keys)
                tmpStr.Add(str);

            tmpStr.Sort();

            foreach (string str in tmpStr)
            {
                if (tmpDic.ContainsKey(str))
                {
                    string key = tmpDic[str].GetDistrict();
                    string value = tmpDic[str].GetArea();
                    if (_DistrictAreName.ContainsKey(key))
                    {
                        _DistrictAreName[key].Add(value);
                    }
                    else
                    {
                        List<string> item = new List<string>();
                        item.Add(value);
                        _DistrictAreName.Add(key, item);
                    }
                }
            }

            return _DistrictAreName;
        }

        /// <summary>
        /// 計算里鄰人數
        /// </summary>
        /// <param name="StudentEntityList"></param>
        public List<StudentEntity> CalDistrictAreaCount(List<StudentEntity> _StudentEntityList)
        {
            _DistrictAreaCounterList.Clear();
            // 比對名稱使用
            List<string> tmpCmpName = new List<string>();
            foreach (StudentEntity se in _StudentEntityList)
            {
                string PermanentDistrict = "";
                string PermanentArea = "";
                if (se.PermanentDistrict != null)
                    PermanentDistrict = se.PermanentDistrict.Trim();
                if (se.PermanentArea != null)
                    PermanentArea = se.PermanentArea.Trim();
                string key = PermanentDistrict + PermanentArea;
                if (tmpCmpName.Contains(key))
                {
                    foreach (DistrictAreaCounterBase sacb in _DistrictAreaCounterList)
                    {
                        if (sacb.GetDistrict() == PermanentDistrict && sacb.GetArea() == PermanentArea)
                        {
                            se.Memo += sacb.AddCount(PermanentDistrict, PermanentArea, se.Gender);
                        }
                    }
                }
                else
                {
                    DistrictAreaCounterBase sacb = new DistrictAreaCounterBase();
                    sacb.AddCount(PermanentDistrict, PermanentArea, se.Gender);
                    _DistrictAreaCounterList.Add(sacb);
                }
                tmpCmpName.Add(key);
            }
            return _StudentEntityList;
        }
    }
}
