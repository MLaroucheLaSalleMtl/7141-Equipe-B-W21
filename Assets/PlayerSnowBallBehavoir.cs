using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnowBallBehavoir : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
