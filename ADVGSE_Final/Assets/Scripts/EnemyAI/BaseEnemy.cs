using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int damagePower;
    [SerializeField] int enemyHealth;

    [Header("Enemy Gun")]
    [SerializeField] GameObject ememyBulletPrefab;
    [SerializeField] Transform enemyBulletSpawnPoint;
    [SerializeField] bool isEnemyShooter;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletCooldown;
    private float bulletTimer;

    private void Update()
    {
        bulletTimer -= Time.deltaTime;

        if (isEnemyShooter)
        {
            //only shoots if enemy bullet cooldown has ended
            if (bulletTimer > 0) {
                enemyShoot();
            }
        }
    }

        //When the player collides with the enemy, they take damage.
        private void OnCollisionEnter(Collision collision)
        {
            //uses stats instance to subtract damagePower from player's health
            //passes in damage player takes
            PlayerStats.Instance.PlayerLoseHealth(damagePower);
        }

        private void enemyShoot()
        {
            bulletTimer = bulletCooldown;

            //creates a bullet gameobject using the bullet's prefab at the current pos + rot of player
            GameObject bulletObj = Instantiate(ememyBulletPrefab, enemyBulletSpawnPoint.transform.position, enemyBulletSpawnPoint.transform.rotation) as GameObject;

            //obtains temp bullet obj's rigidbody and applies the force to move it forward
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletRig.transform.forward * bulletSpeed);

            //destroys after 5s
            Destroy(bulletObj, 5f);
        }
    }

