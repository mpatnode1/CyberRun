using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    //script for when player falls off map and has instant death.
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == "Player")
        {
            PlayerStats.Instance.PlayerLoseHealth(4);
        }
    }
}
