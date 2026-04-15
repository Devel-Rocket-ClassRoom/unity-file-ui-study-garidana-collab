using System.IO;
using Unity.VisualScripting;
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
        // 1-1 세이브 폴더 준비
        saveDir = Path.Combine(Application.persistentDataPath, "SaveData");
        saveText1Path = Path.Combine(Application.persistentDataPath, "SaveData", "save1.txt");
        saveText2Path = Path.Combine(Application.persistentDataPath, "SaveData", "save2.txt");
        saveText3Path = Path.Combine(Application.persistentDataPath, "SaveData", "save3.txt");
        // 복사본 파일 경로
        saveText1BackUpPath = Path.Combine(Application.persistentDataPath, "SaveData", "save1_backup.txt");

        // 1-1 파일 생성
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            Debug.Log($"폴더 생성: {saveDir}");
        }
        else
        {
            Debug.Log ($"폴더가 이미 존재합니다.");
        }

        string text1 = "앙기모띠";
        File.WriteAllText(saveText1Path, text1);
        string text2 = "잉기모링";
        File.WriteAllText(saveText2Path, text2);
        string text3 = "이이이잉";
        File.WriteAllText(saveText3Path, text3);

        string[] files = Directory.GetFiles(saveDir);
        
        // 1-2 파일 목록 출력
        Debug.Log ($"=== 세이브 파일 목록 ===");
        foreach (string file in files)
        {
            Debug.Log($"- {Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }
    }

    void Update()
    {
        // 1-3 파일 복사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (File.Exists(saveText1BackUpPath))
            {
                Debug.Log($"파일이 이미 존재합니다.");
            }
            else
            {
                File.Copy(saveText1Path, saveText1BackUpPath, true);
                Debug.Log($"{Path.GetFileName(saveText1Path)} -> {Path.GetFileName(saveText1BackUpPath)} 복사완료");
            }
        }   
        
        // 1-4 파일 삭제
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!File.Exists(saveText3Path))
            {
                Debug.Log($"파일이 존재하지 않습니다.");
            }
            else
            {
                File.Delete(saveText3Path);
                Debug.Log($"{Path.GetFileName(saveText3Path)} 삭제 완료");
            }
        }

        // 1-5 최종 확인 (작업 후 파일 목록 출력)
        if (Input.GetKeyDown(KeyCode.L))
        {
            string[] files = Directory.GetFiles(saveDir);

            Debug.Log ($"=== 세이브 파일 목록 ===");
            foreach (string file in files)
            {
                Debug.Log($"- {Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }

        }
        
    }
}
