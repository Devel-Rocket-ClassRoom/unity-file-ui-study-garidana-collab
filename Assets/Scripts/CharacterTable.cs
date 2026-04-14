using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;


public class CharacterData
{
    public string Id {get; set;}
    // public CharTypes Type{get; set;}
    public string Name {get;set;}
    public string Desc {get; set;}
    public int Attack {get; set;}
    public int Defense {get; set;}

    public string Icon {get; set;}

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Name} / {Desc} / {Attack} / {Defense} / {Icon}";
    }

    
}

public class CharacterTable : DataTable
{
    private readonly Dictionary <string, CharacterData> table = new ();

    public override void Load (string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }
        return table [id];
    }
}
