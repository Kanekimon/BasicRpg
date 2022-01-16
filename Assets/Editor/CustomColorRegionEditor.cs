using Assets.Scripts.Data;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorRegionData))]
public class CustomColorRegionEditor : Editor
{
    string[] colors = { "red", "green", "blue" };


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ColorRegionData crd = ((ColorRegionData)target);

        crd.mode = EditorGUILayout.Popup(crd.mode, colors);
        

        for(int i = crd.sections.Count - 1; i >= 0; i--)
        {

            EditorGUILayout.BeginHorizontal();
            crd.sections[i].ColorValue = EditorGUILayout.Slider(crd.sections[i].ColorValue, 1f, 255f);
            EditorGUILayout.ColorField(GetColor(crd.mode, crd.sections[i].ColorValue));
            crd.sections[i].Prefab = (GameObject) EditorGUILayout.ObjectField(crd.sections[i].Prefab, typeof(GameObject));
            if(GUILayout.Button("-"))
                crd.sections.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }


        if (GUILayout.Button("Add Section"))
        {
            SectionObject ob  = new SectionObject();
            ob.Color = Color.white;
            crd.sections.Add(ob);
        }
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(crd);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    public Color GetColor(int mode, float val)
    {
        float converted = Converter.ConvertColor(val);
        switch (mode)
        {
            case 0: return new Color(converted, 0, 0);
            case 1: return new Color(0, converted, 0);
            case 2: return new Color(0,0, converted);
        }
        return Color.white;
    }


}



