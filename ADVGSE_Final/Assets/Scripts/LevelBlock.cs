using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * LevelManager.Instance.levelMovementSpeed;
    }
}
