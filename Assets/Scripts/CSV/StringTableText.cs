using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class StringTableText : MonoBehaviour
{


    public string id;
    public TextMeshProUGUI text;
    public Languages language;

    // private void Start()
    // {
    //     OnChangedId();
    // }
    private void Update()
    {
        OnChangedId();
    }

    private void OnChangedId()
    {
        Variables.Languages = language;
        text.text = DataTableManager.StringTable.Get(id);
    }

    private void OnValidate()
    {
        
    }

    public void ChangeLanguage(Languages lang)
    {
        language = lang;
    }
}
