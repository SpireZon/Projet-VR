// FallingBit.cs — à attacher automatiquement par BinaryRain
using UnityEngine;

public class FallingBit : MonoBehaviour
{
    public float speed = 1.5f;
    float resetHeight = -10f;
    float startHeight;

    void Start() => startHeight = transform.position.y + 30f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < resetHeight)
        {
            Vector3 p = transform.position;
            p.y = startHeight;
            transform.position = p;
        }
    }
}