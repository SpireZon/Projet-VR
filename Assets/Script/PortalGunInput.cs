using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PortalGunInput : MonoBehaviour
{
    private PortalGun portalGun;
    private XRGrabInteractable grab;
    private bool isHeld = false;
    private bool triggerWasPressed = false;
    private InputDevice controller;

    void Start()
    {
        portalGun = GetComponent<PortalGun>();
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void Update()
    {
        if (!isHeld) return;

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && !triggerWasPressed)
        {
            portalGun.Shoot();
        }

        triggerWasPressed = triggerPressed;
    }
}
