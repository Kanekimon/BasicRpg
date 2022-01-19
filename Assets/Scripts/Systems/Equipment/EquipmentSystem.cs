using Assets.Scripts.Entity.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems.Equipment
{
    public class EquipmentSystem : MonoBehaviour
    {
        Dictionary<EquipmentType, ItemData> _equipped;


        private void Awake()
        {
            _equipped = new Dictionary<EquipmentType, ItemData>();
            _equipped.Add(EquipmentType.lefthand, null);
            _equipped.Add(EquipmentType.righthand, null);
            _equipped.Add(EquipmentType.helmet, null);
            _equipped.Add(EquipmentType.chest, null);
            _equipped.Add(EquipmentType.shoulder, null);
            _equipped.Add(EquipmentType.pants, null);
            _equipped.Add(EquipmentType.boots, null);
        }

        public Dictionary<EquipmentType,ItemData> Equipped { get { return _equipped; } }

        /// <summary>
        /// Equips a given item, and returns the item equipped before
        /// returns null if no item was equiped
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ItemData EquipItem(ItemData item)
        {

            ItemData oldItem = null;

            if(item == null || item.EquipmentType == EquipmentType.none)
                return null;


            bool isAlreadyEquipped = _equipped.ContainsKey(item.EquipmentType);

            if(isAlreadyEquipped)
                oldItem = _equipped[item.EquipmentType];

            _equipped[item.EquipmentType] = item;


            return oldItem;
        }

        /// <summary>
        /// Unequips item at given slot 
        /// Returns null if no item was equipped
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public ItemData UnequipItem(EquipmentType slot)
        {
            ItemData old = _equipped[slot];

            _equipped[slot] = null;
            return old;
        }


    }
}
