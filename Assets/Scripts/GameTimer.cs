using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startingTime = 60f;
    private float remainingTime;
    public event System.Action TimerExpired;
    private bool timerIsRunning = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        remainingTime = startingTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsRunning)
        {
            remainingTime = Mathf.Max(0f, remainingTime - Time.deltaTime);

            if (remainingTime <= 0f)
            {
                StopTimer();
                TimerExpired?.Invoke();

            }
        }
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void HalveRemainingTime()
    {
        remainingTime /= 2.0f;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }
}
