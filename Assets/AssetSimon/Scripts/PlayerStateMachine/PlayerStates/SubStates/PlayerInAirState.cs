/*
 Permet de gerer le comportement du personnage lorsqu'il est dans les air
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    private int xInput;
    private bool isGrounded;
    private bool jumpInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isGrounded = player.CheckifGrounded();

        xInput = player.InputMove.NormInputX;
        jumpInput = player.InputMove.JumpInput;

        if(isGrounded && player.CurrentVelocity.y < 0.01f)//si le personnage touche au sol et que sa velocité.y est presque null, change l'tat du personnage vers LandState
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())//autrement, si le personnage a encore la possibilité de sauter(double jump par exemple),  change l'etat du personnage vers JumpState
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else//sinon, le personnage peu bouger horizontalement dans les air
        {
            player.FlipCheck(xInput);
            player.SetVelocityX(playerData.speedX * xInput);
        }
    }

}
