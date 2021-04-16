using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// (Fabian)
/// Voici un script qui a été codé sans source externe, juste ma logique.  
/// 
public class IceCream : MonoBehaviour
{
    private bool isConsumed = false;
    private AudioSource clip;
    private SpriteRenderer sprite;
    private Collider2D col;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        clip = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //S'il y a une collision avec un objet avec le tag "Player"
        {
            if (!isConsumed)
            {
                sprite.enabled = false;
                col.enabled = false;
                clip.Play();
                playerData.hp = playerData.maxHp; //Remet le health du player au maximum
                isConsumed = false;
            }
            yield return new WaitForSeconds(0.3f);
            DestroySnowBallStack(); //Détruit le projectile
        }
    }

    void DestroySnowBallStack()
    {
        Destroy(gameObject);
    }
}
