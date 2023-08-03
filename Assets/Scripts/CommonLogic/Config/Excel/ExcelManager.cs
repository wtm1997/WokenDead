using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLogic
{
    public class ExcelManager
    {
        private readonly Dictionary<Type, ExcelData> _excelDataDic
            = new Dictionary<Type, ExcelData>();

        public void AddExcelData<T>(byte[] rawData) where T : ExcelItemBase, new()
        {
            var type = typeof(T);
            var data = new ExcelData();
            data.Init<T>(rawData);
            _excelDataDic.Add(type, data);
        }

        private ExcelData GetExcelData<T>() where T : ExcelItemBase
        {
            var type = typeof(T);

            if (_excelDataDic.ContainsKey(type))
                return _excelDataDic[type];

            Log.Error($"没有表 {type.Name}");
            return null;
        }

        public bool HasExcelItem<T>(string targetKey) where T : ExcelItemBase
        {
            var excelData = GetExcelData<T>();
            if (excelData != null)
                return excelData.HasExcelItem<T>(targetKey);

            return false;
        }

        public T GetExcelItem<T>(string key) where T : ExcelItemBase
        {
            var excelData = GetExcelData<T>();

            if (excelData != null)
                return excelData.GetExcelItem<T>(key);

            return null;
        }

        public List<ExcelItemBase> GetExcelItems<T>() where T : ExcelItemBase
        {
            var excelData = GetExcelData<T>();

            if (excelData != null)
            {
                return excelData.dict.List;
            }

            return null;
        }

        public void Clear()
        {
            _excelDataDic.Clear();
        }
    }
}