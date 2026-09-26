using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KeyPickup : MonoBehaviour
{
    [SerializeField] private KeyColor keyColor;
    GameObject gameManagerObject;
    private ColorUnlockManager colorUnlockManager;

    private void Awake()
    {
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObject == null)
        {
            Debug.LogError($"{name} could not find the GameManager.");
            return;
        }

        colorUnlockManager = gameManagerObject.GetComponent<ColorUnlockManager>();

        if (colorUnlockManager == null)
        {
            Debug.LogError($"{name} could not find a ColorUnlockManager in the scene.");
        }
    }

    private void Start()
    {
        if (colorUnlockManager == null) return;

        colorUnlockManager.ColorUnlocked += HandleKeyColorUnlocked;

        if (colorUnlockManager.IsColorUnlocked(keyColor))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (colorUnlockManager != null)
        {
            colorUnlockManager.ColorUnlocked -= HandleKeyColorUnlocked;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
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
