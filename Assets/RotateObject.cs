using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 30f;
    public Vector3 rotationAxis = Vector3.up;
    
    private bool isRotating = true;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}