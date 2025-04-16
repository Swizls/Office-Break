using UnityEngine;

public class CopyCameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, _cameraTransform.eulerAngles.y, transform.eulerAngles.z);        
    }
}