using System;
using System.Text;

namespace CommonLogic
{
    //所有数据第一列一定要是 SKey
    public abstract class ExcelItemBase
    {
        public static string necessaryId = "SKey";
        public string SKey;

        public abstract void OnInit(byte[] rawDatas, ref int index);
    }
}