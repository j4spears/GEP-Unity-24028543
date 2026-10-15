using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private GameManager gameManager;

    private bool playerInRange = false;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && gameManager.CurrentGameState == gameManager.GameplayState)
        {
            Pickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
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