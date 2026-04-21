using System;
using Newtonsoft.Json;
using UnityEngine;


[Serializable]
public class SaveItemData
{
    public Guid instanceId { get; set; }

    //Json 컨버터 어트리뷰트 사용
    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData itemData { get; set; }
    public DateTime obtainTime { get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.itemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }

    public SaveItemData()
    {
        // 난수 리턴이라 생각하면 됨
        instanceId = Guid.NewGuid();
        obtainTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{instanceId}\n{obtainTime}\n{itemData.Id}";
    }
}
