using Assets.Scripts.Systems.Attributes.AttributeComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems.Attributes
{
    public class AttributeSystem : MonoBehaviour
    {
        [SerializeField]
        private List<AttributeComponent> _attributes;



        private void Awake()
        {
            _attributes = new List<AttributeComponent>();
            _attributes.Add(this.gameObject.AddComponent<HealthAttribute>());
        }

        /// <summary>
        /// Gets Component by name
        /// </summary>
        /// <param name="pName">Name of component</param>
        /// <returns></returns>
        public AttributeComponent GetAttributeComponent(string pName)
        {
            return _attributes.Where(a => a.GetName().Equals(pName)).FirstOrDefault();
        }

        public void DecreaseAttributeValue(string pName, float pValue)
        {
            GetAttributeComponent(pName).DecreaseValue(pValue);
        }

        public void IncreaseAttributeValue(string pName, float pValue)
        {
            GetAttributeComponent(pName).IncreaseValue(pValue);
        }

    }
}
