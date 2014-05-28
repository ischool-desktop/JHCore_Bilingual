using IRewriteAPI_JH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    class BasicTeacherItem_API : ITeacherDatailtemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<TeacherItem>();
        }
    }
}
