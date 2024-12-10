using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Gets the BaseEnemy script from the enemy destroyed by player's bullet
        BaseEnemy tempEnemy = other.GetComponent<BaseEnemy>();

        //base enemy script was found
        if (tempEnemy != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                //adjusts player's score based off enemy's point value
                PlayerStats.Instance.PlayerScored(tempEnemy.pointsWorth);

                //plays enemy death sound effect
                AudioManager.Instance.PlayEnemyDeath();

                //destroys bullet and enemy
                Destroy(other.gameObject);
                Destroy(gameObject);
            } 
        }
    }
}
