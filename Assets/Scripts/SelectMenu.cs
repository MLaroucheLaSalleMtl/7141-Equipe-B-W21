using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultBtn = null;

    public void PanelToggle (int position)
    {  //Active et desactive les panneaux et boutons
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);//Activer le panel a la position x
            if(position == i)
            {
                defaultBtn[i].Select();//Mettre le focus sur le bouton du panel selectionne
            }
            else
            {

            }
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        PanelToggle(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
