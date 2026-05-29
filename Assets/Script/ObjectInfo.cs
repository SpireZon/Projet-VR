using UnityEngine;
using TMPro;

public class ObjectInfo : MonoBehaviour
{
    [Header("Informations de l'objet")]
    public string objectName = "Nom de l'objet";
    public string objectDescription = "Description de l'objet";

    [Header("UI")]
    public GameObject infoPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private bool isVisible = false;

    public void ToggleInfo()
    {
        isVisible = !isVisible;
        infoPanel.SetActive(isVisible);

        if (isVisible)
        {
            nameText.text = objectName;
            descriptionText.text = objectDescription;
        }
    }
}