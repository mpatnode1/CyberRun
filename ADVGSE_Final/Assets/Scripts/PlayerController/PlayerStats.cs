using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;

    private int playerScore { get; set; }
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    private int playerHealth { get; set; }
    public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }

    [SerializeField] GameObject Player;
    /// <summary>
    /// Used to end the game when the player dies.
    /// </summary>
    [SerializeField] LoadGame loadGame;

    public static PlayerStats Instance
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

        //set starting amount for player score and health
        playerScore = 0;
        playerHealth = 4;
    }

    /// <summary>
    /// Can be called from other scripts to increase the player's score.
    /// </summary>
    /// <param name="scoreEarned"></param>
    public void PlayerScored(int scoreEarned)
    {
        //adds the amount the player just scored to their previous total
        playerScore += scoreEarned;

        //updates the scoreboard with their new total
        UIManager.Instance.UpdateScoreboard(playerScore);
    }

    /// <summary>
    /// Can be called from other scripts for the player to take damage.
    /// </summary>
    /// <param name="damage">Amount subtracted from player health.</param>
    public void PlayerLoseHealth(int damage)
    {

        //Removes one of the bars of health from the UI
        int i = 1;
        while(i == damage || i < damage)
        {
            UIManager.Instance.RemoveHealthBar(); i++;
        }

        playerHealth -= damage;

        CheckDeath();
    }


    public void CheckDeath()
    {
        if (playerHealth <= 0)
        {
            //Player Death
            PlayerPrefs.SetInt("PlayerScore", playerScore);
            if(PlayerPrefs.GetInt("HighScore", 0) == 0 || PlayerPrefs.GetInt("PlayerScore", playerScore) > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", playerScore);
            }

            loadGame.LoadLoseScreen();
        }
    }

}
