using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private HashSet<KeyColor> collectedKeys = new();
    public event System.Action<KeyColor> KeyCollected;
    public event System.Action<KeyColor> KeyConsumed;

    public bool CollectKey(KeyColor color)
    {
        bool collected = collectedKeys.Add(color);

        if (collected)
        {
            KeyCollected?.Invoke(color);
        }

        return collected;
    }

    public bool CheckKey(KeyColor color)
    {
        return collectedKeys.Contains(color);
    }

    public bool ConsumeKey(KeyColor color)
    {
        bool removed = collectedKeys.Remove(color);

        if (removed)
        {
            KeyConsumed?.Invoke(color);
        }

        return removed;
    }

}
