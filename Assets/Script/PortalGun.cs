using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject portalA;
    public GameObject portalB;
    public Transform rayOrigin;

    private int nextPortal = 0;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, 100f))
        {
            if (nextPortal == 0)
            {
                portalA.SetActive(true);
                portalA.transform.position = hit.point + hit.normal * 0.01f;
                portalA.transform.rotation = Quaternion.LookRotation(-hit.normal);
                nextPortal = 1;
            }
            else
            {
                portalB.SetActive(true);
                portalB.transform.position = hit.point + hit.normal * 0.01f;
                portalB.transform.rotation = Quaternion.LookRotation(-hit.normal);
                nextPortal = 0;
            }
        }
    }
}
