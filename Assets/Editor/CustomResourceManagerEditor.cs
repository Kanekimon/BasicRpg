using Assets.Scripts.Entity.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceManager))]
class CustomResourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        ResourceManager resM = (ResourceManager)target;

        if (GUILayout.Button("Spawn"))
        {
            resM.SpawnResources();
        }

    }
}

