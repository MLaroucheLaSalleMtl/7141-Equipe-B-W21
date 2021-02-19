using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 RawMoveInput { get; private set; } //le input de mouvement
    public int NormInputX { get; private set; } //le input pour le deplacement horizontale
    public int NormInputY { get; private set; } //le input pour le deplacement vertical (saut et/ou grimper)
  
    public bool JumpInput { get; private set; }
    public void OnMove(InputAction.CallbackContext context)
    {
        RawMoveInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMoveInput * Vector2.up).normalized.y;
    }
  
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;
}
