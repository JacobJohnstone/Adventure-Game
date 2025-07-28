using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpPower;


    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;

    private float horizontal;
    private float sprintSpeed;
    private float maxSpeed;

    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    public void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sprintSpeed = speed + 5;
        maxSpeed = speed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        horizontal = controls.FindAction("Move").ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontal * speed;
        float speedDiff = targetSpeed - rb.linearVelocityX;
        float force = speedDiff * acceleration;
        if (!IsGrounded())
        {
            force = force / 2;
        }
        rb.AddForce(Vector2.right * force);

        if(Mathf.Abs(rb.linearVelocityX) > speed)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        }
    }



    #region PLAYER_CONTROLS
    public void Move(InputAction.CallbackContext context)
    {
        if (MainManager.instance.canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded() && MainManager.instance.canMove)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
    }

    private bool IsGrounded()
    {
        
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        } else
        {
            return false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            speed = sprintSpeed;
        } else
        {
            speed = maxSpeed;
        }
    }
    
    #endregion

}
