/*
Ce script est le script Parent de tout les differents états du Main Character.
Il donne les atribut de base a touts
https://www.raywenderlich.com/6034380-state-pattern-using-unity
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    public PlayerData playerData;
    protected bool isAnimDone;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;//reference vers le player
        this.stateMachine = stateMachine;//reference vers le state machine
        this.playerData = playerData;//reference vers playerData
        this.animBoolName = animBoolName;//reference vers le animation trigger
    }

    public virtual void Enter() //se fait appeller quand on entre dans un state
    { 
        DoCheck();
        player.Anim.SetBool(animBoolName, true);//active le animation trigger
        startTime = Time.time;//initialise le timer de l'animation
        //Debug.Log(animBoolName);
        isAnimDone = false; 

    }
    public virtual void Exit()//se fait appeller quand on quitte un state
    {
       player.Anim.SetBool(animBoolName, false);//pour sortir de l'état
    }
    public virtual void LogicUpdate() { } //se fait appeller a tout les frames
    public virtual void PhysicsUpdate() { } //se fait appeller a chaque fixedupdate
    public virtual void DoCheck() { } //est appeller par enter et/ou PhisicsUpdate pour executer des taches specifiques

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger()//se fait appeller a la fin d'un mouvement pour le terminer
    {
        isAnimDone = true;
    }

}
