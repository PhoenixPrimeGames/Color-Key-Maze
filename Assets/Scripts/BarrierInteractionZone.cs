using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BarrierInteractionZone : MonoBehaviour
{
    private ColorBarrier barrier;
    private PlayerInteraction currentInteraction;

    private void Awake()
    {
        // Find the ColorBarrier belonging to this interaction zone.
        barrier = GetComponentInParent<ColorBarrier>();

        if (barrier == null)
        {
            Debug.LogError("BarrierInteractionZone could not find a parent ColorBarrier.");
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();
        if (interaction == null) return;

        bool registered = interaction.RegisterBarrier(barrier);
        if (registered)
        {
            currentInteraction = interaction;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();
        if (interaction == null) return;

        interaction.UnregisterBarrier(barrier);

        if (interaction == currentInteraction)
        {
            currentInteraction = null;
        }
    }

    public void DisableInteraction()
    {
        if (currentInteraction != null)
        {
            currentInteraction.UnregisterBarrier(barrier);
            currentInteraction = null;
        }
        gameObject.SetActive(false);

    }
}
