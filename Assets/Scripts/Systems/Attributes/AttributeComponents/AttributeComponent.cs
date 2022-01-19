using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeComponent : MonoBehaviour
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected float _value;
    [SerializeField]
    protected float _maxValue;

    public string Name { get { return _name; } }   
    public float Value { get { return _value; } }
    public float MaxValue { get { return _maxValue; } }


    public string GetName() { return _name; }

    public float GetValue() { return _value; }

    public virtual void DecreaseValue(float pValue)
    {
        _value -= pValue;
    }

    public virtual void IncreaseValue(float pValue)
    {
        _value += pValue;
    }

}
