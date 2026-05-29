using UnityEngine;

public class PCDevice : MonoBehaviour
{
    [Header("Config")]
    public int portCount = 1;

    private DevicePort networkPort;
    private GameObject statusLight;
    private Renderer statusRenderer;

    void Start()
    {
        GeneratePC();
    }

    void GeneratePC()
    {
        // Tour PC
        GameObject tower = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tower.transform.parent = transform;
        tower.transform.localPosition = Vector3.zero;
        tower.transform.localScale = new Vector3(0.4f, 0.8f, 0.4f);

        Renderer r = tower.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        r.material.color = new Color(0.85f, 0.85f, 0.85f);

        // Lumière de statut réseau
        statusLight = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        statusLight.transform.parent = transform;
        statusLight.transform.localPosition = new Vector3(0.22f, 0.2f, 0.15f);
        statusLight.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);

        statusRenderer = statusLight.GetComponent<Renderer>();
        statusRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        SetStatus(false);

        CreateNetworkPort();
    }

    void CreateNetworkPort()
    {
        GameObject portObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        portObj.transform.parent = transform;
        portObj.transform.localScale = new Vector3(0.08f, 0.06f, 0.05f);
        portObj.transform.localPosition = new Vector3(0.22f, -0.1f, 0.15f);

        Renderer pr = portObj.GetComponent<Renderer>();
        pr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        pr.material.color = new Color(0.1f, 0.1f, 0.1f);

        BoxCollider bc = portObj.GetComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.size = new Vector3(1.5f, 1.5f, 1.5f);

        // LED port
        GameObject led = GameObject.CreatePrimitive(PrimitiveType.Cube);
        led.transform.parent = transform;
        led.transform.localScale = new Vector3(0.04f, 0.02f, 0.02f);
        led.transform.localPosition = new Vector3(0.22f, -0.06f, 0.15f);

        Renderer lr = led.GetComponent<Renderer>();
        lr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        networkPort = portObj.AddComponent<DevicePort>();
        networkPort.led = led;
        networkPort.portIndex = 0;
        networkPort.ownerDevice = gameObject;
        networkPort.deviceType = DeviceType.PC;
        networkPort.pcDevice = this;
    }

    public void SetStatus(bool connected)
    {
        if (statusRenderer == null) return;

        if (connected)
        {
            statusRenderer.material.color = Color.green;
            statusRenderer.material.EnableKeyword("_EMISSION");
            statusRenderer.material.SetColor("_EmissionColor", Color.green * 2f);
            Debug.Log("PC : CONNECTÉ à internet !");
        }
        else
        {
            statusRenderer.material.color = Color.red;
            statusRenderer.material.EnableKeyword("_EMISSION");
            statusRenderer.material.SetColor("_EmissionColor", Color.red * 1.5f);
            Debug.Log("PC : PAS de connexion !");
        }
    }
}
