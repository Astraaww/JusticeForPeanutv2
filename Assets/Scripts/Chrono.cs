using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Chrono : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;
    public bool isEnded = false;

    [Header("End Canvas")]
    public Canvas canvasToDisable;
    public Canvas canvasToEnable;

    [Header("End Camera")]
    public Camera cameraToDisable;
    public Camera cameraToEnable;

    private ScoreSystem scoreSystem;
    private PanierManager panierManager;

    public int bestScore = 0;

    [Header("sound")]
    public AudioClip ambiantMusic;
    private AudioSource perso_AudioSource;

    private void Start()
    {
        canvasToEnable.enabled = false; //Disable le canvas de menu de fin de jeu

        scoreSystem = FindObjectOfType<ScoreSystem>(); // accède au script de ScoreSystem et PanierManager
        panierManager = FindObjectOfType<PanierManager>();
    }
    private void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime; //permet de faire décrémenter le timer

        if (hasLimit && ((countDown && currentTime <= timerLimit)))  //si le timer atteind 0
        {
            currentTime = timerLimit; 
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;

            if(scoreSystem.currentScore > bestScore)
            {
                bestScore = scoreSystem.currentScore;
            }

            scoreText.text = ("Score : " + scoreSystem.currentScore.ToString());
            bestScoreText.text = ("Meilleur Score : " + bestScore.ToString());

            canvasToDisable.enabled = false; //enable le canvas de fin de jeu et disable le canvas avec l'ui de jeu
            canvasToEnable.enabled = true;

            cameraToDisable.enabled = false; //enable la camera des menus et disable la camera du jeu
            cameraToEnable.enabled = true;

            Cursor.visible = true; //Fait réapparaitre la souris mais l'empêche de sortir de l'écran
            Cursor.lockState = CursorLockMode.Confined;

            isEnded = true;

            Time.timeScale = 0f; //arrête le temps
        }
        SetTimerText();
    }

    private void SetTimerText() //Affiche le chrono + gère le nb de décimal affichés
    {
        timerText.text = currentTime.ToString("0.00");
    }

    public void RestartGame()
    {
        currentTime = 0f; //reset le timer
        scoreSystem.currentScore = 0; //reset les points
        panierManager.transform.position = panierManager.startPos; //reset la positions du panier
        Time.timeScale = 1f; //remet le temps en marche
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
