using UnityEngine;

public enum DeviceType { Router, PC, Switch }

public class DevicePort : MonoBehaviour
{
    public int portIndex;
    public GameObject led;
    public GameObject ownerDevice;
    public DeviceType deviceType;
    public PCDevice pcDevice = null;

    public NetworkCable connectedCable = null;

    private Renderer ledRenderer;

    void Start()
    {
        if (led != null)
            ledRenderer = led.GetComponent<Renderer>();
        UpdateStatus();
    }

    public void ConnectCable(NetworkCable cable)
    {
        if (cable == null) return;
        connectedCable = cable;
        UpdateStatus();
        Debug.Log($"{deviceType} port {portIndex} : câble branché !");
    }

    public void DisconnectCable()
    {
        if (deviceType == DeviceType.Router)
            NotifySwitchInternet(false);

        connectedCable = null;
        UpdateStatus();
        Debug.Log($"{deviceType} port {portIndex} : câble débranché !");
    }

    public void UpdateStatus()
    {
        if (connectedCable == null)
        {
            SetLED(Color.black, false);
            if (deviceType == DeviceType.PC && pcDevice != null)
                pcDevice.SetStatus(false);
            return;
        }

        if (deviceType == DeviceType.Router)
        {
            NotifySwitchInternet(true);
            SetLED(Color.green, true);
        }
        else if (deviceType == DeviceType.PC)
        {
            bool hasInternet = CheckSwitchInternet();
            SetLED(hasInternet ? Color.green : Color.red, true);
            if (pcDevice != null)
                pcDevice.SetStatus(hasInternet);
        }
    }

    void NotifySwitchInternet(bool state)
    {
        if (connectedCable == null) return;

        SwitchPort otherPort = GetOtherSwitchPort();
        if (otherPort != null && otherPort.networkSwitch != null)
            otherPort.networkSwitch.SetInternet(state);
    }

    SwitchPort GetOtherSwitchPort()
    {
        if (connectedCable == null) return null;

        if (connectedCable.connectedPortA != null)
            return connectedCable.connectedPortA;

        if (connectedCable.connectedPortB != null)
            return connectedCable.connectedPortB;

        return null;
    }

    bool CheckSwitchInternet()
    {
        if (connectedCable == null) return false;

        SwitchPort sp = GetOtherSwitchPort();
        if (sp != null && sp.networkSwitch != null)
            return sp.networkSwitch.hasInternet;

        return false;
    }

    void SetLED(Color color, bool emissive)
    {
        if (ledRenderer == null) return;
        ledRenderer.material.color = color;
        if (emissive)
        {
            ledRenderer.material.EnableKeyword("_EMISSION");
            ledRenderer.material.SetColor("_EmissionColor", color * 2f);
        }
        else
        {
            ledRenderer.material.DisableKeyword("_EMISSION");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CableConnector connector = other.GetComponent<CableConnector>();
        if (connector == null) return;
        if (connectedCable != null) return;
        if (connector.connectedDevicePort != null) return;

        connector.cable.ConnectToDevicePort(this, connector);
    }

    void OnTriggerExit(Collider other)
    {
        CableConnector connector = other.GetComponent<CableConnector>();
        if (connector == null) return;
        if (connectedCable == connector.cable)
            connector.cable.DisconnectFromDevicePort(this, connector);
    }
}