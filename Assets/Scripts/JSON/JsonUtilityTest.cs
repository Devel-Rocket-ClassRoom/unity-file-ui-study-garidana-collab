using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    // JsonUtility는 Dictionary 지원 안함
    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        { "stage1", 100 },
        { "stage2", 200 },
    };
}

public class JsonUtilityTest : MonoBehaviour
{
        /// <summary>
        /// Save player information to a file and load from the file.
        /// </summary>
        /// <remarks>
        /// Save: If the '1' key is pressed, a <see cref="PlayerInfo"/> object is created and saved to a file.
        /// Load: If the '2' key is pressed, the contents of the file are read and used to overwrite a <see cref="PlayerInfo"/> object.
        /// </remarks>
    private void Update()
    {
        // Save
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerInfo obj = new PlayerInfo
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
                );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "player.json"
                );
            
            string json = JsonUtility.ToJson(obj, prettyPrint: true); // json 문자열로 변환
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
                );

            string path = Path.Combine(
                pathFolder,
                "player.json"
                );

            string json = File.ReadAllText(path);
            //PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);  //// 개체를 직접 생성하는 메서드
            PlayerInfo obj = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, obj);                   // 개체를 덮어쓰는 메서드

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.lives} / {obj.health} / {obj.position}");
        }
    }
}