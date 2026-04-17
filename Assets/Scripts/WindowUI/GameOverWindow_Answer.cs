using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow_Answer : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;

    private Coroutine routine;

    private const int totalStats = 6;

    private const int statsPerColumn = 3;
    private int[] statsRolls = new int[totalStats];

    private int finalScore;


    public Button nextButton;
    public float statsDelay = 1f;
    public float scoreDuration = 2f;



    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] { leftStatLabel, rightStatLabel };
        statsValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };

        nextButton.onClick.AddListener(OnNext);
    }


    public override void Open()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        base.Open();
        ResetStats();
        routine = StartCoroutine(CoPlayGameOverRoutine());
        // StartCoroutine(PrintScores());
    }


    public override void Close()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        ResetStats();
        base.Close();
        // 코루틴이 종료 되기전에 윈도우가 종료 될 수 있기때문에 Coroutine
        // StopCoroutine(PrintScores());
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    private void ResetStats()
    {
        for (int i = 0; i < totalStats; ++i)
        {
            statsRolls[i] = Random.Range(0, 1000);
        }
        finalScore = Random.Range(0, 10000000);

        for (int i = 0; i < statsLabels.Length; ++i)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }


    private IEnumerator CoPlayGameOverRoutine()
    {
        for (int i = 0; i < totalStats; ++i)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newLine = (i % statsPerColumn == 0) ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newLine}Stat {i}";
            valueText.text = $"{valueText.text}{newLine}{statsRolls[i]:D4}";
        }

        int current = 0;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D8}";
            yield return null;
        }

        scoreValue.text = $"{finalScore:D8}";
        routine = null;
    }





    // private IEnumerator PrintScores()
    // {
    //     leftStatLabel.text = "";
    //     leftStatValue.text = "";
    //     rightStatLabel.text = "";
    //     rightStatValue.text = "";

    //     // 왼쪽 스탯 3줄 출력
    //     for (int i = 1; i < 4; i++)
    //     {
    //         // 0.5초 딜레이주기
    //         yield return new WaitForSeconds(delaytime); // 줄 사이 딜레이

    //         if (i > 1)
    //         {
    //             leftStatLabel.text += "\n";
    //             leftStatValue.text += "\n";
    //         }
    //         leftStatLabel.text += $"Stat {i}";
    //         leftStatValue.text += Random.Range(0, 999).ToString();
    //     }
    //     // 오른쪽 스탯 3줄 출력
    //     for (int i = 4; i <= 6; i++)
    //     {
    //         // 0.5초 딜레이주기
    //         yield return new WaitForSeconds(delaytime);

    //         if (i > 4)
    //         {
    //             rightStatLabel.text += "\n";
    //             rightStatValue.text += "\n";
    //         }
    //         rightStatLabel.text += $"Stat {i}";
    //         rightStatValue.text += Random.Range(0, 999).ToString();
    //     }

    //     int finalScore = Random.Range(0, 999999);
    //     float duration = 1.5f;
    //     float elapsed = 0f;

    //     while (elapsed < duration)
    //     {
    //         elapsed += Time.deltaTime;
    //         float t = elapsed / duration;
    //         scoreValue.text = $"{((int)Mathf.Lerp(0, finalScore, t)):00000000}";
    //         yield return null;
    //     }

    //     scoreValue.text = $"{finalScore:00000000}";
    // }
}
