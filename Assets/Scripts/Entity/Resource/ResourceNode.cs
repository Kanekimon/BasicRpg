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

        public override string GetDescription()
        {
            return "Resource: " + _type.ToString();
        }

        public override void Interact(GameObject interactinWith)
        {
            this.gameObject.GetComponent<InventorySystem>().TransferAllOfFirstItem(interactinWith.GetComponent<InventorySystem>());
            if (this.GetComponent<InventorySystem>().IsInventoryEmpty())
                Destroy(this.gameObject);
            
        }

        public void SetType(ResourceType pType)
        {
            this._type = pType;
        }


    }
}
