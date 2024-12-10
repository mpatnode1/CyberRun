using Unity.VisualScripting;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}
