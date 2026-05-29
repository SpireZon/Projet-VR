using UnityEngine;

public class CableConnector : MonoBehaviour
{
    public NetworkCable cable;
    public bool isConnectorA;
    public SwitchPort connectedPort = null;
    public DevicePort connectedDevicePort = null;
}