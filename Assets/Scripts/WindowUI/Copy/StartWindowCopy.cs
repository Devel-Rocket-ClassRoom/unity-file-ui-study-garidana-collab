using UnityEngine;
using UnityEngine.UI;


public class StartWindowCopy : GenericWindowCopy
{
    public Button continueButton;
    public Button startButton;
    public Button optionButton;
    public bool canContinue;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        startButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }

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
        windowManager.Open(2);
        Debug.Log("OnNewGame()");
    }
    public void OnOption()
    {
        windowManager.Open(3);
        Debug.Log("OnOption()");
    }
}