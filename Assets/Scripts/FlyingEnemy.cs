using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemy : MonoBehaviour
{
   
    public float speed, wallCheckDistance;

    private Rigidbody2D EnemyRB;
    public Transform rightCheck;
    public LayerMask groundLayer;
    private bool facingRight = false, righttouch;
    public float dirX = -1, dirY = 0f;


    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        EnemyRB.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        HitDetection();

    }

    void HitDetection()
    {
        righttouch = Physics2D.Raycast(rightCheck.position, transform.right, wallCheckDistance, groundLayer); //Regarde s'il y a un wall de Detected
        HitLogic();
    }

    void HitLogic()
    {
        if (righttouch && facingRight)
        {
            Flip();
        }
        else if(righttouch && !facingRight)
        {
            Flip();
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rightCheck.position, new Vector2(rightCheck.position.x + wallCheckDistance, rightCheck.position.y));
    }
}

