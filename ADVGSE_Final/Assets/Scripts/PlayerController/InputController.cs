using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// After first left or right key press, player is always moving.
/// This is the current direction player is moving on the x-axis.
/// </summary>
public enum DirectionMoving
{
    NONE,
    LEFT,
    RIGHT,
    WALLSTOPPED,
}

public class InputController : MonoBehaviour
{
    //transform position has the current orientation of the player
    [SerializeField] Transform orientation;

    //variables used for checking if player is on ground (for jump/turn)
    [Header("Ground Check")]
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    private bool grounded;

    //variables used for handling player jump
    [Header("Player Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float jumpCoolDown;
    [SerializeField] float gravity = 9.8f;

    //variables for player shooting
    [Header("Player Shooting")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserSpawnPoint;
    /// <summary>
    /// speed between bullets
    /// </summary>
    [SerializeField] float fireSpeed;
    /// <summary>
    /// seconds to count down from before player can fire again
    /// </summary>
    [SerializeField] private float laserCooldownCount = 3;
    /// <summary>
    /// how many seconds it has been since the player last shot
    /// </summary>
    private float laserCooldown;

    //rigidbody attached to player
    private Rigidbody rb;

    //input actions
    public PlayerInput playerControls;
    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction attack;
    private InputAction jump;

    [Header("Player Speed by Direction")]
    //Speed player is moving on x-axis.
    [SerializeField] float sideSpeed;

    /// <summary>
    /// Current direction player is moving towards on x-axis.
    /// </summary>
    private DirectionMoving currentDirection;
    

    private void Awake()
    {
        currentDirection = DirectionMoving.NONE;

        playerControls = new PlayerInput();
        rb = GetComponent<Rigidbody>();

        //cursor is locked during play
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        moveLeft = playerControls.Player.MoveLeft;
        moveLeft.Enable();
        moveLeft.performed += MoveLeft;

        moveRight = playerControls.Player.MoveRight;
        moveRight.Enable();
        moveRight.performed += MoveRight;

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        moveLeft.Disable();
        moveRight.Disable();
        attack.Disable();
        jump.Disable();
    }

    public void FixedUpdate()
    {
        handleMovement();

        //calculates time since player last shot. 
        laserCooldown -= Time.deltaTime;

        if (isGrounded() == false)
        {
            rb.AddRelativeForce(Vector3.down * gravity);
        }

    }
    void handleMovement()
    {

        if (currentDirection == DirectionMoving.LEFT)
        {
            sideSpeed = -0.05f;
        }
        else if (currentDirection == DirectionMoving.RIGHT)
        {
            sideSpeed = 0.05f;
        }
        else if (currentDirection == DirectionMoving.WALLSTOPPED || currentDirection == DirectionMoving.NONE)
        {
            sideSpeed = 0f;
        }

        //moves the player forward and in the direction of the last left or right button press
        transform.position = new Vector3(transform.position.x + sideSpeed, transform.position.y, transform.position.z);

    }

    void MoveLeft(InputAction.CallbackContext context)
    {
        currentDirection = DirectionMoving.LEFT;
    }

    void MoveRight(InputAction.CallbackContext context)
    {
        currentDirection = DirectionMoving.RIGHT;
    }

    //called when the player hits space
    void Jump(InputAction.CallbackContext context)
    {
        if (grounded == false) return;

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    bool isGrounded()
    {
        if (groundCheck == null) return false;

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(groundCheck.position, groundCheck.TransformDirection(Vector3.up * -1), out hit, 1.1f, ground))
        {
            Debug.DrawRay(groundCheck.position, groundCheck.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(groundCheck.position, groundCheck.TransformDirection(Vector3.forward) * 1000, Color.blue);
            return false;
        }
    }


    void Attack(InputAction.CallbackContext context)
    {
        shootLaserGun();
    }

    /// <summary>
    /// Called when player triggers Fire
    /// Shoots fireball from player's location moving forward. 
    /// </summary>
    void shootLaserGun()
    {
        //returns if the player's cooldown on laser gun has not ended
        if (laserCooldown > 0) return;
        laserCooldown = laserCooldownCount;

        //creates a temp gameobject using the laser's prefab at the current pos + rot of player
        GameObject laserObj = Instantiate(laserPrefab, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;

        //obtains temp laser obj's rigidbody and applies the force to move it forward
        Rigidbody laserRig = laserObj.GetComponent<Rigidbody>();
        laserRig.AddForce(laserRig.transform.forward * fireSpeed);

        AudioManager.Instance.PlayBulletSound();

        //destroys after 5s
        Destroy(laserObj, 5f);
    }

}
