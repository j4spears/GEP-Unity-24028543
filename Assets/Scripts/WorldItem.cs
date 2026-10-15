using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pickup();
            }
        }
    }

    private void Pickup()
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();

        if(inventory.AttemptAutoAddItemToGrid(itemData))
        {
            Destroy(gameObject);
        }
    }
}