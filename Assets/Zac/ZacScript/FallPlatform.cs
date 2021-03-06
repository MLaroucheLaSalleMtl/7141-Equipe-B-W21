using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 origine;
    private PlatformEffector2D pe2d;

    // Start is called before the first frame update
    void Start()
    {
        origine = this.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
        pe2d = GetComponent<PlatformEffector2D>();
        pe2d.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))// si le player touche a la plateforme, la plateforme tombe
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            pe2d.enabled = true;
            Invoke("Reposition", 2.0f);
        }

        else if ((rb2d.bodyType == RigidbodyType2D.Dynamic) && (collision.gameObject.tag != "Player")) // si la platforme touche quelque chose apr�s quelle tombe, elle disparait
        {
            this.gameObject.SetActive(false);
            pe2d.enabled = false;
        }
    }

    private void Reposition() // repositionne la platforme a sa place initiale
    {
        this.transform.position = origine;
        this.gameObject.SetActive(true);
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
