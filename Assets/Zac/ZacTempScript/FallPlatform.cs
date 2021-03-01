using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 origine;

    // Start is called before the first frame update
    void Start()
    {
        origine = this.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            Invoke("Reposition", 2.0f);
        }

        else if ((rb2d.bodyType == RigidbodyType2D.Dynamic) && (collision.gameObject.tag != "Player"))
            this.gameObject.SetActive(false);
    }

    private void Reposition()
    {
        this.transform.position = origine;
        this.gameObject.SetActive(true);
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
