using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerStateCopy
{
    public string playerName;
    public int lives;
    public float health;

    public Vector3 playerPosition;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}

public class JsonTest1Copy : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new();
        jsonSetting.Converters.Add(new Vector3ConverterCopy());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStateCopy obj = new PlayerStateCopy
            {
                playerName = "ABC",
                lives = 10,
                health = 10f,
                playerPosition = new Vector3(3f, 3f, 3f)
            };

            string pathFolderCopy = Path.Combine(
                Application.persistentDataPath,
                "JsonTestCopy"
            );

            if (!Directory.Exists(pathFolderCopy))
            {
                Directory.CreateDirectory(pathFolderCopy);
            }

            string pathCopy = Path.Combine(
                pathFolderCopy,
                "player2Copy.json"
            );

            string json = JsonConvert.SerializeObject(obj,
            Formatting.Indented, jsonSetting);
            File.WriteAllText(pathCopy, json);


            Debug.Log(pathCopy);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string pathFolderCopy = Path.Combine(
                Application.persistentDataPath,
                "JsonTestCopy"
            );
            string pahtCopy = Path.Combine(
                pathFolderCopy,
                "player2Copy.json"
            );

            string json = File.ReadAllText(pahtCopy);
            PlayerStateCopy obj = JsonConvert.DeserializeObject<PlayerStateCopy>(
                json, jsonSetting);

            Debug.Log(obj);
        }
    }
}