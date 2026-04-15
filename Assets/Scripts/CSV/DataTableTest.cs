using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKR";
    public string NameStringTableEn = "StringTableEN";
    public string NameStringTableJp = "StringTableJP";

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Languages = Languages.Korean;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Languages = Languages.English;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Languages = Languages.Japanese;
        }
    }


    public void OnClickStringTableKr()
    {
        // var table = new StringTable();
        // table.Load(NameStringTableKr);
        Debug.Log (DataTableManager.StringTable.Get("Hello"));

    }
    public void OnclickStringTableEn()
    {
        // var table = new StringTable();
        // table.Load(NameStringTableEn);
        Debug.Log(DataTableManager.StringTable.Get("Bye")); // 키 값 받아와서 value 호출
    }
    public void OnClickStringTableJp()
    {
        // var table = new StringTable();
        // table.Load(NameStringTableJp);
        Debug.Log(DataTableManager.StringTable.Get("You Die"));
    }
}
