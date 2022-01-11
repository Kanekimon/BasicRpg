using Assets.Scripts.Entity.Item;
using Assets.Scripts.Enums;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems.Crafting
{
    public class CraftingSystem : MonoBehaviour
    {
        private GameObject _owner;
        private InventorySystem _inv;

        private void Awake()
        {
            _owner = this.gameObject;
            _inv = GetComponent<InventorySystem>();
        }

        public void CraftItem(int itemid)
        {
            CraftingRecipe recipe = CraftingManager.Instance.GetRecipesForItem(itemid);

            if(recipe == null)
                return;

            InventorySystem inv = this._owner.GetComponent<InventorySystem>();

            bool hasAllItems = HasAllItems(inv, recipe);

            
            if (hasAllItems)
            {
                foreach (CraftingResource res in recipe.Resources)
                {
                    inv.RemoveSpecificAmountFromId(res.ItemId, res.Amount);
                }

                ItemData itemData = ItemManager.Instance.GetItemById(itemid);
                _inv.AddItemToInventory(itemData, recipe.Amount);
                NotificationManager.Instance.ShowNotification($"Successfully crafted {recipe.Amount}x {itemData.Name}", NotificationType.success);
            }
            else
            {
                NotificationManager.Instance.ShowNotification("Not enough resources", NotificationType.error);
            }
        }


        private bool HasAllItems(InventorySystem inv, CraftingRecipe recipe)
        {
            foreach (CraftingResource res in recipe.Resources)
            {
                if (!inv.HasItemWithAmount(res.ItemId, res.Amount))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
