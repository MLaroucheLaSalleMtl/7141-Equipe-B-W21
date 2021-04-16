using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// (Fabian)
/// Voici un script qui a été codé sans source externe, juste ma logique.  
/// </summary>
public class Coin : MonoBehaviour
{
    private bool isConsumed = false;
    private AudioSource clip;
    private SpriteRenderer sprite;
    private Collider2D col;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        clip = GetComponent<AudioSource>();      //prend le component du audioSource
        sprite = GetComponent<SpriteRenderer>(); //prend le component du sprite
        col = GetComponent<Collider2D>();        //prend le component du collider
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))      //S'il y a une collision avec un objet avec le tag "Player"
        {
            if (!isConsumed)                     //si isConsumed est false
            {
                sprite.enabled = false;          //Désactive le sprite
                col.enabled = false;             //Désactive le collider
                clip.Play();                     //Active le son du clip
                playerData.score += 100;         //Ajoute 100 points au score
            }
            yield return new WaitForSeconds(0.3f);
            DestroySnowBallStack();              //Détruit le projectile
        }
    }

    void DestroySnowBallStack()
    {
        Destroy(gameObject);
    }
}
