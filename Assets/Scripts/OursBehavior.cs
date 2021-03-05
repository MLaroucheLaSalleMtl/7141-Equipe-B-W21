using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Pour ce code, je me suis bas� sur la r�f�rence #2 et #1 puis j'ai utilis� ma logique pour faire
/// en sorte que le tout soit fonctionnel. 
/// </summary>

public class OursBehavior : MonoBehaviour
{
    private float 
        timeBwShots, //Temps entre chaque tir
        currentHealth, //Le nombre de poits de vie pr�sent
        knockBackStartTime; //Le d�but du knockBackStartTime

    public float startTimeBtwShots; //La dur�e entre chaque tir

    public GameObject projectile; //GameObject snowball
    private GameObject alive;     //GameObject Alive

    private int facingDirection;  //Direction de l'ours

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
        knockBackDuration; //La dur�e du knockBack

    public Color flashColor;  
    public Color baseColor;
    public SpriteRenderer mySprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alive = transform.Find("Alive").gameObject;
        facingDirection = -1;
        currentHealth = maxHealth;
        timeBwShots = startTimeBtwShots; //Initialise le timebtwShots a la dur�e entre chaque tirs
    }

    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer); //Regarde s'il y a un player
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick);         //Regarde s'il y a un kick

        if (timeBwShots <= 0 && playerDetected) //S'il le timeBtwShots <= 0 et que le player est detected
        {
            Instantiate(projectile, transform.position, Quaternion.identity); //instantiate le projectile
            timeBwShots = startTimeBtwShots;        //Initialise le timebtwShots a la dur�e entre chaque tirs
        }
        else
        {
            timeBwShots -= Time.deltaTime; //Le timebtwShots diminue de 1 a chaque seconde
        }

        if (kickDetected && Time.time >= knockBackStartTime + knockBackDuration) //S'il detect un kick et il n'est pas "knockback"
        {
            ReceiveDamage(1); //Appel la fonction ReceiveDamage avec 1 de dommage
        }

        if (Time.time >= knockBackStartTime + knockBackDuration) //S'il n'est plus dans un "knockback"
        {
            mySprite.color = baseColor; //L'ours reprend sa couleur initiale
        }

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

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
