using UnityEngine;
using TMPro;

public class BinaryRain : MonoBehaviour
{
    [Header("Paramètres")]
    public int count = 200;
    public float radius = 15f;
    public float fallSpeed = 1.5f;
    public TMP_FontAsset font;

    void Start()
    {
        for (int i = 0; i < count; i++)
            SpawnBit();
    }

    void SpawnBit()
    {
        // Position aléatoire sur un cylindre autour du joueur
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float height = Random.Range(-10f, 20f);
        Vector3 pos = new Vector3(
            Mathf.Cos(angle) * radius,
            height,
            Mathf.Sin(angle) * radius
        );

        GameObject go = new GameObject("Bit");
        go.transform.position = pos;
        go.transform.parent = transform;

        // Texte 3D
        TextMeshPro tmp = go.AddComponent<TextMeshPro>();
        tmp.text = Random.value > 0.5f ? "1" : "0";
        tmp.fontSize = Random.Range(2f, 5f);
        tmp.color = new Color(0f, 1f, 0.9f, Random.Range(0.3f, 1f)); // cyan
        tmp.alignment = TextAlignmentOptions.Center;
        if (font != null) tmp.font = font;

        // Composant de chute
        go.AddComponent<FallingBit>().speed = fallSpeed * Random.Range(0.5f, 2f);
    }
}