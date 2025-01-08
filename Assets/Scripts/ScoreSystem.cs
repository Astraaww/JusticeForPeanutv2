using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;

    public int currentScore = 0;

    //Augmente le score
    public void AugmenteScore()
    {
        currentScore += 1;
        scoreText.SetText(currentScore.ToString());
        //Modifie le texte du canva
    }
}