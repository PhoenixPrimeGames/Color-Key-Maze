using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private HashSet<KeyColor> collectedKeys = new();

    public void CollectKey(KeyColor color)
    {
        collectedKeys.Add(color);
        Debug.Log(color + " picked up? " + CheckKey(color));
    }

    public bool CheckKey(KeyColor color)
    {
        return collectedKeys.Contains(color);
    }

    public bool ConsumeKey(KeyColor color)
    {
        return collectedKeys.Remove(color);
    }

}
