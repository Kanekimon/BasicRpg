using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using Assets.Scripts.Player;
using Assets.Scripts.Systems.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Utility
    {
        public delegate void ClickAction();
        //public ClickAction clickAction;

        public static void DeleteAllChildrenFromTransform(Transform tran)
        {
            foreach (Transform t in tran)
            {
                GameObject.Destroy(t.gameObject);
            }
        }

        public static void AddSlot(Transform container, ItemData item, int amount, ClickAction clickAction = null)
        {
            //Transform container = this.Rect.transform.Find("Inventory_Items");
            if (container == null)
                return;

            GameObject slotObject = GameObject.Instantiate(WindowManager.Instance.slot);
            slotObject.transform.parent = container;

            Transform icon = slotObject.transform.Find("Item_icon");



            slotObject.GetComponent<PanelClick>().itemId = item.Id;
            if (clickAction != null)
                slotObject.GetComponent<PanelClick>().leftHandler = (() => clickAction());

            AddDoubleLeftHandler(item, slotObject.GetComponent<PanelClick>());

            slotObject.GetComponent<PanelClick>().rightHandler = (() =>
            {
                UiManager.Instance.OpenContextMenu(item);
            });

            slotObject.GetComponent<PanelHover>().Text = item.Name;

            slotObject.transform.Find("Item_icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(item.Sprite);
            slotObject.transform.Find("Item_amount").gameObject.GetComponent<Text>().text = amount.ToString();
        }

        static void AddDoubleLeftHandler(ItemData item, PanelClick pc)
        {
            if (item.ItemTypes.Contains(ItemType.equipment))
            {
                pc.doubleLeftHandler = (() =>
                {
                    GameObject player = GameManager.Instance.GetPlayer();
                    player.GetComponent<EquipmentSystem>().EquipItem(item);
                    player.GetComponent<InventorySystem>().RemoveSpecificAmountFromId(item.Id, 1);
                    WindowManager.Instance.GetCurrentActive().OnReload();
                });
            }
            if (item.ItemTypes.Contains(ItemType.consumable))
                pc.doubleLeftHandler = (() => ConsumeItem(item));

        }

        static void AddEquipOption(ItemData item)
        {

        }
        static void ConsumeItem(ItemData item)
        {
            //GameManager.Instance.GetPlayer().GetComponent<InventorySystem>()
        }

    }
}
