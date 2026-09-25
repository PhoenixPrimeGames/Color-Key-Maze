using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameTimer))]
public class ColorUnlockManager : MonoBehaviour
{
    private HashSet<KeyColor> unlockedColors = new();
    private GameTimer timer;

    public event System.Action<KeyColor> ColorUnlocked;

    private void Awake()
    {
        timer = GetComponent<GameTimer>();
    }

    // Checks if a barrier color is unlocked
    public bool IsColorUnlocked(KeyColor color)
    {
        // Checks if a color is in the unlockedColors hash set
        bool isUnlocked = unlockedColors.Contains(color);
        Debug.Log($"Is {color} already unlocked? {isUnlocked}");

        return isUnlocked;
    }

    // This unlocks a barrier color
    public bool UnlockColor(KeyColor color)
    {
        // This attempts to add the color to the unlockedColors hash set
        bool unlocked = unlockedColors.Add(color);

        if (unlocked)
        {
            ColorUnlocked?.Invoke(color);
            Debug.Log($"Was {color} successfully unlocked? {unlocked}");
            timer.HalveRemainingTime();
        }

        return unlocked;
    }
}
