using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSnowBallBehavoir : MonoBehaviour
{
    private AudioSource clip;
    private SpriteRenderer sprite;
    private bool canPlay = true;

    private void Start()
    {
        clip = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemie"))
        {
            if (canPlay)
            {
                clip.Play();
                canPlay = false;
            }
            sprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
