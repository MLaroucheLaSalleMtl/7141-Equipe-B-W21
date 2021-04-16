using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerState
{
    private int xInput;
    public PlayerClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.Rigid.gravityScale = 0f;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.Rigid.gravityScale = playerData.gravityScale;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputMove.NormInputX;
        if (playerData.canClimb == false)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            player.SetVelocityY(playerData.climbSpdY);
            player.SetVelocityX(playerData.climbSpdX * xInput);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
