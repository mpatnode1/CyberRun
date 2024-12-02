using UnityEngine;

public class CameraContainer : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    private void Update()
    {
        //move camera with player movement
        transform.position = cameraPosition.position;
    }
}
