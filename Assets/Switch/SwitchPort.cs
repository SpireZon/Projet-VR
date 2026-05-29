using UnityEngine;

public class SwitchPort : MonoBehaviour
{
    public int portIndex;
    public GameObject led;
    public NetworkSwitch networkSwitch;
    public NetworkCable connectedCable = null;

    private Renderer ledRenderer;

    private Color colorOff        = Color.black;
    private Color colorConnected  = Color.green;
    private Color colorActive     = Color.yellow;
    private Color colorNoInternet = Color.red;

    void Start()
    {
        if (led != null)
            ledRenderer = led.GetComponent<Renderer>();
        UpdateLED();
    }

    public void ConnectCable(NetworkCable cable)
    {
        if (cable == null) return;
        connectedCable = cable;
        UpdateLED();
        Debug.Log($"Port {portIndex} : câble branché !");
    }

    public void DisconnectCable()
    {
        connectedCable = null;
        UpdateLED();
        Debug.Log($"Port {portIndex} : câble débranché !");
    }

    public void UpdateLED()
    {
        if (ledRenderer == null) return;
        if (networkSwitch == null) return;

        if (connectedCable == null)
            SetLED(colorOff, false);
        else if (!networkSwitch.hasInternet)
            SetLED(colorNoInternet, true);
        else if (connectedCable.isActive)
            SetLED(colorActive, true);
        else
            SetLED(colorConnected, true);
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
        if (connector.connectedPort != null) return;

        connector.cable.ConnectToPort(this, connector);
    }

    void OnTriggerExit(Collider other)
    {
        CableConnector connector = other.GetComponent<CableConnector>();
        if (connector == null) return;
        if (connectedCable == connector.cable)
            connector.cable.DisconnectFromPort(this, connector);
    }
}