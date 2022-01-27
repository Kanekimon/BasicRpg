using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using Assets.Scripts.Systems.Equipment;
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
            DrawEquipment(GameManager.Instance.GetPlayer().GetComponent<EquipmentSystem>().Equipped);
        }

        private void DrawItems(Dictionary<ItemData, int> items)
        {
            Utility.DeleteAllChildrenFromTransform(this.transform.Find("Inventory_Items"));

            foreach (KeyValuePair<ItemData, int> item in items)
            {
                Utility.AddSlot(slotContainer, item.Key, item.Value);
            }
        }

        private void DrawEquipment(Dictionary<EquipmentType, ItemData> equipment)
        {
            foreach(EquipmentType type in equipment.Keys)
            {
        
                Transform equipContainer = this.transform.Find("Inventory_Equipment");

                Transform slotForEquip = equipContainer.Find($"{type}_Slot");

                if(slotForEquip != null)
                {
                    
                    ItemData item = equipment[type];
                    Sprite icon = null;
                    if (equipment[type] != null)
                    {
                        icon = Resources.Load<Sprite>(item.Sprite);
                        slotForEquip.GetComponent<PanelHover>().Text = item.Name;
                        slotForEquip.GetComponent<PanelClick>().rightHandler = (() =>
                        {
                            UiManager.Instance.OpenContextMenu(item, true, type);
                        });
                        slotForEquip.GetComponent<PanelClick>().doubleLeftHandler = (() =>
                        {
                            ItemData unequipped = GameManager.Instance.GetPlayer().GetComponent<EquipmentSystem>().UnequipItem(item.EquipmentType);
                            if (unequipped != null)
                                GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().AddItemToInventory(unequipped, 1);
                            WindowManager.Instance.GetCurrentActive().OnReload();
                            UiManager.Instance.CloseContextMenu();
                        });
                    }
                    else
                    {
                        icon = Resources.Load<Sprite>("Sprites/placeholder");
                    }
                    Transform itemIcon = slotForEquip.gameObject.transform.GetChild(0);
                    itemIcon.GetComponent<Image>().sprite = icon;

            
                }


            }
        }

        public override void OnReload()
        {
            base.OnReload();
            UiManager.Instance.CloseHoverMenu();
            UiManager.Instance.CloseContextMenu();
            OnOpen();
        }

    }
}
