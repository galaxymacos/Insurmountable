using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Jump,
        DoubleJump,
        Attack,
        Stand,
        Walk,
        Run,
        Block
    }

    [SerializeField] private Transform cameraPosIn3D;


    // Player ability
    [SerializeField] private float doubleJumpForce = 500f;
    [SerializeField] private float dropdownSpeed = 0.05f;

    [SerializeField] private float gravity = 1000f;
    private bool is3D;

    public bool isGliding;

    public bool isGrounded;

    // Player State
    [SerializeField] private float jumpForce = 500f;
    private float lastMoveTime;
    [SerializeField] private float moveSpeed = 10f;

    public PlayerState playerCurrentState;
    public PlayerState playerPreviousState;
    private Rigidbody rb;

    private float startCountdown;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCurrentState = PlayerState.Stand;
    }

    public void ChangePlayerState(PlayerState newPlayerState)
    {
        playerPreviousState = playerCurrentState;
        playerCurrentState = newPlayerState;
    }

    private void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
        if (isGliding)
        {
            FallDown();
            isGliding = false;
        }
        else
        {
            ApplyGravity();
        }
    }

    private void ApplyGravity()
    {
        rb.AddForce(0, -gravity * Time.fixedDeltaTime, 0);
    }

    public bool CheckIfGrounded()
    {
        LayerMask ground = 1 << 11;
        return Physics.Raycast(transform.position, Vector3.down, 1.3f, ground);
    }

    public void Move(float horizontalMovement, float verticalMovement)
    {
        if (GameManager.Instance.is3D)
        {
            var movement = new Vector3(
                horizontalMovement * moveSpeed * Time.fixedDeltaTime,
                0,
                verticalMovement * moveSpeed * Time.fixedDeltaTime
            );
            rb.MovePosition(transform.position + movement);
        }

        if (!GameManager.Instance.is3D)
        {
            var movement = new Vector3(
                horizontalMovement * moveSpeed * Time.fixedDeltaTime,
                0,
                0
            );
            rb.MovePosition(transform.position + movement);
        }
    }

    public void Jump()
    {
        if (playerCurrentState == PlayerState.Stand || playerCurrentState == PlayerState.Walk)
        {
            ResetVerticalVelocity();
            rb.AddForce(new Vector3(0, jumpForce, 0));
            ChangePlayerState(PlayerState.Jump);

        }
        else if (playerCurrentState == PlayerState.Jump)
        {
            ResetVerticalVelocity();
            rb.AddForce(new Vector3(0, doubleJumpForce));
            ChangePlayerState(PlayerState.DoubleJump);
        }
    }

    public void FallDown()
    {
        var playerVelocity = rb.velocity;
        if (rb.velocity.y > Mathf.Epsilon)
        {
            playerVelocity = new Vector3(playerVelocity.x, playerVelocity.y - dropdownSpeed * Time.fixedDeltaTime,
                playerVelocity.z);
            rb.velocity = playerVelocity;
        }
    }

    private void ResetVerticalVelocity()
    {
        var playerVelocity = rb.velocity;
        playerVelocity = new Vector3(playerVelocity.x, 0, playerVelocity.z);
        rb.velocity = playerVelocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.y<=0)    // Fix a bug when jump but playerstate is still walk/stand
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
                    ChangePlayerState(PlayerState.Walk);
                else
                    ChangePlayerState(PlayerState.Stand);
            }
        }
        
    }
}