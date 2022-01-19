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

        public float doubleClickThreshold = 0.2f;
        private float lastClickTime = -1;


        public void OnPointerClick(PointerEventData eventData)
        {
            int clickCount = eventData.clickCount;

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (Time.time - lastClickTime < doubleClickThreshold)
                {
                    if (doubleLeftHandler != null)
                        doubleLeftHandler();
                }
                else
                {
                    if (leftHandler != null)
                        leftHandler();
                }
                lastClickTime = Time.time;

            }
            else if (eventData.button == PointerEventData.InputButton.Right && rightHandler != null)
                rightHandler();
            //UiManager.Instance.ShowItemInfo(ItemManager.Instance.GetItemById(itemId));
        }
    }
}
