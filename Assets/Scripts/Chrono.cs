using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Chrono : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText; //text pour le timer

    [Header("Timer Settings")]
    public float currentTime; //le temps actuel
    public float gameDuration = 10; //la dur�e de la partie
    public bool countDown; 

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit; //limit du timer au dela duquel il ne d�cr�mente plus
    public bool isEnded = false;

    [Header("scripts")]
    private EndMenu endMenu;
    private ScoreSystem scoreSystem;
    private PanierManager panierManager;


    public void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>(); // acc�de au script de ScoreSystem, PanierManager et endMenu
        panierManager = FindObjectOfType<PanierManager>();
        endMenu = FindObjectOfType<EndMenu>();

        currentTime = gameDuration; //temps actuel �gale au temps de la game
    }
    public void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime; //permet de faire d�cr�menter le timer

        if (hasLimit && ((countDown && currentTime <= timerLimit)))  //si le timer atteind 0
        {
            currentTime = timerLimit; //timer = 0
            timerText.color = Color.red; //on passe le text en rouge
            enabled = false; //on d�sactive le chrono
            SetTimerText(); //met a jour l'affichage du timer
            endMenu.GameEnded(); //affiche le menu de fin
        }

        SetTimerText();
    }

    public void SetTimerText() //Affiche le chrono + g�re le nb de d�cimal affich�s
    {
        timerText.text = currentTime.ToString("0.00");
    }
}
