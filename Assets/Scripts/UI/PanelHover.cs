using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    internal class PanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string Text;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!string.IsNullOrEmpty(Text))
                UiManager.Instance.OpenHoverMenu(Text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UiManager.Instance.CloseHoverMenu();

        }
    }
}
