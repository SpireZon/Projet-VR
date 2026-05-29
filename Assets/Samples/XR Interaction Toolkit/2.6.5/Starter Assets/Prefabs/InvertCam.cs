using UnityEngine;

public class InvertCamera : MonoBehaviour
{
    private Quaternion originalRotation;

    void LateUpdate()
    {
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Euler(
            -current.eulerAngles.x,
            current.eulerAngles.y + 180f,
            -current.eulerAngles.z
        );
    }
}