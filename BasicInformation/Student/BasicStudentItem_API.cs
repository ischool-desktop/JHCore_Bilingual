using IRewriteAPI_JH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    class BasicStudentItem_API : IStudentDetailItemAPI
    {
        public FISCA.Presentation.IDetailBulider CreateBasicInfo()
        {
            return new FISCA.Presentation.DetailBulider<StudentItem>();
        }
    }
}
