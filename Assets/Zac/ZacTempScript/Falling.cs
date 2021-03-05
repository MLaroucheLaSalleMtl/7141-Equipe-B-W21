using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private LayerMask mask;
    private Vector2 origine;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.simulated = false;
        origine = this.transform.position;
    }

    // Update is called once per frame
    void Update() // lorsque le player passe sous le glacier, il lui tombe dessus
    {
        RaycastHit2D hit;
        mask = LayerMask.GetMask("Player");
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, 30, mask);
        Debug.DrawRay(this.transform.position, Vector2.down);

        if (hit.collider.CompareTag("Player"))
        {
            rb2d.simulated = true;
        }
        else
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // lorsque le glacier touche quelque chose, il retourne a sa place initiale
    {
        rb2d.simulated = false;
        this.transform.position = origine;
        this.gameObject.SetActive(true);
    }
}
