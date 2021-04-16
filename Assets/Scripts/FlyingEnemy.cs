using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// (Fabian)
/// Ce script a �t� inspir� par la vid�o de la r�f�rence #4, ou je vois comment je peux modifier la direction en x et y et comment 
/// je peut rajouter plusieur points de contact pour faire rebondir le oiseau en une direction voulu d�pendant de ce qu'il touche (ground, wall, roof).
/// Cependant, je n'ai pas eu besoin de tout cela et les codes ressemble beaucoup � ce que j'avais d�j� utilis� pour mes autres ennemies, donc 
/// tout cela pour dire que ce code a �t� modifi� en utilisant ma logique et en me r�f�rent � mes autres ennemis que j'avais d�j� fait.
/// </summary>

public class FlyingEnemy : MonoBehaviour
{
   
    public float speed, wallCheckDistance;

    private Rigidbody2D EnemyRB;                   //RigidBody
    public Transform rightCheck;                   //Ce qui check
    public LayerMask groundLayer;                  //Layer du ground, utlilis�comme un wall 
    private bool facingRight = false, righttouch;  
    public float dirX = -1, dirY = 0f;             // direction en X et en Y


    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
        EnemyRB.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        HitDetection(); //Regarde s'il y a un wall

    }

    void HitDetection()
    {
        righttouch = Physics2D.Raycast(rightCheck.position, transform.right, wallCheckDistance, groundLayer); //Regarde s'il y a un wall de Detected
        HitLogic(); //Appel la fonction Hitlogic si un wall est detected
    }

    void HitLogic() //Flip le bird 
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
    void Flip() //Tourne le bird et change sa direction en x
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

