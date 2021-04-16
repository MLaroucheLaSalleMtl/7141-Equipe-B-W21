using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// (Fabian)
/// Pour ce code, je me suis basé sur la référence #2 (7:37 code) et #1 puis j'ai utilisé ma logique pour faire
/// en sorte que le tout soit fonctionnel et personnalisé pour le jeu. 
/// </summary>

public class OursBehavior : MonoBehaviour
{
    private float 
        timeBwShots, //Temps entre chaque tir
        currentHealth, //Le nombre de poits de vie présent
        knockBackStartTime; //Le début du knockBackStartTime

    public float startTimeBtwShots; //La durée entre chaque tir

    public GameObject projectile; //GameObject snowball
    private GameObject alive;     //GameObject Alive

    private int facingDirection;  //Direction de l'ours
    private int forceLancer = 6;

    private Transform player;     //Joueur

    [SerializeField] private Transform 
        playerCheck,
        kickCheck;

    private bool 
        playerDetected,
        kickDetected;

    [SerializeField] private LayerMask whatIsPlayer; //Layer du joueur
    [SerializeField] private LayerMask whatIsKick;   //Layer du Kick

    [SerializeField] float 
        circleRadius,    //radius du cercle qui detect  le joueur
        kickRadius;      //radius du cercle qui detect le kick du joueur

    [SerializeField]  private float 
        maxHealth,       //Le nombre de points de vie maximal
        knockBackDuration; //La durée du knockBack

    public Color flashColor;  
    public Color baseColor;
    public SpriteRenderer mySprite;
    public HealthBarBehavior healthBar;

    static System.Random rand = new System.Random();

    [Header("Random Objects")]
    public GameObject snowballs;
    public GameObject coins;
    public GameObject iceCream;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        facingDirection = -1;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        timeBwShots = startTimeBtwShots; //Initialise le timebtwShots a la durée entre chaque tirs
    }

    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer); //Regarde s'il y a un player
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);         //Regarde s'il y a un kick

        Shoot();
        TourneVersJoueur();

        if (kickDetected && Time.time >= knockBackStartTime + knockBackDuration) //S'il detect un kick et il n'est pas "knockback"
        {
            ReceiveDamage(1); //Appel la fonction ReceiveDamage avec 1 de dommage
        }

        if (Time.time >= knockBackStartTime + knockBackDuration) //S'il n'est plus dans un "knockback"
        {
            mySprite.color = baseColor; //L'ours reprend sa couleur initiale
        }

    }

    private void OnDrawGizmos()
    {
        
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
            RandomObject();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void TourneVersJoueur()
    {
        //if else qui regarde la position du joueur et qui change la direction de l'ours  pour qu'il regarde le joueur
        if (player.position.x > transform.position.x && facingDirection == -1)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
    }

    private void Shoot()  //fonction pour lancer un snowball
    {
        if (timeBwShots <= 0 && playerDetected) //S'il le timeBtwShots <= 0 et que le player est detected
        {
            GameObject projectileIns=Instantiate(projectile, transform.position, Quaternion.identity); //instantiate le projectile
            projectileIns.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingDirection * forceLancer, 5), ForceMode2D.Impulse);
            timeBwShots = startTimeBtwShots;        //Initialise le timebtwShots a la durée entre chaque tirs
        }
        else
        {
            timeBwShots -= Time.deltaTime; //Le timebtwShots diminue de 1 a chaque seconde
        }
    }

    void RandomObject() 
    {
        int number = rand.Next(3);

        if (number == 0)
        {
            Instantiate(snowballs, alive.transform.position, Quaternion.identity);
        }
        else if (number == 1)
        {
            Instantiate(coins, alive.transform.position, Quaternion.identity);
        }
        else if (number == 2)
        {
            Instantiate(iceCream, alive.transform.position, Quaternion.identity);
        }
    }

}
