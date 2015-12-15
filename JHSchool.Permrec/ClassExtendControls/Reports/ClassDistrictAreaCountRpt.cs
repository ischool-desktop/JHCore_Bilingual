using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    class ClassDistrictAreaCountRpt
    {
        List<DAL.StudentEntity> _StudentEntityList;


        public ClassDistrictAreaCountRpt()
        {
            // 取得所選的班級
            List<ClassRecord> selectClassRec = Class.Instance.SelectedList;
            // 取得所選班級學生基本
            _StudentEntityList = DAL.Transfer.GetSelectStudentEntitys(selectClassRec);
            // 取得地址里鄰
            _StudentEntityList = DAL.Transfer.GetClassDistrictArea(_StudentEntityList);

            // 計算里鄰統計
            DAL.DistrictAreaCounter dac = new JHSchool.Permrec.ClassExtendControls.Reports.DAL.DistrictAreaCounter();
            _StudentEntityList = dac.CalDistrictAreaCount(_StudentEntityList);

            List<DAL.StudentEntity> ErrorData = new List<JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentEntity>();
            foreach (DAL.StudentEntity se in _StudentEntityList)
            {
                if (!string.IsNullOrEmpty(se.Memo))
                    ErrorData.Add(se);
            }


            ClassCellReportManger crm = new ClassCellReportManger();
            crm.ProcessClassDistrictAreaCount("里鄰統計表", "里鄰統計", dac, ErrorData);


        }

    }
}
