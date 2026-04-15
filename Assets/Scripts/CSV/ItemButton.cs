using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemButton : MonoBehaviour
{
    public string Id;
    public TextMeshProUGUI text;
    public Image image;
    public ItemData data;

    public void OnValidate()
    {
        data = DataTableManager.ItemTable.Get(Id);
        text.text = data.Name;
        image.sprite = data.SpriteIcon;
    }

}
