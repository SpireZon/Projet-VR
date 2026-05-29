using UnityEngine;

public class VRWallCollision : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private Camera vrCamera;

    void Start()
    {
        vrCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Repositionne le character controller sous la caméra
        Vector3 headPos = vrCamera.transform.localPosition;
        characterController.center = new Vector3(headPos.x, characterController.center.y, headPos.z);
    }
}
