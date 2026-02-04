using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private enum MenuState
    {
        Pause,
        Unpause,
        PauseConfirmQuit
    }
    private MenuState state = MenuState.Unpause;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject confirmMenu;

    [SerializeField]
    private GameObject ResumeButton;
    [SerializeField]
    private GameObject NoButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //detect key for pausing
        if (Keyboard.current.escapeKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            if (state == MenuState.Pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Pause()
    {
        state = MenuState.Pause;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ResumeButton);
    }

    public void Resume()
    {
        state = MenuState.Unpause;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void AskQuitToMain()
    {
        state = MenuState.PauseConfirmQuit;
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(NoButton);
    }

    public void StopQuitToMain()
    {
        state = MenuState.Pause;
        confirmMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ResumeButton);
    }

    public void ConfirmQuitToMain()
    {
        state = MenuState.Unpause;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

}
