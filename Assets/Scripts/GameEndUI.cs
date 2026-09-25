using TMPro;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    [SerializeField] private TMP_Text resultText;

    public void ShowGameOver()
    {
        endScreen.SetActive(true);
        resultText.text = "GAME OVER";
    }

    public void ShowWin()
    {
        endScreen.SetActive(true);
        resultText.text = "YOU ESCAPED";
    }
}
