using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiCharSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveCharData SaveCharData { get; private set; }
    public Button button;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEmpty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCharacter(SaveCharData.GetRandomCharacter());
        }
    }


    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveCharData = null;
    }


    public void SetCharacter(SaveCharData data)
    {
        SaveCharData = data;
        imageIcon.sprite = SaveCharData.charData.SpriteIcon;
        textName.text = SaveCharData.charData.StringName;
    }
}