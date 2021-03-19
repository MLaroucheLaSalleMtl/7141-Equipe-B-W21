using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingForGround : MonoBehaviour //https://www.youtube.com/watch?v=IgZQjGyB9zg
{
    private float scrollSpeed = -5.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 16);
        transform.localPosition = startPos + Vector3.up * newPos;
    }
}
