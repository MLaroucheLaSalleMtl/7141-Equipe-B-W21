using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Script du cour de GameEngine 1 sur les UI

public class ButtonsBehavior :MonoBehaviour,IPointerEnterHandler,IDeselectHandler,IPointerDownHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);//simule que la souris est hors du bouton
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Button>() != null)//estce que lobjet selecitonner est un boutton
        {
            GetComponent<Button>().onClick.Invoke();//Declenche le press plutot que le release
        }
        Input.ResetInputAxes();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Selectable>().Select();
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
