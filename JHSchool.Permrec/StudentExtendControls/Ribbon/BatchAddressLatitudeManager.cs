using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Data;
using FISCA.DSAUtil;

namespace JHSchool.Permrec.StudentExtendControls.Ribbon
{
    class BatchAddressLatitudeManager
    {
        public enum AddressTye{Permanent,Mailing,Address1}
        private AddressTye _AddressType;        
        private Dictionary<string, JHAddressRecord> _StudAddressRecDic;

        public BatchAddressLatitudeManager(List<string> StudIDList,AddressTye at)
        {
            _AddressType = at;
            _StudAddressRecDic = new Dictionary<string, JHAddressRecord>();

            foreach (JHAddressRecord addRec in JHAddress.SelectByStudentIDs(StudIDList))
            {
                if (!_StudAddressRecDic.ContainsKey(addRec.RefStudentID))
                    _StudAddressRecDic.Add(addRec.RefStudentID, addRec);            
            }

            foreach (KeyValuePair<string, JHAddressRecord> studAddRec in _StudAddressRecDic)
            {
                string Address = "", latitude = "", longitude="";

                if (_AddressType == AddressTye.Permanent)
                    Address = studAddRec.Value.Permanent.County + studAddRec.Value.Permanent.Town + studAddRec.Value.Permanent.District + studAddRec.Value.Permanent.Area + studAddRec.Value.Permanent.Detail;

                if(_AddressType == AddressTye.Mailing )
                    Address = studAddRec.Value.Mailing.County + studAddRec.Value.Mailing.Town + studAddRec.Value.Mailing.District + studAddRec.Value.Mailing.Area + studAddRec.Value.Mailing.Detail;

                if(_AddressType == AddressTye.Address1 )
                    Address = studAddRec.Value.Address1.County + studAddRec.Value.Address1.Town + studAddRec.Value.Address1.District + studAddRec.Value.Address1.Area + studAddRec.Value.Address1.Detail;

                try
                {
                    DSXmlHelper h = new DSXmlHelper("Request");
                    h.AddText(".", Address);

                    DSResponse rsp = FISCA.Authentication.DSAServices.CallService("SmartSchool.Common.QueryCoordinates", new DSRequest(h));
                    h = rsp.GetContent();
                    if (h.GetElement("Error") == null)
                    {
                        latitude = h.GetText("Latitude");
                        longitude = h.GetText("Longitude");

                    }
                }
                catch (Exception ex)
                {
                }


                // 回寫經緯度

                if (_AddressType == AddressTye.Permanent)
                {
                    studAddRec.Value.Permanent.Latitude = latitude;
                    studAddRec.Value.Permanent.Longitude = longitude;
                }

                if (_AddressType == AddressTye.Mailing )
                {
                    studAddRec.Value.Mailing.Latitude = latitude;
                    studAddRec.Value.Mailing.Longitude = longitude;
                }

                if (_AddressType == AddressTye.Address1 )
                {
                    studAddRec.Value.Address1.Latitude = latitude;
                    studAddRec.Value.Address1.Longitude = longitude;
                }
            }

            // 更新 DAL
            List<JHAddressRecord> addRecList = new List<JHAddressRecord>();
            foreach (JHAddressRecord addrec in _StudAddressRecDic.Values)
                addRecList.Add(addrec);

            JHAddress.Update(addRecList);
        }
        
    }
}
