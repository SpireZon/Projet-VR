using UnityEngine;

public class ComputerBuilder : MonoBehaviour
{
    void Start()
    {
        CreateComputer(new Vector3(-3, 0.5f, 10));
CreateComputer(new Vector3(0, 0.5f, 10));
CreateComputer(new Vector3(3, 0.5f, 10));
    }

    void CreateComputer(Vector3 position)
    {
        // Tour
        GameObject tower = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tower.name = "Tower";
        tower.transform.position = new Vector3(position.x, position.y + 0.5f, position.z);
        tower.transform.localScale = new Vector3(0.4f, 1f, 0.4f);
        SetColor(tower, new Color(0.2f, 0.2f, 0.2f));

        // Voyant power vert
        GameObject powerLight = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        powerLight.name = "PowerLED";
        powerLight.transform.position = new Vector3(position.x + 0.18f, position.y + 0.9f, position.z - 0.21f);
        powerLight.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        SetEmissiveColor(powerLight, Color.green);
        AddPointLight(powerLight.transform.position, Color.green, 0.3f, 1.5f);

        // Voyant disque bleu
        GameObject diskLight = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        diskLight.name = "DiskLED";
        diskLight.transform.position = new Vector3(position.x + 0.18f, position.y + 0.8f, position.z - 0.21f);
        diskLight.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        SetEmissiveColor(diskLight, Color.blue);
        AddPointLight(diskLight.transform.position, Color.blue, 0.3f, 1f);

        // Bande LED rouge
        GameObject ledStrip = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ledStrip.name = "LEDStrip";
        ledStrip.transform.position = new Vector3(position.x + 0.21f, position.y + 0.5f, position.z);
        ledStrip.transform.localScale = new Vector3(0.02f, 0.6f, 0.02f);
        SetEmissiveColor(ledStrip, Color.red);
        AddPointLight(ledStrip.transform.position, Color.red, 0.5f, 2f);

        // Ecran
        GameObject screen = GameObject.CreatePrimitive(PrimitiveType.Cube);
        screen.name = "Screen";
        screen.transform.position = new Vector3(position.x, position.y + 1.2f, position.z + 0.5f);
        screen.transform.localScale = new Vector3(0.8f, 0.5f, 0.05f);
        SetColor(screen, new Color(0.1f, 0.1f, 0.1f));

        // Ecran allumé
        GameObject screenDisplay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        screenDisplay.name = "ScreenDisplay";
        screenDisplay.transform.position = new Vector3(position.x, position.y + 1.2f, position.z + 0.52f);
        screenDisplay.transform.localScale = new Vector3(0.7f, 0.4f, 0.01f);
        SetEmissiveColor(screenDisplay, new Color(0.0f, 0.5f, 1.0f));
        AddPointLight(screenDisplay.transform.position, new Color(0.0f, 0.5f, 1.0f), 0.8f, 2f);

        // Pied écran
        GameObject stand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        stand.name = "Stand";
        stand.transform.position = new Vector3(position.x, position.y + 0.85f, position.z + 0.5f);
        stand.transform.localScale = new Vector3(0.05f, 0.3f, 0.05f);
        SetColor(stand, new Color(0.2f, 0.2f, 0.2f));

        // Clavier
        GameObject keyboard = GameObject.CreatePrimitive(PrimitiveType.Cube);
        keyboard.name = "Keyboard";
        keyboard.transform.position = new Vector3(position.x, position.y + 0.02f, position.z + 0.8f);
        keyboard.transform.localScale = new Vector3(0.6f, 0.03f, 0.2f);
        SetColor(keyboard, new Color(0.15f, 0.15f, 0.15f));
        AddPointLight(keyboard.transform.position + Vector3.up * 0.05f, new Color(0f, 0.4f, 1f), 0.3f, 0.8f);

        // Souris
        GameObject mouse = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mouse.name = "Mouse";
        mouse.transform.position = new Vector3(position.x + 0.5f, position.y + 0.02f, position.z + 0.8f);
        mouse.transform.localScale = new Vector3(0.1f, 0.03f, 0.15f);
        SetColor(mouse, new Color(0.15f, 0.15f, 0.15f));
        AddPointLight(mouse.transform.position + Vector3.up * 0.05f, Color.red, 0.1f, 0.5f);
    }

    void SetColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
mat.color = color;
mat.SetFloat("_Metallic", 0.8f);
mat.SetFloat("_Smoothness", 0.5f);
renderer.material = mat;
        }
    }

    void SetEmissiveColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = color;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", color * 2f);
            renderer.material = mat;
        }
    }

    void AddPointLight(Vector3 position, Color color, float range, float intensity)
    {
        GameObject lightObj = new GameObject("PointLight");
        lightObj.transform.position = position;
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = color;
        light.range = range;
        light.intensity = intensity;
    }
}