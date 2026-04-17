// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using System.Collections;

// public class GameOverWindow : GenericWindow
// {
//     public TextMeshProUGUI leftStatLabel;
//     public TextMeshProUGUI leftStatValue;
//     public TextMeshProUGUI rightStatLabel;
//     public TextMeshProUGUI rightStatValue;
//     public TextMeshProUGUI scoreValue;

//     public Button nextButton;

//     public float delaytime;


//     private void Awake()
//     {
//         nextButton.onClick.AddListener(OnNext);
//     }


//     public override void Open()
//     {
//         base.Open();
//         StartCoroutine(PrintScores());
//     }


//     public override void Close()
//     {
//         base.Close();
//         // 코루틴이 종료 되기전에 윈도우가 종료 될 수 있기때문에
//         StopCoroutine(PrintScores());

//     }

//     public void OnNext()
//     {
//         windowManager.Open(0);
//     }

//     private IEnumerator PrintScores()
//     {
//         leftStatLabel.text = "";
//         leftStatValue.text = "";
//         rightStatLabel.text = "";
//         rightStatValue.text = "";

//         // 왼쪽 스탯 3줄 출력
//         for (int i = 1; i < 4; i++)
//         {
//             // 0.5초 딜레이주기
//             yield return new WaitForSeconds(delaytime); // 줄 사이 딜레이

//             if (i > 1)
//             {
//                 leftStatLabel.text += "\n";
//                 leftStatValue.text += "\n";
//             }
//             leftStatLabel.text += $"Stat {i}";
//             leftStatValue.text += Random.Range(0, 999).ToString();
//         }
//         // 오른쪽 스탯 3줄 출력
//         for (int i = 4; i <= 6; i++)
//         {
//             // 0.5초 딜레이주기
//             yield return new WaitForSeconds(delaytime);

//             if (i > 4)
//             {
//                 rightStatLabel.text += "\n";
//                 rightStatValue.text += "\n";
//             }
//             rightStatLabel.text += $"Stat {i}";
//             rightStatValue.text += Random.Range(0, 999).ToString();
//         }

//         int finalScore = Random.Range(0, 999999);
//         float duration = 1.5f;
//         float elapsed = 0f;

//         while (elapsed < duration)
//         {
//             elapsed += Time.deltaTime;
//             float t = elapsed / duration;
//             scoreValue.text = $"{((int)Mathf.Lerp(0, finalScore, t)):00000000}";
//             yield return null;
//         }

//         scoreValue.text = $"{finalScore:00000000}";
//     }
// }
