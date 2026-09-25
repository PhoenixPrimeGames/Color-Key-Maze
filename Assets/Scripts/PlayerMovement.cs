
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        // Reads WASD and arrow-key inputs for horizontal and vertial movement directions.

        horizontal = ((keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) ? -1 : 0) + ((keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) ? 1 : 0);
        vertical = ((keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) ? -1 : 0) + ((keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) ? 1 : 0);

        // Give horizonal movement priority over vertical movement
        if (horizontal != 0) vertical = 0;


        moveInput = new Vector2(horizontal, vertical);
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
    }
}