using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class PanelClick : MonoBehaviour, IPointerClickHandler
    {
        public int itemId;
        public delegate void ClickHandling();
        public ClickHandling leftHandler;
        public ClickHandling rightHandler;
        public ClickHandling doubleLeftHandler;

        float lastClick = 0f;
        float interval = 0.4f;

        public void OnPointerClick(PointerEventData eventData)
        {
            int clickCount = eventData.clickCount;

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if ((lastClick + interval) > Time.time)
                {
                    if (doubleLeftHandler != null)
                        doubleLeftHandler();
                }
                else
                {
                    if (leftHandler != null && !Dragging()) 
                        leftHandler();
                }
                lastClick = Time.time;

            }
            else if (eventData.button == PointerEventData.InputButton.Right && rightHandler != null)
                rightHandler();


            UiManager.Instance.SetLastClicked(this.gameObject);
            //UiManager.Instance.ShowItemInfo(ItemManager.Instance.GetItemById(itemId));
        }

        bool Dragging()
        {
            return this.gameObject.GetComponent<Dragable>() != null && this.gameObject.GetComponent<Dragable>().IsDragging;
        }
    }
}
