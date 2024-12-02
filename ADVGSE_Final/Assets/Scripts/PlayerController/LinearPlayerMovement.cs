using UnityEngine;

public class LinearPlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 7.0f;

    [Header("Ground Check")]
    //variables used for checking if player is on ground (for jump/turn)
    [SerializeField] LayerMask ground;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    bool isGrounded;
    bool isOnTurningTile;

    //variables used for handling player jump
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool isJumpReady = true;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    //transform position has the current orientation of the player
    [SerializeField] Transform orientation;

    private bool turnLeft, turnRight;
    private CharacterController characterController;

    //rigidbody attached to player
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is grounded
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        playerInput();

        //checks if player has reached tile for turning
        RaycastHit hitTemp;
        Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.forward), out hitTemp, ground);
        if (hitTemp.collider != null)
        {
            if (hitTemp.collider.tag == "TurningTile")
            {
                isOnTurningTile = true;
            }
        }

        if (isGrounded && isOnTurningTile)
        {
            turn();
        }

        //move forward
        characterController.SimpleMove(new Vector3(0f, 0f, 0f));
        characterController.Move(transform.forward * speed * Time.deltaTime);
    }

    private void playerInput()
    {

        if (Input.GetKey(jumpKey) && isJumpReady && isGrounded)
        {
            isJumpReady = false;

            jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }

    }

    private void turn()
    {
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight = Input.GetKeyDown(KeyCode.D);

        if (turnLeft)
        {
            transform.Rotate(new Vector3(0f, -90f, 0f));
        }
        else if (turnRight)
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }
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
