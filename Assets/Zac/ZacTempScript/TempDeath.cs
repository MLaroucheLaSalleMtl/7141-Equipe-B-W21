using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDeath : MonoBehaviour
{
    private GameManager code;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance; 
    }

    private void OnCollisionEnter2D(Collision2D collision)  // sa touche le player sa tue le player
    {
        if (collision.gameObject.CompareTag("Player"))
            code.Death();
    }
}
