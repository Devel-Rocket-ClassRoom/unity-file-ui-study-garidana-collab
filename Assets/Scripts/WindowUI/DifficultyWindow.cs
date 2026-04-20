
using UnityEngine;
using UnityEngine.UI;
using System.IO;



// private class DifficultyData
// {
//     public int difficultyIndex;
// }

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public int selected;
    public Button saveButton;
    public Button loadButton;

    private string savePathDir;
    private string savePath;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        saveButton.onClick.AddListener(OnSave);
        loadButton.onClick.AddListener(OnLoad);

        savePathDir = Path.Combine(Application.persistentDataPath, "DifficultyData");
        savePath = Path.Combine(Application.persistentDataPath, "DifficultyData", "difficulty.Json");
    }


    public override void Open()
    {
        base.Open();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active) // 토클은 true false 모두 이벤트 발생하기 때문에
    {
        if (active)
        {
            selected = 0;
            Debug.Log("OnEasy");
        }
    }
    public void OnNormal(bool active)
    {
        if (active)
        {
            selected = 1;
            Debug.Log("OnNormal");
        }
    }
    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;
            Debug.Log("OnHard");
        }
    }

    private void OnCancel()
    {
        windowManager.Open(0);
    }


    private void OnSave()
    {
        if (!Directory.Exists(savePathDir))
        {
            Directory.CreateDirectory(savePathDir);
        }
        else
        {
            Debug.Log("SaveDirectory already Exist");
        }

        File.WriteAllText(savePath, selected.ToString());
        Debug.Log($"Difficulty Saved : {selected}");
    }
    private void OnLoad()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("Save File Not Found");
            return;
        }
        selected = int.Parse(File.ReadAllText(savePath));
        toggles[selected].isOn = true;
        Debug.Log($"Difficulty Loaded : {selected}");
    }
}
