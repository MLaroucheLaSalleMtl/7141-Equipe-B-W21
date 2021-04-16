/*
 Scriptable object qui contient les data du perosnnage joueur
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")] //pour exposer les paramere du joueur dans unity
public class PlayerData : ScriptableObject
{
    [Header("Main stats")]//variables stats du joueur
    public int maxHp = 5;
    [Range(0,5)]public int hp = 5;
    public int lives = 3;
    public bool canTakeDamage = true;
    [Range(0,50)] public int snowBallCount = 10;
    public int score = 0;

    [Header("Move State")]//variable de mouvement horizontale
    [Range( 0.5f,10f)]public float speedX = 5f;
    [Range( 0.01f,1f)]public float smoothDamp = 0.1f;
    public Vector2 zeroVelocity = Vector2.zero;

    [Header("Climb State")]
    public bool canClimb = false;
    public float climbSpdX = 1f;
    public float climbSpdY = 1f;
    public int gravityScale = 4;
    [Range(0.01f, 1f)] public float climbSmoothDamp = 0.1f;

    [Header("Jump State")]//variable de mouvement vertical
    [Range(0.5f,50f)]public float jumpVelocity = 15f;
    public int amountOfJump = 1;


    [Header("Check variables")]//variable check
    public float checkRadius = 0.3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsDamagable;
}
