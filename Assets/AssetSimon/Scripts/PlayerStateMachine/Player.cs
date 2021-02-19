using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; } //reference vers le state machone
    public PlayerIdleState IdleState { get; private set; } //reference vers le Idle state
    public PlayerMoveState MoveState { get; private set; } //reference vers le Move state
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    #endregion

    #region Components
    [SerializeField] private PlayerData playerData;
    public PlayerMovement InputMove { get; private set; }//reference vers le Idle state
    public Animator Anim { get; private set; }
    public Rigidbody2D Rigid { get; private set; }
    //public Vector2 CurrentVelocity { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] private Transform groundCheck;
    #endregion

    #region Other Variables
    public int FacingDirection { get; private set; }
    public Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set;}

    #endregion


    #region Unity CallBack Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputMove = GetComponent<PlayerMovement>();
        Rigid = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = Rigid.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)//applique la vitesse au personnage
    {
        workspace.Set(velocity * playerData.speedX, CurrentVelocity.y);
        Rigid.velocity = Vector2.SmoothDamp(Rigid.velocity, workspace, ref playerData.zeroVelocity,playerData.smoothDamp );
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigid.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions

    public bool CheckifGrounded()
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
    private void FlipCharacter()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
}
