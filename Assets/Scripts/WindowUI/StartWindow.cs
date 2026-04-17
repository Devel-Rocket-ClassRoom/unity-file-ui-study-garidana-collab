using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button startButton;
    public Button optionButton;
    public bool canContinue; // 인스펙터에 버튼이 생성됨


    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue); // 델리게이트 개념 이벤트 
        startButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }

    // private void Start()
    // {
    //     Open();
    // }


    public override void Open()
    {
        continueButton.gameObject.SetActive(canContinue);


        if (!canContinue)
        {
            firstSelected = startButton.gameObject;
        }
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        windowManager.Open(1);
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame()");
    }

    public void OnOption()
    {
        Debug.Log("OnOption()");
    }
}
