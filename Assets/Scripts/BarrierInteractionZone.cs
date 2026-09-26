using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BarrierInteractionZone : MonoBehaviour
{
    private ColorBarrier barrier;
    private PlayerInteraction currentInteraction;

    // On Awake, we assign the ColorBarrier script component to barrier
    private void Awake()
    {
        barrier = GetComponentInParent<ColorBarrier>();

        if (barrier == null)
        {
            Debug.LogError("BarrierInteractionZone could not find a parent ColorBarrier.");
        }
    }

    // On TriggerEnter, we check if the triggering object is the player. We then assign the PlayerInteraction component to interaction.
    // If the component is found, we then register the barrier to the list of barriers available to the player to interact with.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();
        if (interaction == null) return;

        bool registered = interaction.RegisterBarrier(barrier);
        if (registered)
        {
            currentInteraction = interaction;
        }
    }

    // On TriggerExit, we check if the triggering object is the player. We then assign the PlayerInteraction component to interaction.
    // If the component is found, we then unregister the barrier from the list of barriers available to the player to interact with.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();
        if (interaction == null) return;

        interaction.UnregisterBarrier(barrier);

        if (interaction == currentInteraction)
        {
            currentInteraction = null;
        }
    }

    // This allows us to manually disable interaction with barriers, specifically for when the barrier color is unlocked.
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
