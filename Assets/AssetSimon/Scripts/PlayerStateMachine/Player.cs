/*
 Donne les attribut au personage et execute les inputs qu'on lui envoi.
Il s'occupe entre autre de changer la velocite, la direction, les collisions, 
 

Coroutines,Unity Manuel,https://docs.unity3d.com/Manual/Coroutines.html 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; } //reference vers le state machone
    public PlayerIdleState IdleState { get; private set; } //reference vers le Idle state
    public PlayerMoveState MoveState { get; private set; } //reference vers le Move state
    public PlayerJumpState JumpState { get; private set; }//reference vers le Jump state
    public PlayerInAirState InAirState { get; private set; }//reference vers le InAir state
    public PlayerLandState LandState { get; private set; }//reference vers le Land state
    public PlayerKickState KickState { get; private set; }//reference vers le Kick state
    public PlayerKnockBackState KnockBackState { get; private set; }//reference vers le knockback 
    public PlayerThrowState ThrowState { get; private set; }//reference vers le knockback 
    public PlayerClimbState ClimbState { get; private set; }//reference vers le knockback 

    #endregion //reference vers chaque etats de PlayerState et vers le stateMachine

    #region Components
    public PlayerData playerData; //reference vers le player
    public PlayerMovement InputMove { get; private set; }//reference vers PlayerMouvement, qui recoit l'input du input system
    public Animator Anim { get; private set; }//reference vers l'animator
    public Rigidbody2D Rigid { get; private set; }//reference vers le rigidBody2d
    //public Vector2 CurrentVelocity { get; private set; }
    public GameObject snowball;

    public AudioSource clip;
    #endregion

    #region Check Transforms
    [SerializeField] private Transform groundCheck;//reference vers le ground check
    [SerializeField] private GameObject kickHitbox ;//reference vers le hitbox du kick
    [SerializeField] private GameObject snowBallSpawn ;//reference vers le hitbox du kick
    #endregion

    #region Other Variables
    public int FacingDirection { get; private set; } //la direction vers laquelle le personnage face
    public Vector2 workspace;//vecteur pour la vitesse ciblé
    public Vector2 CurrentVelocity { get; private set;}//vecteur pour la vitesse courante

    [Header("Invincibility variables")]
    public Color flashColor; //couleur du flash
    public Color baseColor;//couleur de base
    public float flashDuration;//durée du flash
    public int numOfFlashes;//nombre de flash    
    public SpriteRenderer mySprite;//reference vers le sprite renderer
    private Vector3 offsetL = new Vector3(-0.48f, 0.29f, 0);
    private Vector3 offsetR = new Vector3(0.48f, 0.29f, 0);
    
    #endregion

    #region Unity CallBack Functions
    private void Awake()
    {
        //initialisation du state Machine et des états
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        KickState = new PlayerKickState(this, StateMachine, playerData, "kick");
        ThrowState = new PlayerThrowState(this, StateMachine, playerData, "fire");
        ClimbState = new PlayerClimbState(this, StateMachine, playerData, "climb");
        KnockBackState = new PlayerKnockBackState(this, StateMachine, playerData, "knockback");

    }

    void OnTriggerStay2D(Collider2D collision)//pour detecter la colision avec un collider qui endommage le player
    {
        if (playerData.canTakeDamage)
        {
            if (collision.CompareTag("Enemie"))//verifie le tag du collider touché
            {
                ReceiveDamage(1);//recoit des degat
                StartCoroutine(FlashCo());//devient invulnerable
            }
            
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("SnowBallStack"))
        {
            playerData.snowBallCount += 10;
        }
        if (collision.CompareTag("IceCream"))
        {
            playerData.hp = playerData.maxHp;
        }
        if (collision.CompareTag("Coin"))
        {
            playerData.score += 100;
        }
        if (playerData.canTakeDamage)//si le personnage peu prendre des degat
        {
            if (collision.CompareTag("Projectile"))//verifie le tag du collider touche
            {
                ReceiveDamage(1);//recoit les degat
                StartCoroutine(FlashCo());//devient invulnerable
            }
        }
       
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();//cache l'animator
        InputMove = GetComponent<PlayerMovement>();//cache le input handler
        Rigid = GetComponent<Rigidbody2D>();//cache le rigidbody
        clip = GetComponent<AudioSource>();
        playerData.hp = 5;//set le nombre de hp du joueur a 5
        playerData.canTakeDamage = true;
        playerData.snowBallCount = 10;
        FacingDirection = 1;

        StateMachine.initialize(IdleState);//initialise letat de base a IdleState

    }
    private void Update()
    {
        CurrentVelocity = Rigid.velocity;//vitesse courante
        StateMachine.CurrentState.LogicUpdate();//methode de l'état courant qui est appeller a chaque frame
        
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();//methode de l'état courant qui est appeller a chaque intervalle constant
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)//applique la vitesse au personnage horizontalement
    {
        workspace.Set(velocity * playerData.speedX, CurrentVelocity.y);
        Rigid.velocity = Vector2.SmoothDamp(Rigid.velocity, workspace, ref playerData.zeroVelocity, playerData.smoothDamp );
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)//applique la vitesse au personnage verticalement
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void ReceiveDamage(int damage)
    {
        playerData.hp = playerData.hp - damage;
        StateMachine.ChangeState(KnockBackState);
    }

    public void EnableKickHitbox()
    {
        if (FacingDirection < 0)
        {
            kickHitbox.transform.position = this.transform.position + offsetL;
        }
        else
        {
            kickHitbox.transform.position = this.transform.position + offsetR;
        }
        kickHitbox.SetActive(true);
    }
    public void DisableKickHitbox()
    {
        kickHitbox.SetActive(false);
    }

    #endregion

    #region Check Functions

    public bool CheckifGrounded()//retoure vrai 
    {
        return Physics2D.OverlapCircle(groundCheck.position,playerData.groundCheckRadius,playerData.whatIsGround);
    }
    public void FlipCheck(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            FlipCharacter();
        }
    }
    #endregion
    private void FlipCharacter()//change la direction du personnage, soit gauche ou droite
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);//fait une rotation de 180 degre sur l'axe des y
    }

    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    //*Coroutine
    private IEnumerator FlashCo()//coroutine pour que le personange change de couleur lorsqu'il est invulnerable
    {
        int temp = 0;
        while(temp < numOfFlashes)
        {
            playerData.canTakeDamage = false;//le joueur est invulnerable
            mySprite.color = flashColor;//la couleur du personnage change
            yield return new WaitForSeconds(flashDuration);//temp dattente
            mySprite.color = baseColor;//la couleur du personnage retourne vers l'original
            yield return new WaitForSeconds(flashDuration);//temp dattente
            temp++;
        }
        playerData.canTakeDamage = true;//le joueur redevient vulnerable
    }

    public void ThrowSnowBall()
    {
        GameObject projectileIns1 = Instantiate(snowball, snowBallSpawn.transform.position, Quaternion.identity); //instantiate le projectile
        projectileIns1.GetComponent<Rigidbody2D>().AddForce(new Vector2(FacingDirection * 6, 5), ForceMode2D.Impulse);
        playerData.snowBallCount--;
    }

}
