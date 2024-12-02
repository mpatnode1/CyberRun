using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    //player's speed
    [SerializeField] float moveSpeed;

    //variables used for handling player jump
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool isJumpReady = true;

    [Header("Ground Check")]
    //variables used for checking if player is on ground (for jump)
    [SerializeField] LayerMask ground;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    bool isGrounded;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    //transform position has the current orientation of the player
    [SerializeField] Transform orientation;

    //stores player input
    float horizontalInput;
    float verticalInput;

    //used to store intended movement with facing orientation considered
    Vector3 moveDirection;

    //rigidbody attached to player
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //check if player is grounded
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        //get player input
        playerInput();

    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    /// <summary>
    /// retrieve vertical and horizontal player inputs
    /// </summary>
    private void playerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && isJumpReady && isGrounded)
        {
            isJumpReady = false;

            jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }

    }

    /// <summary>
    /// Calling moves the player based on current input
    /// </summary>
    private void movePlayer()
    {
        //calculates move direction with orientation of player included
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    /// <summary>
    /// Calling makes the player jump
    /// </summary>
    private void jump()
    {
        //resets velocity of y
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Called after player jump cooldown to let player jump again
    /// </summary>
    private void resetJump()
    {
        isJumpReady = true;
    }
}
