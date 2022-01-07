using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Item
{
    [System.Serializable]
    public class ItemData
    {
        private int _id;
        private string _name;
        private string _sprite;
        private float _durability;
        private ResourceType _resourceType;
        private EquipmentType _equipmentType;
        private List<ItemType> _itemTypes;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Sprite { get { return _sprite; } set { _sprite = value; } }
        public float Durability { get { return _durability; } set { _durability = value; } }
        public ResourceType ResourceType { get { return _resourceType; } set { _resourceType = value; } }

        public EquipmentType EquipmentType { get { return _equipmentType; } set { _equipmentType = value; } }
        public List<ItemType> ItemTypes { get { return _itemTypes; } set { _itemTypes = value; } }


        /// <summary>
        /// Creates a new ItemData without any values
        /// </summary>
        public ItemData() { }

    }
}
