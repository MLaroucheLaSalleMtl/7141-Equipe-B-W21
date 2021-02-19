using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected bool isAnimDone;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() //se fait appeller quand on entre dans un state
    { 
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimDone = false;

    }
    public virtual void Exit()//se fait appeller quand on quitte un state
    {
       player.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { } //se fait appeller a tout les frames
    public virtual void PhysicsUpdate() { } //se fait appeller a chaque fixedupdate
    public virtual void DoCheck() { } //est appeller par enter et/ou PhisicsUpdate pour executer des taches specifiques

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger()
    {
        isAnimDone = true;
    }

}
