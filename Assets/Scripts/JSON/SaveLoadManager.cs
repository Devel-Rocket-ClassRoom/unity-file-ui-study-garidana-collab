using UnityEngine;
using SaveDataVC = SaveDataV4;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,       // (.json)
        Encrypted   // (.dat)
    }

    public static SaveMode Mode { get; set; } = SaveMode.Text;

    public static int SaveDataVersion { get; } = 4;
    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";
    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    static SaveLoadManager()
    {
        if (!Load())
        {
            Debug.Log("세이브 파일 로드 실패띠음");
        }
    }

    private static string GetSavefilePath(int slot)
    {
        return GetSavefilePath(slot, Mode);
    }

    private static string GetSavefilePath(int slot, SaveMode mode)
    {
        var ext = Mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,    // 추상 클래스의 실제 객체에 맞게 직렬화/역질렬화 하기 위함
    };



    public static bool Load(int slot = 0) => Load(slot, Mode);
    public static bool Save(int slot = 0) => Save(slot, Mode);



    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSavefilePath(slot, mode);

            switch (Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }

            return true;
        }
        catch (System.Exception)
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        string path = GetSavefilePath(slot, mode);

        if (!File.Exists(path))
        {
            Debug.LogError("파일 없음");
            return Save();
        }

        try
        {
            string json = string.Empty;
            switch (Mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);


            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }
            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError($"Load 예외");
            return false;
        }
    }
}




// using UnityEngine;
// using SaveDataVC = SaveDataV3;
// using Newtonsoft.Json;
// using System.IO;

// public class SaveLoadManager
// {

//     public enum SaveMode
//     {
//         Text,      // Json 텍스트일때는 .json
//         Encrypted,   // AES 암호화 바이너리느 .dat
//     }

//     public static SaveMode Mode { get; set; } = SaveMode.Text;







//     private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";
//     private static readonly string[] SaveFileNames =
//     {
//         "SaveAuto",
//         "Save1",
//         "Save2",
//         "Save3",
//     };
//     public static int SaveDataVersion { get; } = 3;
//     public static SaveDataVC Data { get; set; } = new();

//     private static string GetSaveFilePath(int slot)
//     {
//         return GetSaveFilePath(slot, Mode);
//     }

//     public static string GetSaveFilePath(int slot, SaveMode saveMode)
//     {   // 확장자 구하기
//         var ext = Mode == SaveMode.Text ? ".json" : ".dat";
//         return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
//     }

//     private static JsonSerializerSettings settings = new()
//     {
//         Formatting = Formatting.Indented,
//         TypeNameHandling = TypeNameHandling.All,

//     };


//     public static bool Save(int slot = 0, SaveMode mode)
//     {
//         if (Data == null || slot < 0 || slot >= SaveFileNames.Length) return false;
//         try
//         {
//             if (!Directory.Exists(SaveDirectory))
//             {
//                 Directory.CreateDirectory(SaveDirectory);
//             }
//             string path = GetSaveFilePath(0, mode);
//             var json = JsonConvert.SerializeObject(Data, settings);
//             switch (mode)
//             {
//                 case SaveMode.Text:
//                     File.WriteAllText(path, json)
//                     break;
//                 case SaveMode.Encrypted:
//                     File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
//                     break;
//             }
//             File.WriteAllBytes(path,json);

//             return true;
//         }
//         catch
//         {
//             Debug.LogError("Save 예외");
//             return false;
//         }

//     }

//     public static bool Load(int slot = 0)
//     {
//         if (slot < 0 || slot >= SaveFileNames.Length) return false;
//         string path = Path.Combine(SaveDirectory, SaveFileNames[slot]);
//         if (!File.Exists(path))
//         {
//             return false;
//         }
//         try
//         {

//             byte[] json = File.ReadAllBytes(path);
//             string decryptedJson = CryptoUtil.Decrypt(json);
//             var saveData = JsonConvert.DeserializeObject<SaveData>(decryptedJson, settings);
//             while (saveData.Version < SaveDataVersion)
//             {
//                 Debug.Log(saveData.Version);
//                 saveData = saveData.VersionUp();
//                 Debug.Log(saveData.Version);
//             }
//             Data = saveData as SaveDataVC;
//             return true;
//         }
//         catch
//         {
//             Debug.LogError("Load 예외");
//             return false;
//         }
//     }
// }

