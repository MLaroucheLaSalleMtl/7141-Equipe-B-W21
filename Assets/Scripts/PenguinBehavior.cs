using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBehavior : MonoBehaviour
{
    private enum State
    {
        Walking,
        Jumping,
        KnockBack,
        Dead
    }

    private State currentState;

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Jumping:
                UpdateJumpingState();
                break;
            case State.KnockBack:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockBackDuration,
        JumpDuration,
        jumpHeight;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        jumpGroundCheck,
        playerCheck,
        player;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 knockbackSpeed;

    private float
        currentHealth,
        knockbackStartTime,
        jumpStartTime;

    [SerializeField] float circleRadius;

    private int
        facingDirection,
        damageDirection;

    private Vector2 movement;
    [SerializeField] Vector2 boxSize;

    private bool
        groundDetected,
        wallDetected,
        playerDetected,
        isGrounded;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        facingDirection = -1;
    }

    //--------WALKING STATE---------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else if (playerDetected)
        {
           isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround);
           jumpStartTime = Time.time;
           JumpAttack();
           SwitchState(State.Jumping);
        } else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;

        }
    }

    private void ExitWalkingState()
    {

    }

    //--------  JUMPING STATE  -------------------------

    private void EnterJumpingState()
    {
       
    }

    private void UpdateJumpingState()
    {
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
        aliveRb.velocity = movement;
        isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround);
        //JumpAttack();

      if (Time.time >= jumpStartTime + JumpDuration && isGrounded)
        {
            SwitchState(State.Walking);
        }
        
    }

    private void ExitJumpingState()
    {
         
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            aliveRb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }
    }

    //--------  KNOCKBACK STATE ------------------------


    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x = damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockBackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    //--------- DEAD STATE -------------------------------

    private void EnterDeadState()
    {
        //Spawns dead effects
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //------OTHER FUNCTIONS ------------------------------

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle

        if (currentHealth > 0.0f)
        {
            SwitchState(State.KnockBack);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Jumping:
                ExitJumpingState();
                break;
            case State.KnockBack:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawCube(jumpGroundCheck.position, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerCheck.position, circleRadius);
    }

}
