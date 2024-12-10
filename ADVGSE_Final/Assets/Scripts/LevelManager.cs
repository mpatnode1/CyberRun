using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] possibleLevels;

    /// <summary>
    /// Max Z starting position for level to spawn at.
    /// </summary>
    [SerializeField] private float startPosition;

    [SerializeField] public float levelMovementSpeed = 1f;

    [SerializeField] private float spawnerCount = 9;
    private float spawnerTimer;

    private static LevelManager instance;

    private Vector3 spawnPoint;

    public static LevelManager Instance
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

        spawnerTimer = spawnerCount;
        spawnPoint = new Vector3(0, 0, startPosition);
    }

    private void Update()
    {
        spawnerTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (spawnerTimer < 0)
        {
            GameObject newLevelBlock = possibleLevels[Random.Range(0, possibleLevels.Length)];
            Instantiate(newLevelBlock, spawnPoint, gameObject.transform.rotation, gameObject.transform);
            spawnerTimer = spawnerCount;
        }
    }
}
