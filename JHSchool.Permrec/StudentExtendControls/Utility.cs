using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.StudentExtendControls
{
    class Utility
    {
        /// <summary>
        /// 稱謂預設資料
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRelationshipList()
        {
            List<string> retVal = new List<string>();
            string item = "父女,父子,母女,母子,伯叔姑姪甥,姊妹,祖孫";
//            string item ="父,母,祖父,祖母,外公,外婆,伯,叔,舅,姑,姨,伯母,嬸,舅媽,姑丈,姨丈,兄,姊,弟,妹,堂兄,堂姊,堂弟,堂妹,表兄,表姊,表弟,表妹,養父,養母,院長";
            retVal.AddRange(item.Split(',').ToArray());
            return retVal;
        }

        /// <summary>
        /// 職業預設資料
        /// </summary>
        /// <returns></returns>
        public static List<string> GetJobList()
        {
            List<string> retVal = new List<string>();
            string item = "工,商,軍,公,教,警,醫,農,漁,林,服務,金融,自由,家管,退休,無";
            retVal.AddRange(item.Split(',').ToArray());
            return retVal;
        }

        /// <summary>
        /// 國籍預設資料
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNationalityList()
        {
            List<string> retVal = new List<string>();
            string item = "中華民國,中華人民共合國,孟加拉,緬甸,印尼,日本,韓國,馬來西亞,菲律賓,新加坡,泰國,越南,汶萊,澳大利亞,紐西蘭,埃及,南非,法國,義大利,瑞典,英國,德國,加拿大,哥斯大黎加,瓜地馬拉,美國,阿根廷,巴西,哥倫比亞,巴拉圭,烏拉圭,其他";
            retVal.AddRange(item.Split(',').ToArray());
            return retVal;
        }

        /// <summary>
        /// 最高學歷預設資料
        /// </summary>
        /// <returns></returns>
        public static List<string> GetEducationDegreeList()
        {
            List<string> retVal = new List<string>();
            string item = "無,國小,國中,高中,專科,大學,碩士,博士,其它";            
            retVal.AddRange(item.Split(',').ToArray());
            return retVal;
        }
    }
}
