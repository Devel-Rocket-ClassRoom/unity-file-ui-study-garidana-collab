
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; set; } // 세이브 파일의 버전
    public abstract SaveData VersionUp();
}


[System.Serializable]
public class SaveDataV1 : SaveData
{

    public string PlayerName { get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }
    // 버전 업 할 시 변경해줄 내용
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        return saveData;
    }

}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty; //  초기값 비어있음
    public int Gold = 0;


    public SaveDataV2()
    {
        Version = 2;
    }


    public override SaveData VersionUp()
    {
        var data = new SaveDataV3();
        data.Name = Name;
        data.Gold = Gold;
        return data;
        // var saveData = new SaveDataV3();
        // saveData.Name = Name;
        // saveData.Gold = Gold;
        // //saveData.ItemId = ItemTable.GetItem();
        // return saveData;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty; //  초기값 비어있음
    public int Gold = 0;
    public List<string> ItemList { get; set; } = new List<string>();
    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        SaveDataV4 data = new();
        data.Name = Name;
        data.Gold = Gold;
        foreach (var id in ItemList)
        {
            SaveItemData itemData = new();
            itemData.itemData = DataTableManager.ItemTable.Get(id);

            data.ItemList.Add(itemData);
        }
        return data;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList { get; set; } = new List<SaveItemData>();
    public List<SaveCharData> CharList { get; set; } = new List<SaveCharData>();
    public UiInvenSlotList.SortingOptions ItemSorting = UiInvenSlotList.SortingOptions.NameAcsending;
    public UiInvenSlotList.FilteringOptions ItemFiltering = UiInvenSlotList.FilteringOptions.None;
    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}
