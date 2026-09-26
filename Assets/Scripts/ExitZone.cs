using UnityEngine;

public class ExitZone : MonoBehaviour
{
    GameObject gameManagerObject;
    private GameManager gameManager;

    private void Awake()
    {
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObject == null)
        {
            Debug.LogError($"{name} could not find the GameManager.");
            return;
        }

        gameManager = gameManagerObject.GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        gameManager.WinGame();
    }
}
