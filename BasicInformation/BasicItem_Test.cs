using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    class BasicItem_Test : JHSchool.API.DetailItemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<BaseInfoPalmerwormItem_double>();
        }
    }
}
