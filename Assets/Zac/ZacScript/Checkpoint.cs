using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Checkpoint : MonoBehaviour
{
    private GameManager code;
    private BoxCollider2D col2d;

    private SpriteRenderer sprtR;
    [SerializeField] private Sprite lit;

    private Animator anim;
    private AudioSource crack;

    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        sprtR = GetComponent<SpriteRenderer>();
        col2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsFire", false);
        crack = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Lorsque le player touche au checkpoint(bonfire), il s'allume
    {
        if ((collision.CompareTag("Player")) && (col2d.isActiveAndEnabled))
        {
            code.GetCheckpoint(this.gameObject);
            sprtR.sprite = lit;
            col2d.enabled = false;
            anim.SetBool("IsFire", true);
            crack.Play();
        }
    }
}
