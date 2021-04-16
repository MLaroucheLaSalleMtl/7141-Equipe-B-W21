using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// (Fabian)
///  Ce script combine plusieurs fonctionnalités des ennemis et je les ai modifier utilisant ma logique pour qu'ils soient intéressantes. Par exemple,
///  il lance des snowballs comme le bear mais celui-ci instantie  snowballs à la fois. Il a aussi une fonction accelerate qui se démarqe des ennemis communs.
/// </summary>
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
        groundCheck,              //Ce qui Check le ground
        wallCheck,                //Ce qui Check le Wall
        playerCheck,              //Ce qui Check le Player
        kickCheck;                //Ce qui Check le kick

    private bool
        groundDetected,           //bool de la détection du ground
        wallDetected,             //bool de la détection du wall
        playerDetected,           //bool de la détection du player
        kickDetected;             //bool de la détection du kick

    [SerializeField] private LayerMask whatIsPlayer; //Layer du joueur
    [SerializeField] private LayerMask whatIsKick;   //Layer du Kick
    [SerializeField] private LayerMask whatIsGround;   //Layer du Ground

    [SerializeField]
    float
        circleRadius,    //radius du cercle qui detect  le joueur
        kickRadius;      //radius du cercle qui detect le kick du joueur

    [SerializeField]
    public float
        groundCheckDistance,    //Distance du ground
        wallCheckDistance,      //Distance d'un wall
        movementSpeed,          //Vitesse du mouvement
        currentHealth,          //points de vie
        maxHealth,              //Le nombre de points de vie maximal
        knockBackDuration;      //La durée du knockBack

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
        
        if (!groundDetected || wallDetected) //si le ground ou le wall n'est pas detected
        {
            Flip(); //Appel la fonction Flip()
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
            GameObject projectileIns2 = Instantiate(projectile, aliveRb.position, Quaternion.identity); 
            GameObject projectileIns3 = Instantiate(projectile, aliveRb.position, Quaternion.identity); 
            projectileIns1.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 5), ForceMode2D.Impulse); //Applique une force au projectile
            projectileIns2.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 8), ForceMode2D.Impulse);
            projectileIns3.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 11), ForceMode2D.Impulse);
            timeBwShots = startTimeBtwShots;        //Initialise le timebtwShots a la durée entre chaque tirs
        }
        else
        {
            timeBwShots -= Time.deltaTime; //Le timebtwShots diminue de 1 a chaque seconde
        }
    }

    private void Accelerate() //fonction qui accélère le boss
    {
        if (!playerDetected) //si le player n'est pas detected
        {
            movement.Set((movementSpeed*2) * facingDirection, aliveRb.velocity.y); //Accélèration de 2x le mouvement du Boss
            aliveRb.velocity = movement;
        }
        else
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y); //Se déplace à une vitesse normale
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
