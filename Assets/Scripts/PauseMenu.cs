using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    private PlayerInput playerInput;
    private InputAction pause;

    public Camera mainCamera;
    public Camera uiCamera;

    public Canvas canvasToDisable;

    private Chrono chrono;
    

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        uiCamera.enabled = false;

        chrono = FindObjectOfType<Chrono>(); //acc�de au script Chrono
    }

    private void OnEnable()
    {
        pause = playerInput.Menu.Pause;
        pause.Enable();

        pause.performed += PressPause;
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); //Active le canvas du menu pause et d�sactive celui du jeu
        canvasToDisable.enabled = false;

        Time.timeScale = 0f; //arr�te le temps 

        mainCamera.enabled = false; //disable la camera de jeu et enable celle de l'UI
        uiCamera.enabled = true;

        Cursor.visible = true; //Fait r�apparaitre la souris mais l'emp�che de sortir de l'�cran
        Cursor.lockState = CursorLockMode.Confined;

        isPaused = true;
    }

    public void ResumGame()
    {
        pauseMenu.SetActive(false);
        canvasToDisable.enabled = true;

        Time.timeScale = 1f;

        mainCamera.enabled = true;
        uiCamera.enabled = false;
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }
   
    private void PressPause(InputAction.CallbackContext context)
    {
        if (isPaused) //resume la partie si on r�appuit sur la touche P
        {
           ResumGame();
        }
        else
        {
            if (chrono.isEnded == false)  //permet de ne mettre en pause seulement si on est pas dans l'�cran de fin de jeu
            {
               PauseGame();
            }
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UISene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
