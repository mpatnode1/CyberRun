using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Stores the four images that are each a bar of player health, when one is deactivated, visually the player loses health.
    /// </summary>
    [SerializeField] private GameObject[] healthIcons;

    /// <summary>
    /// Gui to adjust the score.
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreGUI;

    private static UIManager instance;

    private int currentHealthCounter = 3;


    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void RemoveHealthBar()
    {
        if (currentHealthCounter >= 0)
        {
            healthIcons[currentHealthCounter].SetActive(false);
            currentHealthCounter--;
        }
    }

    /// <summary>
    /// Can be called by other scripts to update the scoreboard text
    /// </summary>
    /// <param name="newScore">New score for updated scoreboard</param>
    public void UpdateScoreboard(int newScore)
    {
        scoreGUI.text = newScore.ToString();
    }
}
