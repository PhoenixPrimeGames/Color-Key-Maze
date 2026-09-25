using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();

        if (player == null) return;

        gameManager.WinGame();
    }
}
