using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// (Fabian)
/// Ce code provient de la référence #6 et les valeurs comme le offset et les couleur ont été ajuster pour bien
/// apparaître dans le jeu. 
/// </summary>


public class HealthBarBehavior : MonoBehaviour
{
    public Slider Slider; 
    public Color Low;     //couleur rouge
    public Color High;    //couleur verte
    public Vector3 Offset;  //offset pour la hauteur de la health bar

   public void SetHealth(float health, float maxHealth) //Ajuste le health bar en fonction de sopn current health et max health
    {
        Slider.gameObject.SetActive(health < maxHealth);  
        Slider.value = health;
        Slider.maxValue = maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue); //Active et remplie le slider avec les couleurs voulu dépendant du current health.
    }

    
    void Update() //Permet au health bar de rester au dessus de l'ennemy
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
