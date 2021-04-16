using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// (Fabian)
/// Pour cette partie du code, je me suis basé sur la vidéo #1 dans les références puis ensuite ma logique pour
/// faire en sorte que tout fonctionne bien et soit personnalisé pour le jeu. La viédo a été utile pour comprendre comment les states 
/// fonctionne et comment gérer les mouvements des ennemis avec des obstacles (wall, etc)
/// </summary>

public class NarwalBehavior : MonoBehaviour
{
    private enum State // Les States du Narwal
    {
        Walking,
        KnockBack
    }

    private State currentState; //State utilisé présentement

    private void Update() //Update le walkingState en premier 
    {
        switch (currentState)
        {
            case State.Walking: 
                UpdateWalkingState(); 
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
        knockBackDuration;   //La durée du knockback

    [SerializeField] private Transform
        groundCheck,         //Ce qui Check le ground
        wallCheck,           //Ce qui Check le Wall
        kickCheck;           //Ce qui check le Kick

    [SerializeField] private LayerMask whatIsGround; //Layer de Ground
    [SerializeField] private LayerMask whatIsKick;   //Layer de Kick
    [SerializeField] private LayerMask whatIsEnemy;  //Layer du Enemy
   
    [SerializeField] float kickRadius; //Le radius de la detection du kick

    private float
        currentHealth,      //nombre de points de vie
        knockbackStartTime; //Le début(temps) du knockback 

    private int
        facingDirection;    //La direction du personnage
        
    private Vector2 movement;  //mouvement

    private bool
        groundDetected,     //bool de la détection du ground
        wallDetected,       //bool de la détection d'un wall   
        kickDetected,       //bool de la déection d'un kick
        enemyDetected;      //bool de la détection d'un ennemie

    private GameObject alive;     
    private Rigidbody2D aliveRb;
    

    public Color flashColor; //Couleur du personnage lorsqu'il reçoit des dommages
    public Color baseColor;  //Couleur normal du personnage
    public SpriteRenderer mySprite; //Sprite du personnage
    public HealthBarBehavior healthBar;

    static System.Random rand = new System.Random(); //system random

    [Header("Random Objects")]
    public GameObject snowballs; //GameObject des snowballs
    public GameObject coins;     //GameObject du coin
    public GameObject iceCream;  //GameObject du icecream

    private void Start()
    {
        alive = transform.Find("Alive").gameObject; //Cherche le GameObject "Alive"
        aliveRb = alive.GetComponent<Rigidbody2D>(); //Prend le component RigidBody2D
        currentHealth = maxHealth; //Initialise le currentHealth au maxHealth
        facingDirection = -1; //La direction initiale du Narwal est -1
        healthBar.SetHealth(currentHealth, maxHealth);  //Ajuste la Health bar
    }

    //--------WALKING STATE---------------------

    private void UpdateWalkingState() //Fonction initiale du Narwal
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); //Regarde si le ground est Detected
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround); //Regarde s'il y a un wall de Detected
        kickDetected = Physics2D.OverlapCircle(kickCheck.position, kickRadius, whatIsKick); //Regarde s'il y a un kick de detected
        enemyDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsEnemy); //Regarde s'il y a un ennemie de Detected
        mySprite.color = baseColor; //Mets la couleur de base au Narwal

        if (!groundDetected || wallDetected || enemyDetected) //Si le ground n'est pas detected ou il y a un wall detected ou un ennemie detected
        {
            Flip(); //Le personnage change de direction
        }
        else if (kickDetected) //S'il y a un kick detected
        {
            knockbackStartTime = Time.time; //Initialise le knockbackStartTime
            ReceiveDamage(1);               //Appel la fonction ReceiveDamage() avec un dommage de 1
            SwitchState(State.KnockBack);   //Change de state pour passer au knockBackState
            
        } 
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y); //Le personnage se déplace
            aliveRb.velocity = movement;                                       //La vitesse du Narwal = mouvement
        }
    }

    //--------  KNOCKBACK STATE ------------------------

    private void UpdateKnockbackState() //Fonction utilisé lorsque le Narwal est dans le knockbackState
    {
        mySprite.color = flashColor; //Le narwal change de couleur(rouge) 
        if(Time.time >= knockbackStartTime + knockBackDuration) //Si le temps du knockbackDuration est écoulé
        {
            SwitchState(State.Walking); //Change de state pour passer au walkingState
        }
    }

    //------OTHER FUNCTIONS ------------------------------

    private void ReceiveDamage(int damage) //Fonction qui reçoit les dégats
    {
        currentHealth -= damage;           //currentHealth diminue
        healthBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)         //Si le currentHealth est égale ou plus bas que zéro
        {
            Destroy(gameObject);           //Appel la fonction Destroy()
            RandomObject();                //Appel la fonction RandomObjects()
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
        else if ( number == 1) //Si 1, il instantiate un coin
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(kickCheck.position, kickRadius);
    }
}

   

