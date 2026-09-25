using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KeyPickup : MonoBehaviour
{
    [SerializeField] private KeyColor keyColor;
    private ColorUnlockManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<ColorUnlockManager>();

        if (manager == null)
        {
            Debug.LogError($"{name} could not find a ColorUnlockManager in the scene.");
        }
    }

    private void Start()
    {
        if (manager == null) return;

        manager.ColorUnlocked += HandleKeyColorUnlocked;

        if (manager.IsColorUnlocked(keyColor))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.ColorUnlocked -= HandleKeyColorUnlocked;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the triggering object has access to the player inventory
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        // If it does, then we assume it's the player
        if (inventory == null)
        {
            return;
        }
        
        inventory.CollectKey(keyColor);
        Destroy(gameObject);
        
    }

    private void HandleKeyColorUnlocked(KeyColor unlockedColor)
    {
        if (unlockedColor != keyColor)
        {
            return;
        }

        Destroy(gameObject);
    }
}
