using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSnowBallBehavoir : MonoBehaviour
{
    private AudioSource clip;//reference vers le clip
    private SpriteRenderer sprite;//reference vers le sprite
    private bool canPlay = true;//bool pour faire jouer le son 1 seul fois
    private Collider2D col;//reference vers le collider
    [SerializeField]private GameObject particles;//reference vers le particle system

    private void Start()
    {
        clip = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();    
    }
    // Start is called before the first frame update
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemie"))//si la boulle de neige touche le sol ou un enemie
        {
            if (canPlay)
            {
                clip.Play();//jou le son une seul fois
                canPlay = false;
            }
            sprite.enabled = false;//desactive le sprite
            col.enabled = false;//desactive le collider
            GameObject newParticles = Instantiate(particles, transform.position, Quaternion.identity);//instantie l'effet de particule
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }
    }
}
