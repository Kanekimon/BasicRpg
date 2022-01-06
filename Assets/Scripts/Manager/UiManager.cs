using Assets.Scripts.Entity.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public RectTransform inventory;


    private InventorySystem _inventorySystem;
    private List<GameObject> _slots = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {

        Transform slotContainer = inventory.transform.Find("Inventory_Items");
        foreach(Transform slot in slotContainer)
        {
            _slots.Add(slot.gameObject);
        }
        _inventorySystem = GameManager.Instance.GetPlayer().GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
            if (inventory.gameObject.activeInHierarchy)
            {
                DrawItems(GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().GetAllItems());
            }
        }
    }

    private void DrawItems(Dictionary<ItemData, int> items)
    {

        int slot_index = 0;
        foreach(KeyValuePair<ItemData, int> item in items)
        {
            GameObject slot = _slots[slot_index++];
            slot.GetComponent<Image>().sprite = item.Key.Sprite;
            slot.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.Value.ToString();
        }
    }
}
