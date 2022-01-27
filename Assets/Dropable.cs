using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropable : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    GameObject hoverObject;

    public delegate DropHandler DropHandler();

    public DropHandler dropAction { get; set; }


    public void OnDrop(PointerEventData eventData)
    {
        dropAction();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }

}
