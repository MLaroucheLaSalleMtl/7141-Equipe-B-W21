using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    [Range( 0.5f,10f)]public float speedX = 5f;
    [Range( 0.01f,1f)]public float smoothDamp = 0.1f;
    public Vector2 zeroVelocity = Vector2.zero;

    [Header("Jump State")]
    [Range(0.5f,50f)]public float jumpVelocity = 15f;

    [Header("Check variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
