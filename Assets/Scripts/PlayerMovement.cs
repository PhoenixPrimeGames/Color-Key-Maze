
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    public event System.Action<bool> SprintingChanged;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        bool sprinting;

        if (keyboard == null)
        {
            moveInput = Vector2.zero;
            CheckIfSprinting(false);
            return;
        }

        // Reads WASD and arrow-key inputs for horizontal and vertial movement directions.

        float horizontal = (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? -1 : 0) + (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1 : 0);
        float vertical = (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed ? -1 : 0) + (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ? 1 : 0);


        moveInput = new Vector2(horizontal, vertical).normalized;

        if (keyboard.leftShiftKey.isPressed)
        {
            moveInput *= sprintMultiplier;
        }

        sprinting = moveInput != Vector2.zero && keyboard.leftShiftKey.isPressed;

        CheckIfSprinting(sprinting);
    }

    private void CheckIfSprinting(bool sprinting)
    {
        if (isSprinting != sprinting)
        {
            isSprinting = sprinting;
            SprintingChanged?.Invoke(isSprinting);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void StopPlayerMovement()
    {
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        enabled = false;

        CheckIfSprinting(false);
    }
}