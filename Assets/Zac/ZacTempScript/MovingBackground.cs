using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] private GameObject cam;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(-((cam.transform.position.x - 13) * 0.022f), -((cam.transform.position.y - 25.5f) * 0.109f), 10f);
    }
}
