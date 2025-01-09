using System.Collections;
using UnityEngine;

public class PanierManager : MonoBehaviour
{
    public bool PanierMit;

    [Header("Color")]
    public Color newColor = Color.green;
    private Renderer rend;
    private Coroutine changeColorCoro;
    private Color originalColor;

    public ScoreSystem scoreSystem;

    public Vector3 startPos;

    //Permet de savoir d�s le lancement quel est le renderer//
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        startPos = transform.position;
    }

    //Foncion et action de la coroutine//
    IEnumerator ChangeColor(Vector3 newPos, float duration, Color newColor)
    {
        if (rend == null)
            yield break;

        yield return new WaitForSeconds(0.3f);

        // Change la couleur � la nouvelle couleur
        rend.material.color = newColor;

        Vector3 startPos = transform.position;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            // D�place le panier
            transform.position = Vector3.Lerp(startPos, newPos, time / duration);

            yield return null;
        }

        transform.position = newPos;

        // Attend un d�lai avant de r�initialiser la couleur
        yield return new WaitForSeconds(0.5f);
        rend.material.color = originalColor; // R�initialise la couleur � l'originale
        changeColorCoro = null; // Lib�re la coroutine
    }

    //la fonction d�clench�e quand le ballon rencontre la zone de collision du panier//
    public void OnDunk()
    {
        if (changeColorCoro != null)
            return;

        if (scoreSystem != null)
        {
            scoreSystem.AugmenteScore();
        }
        //nouvelle position du panier apr�s que le ballon soit rentr�//
        Vector3 newPos = transform.position + Vector3.right * 4;
        changeColorCoro = StartCoroutine(ChangeColor(newPos, 1, newColor));
    }
}
