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
        kickCheck,
        player;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private LayerMask whatIsKick;

    private float
        currentHealth,
        knockbackStartTime,
        jumpStartTime;

    [SerializeField] float circleRadius;
    [SerializeField] float kickRadius;

    private int
        facingDirection;
     
    private Vector2 movement;
    [SerializeField] Vector2 boxSize;

    private bool
        groundDetected,
        wallDetected,
        playerDetected,
        kickDetected,
        enemyDetected,
        isGrounded;

    private GameObject alive;
    private Rigidbody2D aliveRb;
  
    public Color flashColor;
    public Color baseColor;
    public SpriteRenderer mySprite;
    public HealthBarBehavior healthBar;

    static System.Random rand = new System.Random();

    [Header("Random Objects")]
    public GameObject snowballs;
    public GameObject coins;
    public GameObject iceCream;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        facingDirection = -1;
    }

    //--------WALKING STATE---------------------

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer);
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);
        enemyDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsEnemy);
        mySprite.color = baseColor;

        if (!groundDetected || wallDetected || enemyDetected)
        {
            Flip();
        }
        else if (kickDetected)
        {
             knockbackStartTime = Time.time;
             ReceiveDamage(1);
             SwitchState(State.KnockBack);
         }
        else if (playerDetected && !kickDetected)
        {
           isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround);
           jumpStartTime = Time.time;
           JumpAttack();
           SwitchState(State.Jumping);
        } 
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;

        }
    }
    //----------Jumping State --------------------

    private void UpdateJumpingState()
    {
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
        aliveRb.velocity = movement;
        isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround);
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);
        
        if (kickDetected)
        {
            knockbackStartTime = Time.time;
            ReceiveDamage(1);
            SwitchState(State.KnockBack);
        } else
        if (Time.time >= jumpStartTime + JumpDuration && isGrounded)
        {
            SwitchState(State.Walking);
        }
        
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

    private void UpdateKnockbackState()
    {
        mySprite.color = flashColor;

        if (Time.time >= knockbackStartTime + knockBackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    //------OTHER FUNCTIONS ------------------------------

    private void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
            RandomObject();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void RandomObject()
    {
        int number = rand.Next(3);

        if (number == 0)
        {
            Instantiate(snowballs, aliveRb.transform.position, Quaternion.identity);
        }
        else if (number == 1)
        {
            Instantiate(coins, aliveRb.transform.position, Quaternion.identity);
        }
        else if (number == 2)
        {
            Instantiate(iceCream, aliveRb.transform.position, Quaternion.identity);
        }
    }

    private void SwitchState(State state)
    {
        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawCube(jumpGroundCheck.position, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerCheck.position, circleRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(kickCheck.position, kickRadius);

    }

}
