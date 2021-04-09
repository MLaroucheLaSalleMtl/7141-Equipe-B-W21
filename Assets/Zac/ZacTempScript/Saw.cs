using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour  // Code baser sur mon code (Zachary) utiliser dans mon projet de Game Engine 1
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private bool movingState = true;
    private Transform obj;
    private Vector3 newPos;
    [SerializeField] private float smooth;
    [SerializeField] private float pause;
    private float currTime;
    private int index;
    private bool loop = true;


    // Use this for initialization
    void Start()
    {
        obj = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate() // https://www.youtube.com/watch?v=AfwmRYQRsbg 
    {
        if (movingState)
        {
            if (index < wayPoints.Length)
            {
                obj.position = Vector3.Lerp(obj.position, newPos, Time.deltaTime * smooth);
                Watching();
            }
            else if (loop)
            {
                index = 0;
            }
        }
    }

    private void Watching()
    {
        newPos = wayPoints[index].position;
        if (Vector3.Distance(obj.position, wayPoints[index].position) <= 0.1f)
        {
            if (currTime == 0)
                currTime = Time.time; // Pause over the Waypoint
            if ((Time.time - currTime) >= pause)
            {
                index++;
                currTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        movingState = false;
    }

    private void OnTriggerExit(Collider other)
    {
        movingState = true;
    }
}
