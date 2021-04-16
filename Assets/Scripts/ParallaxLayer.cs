using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ce code provient de la source #5 et a été utilisé et recherché par Fabian et Zachary
/// Ce script s'applique à chacun des layers d'un background, on peut modifier leur vitesse de déplacement (position) individuellement
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;
    }
}