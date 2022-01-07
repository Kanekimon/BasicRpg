using Assets.Scripts.Entity.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public static class ItemDataFactory
    {
        public static ItemData CreateItemData(string name, string sprite, float durability, List<ItemType> itemTypes, ResourceType resourceType, EquipmentType equipmentType)
        {
            ItemData newItem = new ItemData();

            newItem.Name = name;
            newItem.Sprite = sprite;//Resources.Load<Sprite>(sprite) ;
            newItem.Durability = durability;
            newItem.ItemTypes = itemTypes;
            newItem.ResourceType = resourceType;
            newItem.EquipmentType = equipmentType;

            return newItem;
        }

    }
}
