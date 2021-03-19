using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBossBehavior : MonoBehaviour
{
    private float
        timeBwShots, //Temps entre chaque tir
        knockBackStartTime; //Le début du knockBackStartTime

    public float startTimeBtwShots; //La durée entre chaque tir

    public GameObject projectile; //GameObject snowball
    private GameObject alive;     //GameObject Alive

    private Rigidbody2D aliveRb;

    private Vector2 movement;
    private int facingDirection;  //Direction de l'ours
    private int forceLancer = 6;

    private Transform player;     //Joueur

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        playerCheck,
        kickCheck;

    private bool
        groundDetected,
        wallDetected,
        playerDetected,
        kickDetected;

    [SerializeField] private LayerMask whatIsPlayer; //Layer du joueur
    [SerializeField] private LayerMask whatIsKick;   //Layer du Kick
    [SerializeField] private LayerMask whatIsGround;   //Layer du Ground

    [SerializeField]
    float
        circleRadius,    //radius du cercle qui detect  le joueur
        kickRadius;      //radius du cercle qui detect le kick du joueur

    [SerializeField]
    public float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        currentHealth,
        maxHealth,       //Le nombre de points de vie maximal
        knockBackDuration; //La durée du knockBack

    public Color flashColor;
    public Color baseColor;
    public SpriteRenderer mySprite;
    public HealthBarBehavior healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>(); //Prend le component RigidBody2D
        facingDirection = -1;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        timeBwShots = startTimeBtwShots; //Initialise le timebtwShots a la durée entre chaque tirs
    }

    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer); //Regarde s'il y a un player
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);         //Regarde s'il y a un kick
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);


        Shoot();
        TourneVersJoueur();
        Accelerate();

        if (kickDetected && Time.time >= knockBackStartTime + knockBackDuration) //S'il detect un kick et il n'est pas "knockback"
        {
            ReceiveDamage(1); //Appel la fonction ReceiveDamage avec 1 de dommage
        }

        if (Time.time >= knockBackStartTime + knockBackDuration) //S'il n'est plus dans un "knockback"
        {
            mySprite.color = baseColor; //L'ours reprend sa couleur initiale
        }
        
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, circleRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(kickCheck.position, kickRadius);
    }

    private void ReceiveDamage(int damage)
    {
        knockBackStartTime = Time.time;
        mySprite.color = flashColor;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
        
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void Shoot()
    {
        if (timeBwShots <= 0 && playerDetected) //S'il le timeBtwShots <= 0 et que le player est detected
        {
            GameObject projectileIns1 = Instantiate(projectile, aliveRb.position, Quaternion.identity); //instantiate le projectile
            GameObject projectileIns2 = Instantiate(projectile, aliveRb.position, Quaternion.identity); //instantiate le projectile
            GameObject projectileIns3 = Instantiate(projectile, aliveRb.position, Quaternion.identity); //instantiate le projectile
            projectileIns1.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 5), ForceMode2D.Impulse);
            projectileIns2.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 8), ForceMode2D.Impulse);
            projectileIns3.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 11), ForceMode2D.Impulse);
            timeBwShots = startTimeBtwShots;        //Initialise le timebtwShots a la durée entre chaque tirs
        }
        else
        {
            timeBwShots -= Time.deltaTime; //Le timebtwShots diminue de 1 a chaque seconde
        }
    }

    private void Accelerate()
    {
        if (!playerDetected)
        {
            movement.Set((movementSpeed*2) * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
        else
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
        aliveRb.velocity = movement;
    }

    private void TourneVersJoueur()
    {
        //if else qui regarde la position du joueur et qui change la direction de l'ours  pour qu'il regarde le joueur
        if (player.position.x > aliveRb.position.x && facingDirection == -1)
        {
            Flip();
        }
        else if (player.position.x < aliveRb.position.x && facingDirection == 1)
        {
            Flip();
        }
    }
}
