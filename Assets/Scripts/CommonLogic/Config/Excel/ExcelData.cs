using System.Collections.Generic;

namespace CommonLogic
{
    public class ExcelData
    {
        public readonly ListDictionary<string, ExcelItemBase> dict
            = new ListDictionary<string, ExcelItemBase>();

        public int Count => dict.Count;

        public void Init<T>(byte[] rawData) where T : ExcelItemBase, new()
        {
            var index = 0;
            var len = ExcelDataParser.GetInt(rawData, ref index);
            for (var i = 0; i < len; i++)
            {
                var item = new T();
                item.OnInit(rawData, ref index);
                AddExcelItem(item);
            }
        }

        public void AddExcelItem(ExcelItemBase excelItem)
        {
            dict.Add(excelItem.SKey, excelItem);
        }

        public bool HasExcelItem<T>(string targetKey) where T : ExcelItemBase
        {
            return dict.ContainsKey(targetKey);
        }

        public T GetExcelItem<T>(string targetKey) where T : ExcelItemBase
        {
            if (string.IsNullOrEmpty(targetKey))
            {
                Log.Error($"表: {typeof(T).Name} key不能为 Null");
                return null;
            }

            if (dict.ContainsKey(targetKey))
            {
                dict.TryGetValue(targetKey, out var excelItem);
                return (T)excelItem;
            }

            Log.Error($"表: {typeof(T).Name} 没有字段: {targetKey}");
            return null;
        }
    }
}
