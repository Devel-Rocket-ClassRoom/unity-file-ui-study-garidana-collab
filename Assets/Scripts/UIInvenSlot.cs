using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInvenSlot : MonoBehaviour
{

    public Image iamgeIcon;
    public TextMeshProUGUI textName;
    public SaveItemData SaveItemData { get; private set; }
    public void SetEmpty()
    {
        iamgeIcon.sprite = null;
        textName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        iamgeIcon.sprite = SaveItemData.itemData.SpriteIcon;
        textName.text = SaveItemData.itemData.StringName;
    }



    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         SetEmpty();
    //     }
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         var saveItemData = new SaveItemData();
    //         saveItemData.itemData = DataTableManager.ItemTable.Get("Item2");

    //         SetItem(saveItemData);
    //     }
    // }
}
