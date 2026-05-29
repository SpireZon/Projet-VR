using UnityEngine;

public class TableSetup : MonoBehaviour
{
    void Start()
{
    CreateTable(new Vector3(0, 0, 5));
}
    void CreateTable(Vector3 position)
    {
        // --- TABLE ---
        GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
        table.name = "Table";
        table.transform.position = new Vector3(position.x, position.y + 0.5f, position.z);
        table.transform.localScale = new Vector3(2.5f, 0.05f, 1f);
        SetColor(table, new Color(0.1f, 0.1f, 0.1f));

        // Pied table gauche
        GameObject piedG = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piedG.name = "PiedGauche";
        piedG.transform.position = new Vector3(position.x - 1.1f, position.y + 0.25f, position.z);
        piedG.transform.localScale = new Vector3(0.05f, 0.5f, 0.05f);
        SetColor(piedG, new Color(0.1f, 0.1f, 0.1f));

        // Pied table droit
        GameObject piedD = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piedD.name = "PiedDroit";
        piedD.transform.position = new Vector3(position.x + 1.1f, position.y + 0.25f, position.z);
        piedD.transform.localScale = new Vector3(0.05f, 0.5f, 0.05f);
        SetColor(piedD, new Color(0.1f, 0.1f, 0.1f));

        // --- ECRAN ---
        GameObject screen = GameObject.CreatePrimitive(PrimitiveType.Cube);
        screen.name = "Ecran";
        screen.transform.position = new Vector3(position.x, position.y + 1.1f, position.z - 0.3f);
        screen.transform.localScale = new Vector3(0.9f, 0.5f, 0.05f);
        SetColor(screen, new Color(0.05f, 0.05f, 0.05f));

        // Ecran allumé
        GameObject screenDisplay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        screenDisplay.name = "EcranDisplay";
        screenDisplay.transform.position = new Vector3(position.x, position.y + 1.1f, position.z - 0.27f);
        screenDisplay.transform.localScale = new Vector3(0.8f, 0.4f, 0.01f);
        SetEmissiveColor(screenDisplay, new Color(0f, 0.6f, 1f));
        AddPointLight(screenDisplay.transform.position, new Color(0f, 0.6f, 1f), 1f, 1.5f);

        // Pied écran
        GameObject piedEcran = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piedEcran.name = "PiedEcran";
        piedEcran.transform.position = new Vector3(position.x, position.y + 0.75f, position.z - 0.3f);
        piedEcran.transform.localScale = new Vector3(0.05f, 0.5f, 0.05f);
        SetColor(piedEcran, new Color(0.15f, 0.15f, 0.15f));

        // --- CLAVIER ---
        GameObject keyboard = GameObject.CreatePrimitive(PrimitiveType.Cube);
        keyboard.name = "Clavier";
        keyboard.transform.position = new Vector3(position.x - 0.2f, position.y + 0.53f, position.z + 0.1f);
        keyboard.transform.localScale = new Vector3(0.6f, 0.02f, 0.2f);
        SetColor(keyboard, new Color(0.1f, 0.1f, 0.1f));
        AddPointLight(keyboard.transform.position + Vector3.up * 0.05f, new Color(0f, 0.4f, 1f), 0.3f, 0.8f);

        // --- SOURIS ---
        GameObject mouse = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mouse.name = "Souris";
        mouse.transform.position = new Vector3(position.x + 0.5f, position.y + 0.53f, position.z + 0.1f);
        mouse.transform.localScale = new Vector3(0.08f, 0.02f, 0.12f);
        SetColor(mouse, new Color(0.1f, 0.1f, 0.1f));
        AddPointLight(mouse.transform.position + Vector3.up * 0.03f, new Color(0f, 0.4f, 1f), 0.1f, 0.5f);

        // --- TABLETTE ---
        GameObject tablette = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tablette.name = "Tablette";
        tablette.transform.position = new Vector3(position.x - 0.9f, position.y + 0.54f, position.z + 0.1f);
        tablette.transform.localScale = new Vector3(0.2f, 0.01f, 0.28f);
        SetColor(tablette, new Color(0.05f, 0.05f, 0.05f));

        // Ecran tablette allumé
        GameObject tabletteScreen = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabletteScreen.name = "TabletteScreen";
        tabletteScreen.transform.position = new Vector3(position.x - 0.9f, position.y + 0.545f, position.z + 0.1f);
        tabletteScreen.transform.localScale = new Vector3(0.18f, 0.01f, 0.25f);
        SetEmissiveColor(tabletteScreen, new Color(0f, 0.5f, 1f));
        AddPointLight(tabletteScreen.transform.position + Vector3.up * 0.05f, new Color(0f, 0.5f, 1f), 0.3f, 0.8f);

        // --- TOURNEVIS ---
        GameObject tournevis = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tournevis.name = "Tournevis";
        tournevis.transform.position = new Vector3(position.x + 0.9f, position.y + 0.55f, position.z + 0.2f);
        tournevis.transform.localScale = new Vector3(0.03f, 0.15f, 0.03f);
        tournevis.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        SetColor(tournevis, new Color(0.6f, 0.6f, 0.6f));

        // Manche tournevis
        GameObject manche = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        manche.name = "Manche";
        manche.transform.position = new Vector3(position.x + 0.9f, position.y + 0.55f, position.z + 0.32f);
        manche.transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
        manche.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        SetColor(manche, new Color(1f, 0.3f, 0f));

        // --- CABLES ---
        GameObject cable1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cable1.name = "Cable1";
        cable1.transform.position = new Vector3(position.x + 0.7f, position.y + 0.53f, position.z - 0.1f);
        cable1.transform.localScale = new Vector3(0.02f, 0.2f, 0.02f);
        cable1.transform.rotation = Quaternion.Euler(0f, 0f, 80f);
        SetColor(cable1, new Color(0f, 0.5f, 1f));

        GameObject cable2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cable2.name = "Cable2";
        cable2.transform.position = new Vector3(position.x + 0.75f, position.y + 0.53f, position.z - 0.05f);
        cable2.transform.localScale = new Vector3(0.02f, 0.2f, 0.02f);
        cable2.transform.rotation = Quaternion.Euler(0f, 0f, 70f);
        SetColor(cable2, new Color(1f, 0.3f, 0f));

        // --- COMPOSANT (circuit) ---
        GameObject composant = GameObject.CreatePrimitive(PrimitiveType.Cube);
        composant.name = "Composant";
        composant.transform.position = new Vector3(position.x + 0.85f, position.y + 0.54f, position.z + 0.35f);
        composant.transform.localScale = new Vector3(0.15f, 0.02f, 0.1f);
        SetColor(composant, new Color(0f, 0.3f, 0f));

        // Puce composant
        GameObject puce = GameObject.CreatePrimitive(PrimitiveType.Cube);
        puce.name = "Puce";
        puce.transform.position = new Vector3(position.x + 0.85f, position.y + 0.555f, position.z + 0.35f);
        puce.transform.localScale = new Vector3(0.05f, 0.01f, 0.05f);
        SetColor(puce, new Color(0.1f, 0.1f, 0.1f));

        // --- LUMIERES TAMISEES BLEUES ---
        // Lumière principale tamisée
        AddPointLight(new Vector3(position.x, position.y + 2.5f, position.z), new Color(0f, 0.5f, 1f), 5f, 0.8f);

        // Lumière sous la table
        AddPointLight(new Vector3(position.x, position.y + 0.1f, position.z), new Color(0f, 0.3f, 1f), 2f, 0.5f);

        // Lumière côté gauche
        AddPointLight(new Vector3(position.x - 1.5f, position.y + 1f, position.z), new Color(0f, 0.4f, 1f), 3f, 0.6f);

        // Lumière côté droit
        AddPointLight(new Vector3(position.x + 1.5f, position.y + 1f, position.z), new Color(0f, 0.4f, 1f), 3f, 0.6f);
    }

    void SetColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            renderer.material.color = color;
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