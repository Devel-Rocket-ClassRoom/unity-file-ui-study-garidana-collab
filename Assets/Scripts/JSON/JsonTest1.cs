using Newtonsoft.Json; // Json관련 메서드 사용 가능 using 문
using System;
using System.IO;
using UnityEngine;

// [Serializable] 어트리뷰트가 붙은 것만 직렬화 역직렬화 지원
[Serializable] // 이게 있어야 SerializeObject, DeserializeObject 사용가능
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    // [JsonConverter(typeof(Vector3Converter))]

    public Vector3 playerPosition;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}

public class JsonTest1 : MonoBehaviour
{

    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new();
        jsonSetting.Converters.Add(new Vector3Converter());
    }
    private void Update()
    {
        // Save
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState obj = new PlayerState
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                playerPosition = new Vector3(3f, 4f, 5f)
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
                "player2.json"
                );

            // 직렬화 메서드
            // formatting indented 는 보기 쉽게 들여쓰기
            string json = JsonConvert.SerializeObject(
                obj, Formatting.Indented, jsonSetting);
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
                "player2.json"
                );

            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(
                json, jsonSetting);

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.lives} / {obj.health} / {obj.playerPosition} ");
        }
    }
}