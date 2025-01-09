using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText; //text pour le score
    public TextMeshProUGUI scoreText; //la je créer un truc qui va me permettre de stocket un tmp qui s'appelle score text

    [Header("End Canvas")]
    public Canvas canvasInGame;
    public Canvas canvasEndMenu;

    [Header("End Camera")]
    public Camera cameraInGame;
    public Camera cameraMenu;

    [Header("scripts")]
    private ScoreSystem scoreSystem;
    private PanierManager panierManager;
    private Chrono chrono;

    public int bestScore;

    private void Awake()
    {
        canvasEndMenu.enabled = false; //Disable le canvas de menu de fin de jeu

        chrono = FindObjectOfType<Chrono>(); //trouve les objets qui contiennent chrono et scoreSystem
        scoreSystem = FindObjectOfType<ScoreSystem>();
        panierManager = FindObjectOfType<PanierManager>();

        bestScore = 0; //au lancement du jeu le meilleur score est 0
    }

    public void GameEnded()
    {

        if (scoreSystem.currentScore > bestScore) //met à jour la valeur de bestScore si elle est inférieur au score de la partie
        {
            bestScore = scoreSystem.currentScore;
        }

        scoreText.text = ("Score : " + scoreSystem.currentScore.ToString()); 
        bestScoreText.text = ("Meilleur Score : " + bestScore.ToString()); //affichage du score et du meilleur score

        canvasInGame.enabled = false; //enable le canvas de fin de jeu et disable le canvas avec l'ui de jeu
        canvasEndMenu.enabled = true;

        cameraInGame.enabled = false; //enable la camera des menus et disable la camera du jeu
        cameraMenu.enabled = true;

        Cursor.visible = true; //Fait réapparaitre la souris mais l'empêche de sortir de l'écran
        Cursor.lockState = CursorLockMode.Confined;

        chrono.isEnded = true;

        Time.timeScale = 0f; //arrête le temps
    }


    public void RestartGame()
    {
        chrono.currentTime = chrono.gameDuration; //reset le timer, les points et la position du panier 
        scoreSystem.currentScore = scoreSystem.startingScore; 
        panierManager.transform.position = panierManager.startPos; 

        Time.timeScale = 1f; //remet le temps en marche

        cameraInGame.enabled = true; //Disable camera fin de game et enable celle de jeu 
        cameraMenu.enabled = false;

        canvasInGame.enabled = true; //Disable canvas fin de game et enable celui de jeu 
        canvasEndMenu.enabled = false;

        Cursor.visible = false; //fait disparaitre le curseur et le lock au milieu de l'écran
        Cursor.lockState = CursorLockMode.Locked;

        chrono.timerText.color = Color.white; //change couleur du chrono

        chrono.enabled = true; // Réactive le script Chrono pour relancer le timer
        chrono.isEnded = false;

        chrono.SetTimerText();
        scoreSystem.SetScoreText();
    }

    public void QuitGame() //quitte le jeu
    {
        Application.Quit();
    }

    public void GoToMainMenu() //load la scène de menu principal
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UISene");
    }
}
