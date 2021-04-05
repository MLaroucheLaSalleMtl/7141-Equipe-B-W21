using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnowBallBehavoir : MonoBehaviour
{

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground") || collision.CompareTag("Enemie"))
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
