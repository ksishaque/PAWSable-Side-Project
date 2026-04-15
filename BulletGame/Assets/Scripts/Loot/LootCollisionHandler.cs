using UnityEngine;

public class LootHandler : MonoBehaviour
{
    [SerializeField] private BaseLootData baseloot;

    void OnTriggerEnter2D(Collider2D collider)
    {

        // Player && Loot Collision 
        Debug.Log("Collision Loot Detected");


        Inventory inventory = collider.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(baseloot);
            Debug.Log("Collision Loot Detected 2");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collision Loot Failed");
        }

    }

}
