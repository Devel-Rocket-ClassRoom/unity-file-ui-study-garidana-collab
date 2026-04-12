using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KeyValueSystem : MonoBehaviour
{
    string settingsDir;
    string settingsPath;

    Dictionary <string, string> settingDic;

    string settingsTextContent = "master_volume=80\nbgm_volume=70\nsfx_volume=90\nlanguage=kr\nshow_damage=true";
    // string masterVol = "master_volume=80";
    // string bgmVol = "bgm_volume=70";
    // string sfxVol = "sfx_volume=90";
    // string lang = "language=kr";
    // string showDmg = "show_damage=true";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsDir = Path.Combine(Application.persistentDataPath, "SettingData");
        settingsPath = Path.Combine(Application.persistentDataPath, "SettingData", "settings.cfg");
    }

    // Update is called once per frame
    void Update()
    {
        // 폴더 SettingsData 생성 및 내부에 settings.cfg 파일 생성 (파일이 없을 시) S
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Directory.Exists(settingsDir))
            {
                Directory.CreateDirectory(settingsDir);
                Debug.Log($"폴더 생성됨 : {settingsDir}");
                
                if (!File.Exists(settingsPath))
                {
                    // File.Create(settingsPath);
                    // File.WriteAllText하면 경로에 알아서 파일을 생성하고 내용을 씀
                    File.WriteAllText(settingsPath, settingsTextContent);
                    Debug.Log($"파일 생성및 쓰기 완료 : {settingsPath}");
                }
                else
                {
                    Debug.Log($"파일이 이미 존재합니다 : {settingsPath}");
                    Debug.Log($"이미 생성된 파일 : {settingsPath}\n내용 : {settingsTextContent}");
                }
            }
            else
            {
                Debug.Log($"폴더가 이미 존재합니다.");
            }

            Debug.Log ($"--- 변경 전 ---");
            Debug.Log ($"{settingsTextContent}");
        }
        // 파일 읽기 및 덮어쓰기 R

        // 에러 사항 : StreamReader가 열려있는 채로 StreamWriter 열려고 하면 에러가 남
        // 이미 사용중인 파일이라 충돌 발생함.
        if (Input.GetKeyDown(KeyCode.R))
        {
            settingDic = new();
            // settingsPath 내용물 읽는 SteamReader
            using (StreamReader sr = new(settingsPath))
            {
                string content;
                while ((content = sr.ReadLine()) != null)
                {
                    // = 으로 Split해서 settingsDic (Dictionary에 파싱해서 저장)
                    string[] parts = content.Split('=');
                    if (parts.Length == 2)
                    {
                        settingDic.Add(parts[0].Trim(), parts[1].Trim());
                    }
                }
                
            }
            // Dictionary 내부 bgm과 language 값 조정
            settingDic["bgm_volume"] = "50";
            settingDic["language"]  = "en";
            using (StreamWriter sw = new(settingsPath))
            {
                foreach (string key in settingDic.Keys)
                {
                    sw.WriteLine($"{key}={settingDic[key]}");
                }
                Debug.Log ($"Dictionary 수정사항 덮어쓰기 완료");
            }
            settingsTextContent = File.ReadAllText(settingsPath);
            Debug.Log ($"--- 최종 파일 내용 ---");
            Debug.Log ($"{settingsTextContent}");
        }
    }
}
