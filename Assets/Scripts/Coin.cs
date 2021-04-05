using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //S'il y a une collision avec un objet avec le tag "Player"
        {
            DestroySnowBallStack(); //Détruit le projectile
        }
    }

    void DestroySnowBallStack()
    {
        Destroy(gameObject);
    }
}
