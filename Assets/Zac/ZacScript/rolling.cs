using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling : MonoBehaviour
{
    private Collider2D trigger;
    [SerializeField] private Vector2 force;
    private Rigidbody2D rb;
    private GameManager code;
    [SerializeField] private float dist, maxDist;
    private Vector2 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        trigger = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        trigger.enabled = true;
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate() // détermine la position du player et, lorsque le player s'éloigne trop, il appelle origine
    {
        dist = Mathf.Sqrt(Mathf.Pow(code.Player.transform.position.x - transform.position.x, 2) + Mathf.Pow(code.Player.transform.position.y - transform.position.y, 2));

        if (rb.bodyType == RigidbodyType2D.Dynamic)
            dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - code.Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - code.Player.transform.position.y, 2));

        if (dist >= maxDist)
        {
            Origine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // lorsque le player rentre dans le trigger, le log ce jete vers lui
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(force);
            trigger.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // lorsque le log touche le joueur, il appelle Origine
    {
        if (collision.gameObject.CompareTag("Player"))
            Origine();
    }

    private void Origine() // le log retourne à sa position initial
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
        transform.rotation = startRot;
        trigger.enabled = true;
    }
}
