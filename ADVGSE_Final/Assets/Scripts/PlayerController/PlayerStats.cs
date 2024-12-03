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

    void Start()
    {
        playerHealth = 3;

    }
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
    }

    /// <summary>
    /// Can be called from other scripts for the player to take damage.
    /// </summary>
    /// <param name="damage">Amount subtracted from player health.</param>
    public void PlayerLoseHealth(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Damage Taken: " + damage + "   New Health: " + playerHealth);
    }


    public void CheckDeath()
    {
        if (playerHealth == 0)
        {
            //Player Death

        }
    }

}
