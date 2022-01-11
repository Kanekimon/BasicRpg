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
        public ClickHandling clickHandler;

        public void OnPointerClick(PointerEventData eventData)
        {
            clickHandler();
            //UiManager.Instance.ShowItemInfo(ItemManager.Instance.GetItemById(itemId));
        }
    }
}
