using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///(Fabian)
/// Pour ce code, je me suis basé sur la référence #3 (0:50) et #1 puis j'ai utilisé ma logique pour faire
/// en sorte que le tout soit fonctionnel et personnalisé pour le jeu. 
/// </summary>

public class PenguinBehavior : MonoBehaviour
{
    private enum State //Les states du penguin
    {
        Walking,
        Jumping,
        KnockBack,
    }

    private State currentState; //state utilisé présentement

    private void Update()  //Update le walkingState en premier 
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
        groundCheckDistance, //Distance du ground
        wallCheckDistance,   //Distance d'un wall
        movementSpeed,       //Vitesse du mouvement
        maxHealth,           //Points de vie Maximum
        knockBackDuration,   //La durée du knockback
        JumpDuration,        //La durée du Jump
        jumpHeight;          //La hauteur du Jump

    [SerializeField]
    private Transform
        groundCheck,         //Ce qui Check le ground
        wallCheck,           //Ce qui Check le Wall
        jumpGroundCheck,     //Ce qui check le ground pour jump
        playerCheck,         //Ce qui check le player
        kickCheck,           //Ce qui check le kick
        player;              //Le player

    [SerializeField] private LayerMask whatIsGround;    //Layer de Ground
    [SerializeField] private LayerMask whatIsPlayer;    //Layer du Player
    [SerializeField] private LayerMask whatIsEnemy;     //Layer du ennemy
   // [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private LayerMask whatIsKick;      //Layer de Kick

    private float
        currentHealth,                                  //nombre de points de vie
        knockbackStartTime,                             //Le début(temps) du knockback
        jumpStartTime;                                  //Le début du JumpStartTime

    [SerializeField] float circleRadius;                //Le radius du circle (qui detect le joueur)
    [SerializeField] float kickRadius;                  //Le radius de la detection du kick

    private int
        facingDirection;                                //La direction du personnage

    private Vector2 movement;                           //mouvement
    [SerializeField] Vector2 boxSize;                   //Box qui detetct le ground

    private bool
        groundDetected,                                 //bool de la détection du ground
        wallDetected,                                   //bool de la détection d'un wall
        playerDetected,                                 //bool de la detection du player
        kickDetected,                                   //bool de la déection d'un kick
        enemyDetected,                                  //bool de la détection d'un ennemie
        isGrounded;                                     //Bool de la detection du ground pour le jump

    private GameObject alive;                        
    private Rigidbody2D aliveRb;
  
    public Color flashColor;                           //Couleur du psrite lorsque le penguin est hit
    public Color baseColor;                            //Couleur du sprite lorsque le penguin est normal
    public SpriteRenderer mySprite;                    //sprite
    public HealthBarBehavior healthBar;                //Healthbar

    static System.Random rand = new System.Random();

    [Header("Random Objects")]
    public GameObject snowballs; //GameObject des snowballs
    public GameObject coins;     //GameObject du coin
    public GameObject iceCream;  //GameObject du icecream

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;     //Cherche le GameObject "Alive"
        aliveRb = alive.GetComponent<Rigidbody2D>();    //Prend le component RigidBody2D
        currentHealth = maxHealth;                      //Initialise le currentHealth au maxHealth
        healthBar.SetHealth(currentHealth, maxHealth);  //Ajuste la Health bar
        facingDirection = -1;                           //La direction initiale du penguin est -1
    }

    //--------WALKING STATE---------------------

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); //raycast qui vérifie s'il y a un sol
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);    //raycast qui vérifie s'ily a un wall
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer);                //Circle qui vérifie si le joueur est detected
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);                        //Circle qui vérifie s'il y a un kick detected
        enemyDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsEnemy);    //raycast qui vérifie s'ily a un autre ennemie detected
        mySprite.color = baseColor;                                                                                //Applique la couleur de base au sprite

        if (!groundDetected || wallDetected || enemyDetected) //S'il est dans les air ou il detect un wall ou il detect un ennemie
        {
            Flip(); //Appel la fonction Flip
        }
        else if (kickDetected) //S'il detect un kick
        {
             knockbackStartTime = Time.time; //KnockbackStartTime commence
             ReceiveDamage(1);               //Appel la fonction Receive Damage
             SwitchState(State.KnockBack);   //Rentre dans le State KnockBack
         }
        else if (playerDetected && !kickDetected) //S'il detect le player et ne detect pas de kick
        {
           isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround); //Vérifie s'il est au sol
           jumpStartTime = Time.time;   //JumpStartTime commence
           JumpAttack();                //Appel la fonction JumpAttack
           SwitchState(State.Jumping);  //Rentre dans le State Jumping
        } 
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y); //Ajustment du mouvement
            aliveRb.velocity = movement;                                       //Applique le mouvement

        }
    }
    //----------Jumping State --------------------

    private void UpdateJumpingState()
    {
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y); // Ajustement du mouvement
        aliveRb.velocity = movement;                                       // Applique le mouvement au rigidbody
        isGrounded = Physics2D.OverlapBox(jumpGroundCheck.position, boxSize, 0, whatIsGround); // (Box) qui regarde si le penguin est Grounded
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);    // (cercle) qui regarde s'il y a un kick detected
        
        if (kickDetected) //S'il y a un kcikdetected
        {
            knockbackStartTime = Time.time; //KnockbackstartTime s'active
            ReceiveDamage(1);               //Appel la fonction Receive Damage
            SwitchState(State.KnockBack);   //Rentre dans le state knockBack
        } 
        else if (Time.time >= jumpStartTime + JumpDuration && isGrounded) // Si le Penguin a fini son saut et q'il est au sol
        {
            SwitchState(State.Walking); //rentre dans le state Walking
        }
        
    }

    void JumpAttack() //Fonction Jump
    {
        float distanceFromPlayer = player.position.x - transform.position.x; //Calcule la distance du joueur par rapport au penguin

        if (isGrounded) //S'il est au sol
        {
            aliveRb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse); //Applique une force pour faire sauter le penguin en considérant la distance du Player.
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

    private void ReceiveDamage(int damage)              //Fonction qui reçoit les dégats
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);  //currentHealth diminue

        if (currentHealth <= 0.0f)                      //Si le currentHealth est égale ou plus bas que zéro
        {
            Destroy(gameObject);                        //Appel la fonction Destroy()
            RandomObject();                             //Appel la fonction RandomObjects();
        }
    }

    private void Flip() //Fonction qui change la direction du Narwal
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void RandomObject() //Fonction qui instantiate un objet random
    {
        int number = rand.Next(3); //Chiffre random entre 0 et 2

        if (number == 0) //Si 0, il instantiate un snowballStack
        {
            Instantiate(snowballs, aliveRb.transform.position, Quaternion.identity);
        }
        else if (number == 1) //Si 1, il instantiate un coin
        {
            Instantiate(coins, aliveRb.transform.position, Quaternion.identity);
        }
        else if (number == 2) //Si 2, il instantiate un iceCream
        {
            Instantiate(iceCream, aliveRb.transform.position, Quaternion.identity);
        }
    }

    private void SwitchState(State state) //Fonction qui change de State
    {
        currentState = state;
    }

    private void OnDrawGizmos() //Fonction qui permet de dessiner les guizmos pour une meilleure référence et des tests
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
