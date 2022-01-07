using Assets.Scripts.Entity.Item;
using Assets.Scripts.Factories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;

        SortedDictionary<int, ItemData> _itemList = new SortedDictionary<int, ItemData>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            LoadItems();
        }




        public ItemData GetItemById(int id)
        {
            if (ItemWithIdExists(id))
                return _itemList[id];
            return null;
        }

        public bool ItemWithIdExists(int id) { return _itemList.ContainsKey(id); }


        public SortedDictionary<int, ItemData> GetAllItems()
        {
            return _itemList;
        }

        public void UpdateItems(SortedDictionary<int, ItemData> updatedItemList)
        {
            _itemList = updatedItemList;
        }

        public void SaveItems()
        {
            string json = JsonConvert.SerializeObject(_itemList.Values, Formatting.Indented);

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"D:\Unity Workspace\BasicRpg\Assets\Json\itemList.json", json);
        }

        public void LoadItems()
        {
            List<ItemData> items = JsonConvert.DeserializeObject<List<ItemData>>(File.ReadAllText(@"D:\Unity Workspace\BasicRpg\Assets\Json\itemList.json"));
            foreach(ItemData item in items)
            {
                _itemList[item.Id] = item;
            }
        }

        public void RegisterItem(ItemData newItem)
        {
            int newId = _itemList.Count == 0 ? 0 : _itemList.Last().Key+1;
            newItem.Id = (newId);
            _itemList.Add(newId, newItem);

            foreach(KeyValuePair<int, ItemData> keyValuePair in _itemList)
            {
                int id = keyValuePair.Key;
                ItemData it = keyValuePair.Value;
                string itemText = $"ID: {id} Name: {it.Name} Sprite: {it.Sprite} Durability: {it.Durability} ResourceType: {it.ResourceType}";
                string lines = new string('-', itemText.Length);
                Debug.Log(lines);
                Debug.Log(itemText);
                foreach(ItemType itemType in it.ItemTypes)
                {
                    Debug.Log($"Type: {itemType.ToString()}");
                }

                Debug.Log(lines);
            }
        }
    }
}
