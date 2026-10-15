using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    public GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && gameManager.CurrentGameState == gameManager.GameplayState)
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