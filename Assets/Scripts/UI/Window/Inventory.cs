using Assets.Scripts.Entity.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Window
{
    public class Inventory : UiWindow
    {
        private InventorySystem _inventorySystem;
        private Transform slotContainer;

        private void Start()
        {
            this.Rect = this.gameObject.GetComponent<RectTransform>();


            _inventorySystem = GameManager.Instance.GetPlayer().GetComponent<InventorySystem>();

        }

        public override void OnOpen()
        {
            base.OnOpen();
            slotContainer = this.transform.Find("Inventory_Items");
            DrawItems(GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().GetAllItems());
        }

        private void DrawItems(Dictionary<ItemData, int> items)
        {
            Utility.DeleteAllChildrenFromTransform(this.transform.Find("Inventory_Items"));
            int slot_index = 0;
            foreach (KeyValuePair<ItemData, int> item in items)
            {
                Utility.AddSlot(slotContainer, item.Key, item.Value);
            }
        }

    }
}
