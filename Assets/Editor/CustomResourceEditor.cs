using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Entity.Resource;
using Assets.Scripts.Entity.Item;
using UnityEngine.UIElements;
using Assets.Scripts.Manager;

[CustomEditor(typeof(ResourceNode))]
 class CustomResourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResourceNode res = (ResourceNode)target;

        if (GUILayout.Button("Random Resource"))
        {

            ItemData item = ItemManager.Instance.GetItemById(Random.Range(0, 5));
            res.SetType(item.ResourceType);
            //res.GetComponent<InventorySystem>().AddItemToInventory(item, Random.Range(0, 100));
        }

        if(GUILayout.Button("Output Inventory"))
        {
            //res.GetComponent<InventorySystem>().OutputInventory();
        }
    }
}
