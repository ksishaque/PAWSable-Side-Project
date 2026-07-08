using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{

    [SerializeField] private GameObject inventory;
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleInventory();
        toggleInventory();
    }

    // Update is called once per frame
    void Update()
    {
       if (Keyboard.current.iKey.wasPressedThisFrame == true)
       {    
            toggleInventory();
       }
    }

    // Toggles the inventory to show or hide
    public void toggleInventory()
    {
        isOpen = !isOpen;
        inventory.SetActive(isOpen);
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
