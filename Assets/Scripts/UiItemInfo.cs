using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiItemInfo : MonoBehaviour
{

    public static readonly string FromatCommon = "{0} : {1}";
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }

    public void SetSaveItemData(SaveItemData saveItemData)
    {
        ItemData data = saveItemData.itemData;

        imageIcon.sprite = data.SpriteIcon;
        textName.text = string.Format(FromatCommon, DataTableManager.StringTable.Get("Name"), data.StringName);
        textDesc.text = string.Format(FromatCommon, DataTableManager.StringTable.Get("Desc"), data.StringDesc);

        string typeId = data.Type.ToString().ToUpper();

        textType.text = string.Format(FromatCommon, DataTableManager.StringTable.Get("Type"), DataTableManager.StringTable.Get(typeId));
        textValue.text = string.Format(FromatCommon, DataTableManager.StringTable.Get("Value"), data.Value);
        textCost.text = string.Format(FromatCommon, DataTableManager.StringTable.Get("Cost"), data.Cost);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEmpty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSaveItemData(SaveItemData.GetRandomItem());

        }
    }
}

