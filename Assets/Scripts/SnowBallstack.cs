using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// (Fabian)
/// Voici un script qui a été codé sans source externe, juste ma logique.  
/// </summary>
public class SnowBallstack : MonoBehaviour
{
    private AudioSource clip;
    private SpriteRenderer sprite;
    private Collider2D col;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        clip = GetComponent<AudioSource>();       //Prend le component de l'audio
        sprite = GetComponent<SpriteRenderer>();  //Prend le component du sprite
        col = GetComponent<Collider2D>();         //Prend le component du collider
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))       //S'il y a une collision avec un objet avec le tag "Player"
        {
            sprite.enabled = false;               //Désactive le sprite
            col.enabled = false;                  //Désactive le collider
            clip.Play();                          //Fait jouer un son
            playerData.snowBallCount += 10;       //Rajoute 10 snowballs au player
            yield return new WaitForSeconds(0.3f); 
            DestroySnowBallStack();               //Détruit le snowballstack (gameObject)
        }
    }

        void DestroySnowBallStack() 
    {
        Destroy(gameObject);
    }
}
