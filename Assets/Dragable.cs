using Assets.Scripts.Entity.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    ItemData item;

    public bool IsDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        IsDragging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
