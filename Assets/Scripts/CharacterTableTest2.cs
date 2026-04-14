using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest2 : MonoBehaviour
{

    // 선택된 아이템 중앙 큰 화면에 출력
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText textAttack;
    public LocalizationText textDefense;


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
        textAttack.text.text = string.Empty;
        textDefense.text.text = string.Empty;

    }

    public void SetCharData(string charId)
    {
        CharacterData data = DataTableManager.CharTable.Get(charId);
        SetCharData(data);
    }

    public void SetCharData(CharacterData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textAttack.id = data.Attack;
        textDefense.id = data.Defense;

        textName.OnChangedId();
        textDesc.OnChangedId();
        textAttack.OnChangedId();
        textDefense.OnChangedId();
    }
}
