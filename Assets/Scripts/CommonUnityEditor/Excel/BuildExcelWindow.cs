#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class BuildExcelEditor : Editor
{
}

public class BuildExcelWindow : EditorWindow
{
    private Vector2 scrollPosition = Vector2.zero;

    private readonly List<string> fileNameList = new List<string>();
    private readonly List<string> filePathList = new List<string>();

    [MenuItem("Tools/Excel 数据导出")]
    public static void ShowExcelWindow()
    {
        GetWindow(typeof(BuildExcelWindow));
    }

    private void Awake()
    {
        titleContent.text = "Excel Editor";
    }

    private void OnEnable()
    {
        GetExcelFile();
    }

    private void OnDisable()
    {
        fileNameList.Clear();
        filePathList.Clear();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate All", GUILayout.Width(200), GUILayout.Height(30)))
        {
            SelectExcelToCodeByIndex(-1);
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition,
            GUILayout.Width(position.width), GUILayout.Height(position.height));

        //创建C#脚本代码
        GUILayout.Space(10);
        GUILayout.Label("Generate C# Script");
        for (var i = 0; i < fileNameList.Count; i++)
        {
            if (GUILayout.Button(fileNameList[i], GUILayout.Width(200), GUILayout.Height(18)))
            {
                SelectExcelToCodeByIndex(i);
            }
        }
        GUILayout.EndScrollView();
    }

    //读取指定路径下的Excel文件名
    private void GetExcelFile()
    {
        fileNameList.Clear();
        filePathList.Clear();

        if (!Directory.Exists(ExcelReader.excelFilePath))
        {
            return;
        }
        var excelFileFullPaths = Directory.GetFiles(ExcelReader.excelFilePath, "*.xlsx");

        if (excelFileFullPaths.Length == 0)
        {
            return;
        }

        foreach (var fullPath in excelFileFullPaths)
        {
            if (fullPath.Contains("~"))
                continue;
            filePathList.Add(fullPath);
        }

        foreach (var fileName in filePathList
            .Select(t => t.Split('/').LastOrDefault()))
        {
            fileNameList.Add(fileName);
        }
    }

    //自动创建C#脚本
    private void SelectExcelToCodeByIndex(int index)
    {
        if (index >= 0 && index < filePathList.Count)
        {
            var fullPath = filePathList[index];
            ExcelReader.ReadOneExcelToCode(fullPath);
        }
        else
        {
            ExcelReader.ReadAllExcelToCode();
        }
    }
}
#endif
