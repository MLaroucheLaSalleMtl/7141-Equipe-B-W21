using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Pour cette partie du code, je me suis inspir� de la vid�o r�f�rence #2 ( pour pouvoir faire
/// fonctionner le d�placement du projectile. Je l'ai ensuite modifier un peu � ma fa�on pour que
/// le tout soit bien fonctionnel.
/// 
/// </summary>

public class Projectile : MonoBehaviour
{
    public float
        speed,
        offset = 1f;

    private Transform player; 
    private Vector2 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Cherche le gameobject player
        target = new Vector2(player.position.x, player.position.y + offset); //La position target est la position du player
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); //Se d�place vers la position du target

        if (transform.position.x == target.x && transform.position.y == target.y) //Si la position du projectile arrive � la position du target
        {
            DestroyProjectile(); //D�truit le projectile
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //S'il y a une collision avec un objet avec le tag "Player"
        {
            DestroyProjectile(); //D�truit le projectile
        }
        else if ( collision.CompareTag("Ground")) //S'il y a une collision avec un objet avec le tag "Ground"
        {
            DestroyProjectile(); //D�truit le projectile
        }
    }

    void DestroyProjectile() //Fonction qui d�truit le projectile
    {
        Destroy(gameObject); //D�truit le gameobject
    }
}
