using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;



[System.Serializable]
public class JsonTest2_Answer : MonoBehaviour
{
    //fileName 저장할 JSON파일 명
    public string fileName = "test.json";
    // persistentDataPath/JsonTest/test.json 전체 경로 프로퍼티
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);
    // 생성 가능한 prefab 이름 배열
    public string[] prefabNames = new string[] { "Cube", "Cylinder", "Sphere", "Capsule" };
    // Json 직렬화 설정 객체 JsonSerializerSettings 사용
    public JsonSerializerSettings jsonSettings;

    // public GameObject cube;
    // 생성된 오브젝트 리스트 (생성해서 넣고 제거하는 리스트)
    private List<GameObject> spawnedObjects = new();
    // 랜덤으로 생성할 오브젝트의 Primitive(3D 오브젝트 모형)
    private PrimitiveType[] primitiveTypes = new PrimitiveType[]
    {
        PrimitiveType.Cube,
        PrimitiveType.Cylinder,
        PrimitiveType.Sphere,
        PrimitiveType.Capsule,

    };

    // Awake시 Json 직렬화 설정 객체 생성
    private void Awake() // 초기화
    {

        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented; // 들여쓰기 포맷으로 JSON 직렬화

        // Unity 전용 타입인 Vector3, Quaternion, Color는 기본 JSON 직렬화가 안되므로 Converter를 만들어 사용해서 추가해줘야 함

        jsonSettings.Converters.Add(new Vector3Converter()); // 만든 벡터3 변환기를 Json 직렬화 객체에 추가
        jsonSettings.Converters.Add(new QuaternionConverter()); // 만든 쿼터니언 변환기를 Json 직렬화 객체에 추가
        jsonSettings.Converters.Add(new ColorConverter());  // 만든 컬러 변환기를 Json 직렬화 객체에 추가
    }

    private void CreateRandomObject() // 랜덤 오브젝트 생성 메서드
    {
        // 선언해놓은 prefab 배열 (cube, cylinder, sphere, capsule) 중에 랜덤으로 선택해서 오브젝트 생성
        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JasonTestObject>(prefabName);

        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV(); // 랜덤 컬러 지정
    }
    // 랜덤 오브젝트 10번 생성
    public void OnCreate()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateRandomObject();
        }
    }
    // Clear 버튼 클릭시 호출할 오브젝트 제거 메서드. (Tag가 TestObject인 오브젝트 제거)
    // 화면에서 제거되어도 JSON 파일에 오브젝트 정보가 남아있음.
    public void OnClear()
    {
        // TestObject 태그가 붙어있는 오브젝트 (prefab에 연결된 오브젝트) 배열을 만듦
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        // 리스트 순회하며 포함된 오브젝트들 제거
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
    }

    // Save 버튼 클릭시 JSON 파일에 오브젝트 정보 저장
    public void OnSave()
    {
        // TestObject 태그가 붙어있는 오브젝트 (prefab에 연결된 오브젝트) 배열을 만듦
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        // 세이브할 오브젝트를 담을 리스트 생성
        var saveList = new List<ObjectSaveData>();
        // obj 리스트 순회하며 saveList에 오브젝트 정보 추가
        foreach (var obj in objs)
        {
            // JasonTestObject의 컴포넌트를 jsonTestObj로 전달
            var jsonTestObj = obj.GetComponent<JasonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());
            // Json 직렬화
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
