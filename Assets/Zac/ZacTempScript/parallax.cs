using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour // https://www.youtube.com/watch?v=zit45k6CUMk (script complet à partir de 7:14)
{
    private float length, startPos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length) 
            startPos -= length;
    }
}
