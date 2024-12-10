using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.PlayerLoseHealth(1);
        }
    }
}
