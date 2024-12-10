using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int damagePower;
    [SerializeField] int enemyHealth;
    [SerializeField] public int pointsWorth;

    private float speed = 0.02f;

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * -1;
    }

    //When the player collides with the enemy, they take damage.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //uses stats instance to subtract damagePower from player's health
            //passes in damage player takes
            PlayerStats.Instance.PlayerLoseHealth(damagePower);

            //plays enemy death sound effect
            AudioManager.Instance.PlayEnemyDeath();

            //kills enemy after collision so player doesn't take multiple hits.
            Destroy(gameObject);
        }
    }
}

