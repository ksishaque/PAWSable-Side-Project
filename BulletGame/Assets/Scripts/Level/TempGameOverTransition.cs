using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TempGameOverTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
