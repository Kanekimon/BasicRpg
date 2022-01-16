using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SectionObject
{
    
    public Color Color { get; set; }

    public float ColorValue { get; set; }
    public GameObject Prefab { get; set; }
}
