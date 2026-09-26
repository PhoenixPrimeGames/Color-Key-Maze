using System.Collections.Generic;
using UnityEngine;

public class ColorUnlockedUI : MonoBehaviour
{
    [SerializeField] private GameObject redUnlockedColorIndicator;
    [SerializeField] private GameObject orangeUnlockedColorIndicator;
    [SerializeField] private GameObject yellowUnlockedColorIndicator;
    [SerializeField] private GameObject greenUnlockedColorIndicator;
    [SerializeField] private GameObject blueUnlockedColorIndicator;
    [SerializeField] private GameObject indigoUnlockedColorIndicator;
    [SerializeField] private GameObject violetUnlockedColorIndicator;
    [SerializeField] private ColorUnlockManager colorUnlockManager;
    private readonly Dictionary<KeyColor, GameObject> barrierIndicators = new();

    private void Start()
    {
        barrierIndicators.Add(KeyColor.RED, redUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.ORANGE, orangeUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.YELLOW, yellowUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.GREEN, greenUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.BLUE, blueUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.INDIGO, indigoUnlockedColorIndicator);
        barrierIndicators.Add(KeyColor.VIOLET, violetUnlockedColorIndicator);

        if (colorUnlockManager != null)
        {
            foreach (KeyValuePair<KeyColor, GameObject> kvp in barrierIndicators)
            {
                bool colorUnlocked = colorUnlockManager.IsColorUnlocked(kvp.Key);
                if (kvp.Value != null) kvp.Value.SetActive(colorUnlocked);
            }

            colorUnlockManager.ColorUnlocked += HandleColorUnlocked;
        }
        else
        {
            Debug.LogError("ColorUnlockedUI is missing its ColorUnlockManager reference.");
            return;
        }
    }

    private void OnDestroy()
    {
        if (colorUnlockManager != null)
        {
            colorUnlockManager.ColorUnlocked -= HandleColorUnlocked;
        }
    }

    private void HandleColorUnlocked(KeyColor color)
    {
        if (barrierIndicators.TryGetValue(color, out GameObject indicator))
        {
            indicator.SetActive(true);
        }
    }
    
}
