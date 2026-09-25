using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInteraction : MonoBehaviour
{
    private List<ColorBarrier> nearbyBarriers = new();
    private const int NULL_SELECTION = -1;
    private int selectedIndex = NULL_SELECTION;
    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogError("PlayerInteraction could not find PlayerInventory.");
        }
    }

    public bool RegisterBarrier(ColorBarrier barrier)
    {
        if (barrier == null || nearbyBarriers.Contains(barrier)) return false;

        nearbyBarriers.Add(barrier);
        if (selectedIndex == NULL_SELECTION) selectedIndex = nearbyBarriers.IndexOf(barrier);

        return true;
    }

    public bool UnregisterBarrier(ColorBarrier barrier)
    {
        int removedIndex = nearbyBarriers.IndexOf(barrier);
        if (barrier == null || removedIndex == NULL_SELECTION) return false;

        nearbyBarriers.RemoveAt(removedIndex);
        if (selectedIndex >= removedIndex)
        {
            if (selectedIndex > 0) selectedIndex--;
            else selectedIndex = nearbyBarriers.Count - 1;
        }

        return true;   
    }

    private void CycleSelection()
    {
        if (nearbyBarriers.Count < 2) return;

        selectedIndex = (selectedIndex + 1) % nearbyBarriers.Count;
    }

    private void InteractWithSelection()
    {
        if (selectedIndex < 0 || selectedIndex >= nearbyBarriers.Count)
        {
            selectedIndex = NULL_SELECTION;
            return;
        }
        ColorBarrier selectedBarrier = nearbyBarriers[selectedIndex];

        Debug.Log($"Interacting with barrier {selectedBarrier.name}");

        selectedBarrier.Interact(inventory);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null) return;

        if (keyboard.tabKey.wasPressedThisFrame) 
        {
            CycleSelection();
            if (selectedIndex != NULL_SELECTION)
            {
                Debug.Log($"Selected barrier: {nearbyBarriers[selectedIndex].name}");
            }
        }
        if (keyboard.eKey.wasPressedThisFrame) 
        {
            InteractWithSelection();
            //Debug.Log("Pressed E key"); 
        }
    }

    public void StopPlayerInteraction()
    {
        enabled = false;
    }
}
