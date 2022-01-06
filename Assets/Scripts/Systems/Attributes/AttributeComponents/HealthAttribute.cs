using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems.Attributes.AttributeComponents
{
    public class HealthAttribute : AttributeComponent
    {
        private void Awake()
        {
            this._name = "health";
            this._value = 100f;
            this._maxValue = _value;
        }

        public override void DecreaseValue(float value)
        {
            _value -= value;
            if(value <= 0)
            {
                //Death code here
            }
        }
    }
}
