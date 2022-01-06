using Assets.Scripts.Entity.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;

        Dictionary<int, ItemData> _itemList = new Dictionary<int, ItemData>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            CreateItemList();
        }



        private void CreateItemList()
        {
            _itemList.Add(0, new ItemData(0, "wood", ResourceType.wood));
            _itemList.Add(1, new ItemData(1, "stone", ResourceType.stone));
            _itemList.Add(2, new ItemData(2, "clay", ResourceType.clay));
            _itemList.Add(3, new ItemData(3, "grass", ResourceType.grass));
            _itemList.Add(4, new ItemData(4, "leaves", ResourceType.leaves));
        }

        public ItemData GetItemById(int id)
        {
            if (ItemWithIdExists(id))
                return _itemList[id];
            return null;
        }

        public bool ItemWithIdExists(int id) { return _itemList.ContainsKey(id); }
    }
}
