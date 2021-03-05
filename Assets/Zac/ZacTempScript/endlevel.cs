using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endlevel : MonoBehaviour
{
    private GameManager code;

    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) // fini le niveau et quit l'application
    {
        if (collision.CompareTag("Player"))
            code.LevelFinish();
    }
}
