using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    [SerializeField] int coinWorth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.PlayerScored(coinWorth);

            Destroy(gameObject);

            AudioManager.Instance.PlayCoinCollect();

        }
    }
}
