/*
Ce script permet de lire l'input de l'input system 
et de passer les variables au player.
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Testing.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 RawMoveInput { get; private set; } //le input brut de mouvement
    public int NormInputX { get; private set; } //le input normaliser pour le deplacement horizontale
    public int NormInputY { get; private set; } //le input normaliser pour le deplacement vertical 

    public bool JumpInput { get; private set; }//le input pour le saut
    public bool KickInput { get; private set; }//le input pour l'attaque

    [SerializeField] private float inputHoldTime = 0.2f;//Temp de latence pour activer le saut

    private float jumpInputStartTime;//temp auquel on active le jump input

    private void Update()
    {
        CheckJumpInputHoldTime();
    }
    public void OnMove(InputAction.CallbackContext context)//method envoyer a l'input system
    {
        RawMoveInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMoveInput * Vector2.up).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)//method envoyer a l'input system
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    public void UseJumpInput() => JumpInput = false;//pour utiliser le saut
    public void UseKickInput() => KickInput = false;//pour utiliser le coup de pied

    private void CheckJumpInputHoldTime()//verifie si le temp de latence du saut est plus petit que le temp de latence precedement declarer. Pour permettre au jooueur de sauter 0.2 sec apres avoir quitter le sol
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnKick(InputAction.CallbackContext context)//method envoyer a l'input system
    {
        if (context.started)
        {
            KickInput = true;
        }
    }
}
