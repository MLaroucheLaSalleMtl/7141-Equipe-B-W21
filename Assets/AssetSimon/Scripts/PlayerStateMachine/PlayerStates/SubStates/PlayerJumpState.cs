/*
 Permet de gerer le comportement du personnage lorsqu'il 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJump;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);
        amountOfJumpLeft--;
        isAbilityDone = true;
    }

    public bool CanJump()
    {
        if (amountOfJumpLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpLeft()
    {
        amountOfJumpLeft = playerData.amountOfJump;
    }

    public void DecreaseAmountOfJumpLeft()
    {
        amountOfJumpLeft--;
    }

}
