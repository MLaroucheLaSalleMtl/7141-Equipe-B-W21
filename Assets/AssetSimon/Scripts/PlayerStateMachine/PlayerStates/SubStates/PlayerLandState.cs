/*
 * 
 * Permet de gerer l'atterissage du personnage pour quitter l'état PlayerInAir.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);//immobilise le personnage
        if (xInput != 0)//si il y a input de mouvement, change l'état du personnage vers MoveState
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (isAnimDone)//si lanimation est terminer, change l'état du personnage vers IdleState
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
