using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Resource
{
    public class ResourceNode : Interactable
    {
        [SerializeField]
        private ResourceType _type;

        public ResourceType Type { get { return _type; } }
        public List<ResourceItem> resources = new List<ResourceItem>();

        public override string GetDescription()
        {
            return "Resource: " + _type.ToString();
        }

        public override void Interact(GameObject interactinWith)
        {
            InventorySystem inv = interactinWith.GetComponent<InventorySystem>();
            if(inv != null)
            {
                foreach(ResourceItem item in resources)
                {
                    int randomAmount = UnityEngine.Random.Range(item.min, item.max+1);
                    inv.AddItemToInventory(ItemManager.Instance.GetItemById(item.itemId), randomAmount);
                }
            }
            Destroy(this.gameObject);
        }

        public void SetType(ResourceType pType)
        {
            this._type = pType;
        }


    }

    [System.Serializable]
    public class ResourceItem
    {
        public int itemId;
        public int min;
        public int max;
    }
}
