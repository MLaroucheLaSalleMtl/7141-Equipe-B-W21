using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private LayerMask mask;
    private Vector2 origine;
    private Animator anim;
    private Collider2D trigger;
    [SerializeField] private bool canIdle = false;
    [SerializeField] private int distance = 100;
    [SerializeField] private float time = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.simulated = false;
        origine = this.transform.position;
        anim = GetComponentInParent<Animator>();
        anim.enabled = true;
        trigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() // lorsque le player passe sous le glacier, il lui tombe dessus
    {
        RaycastHit2D hit;
        mask = LayerMask.GetMask("Player");
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, mask);
        Debug.DrawRay(this.transform.position, Vector2.down);

        if (canIdle)
        {
            InvokeRepeating("Idle", 0.1f, 5.0f);
            InvokeRepeating("NotIdle", 2.0f, 5.0f);
        }

        if (hit.collider.CompareTag("Player"))
        {
            rb2d.simulated = true;
            anim.enabled = false;
            Invoke("Restart", time);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // lorsque le glacier touche quelque chose, il retourne a sa place initiale
    {
        Restart();
    }

    private void Restart()
    {
        rb2d.simulated = false;
        this.transform.position = origine;
        this.gameObject.SetActive(true);
        anim.enabled = true;
    }

    private void Idle()
    {
        anim.SetBool("Idle", true);
    }

    private void NotIdle()
    {
        anim.SetBool("Idle", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            canIdle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canIdle = false;
    }
}
