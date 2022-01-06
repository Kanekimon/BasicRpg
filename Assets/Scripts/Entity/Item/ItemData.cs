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
        private Sprite _sprite;
        private ResourceType _resourceType;



        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public Sprite Sprite { get { return _sprite; } }
        public ResourceType ResourceType { get { return _resourceType; } }

        public ItemData(int pId, string pName, ResourceType pType)
        {
            _id = pId;
            _name = pName;
            _sprite = Resources.Load<Sprite>($"Sprites/{_name}");
            _resourceType = pType;
        }

        public ItemData(int pId, string pName)
        {
            this._id = pId;
            this._name = pName;
        }



    }
}
