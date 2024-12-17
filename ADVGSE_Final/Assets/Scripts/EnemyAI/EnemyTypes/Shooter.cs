using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Enemy Gun")]
    [SerializeField] GameObject ememyBulletPrefab;
    [SerializeField] Transform enemyBulletSpawnPoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletCooldown;
    private float bulletTimer;

    private void Awake()
    {
        bulletTimer = bulletCooldown;
    }

    private void Update()
    {
        bulletTimer -= Time.deltaTime;
    }

    public void FixedUpdate()
    {
            //only shoots if enemy bullet cooldown has ended
            if (bulletTimer < 0)
            {
                enemyShoot();
            }
    }

    private void enemyShoot()
    {
        bulletTimer = bulletCooldown;

        //creates a bullet gameobject using the bullet's prefab at the current pos + rot of player
        GameObject bulletObj = Instantiate(ememyBulletPrefab, enemyBulletSpawnPoint.transform.position, enemyBulletSpawnPoint.transform.rotation, gameObject.transform) as GameObject;

        //obtains temp bullet obj's rigidbody and applies the force to move it forward
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * -1 * bulletSpeed);

        //destroys after 5s
        Destroy(bulletObj, 8f);
    }
}
