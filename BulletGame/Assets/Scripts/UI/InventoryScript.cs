using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{

    [SerializeField] private GameObject inventory;
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Keyboard.current.iKey.wasPressedThisFrame == true)
       {    
            toggleInventory();
       }
    }

    public void toggleInventory()
    {
        isOpen = !isOpen;
        inventory.SetActive(isOpen);
    }
}
