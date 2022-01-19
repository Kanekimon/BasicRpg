using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private GameObject _owner;
    [SerializeField]
    private int _inventorySize;
    private Dictionary<ItemData, int> _inventoryItems;

    private void Awake()
    {
        _owner = this.gameObject;
        _inventoryItems = new Dictionary<ItemData,int>();
       
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Just a debug method to see whats in an inventory
    /// </summary>
    public void OutputInventory()
    {
        foreach(KeyValuePair<ItemData, int> item in _inventoryItems)
        {
            ItemData i = item.Key;
            Debug.Log($"Item Id: {i.Id}    Item Name: {i.Name}    Item Amount: {item.Value}");
        }
    }

    /// <summary>
    /// Adds item to inventory
    /// If item is already in inventory, it adds the incoming amount
    /// else it adds item with given amount
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void AddItemToInventory(ItemData item, int amount)
    {
        ItemData inventoryItem = GetItemFromId(item.Id);

        if (inventoryItem != null)
        {
            _inventoryItems[inventoryItem] += amount;
        }
        else
        {
            _inventoryItems.Add(item, amount);
        }
    }

    /// <summary>
    /// Remove an item completly form inventory
    /// </summary>
    /// <param name="id">Id of specific item</param>
    /// <returns>Tuple consisting of the ItemData and the amount, that was in the inventory</returns>
    public Tuple<ItemData, int> RemoveAllFromId(int id)
    {
        ItemData item = GetItemFromId(id);
        if (item != null)
        {
            Tuple<ItemData, int> result = new Tuple<ItemData, int>(item, _inventoryItems[item]);
            _inventoryItems.Remove(item);
            return result;
        }

        return new Tuple<ItemData, int>(null, 0);

    }

    /// <summary>
    /// Removes a specific amount of an item from the inventory
    /// If specified amount is higher than amount in inventory, 
    /// max amount will be removed
    /// </summary>
    /// <param name="id">Id of Item</param>
    /// <param name="amount">Amount to be removed</param>
    /// <returns>Tuple consisting of the ItemData and the amount, that was specified or max</returns>
    public Tuple<ItemData, int> RemoveSpecificAmountFromId(int id, int amount)
    {
        ItemData item = GetItemFromId(id);
        if(item != null)
        {
            if(GetItemAmount(id) < amount)
                amount = GetItemAmount(id);

            _inventoryItems[item] -= amount;

            if (GetItemAmount(id) == 0)
                _inventoryItems.Remove(item);
            return new Tuple<ItemData, int>(item, amount);
        }
        return null;
    }

    /// <summary>
    /// Checks the amount of a given item in the inventory
    /// returns 0 if item is not in inventory
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetItemAmount(int id)
    {
        if (HasItem(id))
        {
            return _inventoryItems.Where(a => a.Key.Id == id).FirstOrDefault().Value;
        }
        return 0;
    }

    /// <summary>
    /// Check if Item is in inventory
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>m
    public bool HasItem(int id)
    {
        foreach (KeyValuePair<ItemData, int> item in _inventoryItems)
        {
            if (item.Key.Id == id)
                return true;
        }
        return false;
    }


    /// <summary>
    /// Checks if the inventory has an item with a specific amount
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasItemWithAmount(int id, int amount)
    {
        return HasItem(id) && GetItemAmount(id) >= amount;
    }

    /// <summary>
    /// Get ItemData from Id
    /// </summary>
    /// <param name="id">Id of Item</param>
    /// <returns>ItemData if in inventory else null</returns>
    public ItemData GetItemFromId(int id)
    {
        if (HasItem(id))
            return _inventoryItems.Where(a => a.Key.Id == id).FirstOrDefault().Key;
        return null;
    }


    /// <summary>
    /// Transfers specific item with amount to another inventory system
    /// </summary>
    /// <param name="inv">Transfer to this inventorySystem</param>
    /// <param name="id">Item id</param>
    /// <param name="amount">amount to be transfered</param>
    public void TransferToOtherInventory(InventorySystem inv, int id, int amount)
    {
        Tuple<ItemData, int> itemFromInventory = RemoveSpecificAmountFromId(id, amount);
        inv.AddItemToInventory(itemFromInventory.Item1, itemFromInventory.Item2);
    }

    /// <summary>
    /// Transfers all of the first item to another inventory
    /// </summary>
    /// <param name="inv">The inventory to transfer to</param>
    public void TransferAllOfFirstItem(InventorySystem inv)
    {
        KeyValuePair<ItemData, int> invItem = this._inventoryItems.First();
        Tuple<ItemData, int> item = new Tuple<ItemData, int>(invItem.Key,invItem.Value);

        Tuple<ItemData, int> itemFromInventory = RemoveSpecificAmountFromId(item.Item1.Id, item.Item2);
        inv.AddItemToInventory(itemFromInventory.Item1, itemFromInventory.Item2);
    }

    /// <summary>
    /// Transfers all items from this inventory to another
    /// </summary>
    /// <param name="inv">The target inventory</param>
    public void TransferAllToOtherInventory(InventorySystem inv)
    {
        foreach(KeyValuePair<ItemData, int> invItem in _inventoryItems)
        {
            inv.AddItemToInventory(invItem.Key, invItem.Value);
        }
        _inventoryItems.Clear();
    }

    /// <summary>
    /// Check if inventory is empty
    /// </summary>
    /// <returns>true if empty</returns>
    public bool IsInventoryEmpty()
    {
        return _inventoryItems.Count == 0;
    }

    /// <summary>
    /// Returns collection of all items in inventory
    /// </summary>
    /// <returns></returns>
    public Dictionary<ItemData, int> GetAllItems()
    {
        return this._inventoryItems;
    }
}
