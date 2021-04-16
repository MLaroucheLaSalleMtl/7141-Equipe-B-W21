using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ce code provient de la source #5 et a �t� utilis� et recherch� par Fabian et Zachary
/// Ce script s'applique � chacun des layers d'un background, on peut modifier leur vitesse de d�placement (position) individuellement
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