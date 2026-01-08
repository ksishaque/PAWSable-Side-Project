using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    //For all menu states
    public enum MenuState { 
        Main,
        Confirm
    };
    //Current menu states
    public MenuState menuState;

    //Menus to be processed
    public GameObject mainMenu;
    public GameObject confirmMenu;

    //Buttons to focus on when switching menus
    public GameObject deselectButton;
    public GameObject startButton;

    //going to desired scene **CHANGE LATER TO BE GENERALIZED**
    [SerializeField] private string levelSceneName;

    //Start of scene, do this
    private void Awake()
    {
        menuState = MenuState.Main;
    }

    private void Update()
    {
        //Change the state of the main menu
        switch (menuState)
        {
            case MenuState.Main:
                mainMenu.SetActive(true);
                confirmMenu.SetActive(false);
                break;
            case MenuState.Confirm:
                confirmMenu.SetActive(true);
                mainMenu.SetActive(false);
                break;
        }

    }

    //ALL BUTTON LOGIC
    public void Play()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void AskQuit()
    {
        menuState = MenuState.Confirm;
        EventSystem.current.SetSelectedGameObject(deselectButton);
    }

    public void StopQuit()
    {
        menuState = MenuState.Main;
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void ConfirmQuit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
