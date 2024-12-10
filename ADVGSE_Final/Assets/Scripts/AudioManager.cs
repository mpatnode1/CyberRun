using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource enemyDeathSound;
    [SerializeField] AudioSource bulletLaunchSound;
    [SerializeField] AudioSource coinCollectSound;

    private static AudioManager instance;

    public static AudioManager Instance
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

    public void PlayEnemyDeath()
    {
        enemyDeathSound.Play();
    }


    public void PlayCoinCollect()
    {
        coinCollectSound.Play();
    }

    public void PlayBulletSound()
    {
        bulletLaunchSound.Play();
    }

}
