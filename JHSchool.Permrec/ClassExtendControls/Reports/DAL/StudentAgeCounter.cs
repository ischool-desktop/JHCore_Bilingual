using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class StudentAgeCounter
    {
        private Dictionary<string, int> _AgeTotalCountItemDic;
        private Dictionary<string, int> _AgeBoyCountItemDic;
        private Dictionary<string, int> _AgeGirlCountItemDic;
        private Dictionary<string, int> _AgeNoGenderCountItemDic;
        /// <summary>
        /// 工作表項目
        /// </summary>
        public List<string> wsItems;
        public enum AgeCountType { 男生, 女生, 全部, 未分性別 };

        public StudentAgeCounter()
        {
            _AgeTotalCountItemDic = new Dictionary<string, int>();
            _AgeBoyCountItemDic = new Dictionary<string, int>();
            _AgeGirlCountItemDic = new Dictionary<string, int>();
            _AgeNoGenderCountItemDic = new Dictionary<string, int>();
            wsItems = new List<string>();
        }


        /// <summary>
        /// 新增年齡統計項目
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public bool AddAgeCounterItem(string ItemName)
        {
            if (_AgeTotalCountItemDic.ContainsKey(ItemName))
                return false;
            else
            {
                _AgeTotalCountItemDic.Add(ItemName, 0);
                return true;
            }
        }

        /// <summary>
        /// 新增一批年齡統計項目
        /// </summary>
        /// <param name="ItemsName"></param>
        /// <returns></returns>
        public bool AddAgeCounterItems(List<string> ItemsName)
        {
            bool checkName = false;
            string tempName = "";

            // 同名檢查
            ItemsName.Sort();
            foreach (string str in ItemsName)
            {
                if (str == tempName)
                    checkName = true;
                tempName = str;
            }

            // 是否和已有項目同名檢查
            foreach (string str in ItemsName)
                if (_AgeTotalCountItemDic.ContainsKey(str))
                    checkName = true;

            // 當已有名稱全部不做
            if (checkName)
            {
                return false;
            }
            else
            {
                foreach (string str in ItemsName)
                {
                    _AgeTotalCountItemDic.Add(str + "_", 0);
                    _AgeTotalCountItemDic.Add(str + "_1", 0);
                    _AgeTotalCountItemDic.Add(str + "_2", 0);
                    _AgeTotalCountItemDic.Add(str + "_3", 0);
                    _AgeBoyCountItemDic.Add(str + "_", 0);
                    _AgeBoyCountItemDic.Add(str + "_1", 0);
                    _AgeBoyCountItemDic.Add(str + "_2", 0);
                    _AgeBoyCountItemDic.Add(str + "_3", 0);
                    _AgeGirlCountItemDic.Add(str + "_", 0);
                    _AgeGirlCountItemDic.Add(str + "_1", 0);
                    _AgeGirlCountItemDic.Add(str + "_2", 0);
                    _AgeGirlCountItemDic.Add(str + "_3", 0);
                    _AgeNoGenderCountItemDic.Add(str + "_", 0);
                    _AgeNoGenderCountItemDic.Add(str + "_1", 0);
                    _AgeNoGenderCountItemDic.Add(str + "_2", 0);
                    _AgeNoGenderCountItemDic.Add(str + "_3", 0);
                }
                return true;
            }

        }

        /// <summary>
        /// 新增累加至年齡統計項目(回傳是否成功)
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public bool AddAgeCounterItemValueP(string ItemName, string Gender, string GradeYear)
        {
            int gr=0;

            if (int.TryParse(GradeYear, out gr))
            {
                if (gr > 6)
                    gr -= 6;
            }

            ItemName += "_" + gr.ToString ();
            if (_AgeTotalCountItemDic.ContainsKey(ItemName))
            {
                if (Gender == "男")
                    _AgeBoyCountItemDic[ItemName]++;

                if (Gender == "女")
                    _AgeGirlCountItemDic[ItemName]++;

                if (string.IsNullOrEmpty(Gender))
                {
                    _AgeNoGenderCountItemDic[ItemName]++;
                }

                _AgeTotalCountItemDic[ItemName]++;
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// 新增累加至年齡統計項目,會回傳錯誤內容，如果沒有傳""
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="se"></param>
        /// <returns></returns>
        public string AddAgeCounterItemValue(string ItemName, StudentEntity se)
        {
            string Msg = "";

            int gr = 0;

            if (int.TryParse(se.GradeYear, out gr))
            {
                if (gr > 6)
                    gr -= 6;
            }

            ItemName += "_" + gr.ToString();
            if (_AgeTotalCountItemDic.ContainsKey(ItemName))
            {
                if (se.Gender == "男")
                    _AgeBoyCountItemDic[ItemName]++;

                if (se.Gender == "女")
                    _AgeGirlCountItemDic[ItemName]++;

                if (string.IsNullOrEmpty(se.Gender))
                {
                    _AgeNoGenderCountItemDic[ItemName]++;
                    Msg = "沒有性別";
                }
                _AgeTotalCountItemDic[ItemName]++;
            }
            return Msg;
        }

        /// <summary>
        /// 整數相除*100，給百分比顯示用
        /// </summary>
        /// <param name="FirstInt"></param>
        /// <param name="SecondInt"></param>
        /// <param name="RoundRange"></param>
        /// <returns></returns>        
        public decimal IntRoundToDecimalForPercentage(int FirstInt, int SecondInt, int RoundRange)
        {
            decimal returnValue = 0;

            if (FirstInt > 0 && SecondInt > 0)
                returnValue = Math.Round(((decimal)FirstInt / (decimal)SecondInt) * 100, RoundRange, MidpointRounding.AwayFromZero);
            return returnValue;
        }

        /// <summary>
        /// 將年齡統計項目值初始化為0
        /// </summary>
        public void ResetAgeCounterItemValue()
        {
            List<string> items = new List<string>();
            foreach (KeyValuePair<string, int> val in _AgeTotalCountItemDic)
                items.Add(val.Key);

            foreach (string str in items)
            {
                _AgeTotalCountItemDic[str] = 0;
                _AgeBoyCountItemDic[str] = 0;
                _AgeGirlCountItemDic[str] = 0;
                _AgeNoGenderCountItemDic[str] = 0;
            }

        }

        /// <summary>
        /// 將年齡統計項目清空
        /// </summary>
        public void ClearAgeCounterItems()
        {
            _AgeTotalCountItemDic.Clear();
            _AgeBoyCountItemDic.Clear();
            _AgeGirlCountItemDic.Clear();
            _AgeNoGenderCountItemDic.Clear();
        }


        /// <summary>
        /// 取得年齡統計項目與值
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetAllAgeCountItemValue(AgeCountType act)
        {
            Dictionary<string, int> returnDic = null;

            if (act == AgeCountType.全部)
                returnDic = _AgeTotalCountItemDic;

            if (act == AgeCountType.男生)
                returnDic = _AgeBoyCountItemDic;

            if (act == AgeCountType.女生)
                returnDic = _AgeGirlCountItemDic;

            if (act == AgeCountType.未分性別)
                returnDic = _AgeNoGenderCountItemDic;
            return returnDic;
        }


        /// <summary>
        /// 取得某個年齡統計項目的值,找不到回傳-1
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public int GetAgeCounterItemValue(string ItemName, AgeCountType act, string GradeYear)
        {
            int returnValue = -1;
            ItemName += "_" + GradeYear;

            if (act == AgeCountType.全部)
                if (_AgeTotalCountItemDic.ContainsKey(ItemName))
                    returnValue = _AgeTotalCountItemDic[ItemName];

            if (act == AgeCountType.男生)
                if (_AgeBoyCountItemDic.ContainsKey(ItemName))
                    returnValue = _AgeBoyCountItemDic[ItemName];

            if (act == AgeCountType.女生)
                if (_AgeGirlCountItemDic.ContainsKey(ItemName))
                    returnValue = _AgeGirlCountItemDic[ItemName];

            if (act == AgeCountType.未分性別)
                if (_AgeNoGenderCountItemDic.ContainsKey(ItemName))
                    returnValue = _AgeNoGenderCountItemDic[ItemName];

            return returnValue;
        }
    }
}
