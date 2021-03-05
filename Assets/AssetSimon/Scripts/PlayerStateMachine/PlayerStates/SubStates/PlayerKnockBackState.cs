/*
 Permet de gerer le comportement du personnage quand il prend des dégat
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBackState : PlayerState
{
    public PlayerKnockBackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);//immobilise le personnage
        if (isAnimDone)
        {
            stateMachine.ChangeState(player.IdleState);//quand l'animation est terminer, change l'état du personnage vers IdleState
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
