using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    //private float worldX;
    //private float worldY;
    private float localX;
    private float localY;

    [SerializeField] private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(-((cam.transform.position.x - 13) * 0.022f), -((cam.transform.position.y - 25.5f) * 0.109f), 10f);
    }
}
