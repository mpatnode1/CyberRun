using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawnPoint;
    /// <summary>
    /// speed between bullets
    /// </summary>
    [SerializeField] float fireSpeed;

    [SerializeField] private float timer = 3;
    private float laserTime;

    public void FixedUpdate()
    {
        laserTime -= Time.deltaTime;
    }

    private void OnMouseDown()
    {
        shootFireBall();
    }

    void shootFireBall()
    {
        //Debug.Log(laserTime);

        if (laserTime > 0) return;
        laserTime = timer;

        GameObject laserObj = Instantiate(laserPrefab, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;
        Rigidbody laserRig = laserObj.GetComponent<Rigidbody>();
        laserRig.AddForce(laserRig.transform.forward * fireSpeed);

        Destroy(laserObj, 5f);
    }
}
