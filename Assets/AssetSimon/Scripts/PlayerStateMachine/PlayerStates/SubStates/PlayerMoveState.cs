using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.clips[0].Play();
    }

    public override void Exit()
    {
        player.clips[0].Stop();
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.FlipCheck(xInput);

        player.SetVelocityX(playerData.speedX * xInput);



        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}
