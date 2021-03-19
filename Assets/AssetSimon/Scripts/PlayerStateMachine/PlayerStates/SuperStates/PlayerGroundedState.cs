/*
 Super État qui donne le comportement a tout ses sous états. Il determine le comportement du personnage quand il est au sol.

 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundedState : PlayerState
{
    protected int xInput;//input mouvement horizontal

    private bool JumpInput;//input pour sauter
    private bool Kickinput;//input pour coup de pied
    private bool Fireinput;//input pour coup de pied

    private bool isGrounded;//pour determiner si le personnage touche au sol
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        player.JumpState.ResetAmountOfJumpLeft();  //redonne la possibilite au personnage de sauter
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isGrounded = player.CheckifGrounded();   //verifie i le personnage touche au sol

        xInput = player.InputMove.NormInputX;
        JumpInput = player.InputMove.JumpInput;
        Kickinput = player.InputMove.KickInput;
        Fireinput = player.InputMove.FireInput;
        if (Fireinput)
        {
            player.InputMove.UseFireInput();
            stateMachine.ChangeState(player.ThrowState);
        }
        if (Kickinput)//si je joueur kick, utilise le kickInput et change l'etat du personnage vers l'etat PlayerKick 
        {
            player.InputMove.UseKickInput();
            stateMachine.ChangeState(player.KickState);
        }
        if (JumpInput && player.JumpState.CanJump())//si le joueur peu sauter, utilise le jump input et change l'état du personnage vers PlayerJump
        {
            player.InputMove.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }else if (!isGrounded)//Si le joueur ne touche plus au sol, diminue le nombre de saut restant et change l'état du personnage vers PlayerInAir
        {
            player.JumpState.DecreaseAmountOfJumpLeft();
            stateMachine.ChangeState(player.InAirState);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
