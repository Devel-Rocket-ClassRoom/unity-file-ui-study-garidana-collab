using System;


// 전역 상수 및 상태관리 Define.cs


// 언어 구조체
public enum Languages
{
    Korean,
    English,
    Japanese,
}
// Variables 정적 클래스
public static class Variables
{
    // 언어 발생 이벤트
    public static event System.Action OnLanguageChanged;
    private static Languages language = Languages.Korean;
    public static Languages Languages
    {
        // 읽기 속성
        get
        {
            return language;
        }
        // 쓰기 속성
        set
        {
            // 설정된 언어가 기존의 언어와 같을시 return;
            if (language == value)
            {
                return;
            }
            // 기존언어와 설정언어가 다를 시 language에 할당 및 DataTableManager의 ChangeLanguage메서드 (language매개변수) 호출
            language = value;
            DataTableManager.ChangeLanguage(language);
            // 언어 변경되면 OnLanguageChanged 이벤트 발동
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