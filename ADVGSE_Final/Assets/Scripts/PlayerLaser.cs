using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerStats.Instance.PlayerScore += 1;
            Debug.Log("You've hit an enemy!");

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
