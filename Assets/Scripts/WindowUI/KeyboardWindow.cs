
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    // 자식의 컨트롤들을 관리해주는 스크립트

    // InputField 키 입력마다 밀어넣기 지울때마다 당겨오기
    private readonly StringBuilder sb = new();

    public TextMeshProUGUI inputField;
    public GameObject rootKeyboard;

    public int maxChars = 7;
    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => OnKey(text.text));
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > cursorDelay)
        {
            timer = 0;
            blink = !blink;
            UpdateInputField();
        }
    }

    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;
        base.Open();
        UpdateInputField();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxChars)
        {
            sb.Append(key);
            UpdateInputField();
        }

    }

    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxChars && !blink;

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
        UpdateInputField();
    }

    public void OnDelete()
    {
        if (sb.Length > 0)
        {
            sb.Length -= 1;
            UpdateInputField();
        }
    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }
}
