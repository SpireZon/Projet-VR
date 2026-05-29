using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform destination;
    private bool isTeleporting = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isTeleporting) return;

        destination.GetComponent<PortalTeleporter>().isTeleporting = true;

        Transform player = other.transform.root;
        player.position = destination.position + destination.forward * 1.5f;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isTeleporting = false;
    }
}
