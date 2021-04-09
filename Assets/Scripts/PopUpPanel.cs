using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject popUpPanel;

    bool playerInRange = false;

    void Update()
    {
        PlayerInRange();
        popUpPanel.SetActive(playerInRange == true);
    }

    void PlayerInRange()
    {
        if (transform.position.x - player.transform.position.x <= 0.75f && transform.position.x - player.transform.position.x >= -0.75f)
        {
            playerInRange = true;
        }
        else playerInRange = false;

    }
}
