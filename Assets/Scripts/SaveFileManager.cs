using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    string saveDir;
    string saveText1Path;
    string saveText2Path;
    string saveText3Path;
    string saveText1BackUpPath;

    void Start()
    {
        saveDir = Path.Combine(Application.persistentDataPath, "SaveData");
        saveText1Path = Path.Combine(Application.persistentDataPath, "SaveData", "save1.txt");
        saveText2Path = Path.Combine(Application.persistentDataPath, "SaveData", "save2.txt");
        saveText3Path = Path.Combine(Application.persistentDataPath, "SaveData", "save3.txt");
        saveText1BackUpPath = Path.Combine(Application.persistentDataPath, "SaveData", "save1_backup.txt");

        string text1 = "앙기모띠";
        File.WriteAllText(saveText1Path, text1);
        string text2 = "잉기모링";
        File.WriteAllText(saveText2Path, text2);
        string text3 = "이이이잉";
        File.WriteAllText(saveText3Path, text3);


        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            Debug.Log($"폴더 생성: {saveDir}");
        }
        else
        {
            Debug.Log ($"폴더가 이미 존재합니다.");
        }

        string[] files = Directory.GetFiles(saveDir);
        
        Debug.Log ($"=== 세이브 파일 목록 ===");
        foreach (string file in files)
        {
            Debug.Log($"- {Path.GetFileName(file)}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (File.Exists(saveText1Path))
            {
                
            }
        }
    }
}
