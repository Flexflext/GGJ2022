using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


 public enum EPlayer
{
    First,
    Second
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EPlayer player;

    [Header("MoveParam")]
    [SerializeField] private float moveSpeed;
    private float maxMoveSpeed;
    private Vector3 currentMove;
    private bool isLookingRight = true;

    [Header("JumpParam")]
    [SerializeField] private float jumpSpeed;
    private float maxJumpSpeed;
    private bool canDoubleJump = true;
    private bool isGrounded;

    [Header("GroundCheck")]
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float extraDownVelo; 
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayer;
    private float curCoyoteTime = 0.1f;


    [Header("Delays")]
    [SerializeField] private PlayerUI ui;
    [SerializeField] private float doubleJumpCooldown;
    [SerializeField] private float trapActivationCooldown;
    private float currentJumpCoolDown;
    private float currentTrapCoolDown;
    private bool canChangeTrap = true;

    [Header("VFX")]
    [SerializeField] private VisualEffect jump;
    [SerializeField] private VisualEffect fall;
    [SerializeField] private VisualEffect walk;
    [SerializeField] private VisualEffect dieVFX;


    [Header("Keys")]
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode trapKey;

    private Rigidbody rb;
    private Animator animator;

    private bool canDie = true;
    private bool wasMoveing;

    private int runHash;
    private int idleHash;

    private PersonalSoundManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioManager = GetComponent<PersonalSoundManager>();

        maxMoveSpeed = moveSpeed;
        maxJumpSpeed = jumpSpeed;
        currentJumpCoolDown = doubleJumpCooldown;
        currentTrapCoolDown = trapActivationCooldown;

        idleHash = Animator.StringToHash("Nada");
        runHash = Animator.StringToHash("Run");
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
        ETrapType type = TrapContainer.Instance.SetNextTrapAndActivate(this.transform.position.x);

        switch (type)
        {
            case ETrapType.platform:
                audioManager.PlaySound(false, ESoundTypes.Platform);
                break;
            case ETrapType.wall:
                audioManager.PlaySound(false, ESoundTypes.Wall);
                break;
            case ETrapType.slow:
                audioManager.PlaySound(false, ESoundTypes.Slow);
                break;
            default:
                break;
        }

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
        audioManager.Stop(ESoundTypes.Walk);
        audioManager.PlaySound(false, ESoundTypes.Jump);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpSpeed);
        jump.Play();
        walk.Stop();
    }

    private void GetInput()
    {
        if (Time.timeScale < 1 || animator == null)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.G))
        {
            audioManager.PlaySound(false, ESoundTypes.Gulp);
        }

        if (currentMove.sqrMagnitude >= (0.1f * 0.1f))
        {
            if (!wasMoveing)
            {
                if (isGrounded)
                {
                    audioManager.PlaySound(false, ESoundTypes.Walk);
                }   
                animator.Play(runHash);
                wasMoveing = true;
            }
        }
        else
        {
            audioManager.Stop(ESoundTypes.Walk);
            animator.Play(idleHash);
            wasMoveing = false;
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
            if (!isGrounded)
            {
                walk.Play();
                fall.Play();
            }

            curCoyoteTime = coyoteTime;
            return true;
        }
        else
        {
            if (curCoyoteTime <= 0)
            {
                rb.velocity += Vector3.down * extraDownVelo * Time.deltaTime;

                return false;
            }
            else
            {
                curCoyoteTime -= Time.deltaTime;

                return true;
            }
            
        }
    }

    private void CheckCoolDowns()
    {
        if (!canDoubleJump)
        {
            CoolDown(ref canDoubleJump, ref currentJumpCoolDown, doubleJumpCooldown);

            if (player == EPlayer.First)
            {
                ui.ChangeTimerOfDoubleJumpUp(currentJumpCoolDown, doubleJumpCooldown);
            }
            else
            {
                ui.ChangeTimerOfDoubleJumpDown(currentJumpCoolDown, doubleJumpCooldown);
            }
        }

        if (!canChangeTrap)
        {
            CoolDown(ref canChangeTrap, ref currentTrapCoolDown, trapActivationCooldown);

            if (player == EPlayer.First)
            {
                ui.ChangeTimerOfTrapUp(currentTrapCoolDown, trapActivationCooldown);
            }
            else
            {
                ui.ChangeTimerOfTrapDown(currentTrapCoolDown, trapActivationCooldown);
            }
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
        audioManager.PlaySound(false, ESoundTypes.Death);
        audioManager.PlaySound(false, ESoundTypes.Splash);
        //Tell Race Manager
        dieVFX.Play();
        rb.velocity = Vector3.zero;
        GameManager.Instance.SetPlayerScore(player);
    }

    private void Win()
    {
        GameManager.Instance.SetPlayerScore(player, true);
    }

    public void RecieveSlow(float _multiplier)
    {
        moveSpeed = maxMoveSpeed * _multiplier;
        jumpSpeed = maxJumpSpeed * _multiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && canDie)
        {
            canDie = false;
            Die();
        }
        else if (other.gameObject.layer == 9)
        {
            Win();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }

}
