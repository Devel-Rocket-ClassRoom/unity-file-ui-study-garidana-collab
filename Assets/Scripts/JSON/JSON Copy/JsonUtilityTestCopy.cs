using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfoCopy
{
    public string playerName;
    public int lives;
    public float health;

    public Vector3 position;

    public Dictionary<string, int> scores = new()
    {
        { "stage1", 100 },
        { "stage2", 200 },    };
}

public class JsonUtilityTestCopy : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerInfoCopy obj = new PlayerInfoCopy
            {
                playerName = "AVS",
                lives = 10,
                health = 10f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTestCopy"
            );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "playerCopy.json"
            );

            string json = JsonUtility.ToJson(obj, prettyPrint: true);
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTestCopy"
            );

            string path = Path.Combine(
                Application.persistentDataPath,
                "playerCopy.json"
            );

            string json = File.ReadAllText(path);
            PlayerInfoCopy obj = new();
            JsonUtility.FromJsonOverwrite(json, obj);

        }
    }
}