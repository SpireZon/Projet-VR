using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkCable : MonoBehaviour
{
    [Header("Config")]
    public Color cableColor = Color.blue;
    public bool isActive = false;

    public SwitchPort connectedPortA = null;
    public SwitchPort connectedPortB = null;
    public DevicePort connectedDevicePortA = null;
    public DevicePort connectedDevicePortB = null;

    private LineRenderer lineRenderer;
    public GameObject connectorA;
    public GameObject connectorB;

    void Start()
    {
        GenerateCable();
        SetupLineRenderer();
        StartCoroutine(SimulateNetworkActivity());
    }

    void Update()
    {
        if (lineRenderer != null && connectorA != null && connectorB != null)
        {
            lineRenderer.SetPosition(0, connectorA.transform.position);
            lineRenderer.SetPosition(1, connectorB.transform.position);
        }
    }

    void GenerateCable()
    {
        connectorA = CreateConnector("ConnectorA", new Vector3(-0.3f, 0f, 0f), true);
        connectorB = CreateConnector("ConnectorB", new Vector3(0.3f, 0f, 0f), false);
    }

    GameObject CreateConnector(string connectorName, Vector3 localPos, bool isA)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = connectorName;
        obj.transform.parent = transform;
        obj.transform.localScale = new Vector3(0.08f, 0.06f, 0.04f);
        obj.transform.localPosition = localPos;

        Renderer r = obj.GetComponent<Renderer>();
        if (r != null)
        {
            r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            r.material.color = cableColor;
        }

        // Collider NON trigger pour pouvoir grab
        BoxCollider col = obj.GetComponent<BoxCollider>();
        if (col != null) col.isTrigger = false;

        Rigidbody rb = obj.AddComponent<Rigidbody>();
        rb.mass = 0.1f;
        rb.drag = 10f;
        rb.useGravity = false;
        rb.isKinematic = false; // false pour permettre le grab

        XRGrabInteractable grab = obj.AddComponent<XRGrabInteractable>();
        grab.throwOnDetach = false;
        grab.trackPosition = true;
        grab.trackRotation = false;

        // Events grab : désactiver gravité au grab et au relâche
        grab.selectEntered.AddListener((args) =>
        {
            Rigidbody r2 = obj.GetComponent<Rigidbody>();
            if (r2 != null)
            {
                r2.useGravity = false;
                r2.isKinematic = false;
            }
        });

        grab.selectExited.AddListener((args) =>
        {
            Rigidbody r2 = obj.GetComponent<Rigidbody>();
            if (r2 != null)
            {
                r2.useGravity = false;
                r2.velocity = Vector3.zero;
                r2.angularVelocity = Vector3.zero;
            }
        });

        CableConnector cc = obj.AddComponent<CableConnector>();
        cc.cable = this;
        cc.isConnectorA = isA;

        return obj;
    }

    void SetupLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = true;

        // Shader qui marche sans URP spécifique
        Shader shader = Shader.Find("Sprites/Default");
        if (shader == null) shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null) shader = Shader.Find("Unlit/Color");

        Material mat = new Material(shader);
        mat.color = cableColor;
        lineRenderer.material = mat;
        lineRenderer.startColor = cableColor;
        lineRenderer.endColor = cableColor;
    }

    // ---- Connexion Switch ----
    public void ConnectToPort(SwitchPort port, CableConnector connector)
    {
        if (port == null || connector == null) return;

        if (connector.isConnectorA)
            connectedPortA = port;
        else
            connectedPortB = port;

        connector.connectedPort = port;
        port.ConnectCable(this);
        SnapAndFreeze(connector, port.transform.position);
    }

    public void DisconnectFromPort(SwitchPort port, CableConnector connector)
    {
        if (port == null || connector == null) return;

        if (connector.isConnectorA)
            connectedPortA = null;
        else
            connectedPortB = null;

        connector.connectedPort = null;
        port.DisconnectCable();
        Unfreeze(connector);
    }

    // ---- Connexion Device ----
    public void ConnectToDevicePort(DevicePort port, CableConnector connector)
    {
        if (port == null || connector == null) return;

        if (connector.isConnectorA)
            connectedDevicePortA = port;
        else
            connectedDevicePortB = port;

        connector.connectedDevicePort = port;
        port.ConnectCable(this);
        SnapAndFreeze(connector, port.transform.position);
    }

    public void DisconnectFromDevicePort(DevicePort port, CableConnector connector)
    {
        if (port == null || connector == null) return;

        if (connector.isConnectorA)
            connectedDevicePortA = null;
        else
            connectedDevicePortB = null;

        connector.connectedDevicePort = null;
        port.DisconnectCable();
        Unfreeze(connector);
    }

    void SnapAndFreeze(CableConnector connector, Vector3 position)
    {
        if (connector == null) return;
        connector.transform.position = position;
        Rigidbody rb = connector.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
    }

    void Unfreeze(CableConnector connector)
    {
        if (connector == null) return;
        Rigidbody rb = connector.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }

    public void NotifyPCUpdate()
    {
        connectedDevicePortA?.UpdateStatus();
        connectedDevicePortB?.UpdateStatus();
    }

    IEnumerator SimulateNetworkActivity()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));

            bool switchConnected = connectedPortA != null && connectedPortB != null;
            bool deviceConnected = (connectedPortA != null || connectedPortB != null)
                                && (connectedDevicePortA != null || connectedDevicePortB != null);

            if (switchConnected || deviceConnected)
            {
                if (CheckInternetInChain())
                {
                    isActive = true;
                    connectedPortA?.UpdateLED();
                    connectedPortB?.UpdateLED();

                    yield return new WaitForSeconds(0.3f);

                    isActive = false;
                    connectedPortA?.UpdateLED();
                    connectedPortB?.UpdateLED();
                }
            }
        }
    }

    bool CheckInternetInChain()
    {
        if (connectedPortA != null && connectedPortA.networkSwitch != null)
            if (connectedPortA.networkSwitch.hasInternet) return true;

        if (connectedPortB != null && connectedPortB.networkSwitch != null)
            if (connectedPortB.networkSwitch.hasInternet) return true;

        if (connectedDevicePortA != null && connectedDevicePortA.deviceType == DeviceType.Router)
            return true;

        if (connectedDevicePortB != null && connectedDevicePortB.deviceType == DeviceType.Router)
            return true;

        return false;
    }
}
