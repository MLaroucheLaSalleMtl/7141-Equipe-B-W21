/*
 Permet de gerer le comportement du coup de pied du personnage, l'état PlayerKick
 
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickState : PlayerAbilityState
{
    public PlayerKickState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);//immobilise le personnage pendant l'action
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        AbilityIsDone();
    }
}

