using System;
using UnityEngine;

public class SprintIndicatorUI : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject sprintIndicator;

    void OnEnable()
    {
        if (playerMovement != null) playerMovement.SprintingChanged += HandleSprintingChanged;

        sprintIndicator.SetActive(false);
    }

    void OnDisable()
    {
        if (playerMovement != null) playerMovement.SprintingChanged -= HandleSprintingChanged;

        sprintIndicator.SetActive(false);
    }

    private void HandleSprintingChanged(bool isSprinting)
    {
        if (sprintIndicator != null) sprintIndicator.SetActive(isSprinting);
    }
}
