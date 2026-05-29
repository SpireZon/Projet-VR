using UnityEngine;

public class Bouton : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = destination.position;
        }
    }
}
