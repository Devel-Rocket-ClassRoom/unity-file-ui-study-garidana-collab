using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class GameOverWindowCopy : GenericWindowCopy
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;

    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    public float delayTime;

    public override void Open()
    {
        base.Open();
        StartCoroutine(PrintScores());
    }

    private IEnumerator PrintScores()
    {
        // 기본 값 할당
        leftStatLabel.text = string.Empty;
        leftStatValue.text = string.Empty;
        rightStatLabel.text = string.Empty;
        rightStatValue.text = string.Empty;

        for (int i = 1; i < 4; i++)
        {
            yield return new WaitForSeconds(delayTime);

            leftStatLabel.text += "\n";
            leftStatValue.text += "\n";
            if (i > 1)
            {
                leftStatLabel.text = $"Stat {i}";
                leftStatValue.text = Random.Range(100, 999).ToString();
            }
        }
        for (int i = 4; i <= 6; i++)
        {
            yield return new WaitForSeconds(delayTime);

            if (i > 4)
            {
                rightStatLabel.text += "\n";
                rightStatValue.text += "\n";
            }
            rightStatLabel.text = $"Stat {i}";
            rightStatValue.text = Random.Range(100, 999).ToString();
        }

        int finalScore = Random.Range(0, 9999999);
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            scoreValue.text = $"{((int)Mathf.Lerp(0, finalScore, t)):0000000}";
            yield return null;
        }

        scoreValue.text = $"{finalScore:00000000}";
    }
}