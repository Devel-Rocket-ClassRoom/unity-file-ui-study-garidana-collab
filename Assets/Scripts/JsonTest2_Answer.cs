using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;



[System.Serializable]
public class JsonTest2_Answer : MonoBehaviour
{
    public string fileName = "test.json";
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    public string[] prefabNames = new string[] { "Cube", "Cylinder", "Sphere", "Capsule" };

    public JsonSerializerSettings jsonSettings;

    public GameObject cube;

    private List<GameObject> spawnedObjects = new();

    private PrimitiveType[] primitiveTypes = new PrimitiveType[]
    {
        PrimitiveType.Cube,
        PrimitiveType.Cylinder,
        PrimitiveType.Sphere,
        PrimitiveType.Capsule,

    };


    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    private void CreateRandomObject()
    {


        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JasonTestObject>(prefabName);

        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();


    }

    public void OnCreate()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateRandomObject();
        }
    }

    public void OnClear()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");

        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }


    public void OnSave()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        var saveList = new List<ObjectSaveData>();
        foreach (var obj in objs)
        {
            var jsonTestObj = obj.GetComponent<JasonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());

            var json = JsonConvert.SerializeObject(saveList, jsonSettings);
            File.WriteAllText(FileFullPath, json);
        }
    }

    public void OnLoad()
    {
        OnClear();

        var json = File.ReadAllText(FileFullPath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json, jsonSettings);
        foreach (var saveData in saveList)
        {
            var prefab = Resources.Load<JasonTestObject>(saveData.prefabName);
            var jsonTestObj = Instantiate(prefab);
            jsonTestObj.Set(saveData);
        }

    }
    // public void Save()
    // {
    //     // SomeClass obj = new SomeClass();
    //     // obj.pos = cube.transform.position;
    //     // obj.rot = cube.transform.rotation;
    //     // obj.color = cube.GetComponent<MeshRenderer>().material.color;
    //     // obj.scale = cube.transform.localScale;
    //     // var json = JsonConvert.SerializeObject(obj, jsonSettings);
    //     // File.WriteAllText(FileFullPath, json);




    //     List<SomeClass> dataList = new();

    //     foreach (var obj in spawnedObjects)
    //     {
    //         dataList.Add(new SomeClass
    //         {
    //             type = obj.GetComponent<MeshFilter>().sharedMesh.name switch
    //             {
    //                 "Cube" => PrimitiveType.Cube,
    //                 "Cylinder" => PrimitiveType.Cylinder,
    //                 "Sphere" => PrimitiveType.Sphere,
    //                 "Capsule" => PrimitiveType.Capsule,
    //                 _ => PrimitiveType.Cube
    //             },
    //             pos = obj.transform.position,
    //             rot = obj.transform.rotation,
    //             color = obj.GetComponent<MeshRenderer>().material.color,
    //             scale = obj.transform.localScale
    //         });
    //     }
    //     string dirPath = Path.GetDirectoryName(FileFullPath);
    //     if (!Directory.Exists(dirPath))
    //     {
    //         Directory.CreateDirectory(dirPath);
    //     }
    //     var json = JsonConvert.SerializeObject(dataList, jsonSettings);
    //     File.WriteAllText(FileFullPath, json);

    //     Debug.Log($"저장 완료 : {dataList.Count}개");

    // }

    // public void Load()
    // {
    //     // var json = File.ReadAllText(FileFullPath);
    //     // var obj = JsonConvert.DeserializeObject<SomeClass>(json, jsonSettings);
    //     // cube.transform.position = obj.pos;
    //     // cube.transform.rotation = obj.rot;
    //     // cube.GetComponent<MeshRenderer>().material.color = obj.color;
    //     // cube.transform.localScale = obj.scale;
    //     // Debug.Log(obj);

    //     if (!File.Exists(FileFullPath)) return;
    //     foreach (var obj in spawnedObjects)
    //     {
    //         Destroy(obj);
    //     }
    //     spawnedObjects.Clear();

    //     var dataList = JsonConvert.DeserializeObject<List<SomeClass>>(File.ReadAllText(FileFullPath), jsonSettings);
    //     foreach (var data in dataList)
    //     {
    //         GameObject obj = GameObject.CreatePrimitive(data.type);
    //         obj.transform.position = data.pos;
    //         obj.transform.rotation = data.rot;
    //         obj.transform.localScale = data.scale;
    //         obj.GetComponent<MeshRenderer>().material.color = data.color;


    //         spawnedObjects.Add(obj);

    //     }
    //     Debug.Log($"로드 완료 : {dataList.Count}개");
    // }


    // public void Generate()
    // {
    //     // 랜덤 도형 생성
    //     PrimitiveType type = primitiveTypes[UnityEngine.Random.Range(0, primitiveTypes.Length)];
    //     GameObject obj = GameObject.CreatePrimitive(type);

    //     obj.transform.position = new Vector3(
    //         UnityEngine.Random.Range(10, 860),
    //         UnityEngine.Random.Range(0, 480),
    //         UnityEngine.Random.Range(0, 100));
    //     obj.transform.rotation = Quaternion.Euler(
    //         UnityEngine.Random.Range(-180, 180),
    //         UnityEngine.Random.Range(-180, 180),
    //         UnityEngine.Random.Range(-180, 180));
    //     obj.transform.localScale = new Vector3(
    //         UnityEngine.Random.Range(0.1f, 20f),
    //         UnityEngine.Random.Range(0.1f, 20f),
    //         UnityEngine.Random.Range(0.1f, 20f));
    //     obj.GetComponent<MeshRenderer>().material.color = new Color(
    //         UnityEngine.Random.Range(0f, 1f),
    //         UnityEngine.Random.Range(0f, 1f),
    //         UnityEngine.Random.Range(0f, 1f));


    //     spawnedObjects.Add(obj);
    // }
}
