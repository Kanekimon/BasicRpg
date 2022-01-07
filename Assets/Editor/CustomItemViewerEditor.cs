using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

public class CustomItemViewerEditor : EditorWindow
{

    Vector2 _scrollPos;
    static CustomItemViewerEditor customItemViewerEditor;
    Dictionary<int, AnimFloat> _anims = new Dictionary<int, AnimFloat>();
    bool isInit = true;
    SortedDictionary<int, ItemData> allItems = new SortedDictionary<int, ItemData>();

   [MenuItem("Window/ItemViewer")]
    static void Init()
    {
        customItemViewerEditor = (CustomItemViewerEditor)GetWindow(typeof(CustomItemViewerEditor));
        customItemViewerEditor.Show();
    }

    public void OnGUI()
    {

        if (allItems != ItemManager.Instance.GetAllItems())
        {
            allItems = ItemManager.Instance.GetAllItems();
            isInit = true;
        }


        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));

        if (isInit)
        {
            _anims.Clear(); 
            foreach (KeyValuePair<int, ItemData> item in allItems)
            {
                _anims[item.Key] = new AnimFloat(0.0001f);
                
            }
            isInit = false;
        }

        foreach (KeyValuePair<int, ItemData> item in allItems)
        {
            CreateItemContainer(item.Value, item.Key);
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Update Items"))
        {
            ItemManager.Instance.UpdateItems(allItems);
        }


        if (GUILayout.Button("AddItem"))
        {
            ItemCreatorWindow itemCreatorWindow = new ItemCreatorWindow();
            itemCreatorWindow.isFromViewer = true;
            itemCreatorWindow.Show();
            Close();
        }
        if (GUILayout.Button("Save"))
        {
            ItemManager.Instance.SaveItems();
        }
        if (GUILayout.Button("Close"))
        {
            Close();
        }


        GUILayout.EndHorizontal();

    }


    void CreateItemContainer(ItemData itemData, int id)
    {
        bool on = _anims[id].value == 1;
        if (GUILayout.Button(on ? $"Show {itemData.Name}" : $"Hide {itemData.Name}"))
        {
            _anims[id].target = on ? 0.0001f : 1f;
            _anims[id].speed = 1f;

            var env = new UnityEvent();
            env.AddListener(() => Repaint());
            _anims[id].valueChanged = env;
        }



        EditorGUILayout.BeginFadeGroup(_anims[id].value);
        GuiLine();

        EditorGUILayout.LabelField("ID: " + itemData.Id);
        itemData.Name = EditorGUILayout.TextField("Name: ", itemData.Name);

        var texture = AssetPreview.GetAssetPreview(Resources.Load<Sprite>(itemData.Sprite)); 
        GUILayout.Label(texture);

        itemData.Durability = float.Parse(EditorGUILayout.TextField("Durability: ", itemData.Durability.ToString()));
        itemData.ResourceType = (ResourceType)EditorGUILayout.EnumPopup("ResourceType: ", itemData.ResourceType);
        itemData.EquipmentType = (EquipmentType)EditorGUILayout.EnumPopup("EquipmentType: ", itemData.EquipmentType);

        int toRemove = -1;

        for (int i = 0; i < itemData.ItemTypes.Count; i++)
        {
            GUILayout.BeginHorizontal();
            itemData.ItemTypes[i] = (ItemType)EditorGUILayout.EnumPopup("ItemType: ", itemData.ItemTypes[i]);
            if (GUILayout.Button("-"))
                toRemove = i;
            GUILayout.EndHorizontal();
        }

        if (toRemove != -1)
            itemData.ItemTypes.RemoveAt(toRemove);

        if (GUILayout.Button("Add ItemType"))
        {
            itemData.ItemTypes.Add(ItemType.resource);
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndFadeGroup();
    }


    void GuiLine(int i_height = 1)

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }

}

