/*
 Ce scripte
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBehavior : MonoBehaviour
{
    public LayerMask whatIsDamageable;
    [SerializeField]private Transform hitbox;


    private void Update()
    {
        CheckAttackHitBox();
    }
    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObj = Physics2D.OverlapCircleAll(hitbox.position, hitbox.localScale.x, whatIsDamageable);

        foreach (Collider2D colliders in detectedObj)
        {
            colliders.transform.parent.SendMessage("ReceiveDamage",10);
        }

    }
}
