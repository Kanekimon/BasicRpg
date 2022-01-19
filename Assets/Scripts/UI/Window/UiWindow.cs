using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Window
{
    public class UiWindow : MonoBehaviour
    {
        public RectTransform Rect;
        public virtual void OnOpen() { }
        public virtual void OnClose() { }

        public virtual void OnReload() { }

    }
}
