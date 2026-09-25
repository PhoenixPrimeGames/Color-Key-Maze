using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class ColorBarrier : MonoBehaviour
{
    [SerializeField] private KeyColor barrierColor;
    private ColorUnlockManager manager;
    private BoxCollider2D solidCollider;
    private BarrierInteractionZone interactionZone;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        manager = FindFirstObjectByType<ColorUnlockManager>();
        solidCollider = GetComponent<BoxCollider2D>();
        interactionZone = GetComponentInChildren<BarrierInteractionZone>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (manager == null)
        {
            Debug.LogError($"{name} could not find a ColorUnlockManager in the scene.");
        }

        if (solidCollider == null)
        {
            Debug.LogError($"{name} could not find its solid BoxCollider2D.");
        }

        if (interactionZone == null)
        {
            Debug.LogError($"{name} could not find its BarrierInteractionZone.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError($"{name} could not find its SpriteRenderer.");
        }
    }

    private void Start()
    {
        if (manager == null) return;

        manager.ColorUnlocked += HandleColorUnlocked;

        if (manager.IsColorUnlocked(barrierColor))
        {
            OpenBarrier();
        }
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.ColorUnlocked -= HandleColorUnlocked;
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (manager == null)
        {
            Debug.LogError($"{name} has no ColorUnlockManager reference.");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError($"{name} received a null PlayerInventory.");
            return;
        }

        if (!manager.IsColorUnlocked(barrierColor))
        {
            if (inventory.CheckKey(barrierColor))
            {
                Debug.Log($"Player has the {barrierColor} key");
                manager.UnlockColor(barrierColor);
            }
            else
            {
                Debug.Log($"The {barrierColor} barrier remains locked");
            }
        } 
    }

    private void HandleColorUnlocked(KeyColor unlockedColor)
    {
        if (unlockedColor != barrierColor)
        {
            return;
        }

        OpenBarrier();
    }

    private void OpenBarrier()
    {
        solidCollider.enabled = false;
        interactionZone.DisableInteraction();
        spriteRenderer.enabled = false;
    }
}
