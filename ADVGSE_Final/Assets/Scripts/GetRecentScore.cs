using TMPro;
using UnityEngine;

public class GetRecentScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recentScoreText;

    private void Awake()
    {
            recentScoreText.text = "Score: " + (PlayerPrefs.GetInt("PlayerScore", 0).ToString());

    }
}
