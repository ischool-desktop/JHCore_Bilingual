using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInformation
{
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        static public void Main()
        {
            //覆蓋 - 學生基本資料項目
            FISCA.InteractionService.RegisterAPI<JHSchool.API.DetailItemAPI>(new BasicItem_Test());

            //啟動更新事件
            EventHandler eh = FISCA.InteractionService.PublishEvent("20140424_A001");
            eh(null, EventArgs.Empty);
        }
    }
}
