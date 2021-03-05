/*
 Ce script donne les comportment de base des abilités, aux états JumpState et KickState
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;//variable pour desactiver l'abilité

    protected bool isGrounded;//variable pour verifié si le perosnnage touche le sol
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckifGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)//si l'abilité est terminer et que le personnage est immobile, change l'état vers PlayerIdle
        {
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);//sinon, change son état vers PlayerInAir
            }
        }
    }

    public void AbilityIsDone()
    {
        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
