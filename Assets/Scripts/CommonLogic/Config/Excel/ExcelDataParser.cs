using System;
using System.Text;

namespace CommonLogic
{
    public static class ExcelDataParser
    {
        public static int GetInt(byte[] rawDatas, ref int index)
        {
            var value = BitConverter.ToInt32(rawDatas, index);
            index += sizeof(int);
            return value;
        }

        public static float GetFloat(byte[] rawDatas, ref int index)
        {
            var value = BitConverter.ToSingle(rawDatas, index);
            index += sizeof(float);
            return value;
        }

        public static int[] GetIntArr(byte[] rawDatas, ref int index)
        {
            var len = GetInt(rawDatas, ref index);
            if (len == 0)
            {
                return Array.Empty<int>();
            }

            var value = new int[len];
            for (var i = 0; i < len; i++)
            {
                var valueTemp = GetInt(rawDatas, ref index);
                value[i] = valueTemp;
            }

            return value;
        }

        public static string GetString(byte[] rawDatas, ref int index)
        {
            var len = GetInt(rawDatas, ref index);
            if (len == 0)
            {
                return string.Empty;
            }

            var str = Encoding.UTF8.GetString(rawDatas, index, len);
            index += len;

            str = str.Replace("\\n", "\n");
            return str;
        }

        public static string[] GetStringArr(byte[] rawDatas, ref int index)
        {
            var len = GetInt(rawDatas, ref index);
            if (len == 0)
            {
                return Array.Empty<string>();
            }

            var value = new string[len];
            for (var i = 0; i < len; i++)
            {
                var valueTemp = GetString(rawDatas, ref index);
                value[i] = valueTemp;
            }

            return value;
        }
    }
}