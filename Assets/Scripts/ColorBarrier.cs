using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class ColorBarrier : MonoBehaviour
{
    [SerializeField] private KeyColor barrierColor;
    GameObject gameManagerObject;
    private ColorUnlockManager colorUnlockManager;
    private BoxCollider2D solidCollider;
    private BarrierInteractionZone interactionZone;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObject == null)
        {
            Debug.LogError($"{name} could not find the GameManager.");
            return;
        }

        colorUnlockManager = gameManagerObject.GetComponent<ColorUnlockManager>();
        solidCollider = GetComponent<BoxCollider2D>();
        interactionZone = GetComponentInChildren<BarrierInteractionZone>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (colorUnlockManager == null)
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
        if (colorUnlockManager == null) return;

        colorUnlockManager.ColorUnlocked += HandleColorUnlocked;

        if (colorUnlockManager.IsColorUnlocked(barrierColor))
        {
            OpenBarrier();
        }
    }

    private void OnDestroy()
    {
        if (colorUnlockManager != null)
        {
            colorUnlockManager.ColorUnlocked -= HandleColorUnlocked;
        }
    }

    public void Interact(PlayerInventory inventory)
    {
        if (colorUnlockManager == null)
        {
            Debug.LogError($"{name} has no ColorUnlockManager reference.");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError($"{name} received a null PlayerInventory.");
            return;
        }

        if (!colorUnlockManager.IsColorUnlocked(barrierColor) && inventory.CheckKey(barrierColor) && colorUnlockManager.UnlockColor(barrierColor))
        {
            inventory.ConsumeKey(barrierColor);
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
