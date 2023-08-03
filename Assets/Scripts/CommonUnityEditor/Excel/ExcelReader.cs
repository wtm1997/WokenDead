#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ExcelDataReader;

public static class ExcelReader
{
    //Excel文件夹路径
    public static string excelFilePath = Application.dataPath + "/../../Config/";

    //自动生成的C#文件路径
    public static string excelCodePath = Application.dataPath + "/Scripts/Battle/Generator/Config/";
    public static string excelGameCodePath = Application.dataPath + "/Scripts/Game/Generator/Config/";

    public static string excelDataPath = Application.dataPath + "/GameRes/TextAssets/ConfigData/";
    public static string excelGameDataPath = Application.dataPath + "/GameRes/TextAssets/ConfigData/";

    //Excel第2行对应字段名称 第3行是字段名称描述
    const int Column_Name = 2;

    //Excel第4行对应字段类型
    const int Column_Type = 4;

    //Excel第5行以后都是实际内容
    const int Column_Value = 5;

    #region 创建Excel相对应的C#代码
    //创建Excel对应的C#类
    public static void ReadAllExcelToCode()
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        var excelFileFullPaths = Directory.GetFiles(excelFilePath, "*.xlsx");

        if (excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }

        //遍历所有Excel，创建C#类
        foreach (var excelFileFullPath in excelFileFullPaths)
        {
            if (excelFileFullPath.Contains("~"))
                continue;
            if (excelFileFullPath.Contains("Blockword")) //TODO 暂时不导出
                continue;
            ReadOneExcelToCode(excelFileFullPath);
        }
    }

    //创建Excel对应的C#类
    public static void ReadOneExcelToCode(string excelFileFullPath)
    {
        //解析Excel获取中间数据
        var excelMediumDatas = CreateClassCodeByExcelPath(excelFileFullPath);
        foreach (var excelMediumData in excelMediumDatas)
        {
            if (excelMediumData != null)
            {
                var excelName = excelMediumData.excelName;

                var data = ExcelCodeCreater.Create(excelMediumData);
                if (data != null)
                {
                    //gen Code
                    var codePath = excelName.Contains("Game") ? excelGameCodePath : excelCodePath;
                    WriteCodeStrToSave(codePath, excelName + "ExcelItem", data.codeStr);

                    //gen Datas
                    var dataPath = excelName.Contains("Game") ? excelGameDataPath : excelDataPath;
                    WriteDatasToSave(dataPath, excelName + "ExcelData", data.datas);

                    //sync Server
                    if (excelName.Contains("Game"))
                    {
                        var clientPath = dataPath + "/";
                        var serverPath = "../Server/Game/bin/Debug/net6.0/ConfigData/";
                        var fileName = excelName + "ExcelData.bytes";
                        File.Copy(clientPath + fileName, serverPath + fileName, true);
                    }

                    continue;
                }
            }

            //生成失败
            Debug.LogError("Auto Create Excel Scripts Fail : " +
                           (excelMediumData == null ? "" : excelMediumData.excelName));
        }

        UnityEditor.AssetDatabase.Refresh();
    }
    #endregion

    #region 读写操作
    //解析Excel，创建中间数据
    private static List<ExcelTempData> CreateClassCodeByExcelPath(string excelFileFullPath)
    {
        if (string.IsNullOrEmpty(excelFileFullPath))
            return null;

        excelFileFullPath = excelFileFullPath.Replace("\\", "/");

        using var stream =
            new FileStream(excelFileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        //解析Excel
        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //无效Excel
        if (excelReader == null)
        {
            Debug.LogError("Invalid excel ：" + excelFileFullPath);
            return null;
        }

        var excelMediumDatas = new List<ExcelTempData>();

        var excelTempData = CreateClassCodeByExcelPath(excelReader);
        if (excelTempData != null) excelMediumDatas.Add(excelTempData);

        while (excelReader.NextResult())
        {
            excelTempData = CreateClassCodeByExcelPath(excelReader);
            if (excelTempData != null) excelMediumDatas.Add(excelTempData);
        }

        return excelMediumDatas;
    }

    private static ExcelTempData CreateClassCodeByExcelPath(IExcelDataReader excelReader)
    {
        //<数据名称,数据类型>
        KeyValuePair<string, string>[] propertyNameTypes = null;
        //List<KeyValuePair<数据名称, 单元格数据值>[]>，所有数据值，按行记录
        var allItemValueColumnList = new List<Dictionary<string, string>>();

        //每行数据数量
        var propertyCount = 0;
        //当前遍历行，从1开始
        var curColumnIndex = 1;
        //开始读取，按行遍历
        while (excelReader.Read())
        {
            if (excelReader.FieldCount == 0)
                continue;

            //读取一行的数据
            var datas = new string[excelReader.FieldCount];

            for (var j = 0; j < excelReader.FieldCount; ++j)
            {
                //赋值一行的每一个单元格数据
                var valueObj = excelReader.GetValue(j);
                datas[j] = valueObj?.ToString();
            }

            //空行/第一个单元格为空，视为无效数据
            if (datas.Length == 0 || string.IsNullOrEmpty(datas[0]))
            {
                curColumnIndex++;
                continue;
            }

            //数据行
            if (curColumnIndex >= Column_Value)
            {
                //数据无效
                if (propertyCount <= 0)
                    return null;

                var itemDic = new Dictionary<string, string>(propertyCount);
                //遍历一行里的每个单元格数据
                for (var j = 0; j < propertyCount; j++)
                {
                    //判断长度
                    if (propertyNameTypes == null || propertyNameTypes[j].Key == null) continue;
                    if (j < datas.Length)
                        itemDic[propertyNameTypes[j].Key] = datas[j];
                    else
                        itemDic[propertyNameTypes[j].Key] = null;
                }

                allItemValueColumnList.Add(itemDic);
            }
            //数据名称行
            else if (curColumnIndex == Column_Name)
            {
                //确定每行的数据数量
                propertyCount += datas.Length;
                if (propertyCount <= 0)
                    return null;
                //记录数据名称
                propertyNameTypes = new KeyValuePair<string, string>[propertyCount];
                for (var i = 0; i < propertyCount; i++)
                {
                    if (datas[i] != null)
                        propertyNameTypes[i] = new KeyValuePair<string, string>(datas[i], null);
                }
            }
            //数据类型行
            else if (curColumnIndex == Column_Type)
            {
                //数据类型数量少于指定数量，数据无效
                if (propertyCount <= 0 || datas.Length < propertyCount)
                    return null;
                //记录数据名称及类型
                for (var i = 0; i < propertyCount; i++)
                {
                    propertyNameTypes[i] = new KeyValuePair<string, string>(propertyNameTypes[i].Key, datas[i]);
                }
            }

            curColumnIndex++;
        }

        if (propertyNameTypes != null
            && (propertyNameTypes.Length == 0 || allItemValueColumnList.Count == 0))
            return null;

        var excelMediumData = new ExcelTempData();
        //类名
        excelMediumData.excelName = excelReader.Name;
        //Dictionary<数据名称,数据类型>
        excelMediumData.propertyNameTypeDic = new Dictionary<string, string>();
        //转换存储格式
        for (var i = 0; i < propertyCount; i++)
        {
            if (propertyNameTypes != null && propertyNameTypes[i].Key == null)
                continue;
            //数据名重复，数据无效
            if (excelMediumData.propertyNameTypeDic.ContainsKey(propertyNameTypes[i].Key))
                return null;
            excelMediumData.propertyNameTypeDic.Add(propertyNameTypes[i].Key, propertyNameTypes[i].Value);
        }

        excelMediumData.allItemValueColumnList = allItemValueColumnList;
        return excelMediumData;
    }

    //写文件
    private static bool WriteCodeStrToSave(string writeFilePath, string codeFileName, string classCodeStr)
    {
        if (string.IsNullOrEmpty(codeFileName) || string.IsNullOrEmpty(classCodeStr))
            return false;
        //检查导出路径
        if (!Directory.Exists(writeFilePath))
            Directory.CreateDirectory(writeFilePath);
        //写文件，生成CS类文件
        File.WriteAllText(writeFilePath + "/" + codeFileName + ".cs", classCodeStr);
        // UnityEditor.AssetDatabase.Refresh();
        return true;
    }

    private static bool WriteDatasToSave(string writeFilePath, string codeFileName, byte[] rawDatas)
    {
        if (string.IsNullOrEmpty(codeFileName) || rawDatas.Length == 0)
            return false;
        //检查导出路径
        if (!Directory.Exists(writeFilePath))
            Directory.CreateDirectory(writeFilePath);
        //写文件，生成CS类文件
        File.WriteAllBytes(writeFilePath + "/" + codeFileName + ".bytes", rawDatas);
        // UnityEditor.AssetDatabase.Refresh();
        return true;
    }
    #endregion

}

//Excel中间数据
public class ExcelTempData
{
    //Excel名字
    public string excelName;

    //Dictionary<字段名称, 字段类型>，记录类的所有字段及其类型
    public Dictionary<string, string> propertyNameTypeDic;

    //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
    public List<Dictionary<string, string>> allItemValueColumnList;
}

#endif
