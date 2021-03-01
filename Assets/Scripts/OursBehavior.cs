using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursBehavior : MonoBehaviour
{
    private float timeBwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    private Transform player;
    [SerializeField] private Transform playerCheck;

    private bool playerDetected;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] float circleRadius;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, circleRadius, whatIsPlayer);

        if (timeBwShots <= 0 && playerDetected)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBwShots = startTimeBtwShots;
        }
        else
        {
            timeBwShots -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, circleRadius);
    }
}
