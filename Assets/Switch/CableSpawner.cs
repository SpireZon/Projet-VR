using UnityEngine;

public class CableSpawner : MonoBehaviour
{
    public int numberOfCables = 3;
    public Color[] cableColors = { Color.blue, Color.yellow, Color.red };

    void Start()
    {
        for (int i = 0; i < numberOfCables; i++)
        {
            GameObject cableObj = new GameObject("NetworkCable_" + i);
            cableObj.transform.position = new Vector3(i * 0.5f - 0.5f, 1f, 0f);

            NetworkCable cable = cableObj.AddComponent<NetworkCable>();
            cable.cableColor = i < cableColors.Length ? cableColors[i] : Color.white;
        }
    }
}