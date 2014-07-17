using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    class Permissions
    {
        public static string 班級點名單_雙語部 { get { return "雙語部.BasicInformation.ClubPointForm"; } }
        public static bool 班級點名單_雙語部權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[班級點名單_雙語部].Executable;
            }
        }

        public static string 缺曠週報表_依假別_雙語部 { get { return "雙語部.BasicInformation.WARCByAbsence"; } }
        public static bool 缺曠週報表_依假別_雙語部權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[缺曠週報表_依假別_雙語部].Executable;
            }
        }
    }
}
