using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity.Item
{
    [System.Serializable]
    public class EquipmentData 
    {
        private Dictionary<string, float> _modifier = new Dictionary<string, float>();



        public Dictionary<string, float> GetModifier()
        {
            return _modifier;
        }

    }
}
