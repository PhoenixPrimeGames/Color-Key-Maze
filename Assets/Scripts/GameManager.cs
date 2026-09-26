using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    Playing,
    Won,
    Lost
}

[RequireComponent(typeof(GameTimer))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private GameEndUI endUI;
    private GameState currentState;
    private GameTimer timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timer = GetComponent<GameTimer>();
        currentState = GameState.Playing;

        if (timer == null) return;

        timer.TimerExpired += HandleExpiredTimer;
    }

    private void OnDestroy()
    {
        if (timer != null)
        {
            timer.TimerExpired -= HandleExpiredTimer;
        }   
    }

    public void WinGame()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Won;
            endUI.ShowWin();
            StopGame();
        }
    }

    public void LoseGame()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Lost;
            endUI.ShowGameOver();
            StopGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleExpiredTimer()
    {
        LoseGame();
    }

    private void StopGame()
    {
        timer.StopTimer();
        playerMovement.StopPlayerMovement();
        playerInteraction.StopPlayerInteraction();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
