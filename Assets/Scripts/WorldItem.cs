using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pickup();
            }
        }
    }

    void Pickup()
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();

        if(inventory.AttemptAutoAddItemToGrid(itemData))
        {
            Destroy(gameObject);
            //print("Picked up " + itemData.itemName);
        }
        else
        {
            //print("Inventory full, cannot pick up " + itemData.itemName);
        }
    }
}
