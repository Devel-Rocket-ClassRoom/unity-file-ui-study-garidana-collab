using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest1 : MonoBehaviour
{
    // 작은 슬롯 붙이는 스크립트

    public string characterId;
    public Image icon;
    public LocalizationText textName;

    public CharacterTableTest2 characterInfo;

    public void OnEnable()
    {
        OnChangeCharacterId();
    }

    public void OnValidate()
    {
        OnChangeCharacterId();
    }
    public void OnChangeCharacterId()
    {
        CharacterData data = DataTableManager.CharTable.Get(characterId);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
        
    }

    public void OnClick()
    {
        characterInfo.SetCharData(characterId);
    }
}
