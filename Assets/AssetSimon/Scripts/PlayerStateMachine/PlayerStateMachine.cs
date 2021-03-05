/*
 Ce script permet de gerer l'initialisation et de changement d'un etat a un autre
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
   
    public PlayerState CurrentState { get; private set; }//reference vers l'état courant du personnage

    public void initialize(PlayerState startingState)//initialise létat de base du personnage
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)//pour passer d'un etat a un autre
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();

    }
}
