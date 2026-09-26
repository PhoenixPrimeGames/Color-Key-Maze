using System.Collections.Generic;
using UnityEngine;

public class KeyInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject redKeyIndicator;
    [SerializeField] private GameObject orangeKeyIndicator;
    [SerializeField] private GameObject yellowKeyIndicator;
    [SerializeField] private GameObject greenKeyIndicator;
    [SerializeField] private GameObject blueKeyIndicator;
    [SerializeField] private GameObject indigoKeyIndicator;
    [SerializeField] private GameObject violetKeyIndicator;
    [SerializeField] private PlayerInventory playerInventory;

    private readonly Dictionary<KeyColor,GameObject> keyIndicators = new();

    private void Start()
    {
        keyIndicators.Add(KeyColor.RED,redKeyIndicator);
        keyIndicators.Add(KeyColor.ORANGE,orangeKeyIndicator);
        keyIndicators.Add(KeyColor.YELLOW,yellowKeyIndicator);
        keyIndicators.Add(KeyColor.GREEN,greenKeyIndicator);
        keyIndicators.Add(KeyColor.BLUE,blueKeyIndicator);
        keyIndicators.Add(KeyColor.INDIGO,indigoKeyIndicator);
        keyIndicators.Add(KeyColor.VIOLET,violetKeyIndicator);

        if (playerInventory != null)
        {
            foreach (KeyValuePair<KeyColor, GameObject> kvp in keyIndicators)
            {
                bool hasKey = playerInventory.CheckKey(kvp.Key);
                if (kvp.Value != null) kvp.Value.SetActive(hasKey);
            }

            playerInventory.KeyCollected += HandleKeyCollected;
            playerInventory.KeyConsumed += HandleKeyConsumed;
        }
        else
        {
            Debug.LogError("KeyInventoryUI is missing its PlayerInventory reference.");
            return;
        }
        

    }

    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.KeyCollected -= HandleKeyCollected;
            playerInventory.KeyConsumed -= HandleKeyConsumed;
        }
        
    }

    private void HandleKeyCollected(KeyColor color)
    {
        if (keyIndicators.TryGetValue(color, out GameObject indicator))
        {
            indicator.SetActive(true);
        }
    }

    private void HandleKeyConsumed(KeyColor color)
    {
        if (keyIndicators.TryGetValue(color, out GameObject indicator))
        {
            indicator.SetActive(false);
        }

    }
}
