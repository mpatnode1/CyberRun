using UnityEngine;
using TMPro;

public class GetHighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;

    private void Awake()
    {

        if (PlayerPrefs.GetInt("HighScore", 0) != 0)
        {
            highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        else
        {
            highscoreText.text = string.Empty;
        }
    }
}
