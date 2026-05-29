using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MatrixEffect : MonoBehaviour
{
    [Header("Paramètres")]
    public float vitesse = 2f;
    public Color couleurChiffres = Color.green;

    private List<GameObject> chiffresActifs = new List<GameObject>();
    private string caracteres = "01アイウエオカキクケコ0110";

    void Start()
    {
        // Mur Avant (Z = 12.5, X de -5 à 5)
        for (float x = -4.5f; x <= 4.5f; x += 1.5f)
            StartCoroutine(SpawnColonne(new Vector3(x, 5f, 12.2f), 180f, Random.Range(0f, 1f)));

        // Mur Arrière (Z = -12.5, X de -5 à 5)
        for (float x = -4.5f; x <= 4.5f; x += 1.5f)
            StartCoroutine(SpawnColonne(new Vector3(x, 5f, -12.2f), 0f, Random.Range(0f, 1f)));

        // Mur Gauche (X = -5, Z de -12 à 12)
        for (float z = -11f; z <= 11f; z += 2f)
            StartCoroutine(SpawnColonne(new Vector3(-4.8f, 5f, z), 90f, Random.Range(0f, 1f)));

        // Mur Droit (X = 5, Z de -12 à 12)
        for (float z = -11f; z <= 11f; z += 2f)
            StartCoroutine(SpawnColonne(new Vector3(4.8f, 5f, z), -90f, Random.Range(0f, 1f)));
    }

    IEnumerator SpawnColonne(Vector3 positionDepart, float rotationY, float delai)
    {
        yield return new WaitForSeconds(delai);

        while (true)
        {
            Vector3 pos = positionDepart;

            for (int i = 0; i < 8; i++)
            {
                GameObject chiffre = new GameObject("Chiffre");
                chiffre.transform.position = pos;
                chiffre.transform.rotation = Quaternion.Euler(0, rotationY, 0);

                TextMeshPro tmp = chiffre.AddComponent<TextMeshPro>();
                tmp.text = caracteres[Random.Range(0, caracteres.Length)].ToString();
                tmp.fontSize = 3f;
                tmp.color = CouleurActuelle(i);
                tmp.alignment = TextAlignmentOptions.Center;

                chiffresActifs.Add(chiffre);
                StartCoroutine(FaireDescendre(chiffre, vitesse));

                pos += Vector3.down * 0.6f;
                yield return new WaitForSeconds(0.08f);
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    Color CouleurActuelle(int index)
    {
        if (index == 0) return Color.white;
        float alpha = 1f - (index / 10f);
        return new Color(couleurChiffres.r, couleurChiffres.g, couleurChiffres.b, alpha);
    }

    IEnumerator FaireDescendre(GameObject chiffre, float vitesse)
    {
        float duree = 3f;
        float temps = 0f;

        while (temps < duree && chiffre != null)
        {
            if (chiffre != null)
                chiffre.transform.position += Vector3.down * vitesse * Time.deltaTime;
            temps += Time.deltaTime;
            yield return null;
        }

        if (chiffre != null)
        {
            chiffresActifs.Remove(chiffre);
            Destroy(chiffre);
        }
    }
}