using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SeedSpread))]
public class CustomSeedSpreadEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SeedSpread seed = (SeedSpread)target;

        if (GUILayout.Button("Spread"))
        {
            seed.Spread();
        }
    }
}

