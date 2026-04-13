using System;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;


    private static Languages language = Languages.Korean;
    public static Languages Languages
    {
        get
        {
            return language;
        }
        set
        {
            if (language == value)
            {
                return;
            }
            language = value;
            DataTableManager.ChangeLanguage(language);

            OnLanguageChanged?.Invoke();
        }
    }
}

// 아이디 모음 역할하는 정적 클래스
public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKR",
        "StringTableEN",
        "StringTableJP",
    };
    public static string String => StringTableIds[(int)Variables.Languages];

}