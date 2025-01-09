using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;

    public int currentScore;
    public int startingScore;

    [Header("Sound")]
    public AudioSource source;
    public AudioClip clip;

    private void Awake() 
    {
        startingScore = 0;
        currentScore = startingScore;
    }
    public void AugmenteScore()
    {
        currentScore += 1; //incrémente le score
        SetScoreText();
        source.PlayOneShot(clip);
    }

    public void SetScoreText()
    {
        scoreText.SetText(currentScore.ToString()); //Modifie le texte du canva
    }
}