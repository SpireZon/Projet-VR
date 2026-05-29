using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    [Header("Couleurs")]
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

    private Renderer objectRenderer;
    private bool isHighlighted = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
            objectRenderer.material.color = normalColor;
    }

    public void OnHoverEnter()
    {
        isHighlighted = true;
        if (objectRenderer != null)
            objectRenderer.material.color = highlightColor;
    }

    public void OnHoverExit()
    {
        isHighlighted = false;
        if (objectRenderer != null)
            objectRenderer.material.color = normalColor;
    }
}