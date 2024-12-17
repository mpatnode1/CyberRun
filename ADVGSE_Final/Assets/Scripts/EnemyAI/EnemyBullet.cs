using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This is happening");
       if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.PlayerLoseHealth(1);
        }
    }
}
