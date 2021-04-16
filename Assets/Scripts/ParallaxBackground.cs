using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce code provient de la source #5 et a été utilisé et recherché par Fabian et Zachary
/// </summary>
[ExecuteInEditMode]


public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera; 
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;
        SetLayers();
    }

    void SetLayers() //Renomme et prend les components (layers du background)
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null) //Renomme les layers en ordre
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }
    void Move(float delta) //Déplace les layers
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }

}
