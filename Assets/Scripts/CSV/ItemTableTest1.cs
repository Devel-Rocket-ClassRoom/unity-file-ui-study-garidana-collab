using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest1 : MonoBehaviour
{
    // 작은 슬롯 붙이는 스크립트

    public string itemId;
    public Image icon;
    public LocalizationText textName;

    public ItemTableTest2 itemInfo;

    public void OnEnable()
    {
        OnChangeItemId();
    }

    public void OnValidate()
    {
        OnChangeItemId();
    }
    public void OnChangeItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
        
    }

    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }
}
