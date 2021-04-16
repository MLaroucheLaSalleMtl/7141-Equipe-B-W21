using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// (Fabian)
/// Ce script a complètement été créer en utilisant ma logique.
/// </summary>
public class PopUpPanel : MonoBehaviour
{
    [SerializeField] private GameObject player; // prend le gameObject du Player

    [SerializeField] private GameObject popUpPanel; //Prend le panel

    bool playerInRange = false;         //PlayerInRange = false

    void Update()
    {
        PlayerInRange();                             //Appel la fonction PlayerInRange
        popUpPanel.SetActive(playerInRange == true); //Active le panel lorsque PlayerInRange = true
    }

    void PlayerInRange()
    {   //Si le joueur est devant l'objet (petite distance devant l'objet de -0.75f ou + 0.75f), PlayerInRange devient alors true
        if (transform.position.x - player.transform.position.x <= 0.75f && transform.position.x - player.transform.position.x >= -0.75f) 
        {
            playerInRange = true;
        } //Sinon PlayerInRange reste false 
        else playerInRange = false;

    }
}
