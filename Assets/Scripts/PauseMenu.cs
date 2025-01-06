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
    

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        uiCamera.enabled = false;
        Debug.Log("False");
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        mainCamera.enabled = false;
        uiCamera.enabled = true;
        canvasToDisable.enabled = false;
        isPaused = true;
    }

    public void ResumGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        mainCamera.enabled = true;
        uiCamera.enabled = false;
        canvasToDisable.enabled = true;
        isPaused = false;
    }
   
    private void PressPause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            ResumGame();
        }
        else
        {
            PauseGame();
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
