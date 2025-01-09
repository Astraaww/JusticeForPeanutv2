using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Chrono : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText; //text pour le timer

    [Header("Timer Settings")]
    public float currentTime; //le temps actuel
    public float gameDuration = 10; //la durée de la partie
    public bool countDown; 

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit; //limit du timer au dela duquel il ne décrémente plus
    public bool isEnded = false;

    [Header("scripts")]
    private EndMenu endMenu;
    private ScoreSystem scoreSystem;
    private PanierManager panierManager;


    public void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>(); // accède au script de ScoreSystem, PanierManager et endMenu
        panierManager = FindObjectOfType<PanierManager>();
        endMenu = FindObjectOfType<EndMenu>();

        currentTime = gameDuration; //temps actuel égale au temps de la game
    }
    public void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime; //permet de faire décrémenter le timer

        if (hasLimit && ((countDown && currentTime <= timerLimit)))  //si le timer atteind 0
        {
            currentTime = timerLimit; //timer = 0
            timerText.color = Color.red; //on passe le text en rouge
            enabled = false; //on désactive le chrono
            SetTimerText(); //met a jour l'affichage du timer
            endMenu.GameEnded(); //affiche le menu de fin
        }

        SetTimerText();
    }

    public void SetTimerText() //Affiche le chrono + gère le nb de décimal affichés
    {
        timerText.text = currentTime.ToString("0.00");
    }
}
