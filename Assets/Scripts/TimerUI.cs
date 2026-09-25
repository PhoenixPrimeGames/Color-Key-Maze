using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = gameTimer.GetRemainingTime();

        int totalSeconds = Mathf.CeilToInt(remainingTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
