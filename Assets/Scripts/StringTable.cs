using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id {get; set;}    
        public string String {get; set;}
    }

    // 한번 만들어 놓고 계속 사용하기에 readonly 적합
    private readonly Dictionary<string, string> table = new ();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);
        // 딕셔너리에 불러온 내용 쓰기 (추가)
        foreach (Data data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogError($"키 중복 : {data.Id}");
            }
        }
    }

    public static readonly string Unknown = "키 없음";

    public string Get (string key)
    {
        if (!table.ContainsKey(key))
        {
            return Unknown;
        }
        return table[key];
    }
    
}
