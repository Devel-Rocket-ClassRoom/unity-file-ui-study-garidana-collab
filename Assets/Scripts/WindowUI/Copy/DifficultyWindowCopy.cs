using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindowCopy : GenericWindowCopy
{
    // OnEasy, OnNormal, OnHard 상태를 저장해줄 toggles 배열
    public Toggle[] toggles;
    public int selected;


    // OnEasy, OnNormal, OnHard 만들어 줄것 ;;
    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }

    public override void Open()
    {
        // base.Open()은 상속받은 부모의 클래스의 Open 메서드를 호출
        // GenericWindow의 Open은 gameObject.SetActive(true);
        base.Open();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            Debug.Log("OnEasy");
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            Debug.Log("OnNormal");
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            Debug.Log("OnHard");
        }
    }

}