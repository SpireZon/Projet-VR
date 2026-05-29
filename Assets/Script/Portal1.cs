using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = exit.position;
        }
    }
}
