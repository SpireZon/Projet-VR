using UnityEngine;
using System.Collections.Generic;

public class Router : MonoBehaviour
{
    [Header("Config")]
    public int portCount = 4;

    [HideInInspector]
    public List<DevicePort> ports = new List<DevicePort>();

    void Start()
    {
        GenerateRouter();
    }

    void GenerateRouter()
    {
        // Corps du router
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.transform.parent = transform;
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(1f, 0.2f, 0.6f);

        Renderer r = body.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        r.material.color = new Color(0.1f, 0.3f, 0.6f); // Bleu pour le router

        // Antennes décoratives
        CreateAntenna(new Vector3(-0.3f, 0.2f, 0.2f));
        CreateAntenna(new Vector3(0.3f, 0.2f, 0.2f));

        // Label
        Debug.Log("Router créé avec " + portCount + " ports");

        // Ports
        for (int i = 0; i < portCount; i++)
        {
            CreatePort(i);
        }
    }

    void CreateAntenna(Vector3 localPos)
    {
        GameObject antenna = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        antenna.transform.parent = transform;
        antenna.transform.localPosition = localPos;
        antenna.transform.localScale = new Vector3(0.03f, 0.15f, 0.03f);

        Renderer r = antenna.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        r.material.color = new Color(0.15f, 0.15f, 0.15f);
    }

    void CreatePort(int index)
    {
        float spacing = 0.8f / portCount;
        float startX = -0.4f + spacing / 2;

        GameObject portObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        portObj.transform.parent = transform;
        portObj.transform.localScale = new Vector3(0.08f, 0.06f, 0.05f);
        portObj.transform.localPosition = new Vector3(
            startX + index * spacing,
            0.08f,
            -0.28f
        );

        Renderer pr = portObj.GetComponent<Renderer>();
        pr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        pr.material.color = new Color(0.1f, 0.1f, 0.1f);

        BoxCollider bc = portObj.GetComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.size = new Vector3(1.5f, 1.5f, 1.5f);

        // LED du port
        GameObject led = GameObject.CreatePrimitive(PrimitiveType.Cube);
        led.transform.parent = transform;
        led.transform.localScale = new Vector3(0.04f, 0.02f, 0.02f);
        led.transform.localPosition = new Vector3(
            startX + index * spacing,
            0.12f,
            -0.28f
        );

        Renderer lr = led.GetComponent<Renderer>();
        lr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        lr.material.color = Color.black;

        DevicePort dp = portObj.AddComponent<DevicePort>();
        dp.led = led;
        dp.portIndex = index;
        dp.ownerDevice = gameObject;
        dp.deviceType = DeviceType.Router;
        ports.Add(dp);
    }
}