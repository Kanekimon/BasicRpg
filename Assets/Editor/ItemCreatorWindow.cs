using Assets.Scripts.Entity.Item;
using Assets.Scripts.Factories;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ItemCreatorWindow : EditorWindow
{

    string _itemName = "New Item";
    string _sprite = "Sprite/";
    float _durability = 0.0f;
    List<ItemType> _itemTypes = new List<ItemType>();
    ResourceType _resourceType;
    EquipmentType _equipmentType;
    public bool isFromViewer = false;


    [MenuItem("Window/ItemCreator")]
    static void Init()
    {
        ItemCreatorWindow itemCreatorWindow = (ItemCreatorWindow)GetWindow(typeof(ItemCreatorWindow));
        itemCreatorWindow.Show();
    }

    private void OnGUI()
    {
        _itemName = EditorGUILayout.TextField("Name", _itemName);


        GUILayout.BeginHorizontal();
        _sprite = EditorGUILayout.TextField("Sprite Url", _sprite);
        if (GUILayout.Button("Search"))
        {
            _sprite = EditorUtility.OpenFilePanel("Select sprite", "Assets/Resources", "png");
            _sprite = _sprite.Substring(_sprite.LastIndexOf("Resources/")+10);
            _sprite = _sprite.Substring(0, _sprite.LastIndexOf("."));
        }
        GUILayout.EndHorizontal();

        _durability = float.Parse(EditorGUILayout.TextField("Durability", _durability.ToString()));
        //_resourceType = (ResourceType) EditorGUILayout.EnumFlagsField("ResourceType", _resourceType);
        _resourceType = (ResourceType) EditorGUILayout.EnumPopup("ResourceType: ", _resourceType);
        _equipmentType = (EquipmentType) EditorGUILayout.EnumPopup("EquipmentType", _equipmentType);

        int toRemove = -1;

        for(int i = 0; i < _itemTypes.Count; i++)
        {
            GUILayout.BeginHorizontal();

            _itemTypes[i] = (ItemType)EditorGUILayout.EnumPopup("ItemType", _itemTypes[i]);
            if (GUILayout.Button("-"))
            {
                toRemove = i;
            }
            GUILayout.EndHorizontal();
        }

        if(toRemove != -1)
        {
            _itemTypes.RemoveAt(toRemove);
        }

        if(GUILayout.Button("Add ItemType"))
        {
            _itemTypes.Add(ItemType.resource);
        }

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Create Item"))
        {
            ItemData newItem = ItemDataFactory.CreateItemData(_itemName, _sprite, _durability, _itemTypes, _resourceType, _equipmentType);
            ItemManager.Instance.RegisterItem(newItem);
            if (isFromViewer)
            {
                CustomItemViewerEditor civ = new CustomItemViewerEditor();
                civ.Show();
                Close();
            }
        }
        if (GUILayout.Button("Close"))
        {
            Close();
        }
        GUILayout.EndHorizontal();
    }


}
