using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ce code provient de la source #5 et a été utilisé et recherché par Fabian et Zachary
/// Ce script s'applique à la caméra du jeu pour que le script background et layers soient fonctionnels

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    void Start()
    {
        oldPosition = transform.position.x;
    }
    void FixedUpdate()
    {
        if (transform.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPosition - transform.position.x;
                onCameraTranslate(delta);
            }
            oldPosition = transform.position.x;
        }
    }
}