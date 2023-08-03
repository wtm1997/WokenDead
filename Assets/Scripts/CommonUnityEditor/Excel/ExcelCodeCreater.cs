#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic;

public class ExcelCodeCreater
{
    public class Data
    {
        public string codeStr;
        public byte[] datas;
    }

    public static Data Create(ExcelTempData excelMediumData)
    {
        if (excelMediumData == null)
            return null;

        //Excel名字
        var excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;
        //Dictionary<字段名称, 字段类型>
        var propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;
        //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
        var allItemValueColumnList = excelMediumData.allItemValueColumnList;
        if (allItemValueColumnList == null || allItemValueColumnList.Count == 0)
            return null;

        //生成代码
        var codeStr = CreateCodeStrByExcelData(excelName,
            propertyNameTypeDic);
        //生成数据
        var datas = CreateDatas(
            propertyNameTypeDic, allItemValueColumnList);

        return new Data()
        {
            codeStr = codeStr,
            datas = datas
        };
    }

    //创建代码，生成数据C#类
    private static string CreateCodeStrByExcelData(string excelName,
                                                   Dictionary<string, string> propertyNameTypeDic)
    {
        //行类名
        var itemClassName = excelName + "ExcelItem";

        //生成类
        var classSource = new StringBuilder();
        classSource.Append("/*Auto Create, Don't Edit !!!*/\n");
        classSource.Append("\n");
        //添加引用
        classSource.Append("using CommonLogic;\n");
        classSource.Append("\n");
        //生成行Class
        classSource.Append("public class " + itemClassName + " : ExcelItemBase\n");
        classSource.Append("{\n");
        //声明所有字段
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append(CreateCodeProperty(item.Key, item.Value));
        }

        classSource.Append("\n");
        //赋值所有字段
        classSource.Append("\tpublic override void OnInit(byte[] rawDatas, ref int index)\n");
        classSource.Append("\t{\n");
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append("\t\t");
            classSource.Append(item.Key);
            classSource.Append(" = ");
            switch (item.Value)
            {
                case "int":
                    classSource.Append("ExcelDataParser.GetInt(rawDatas, ref index);");
                    break;
                case "int[]":
                    classSource.Append("ExcelDataParser.GetIntArr(rawDatas, ref index);");
                    break;
                case "float":
                    classSource.Append("ExcelDataParser.GetFloat(rawDatas, ref index);");
                    break;
                case "string":
                    classSource.Append("ExcelDataParser.GetString(rawDatas, ref index);");
                    break;
                case "string[]":
                    classSource.Append("ExcelDataParser.GetStringArr(rawDatas, ref index);");
                    break;
            }

            classSource.Append("\n");
        }

        classSource.Append("\t}\n");
        classSource.Append("}\n");

        return classSource.ToString();
    }

    //----------

    //声明行数据类字段
    private static string CreateCodeProperty(string name, string type)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        if (name == ExcelItemBase.necessaryId)
            return null;
        //声明
        var propertyStr = "\tpublic " + type + " " + name + ";\n";
        return propertyStr;
    }

    //----------

    //生成数据类
    private static byte[] CreateDatas(Dictionary<string, string> propertyNameTypeDic,
                                      List<Dictionary<string, string>> allItemValueColumnList)
    {
        var rawDataList = new List<byte>();

        var len = allItemValueColumnList.Count;
        WriteInt(rawDataList, len);

        foreach (var dict in allItemValueColumnList)
        {
            foreach (var kv in dict)
            {
                var name = kv.Key;
                var type = propertyNameTypeDic[name];
                var value = kv.Value;

                switch (type)
                {
                    case "int":
                        var valueInt = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                        WriteInt(rawDataList, valueInt);
                        break;
                    case "int[]":
                        WriteIntArr(rawDataList, value);
                        break;
                    case "float":
                        var valueFloat = string.IsNullOrEmpty(value) ? 0f : float.Parse(value);
                        WriteFloat(rawDataList, valueFloat);
                        break;
                    case "string":
                        WriteString(rawDataList, value);
                        break;
                    case "string[]":
                        WriteStringArr(rawDataList, value);
                        break;
                }
            }
        }

        return rawDataList.ToArray();
    }

    static void WriteInt(List<byte> rawDataList, int value)
    {
        var bytes = BitConverter.GetBytes(value);
        rawDataList.AddRange(bytes);
    }

    static void WriteIntArr(List<byte> rawDataList, string value)
    {
        var arrData = Array.Empty<string>();
        var len = 0;

        if (!string.IsNullOrEmpty(value))
        {
            arrData = value.Replace("\n", "").Split(',');
            len = arrData.Length;
        }

        WriteInt(rawDataList, len);
        for (var i = 0; i < len; i++)
        {
            WriteInt(rawDataList, int.Parse(arrData[i]));
        }
    }

    static void WriteFloat(List<byte> rawDataList, float value)
    {
        var bytes = BitConverter.GetBytes(value);
        rawDataList.AddRange(bytes);
    }

    static void WriteString(List<byte> rawDataList, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            value = string.Empty;
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        var len = bytes.Length;
        WriteInt(rawDataList, len);
        rawDataList.AddRange(bytes);
    }

    static void WriteStringArr(List<byte> rawDataList, string value)
    {
        var arrData = Array.Empty<string>();
        var len = 0;

        if (!string.IsNullOrEmpty(value))
        {
            arrData = value.Replace("\n", "").Split(',');
            len = arrData.Length;
        }

        WriteInt(rawDataList, len);
        for (var i = 0; i < len; i++)
        {
            WriteString(rawDataList, arrData[i]);
        }
    }
}

#endif
