using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JHSchool.Permrec.StudentExtendControls
{
    /// <summary>
    /// 取得地址對照
    /// </summary>
    public class CountyTown
    {

        /// <summary>
        /// 地址三碼
        /// </summary>
        XElement _ElmRoot;

        List<string> _CountyList;
        Dictionary<string, Dictionary<string, string>> _ZipTownMapDict;
        Dictionary<string, KeyValuePair<string, string>> _ZipCodeMapDict;

        public CountyTown()
        {
            _CountyList = new List<string>();
            _ZipTownMapDict = new Dictionary<string, Dictionary<string, string>>();
            _ZipCodeMapDict = new Dictionary<string, KeyValuePair<string, string>>();
            try
            {
                _ElmRoot = XElement.Parse(Properties.Resources.CountyTownBelong);
                if (_ElmRoot != null)
                {
                    foreach (XElement elm in _ElmRoot.Elements("Town"))
                    {
                        string county = elm.Attribute("County").Value;
                        string name = elm.Attribute("Name").Value;
                        string code = elm.Attribute("Code").Value;

                        if (!_CountyList.Contains(county))
                            _CountyList.Add(county);

                        if (!_ZipCodeMapDict.ContainsKey(code))
                            _ZipCodeMapDict.Add(code, new KeyValuePair<string, string>(county,name));                        

                        if (!_ZipTownMapDict.ContainsKey(county))
                            _ZipTownMapDict.Add(county, new Dictionary<string, string>());

                        if (!_ZipTownMapDict[county].ContainsKey(name))
                            _ZipTownMapDict[county].Add(name, code);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 取得縣市
        /// </summary>
        /// <returns></returns>
        public List<string> GetCountyList()
        {
            return _CountyList;
        }

        /// <summary>
        /// 取得區郵區對照
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetTownZipCodeDict(string County)
        {
            if (_ZipTownMapDict.ContainsKey(County))
                return _ZipTownMapDict[County];
            else
                return new Dictionary<string, string>();
        }
        
        /// <summary>
        /// 透過郵地區號取得內容
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetTownCountyNameDict(string Code)
        {
            if (_ZipCodeMapDict.ContainsKey(Code))
                return _ZipCodeMapDict[Code];
            else
                return new KeyValuePair<string, string>();
        }
    }

}
