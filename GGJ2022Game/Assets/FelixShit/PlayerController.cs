using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MoveParam")]
    [SerializeField] private float moveSpeed;
    private Vector3 currentMove;
    private bool isLookingRight = true;

    [Header("JumpParam")]
    [SerializeField] private float jumpSpeed;
    private bool canDoubleJump = true;
    private bool isGrounded;

    [Header("GroundCheck")]
    [SerializeField] private float extraDownVelo; 
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayer;


    [Header("Delays")]
    [SerializeField] private float doubleJumpCooldown;
    [SerializeField] private float trapActivationCooldown;
    private float currentJumpCoolDown;
    private float currentTrapCoolDown;
    private bool canChangeTrap = true;


    [Header("Keys")]
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode trapKey;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Get Move Input
        GetInput();

        CheckCoolDowns();
        isGrounded = GroundCheck();
    }

    private void FixedUpdate()
    {
        //Move Player
        Move();
    }

    private void ChangeNextTrap()
    {
        TrapContainer.Instance.SetNextTrapAndActivate(this.transform.position.x);

        canChangeTrap = false;
        currentTrapCoolDown = 0;
    }

    private void CheckJump()
    {
        if (isGrounded)
        {
            Jump();
            isGrounded = false;
        }
        else if (canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
            currentJumpCoolDown = 0;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    private void GetInput()
    {
        currentMove = Vector3.zero;


        if (Input.GetKeyDown(jumpKey))
        {
            CheckJump();
        }

        if (Input.GetKey(moveRightKey))
        {
            currentMove += Vector3.right * moveSpeed;
            if (!isLookingRight)
            {
                FlipChar();
                isLookingRight = true;
            }
        }

        if (Input.GetKey(moveLeftKey))
        {
            currentMove += Vector3.left * moveSpeed;
            if (isLookingRight)
            {
                FlipChar();
                isLookingRight = false;
            }
        }

        if (Input.GetKeyDown(trapKey))
        {
            if (canChangeTrap)
            {
                ChangeNextTrap();
                canChangeTrap = false;
            }
            
        }

    }

    private void FlipChar()
    {
        this.transform.Rotate(new Vector3(0, 180, 0));
    }

    private void Move()
    {
        rb.velocity = new Vector3(currentMove.x * Time.fixedDeltaTime, rb.velocity.y, currentMove.z * Time.fixedDeltaTime);
    }

    private bool GroundCheck()
    {
        if (Physics.CheckSphere(groundCheckPos.position, groundCheckRadius, groundLayer))
        {
            return true;
        }
        else
        {
            rb.velocity += Vector3.down * extraDownVelo * Time.deltaTime;

            return false;
        }
    }

    private void CheckCoolDowns()
    {
        if (!canDoubleJump)
        {
            CoolDown(ref canDoubleJump, ref currentJumpCoolDown, doubleJumpCooldown);
        }

        if (!canChangeTrap)
        {
            CoolDown(ref canChangeTrap, ref currentTrapCoolDown, trapActivationCooldown);
        }

    }

    private void CoolDown(ref bool _tocooldown,ref float _currentcooldown, float _maxtime)
    {
        if (_currentcooldown < _maxtime)
        {
            _currentcooldown += Time.deltaTime;

            if (_currentcooldown >= _maxtime)
            {
                _currentcooldown = _maxtime;
                _tocooldown = !_tocooldown;
            }
        }
    }

    private void Die()
    {
        //Tell Race Manager
    }

    public void RecieveSlow(float _multiplier)
    {
        moveSpeed *= _multiplier;
        jumpSpeed *= _multiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }

}
