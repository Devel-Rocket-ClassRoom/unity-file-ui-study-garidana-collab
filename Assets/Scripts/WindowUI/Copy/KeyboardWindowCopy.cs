using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyBoardWindowCopy : GenericWindowCopy
{
    private readonly StringBuilder sb = new();
    public TextMeshProUGUI inputField;
    public GameObject rooKeyboard;

    public int maxCharacters = 7;
    private float time = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    public override void Open()
    {
        sb.Clear();
        time = 0f;
        blink = false;
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
        }
    }

    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;

        if (showCursor)
        {
            sb.Append('_');
        }
        inputField.SetText(sb);
        if (showCursor)
        {
            sb.Length -= 1;
        }
    }

    public void OnCancel()
    {
        sb.Clear();
    }

    public void OnDelete()
    {
        if (sb.Length > 0)
        {
            sb.Length -= 1;
        }
    }

    public void OnAccept()
    {

    }
}