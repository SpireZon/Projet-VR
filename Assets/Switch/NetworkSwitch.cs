using UnityEngine;
using System.Collections.Generic;

public class NetworkSwitch : MonoBehaviour
{
    [Header("Config")]
    public int portCount = 8;

    [HideInInspector]
    public List<SwitchPort> ports = new List<SwitchPort>();

    // Est-ce que le switch a internet ? (router branché)
    public bool hasInternet = false;

    void Start()
    {
        GenerateSwitch();
    }

    void GenerateSwitch()
    {
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.transform.parent = transform;
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(2f, 0.3f, 0.8f);

        Renderer r = body.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        r.material.color = new Color(0.2f, 0.2f, 0.2f);

        for (int i = 0; i < portCount; i++)
        {
            CreatePort(i);
        }
    }

    void CreatePort(int index)
    {
        float spacing = 1.8f / portCount;
        float startX = -0.9f + spacing / 2;

        GameObject port = GameObject.CreatePrimitive(PrimitiveType.Cube);
        port.transform.parent = transform;
        port.transform.localScale = new Vector3(0.1f, 0.08f, 0.05f);
        port.transform.localPosition = new Vector3(
            startX + index * spacing,
            0.13f,
            -0.38f
        );

        Renderer pr = port.GetComponent<Renderer>();
        pr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        pr.material.color = new Color(0.1f, 0.1f, 0.1f);

        BoxCollider bc = port.GetComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.size = new Vector3(1.2f, 1.2f, 1.2f);

        // LED
        GameObject led = GameObject.CreatePrimitive(PrimitiveType.Cube);
        led.transform.parent = transform;
        led.transform.localScale = new Vector3(0.05f, 0.03f, 0.03f);
        led.transform.localPosition = new Vector3(
            startX + index * spacing,
            0.17f,
            -0.38f
        );

        Renderer lr = led.GetComponent<Renderer>();
        lr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        SwitchPort sp = port.AddComponent<SwitchPort>();
        sp.led = led;
        sp.portIndex = index;
        sp.networkSwitch = this;
        ports.Add(sp);
    }

    public void SetInternet(bool state)
    {
        // --------------------------------------------------------
        // AJOUT : Sécurité anti-boucle infinie (StackOverflow)
        // Si l'état est déjà le bon, on stoppe la propagation ici.
        // --------------------------------------------------------
        if (hasInternet == state) return;

        hasInternet = state;
        foreach (SwitchPort p in ports)
        {
            p.UpdateLED();
        }
        foreach (SwitchPort p in ports)
        {
            if (p.connectedCable != null)
            {
                p.connectedCable.NotifyPCUpdate();
            }
        }
        Debug.Log($"Switch internet : {state}");
    }
}