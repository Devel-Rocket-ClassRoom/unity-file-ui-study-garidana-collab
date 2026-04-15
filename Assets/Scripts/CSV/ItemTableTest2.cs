using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest2 : MonoBehaviour
{

    // 선택된 아이템 중앙 큰 화면에 출력
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;


    public void OnEnable()
    {
        SetEmpty();
    }
    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;
    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);
    }

    public void SetItemData(ItemData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
}
