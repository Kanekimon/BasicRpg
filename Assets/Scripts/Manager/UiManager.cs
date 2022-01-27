using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using Assets.Scripts.Systems.Equipment;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour, IPointerClickHandler
{
    public static UiManager Instance;

    public RectTransform inventory;
    public RectTransform crafting;
    public GameObject contextMenu;
    public GameObject hoverMenu;

    public GameObject clickTarget;

    public List<RectTransform> windows = new List<RectTransform>();

    private InventorySystem _inventorySystem;
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

    }

    // Update is called once per frame
    void Update()
    {


    }


    public GameObject GetCanvas()
    {
        return GameObject.Find("UI");
    }

    public GameObject GetNotificationContainer()
    {
        return GameObject.Find("NotificationContainer");
    }

    public void OpenContextMenu(ItemData item, bool equipped = false, EquipmentType slot = EquipmentType.none)
    {
        contextMenu.SetActive(true);

        Vector3 mousePos = Input.mousePosition;
        mousePos.x += 100;
        contextMenu.transform.position = mousePos;

        if (equipped)
        {
            contextMenu.transform.Find("Unequip").gameObject.SetActive(true);
            contextMenu.transform.Find("Unequip").GetComponent<PanelClick>().leftHandler = (() =>
            {

                ItemData unequipped = GameManager.Instance.GetPlayer().GetComponent<EquipmentSystem>().UnequipItem(slot);
                if(unequipped != null)
                    GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().AddItemToInventory(unequipped, 1);
                WindowManager.Instance.GetCurrentActive().OnReload();
                UiManager.Instance.CloseContextMenu();
            });
        }
        else
        {

            contextMenu.transform.Find("Drop").GetComponent<PanelClick>().leftHandler = (() =>
            {
                GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().RemoveAllFromId(item.Id);
                UiManager.Instance.CloseContextMenu();
                WindowManager.Instance.GetCurrentActive().OnReload();
            });

            if (item.EquipmentType != EquipmentType.none)
            {
                contextMenu.transform.Find("Equip").gameObject.SetActive(true);
                contextMenu.transform.Find("Equip").GetComponent<PanelClick>().leftHandler = (() =>
                {
                    GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().RemoveSpecificAmountFromId(item.Id, 1);
                    GameManager.Instance.GetPlayer().GetComponent<EquipmentSystem>().EquipItem(item);
                    WindowManager.Instance.GetCurrentActive().OnReload();
                    UiManager.Instance.CloseContextMenu();
                });
            }
            if (item.ItemTypes.Contains(ItemType.consumable))
            {
                contextMenu.transform.Find("Use").GetComponent<PanelClick>().leftHandler = (() =>
                {
                    GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().RemoveSpecificAmountFromId(item.Id, 1);
                    WindowManager.Instance.GetCurrentActive().OnReload();
                    UiManager.Instance.CloseContextMenu();
                });
            }
        }
        CloseHoverMenu();

    }

    public void CloseContextMenu()
    {
        foreach(Transform child in contextMenu.transform)
        {
            if(!child.name.Equals("Header"))
                child.gameObject.SetActive(false);
        }

        contextMenu.SetActive(false);
    }

    public void OpenHoverMenu(string text)
    {
        if (!contextMenu.activeInHierarchy)
        {
            hoverMenu.SetActive(true);
            Vector3 mousePos = Input.mousePosition;
            mousePos.x += 100;

            hoverMenu.transform.position = mousePos;
            hoverMenu.transform.Find("Text").GetComponent<Text>().text = text;
        }
    }

    public void CloseHoverMenu()
    {
        hoverMenu.SetActive(false);
    }

    public void SetLastClicked(GameObject last)
    {
        clickTarget = last;


        if (contextMenu.activeInHierarchy && !clickTarget.name.ToLower().Contains("slot"))
        {
            CloseContextMenu();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetLastClicked(eventData.pointerClick);
    }
}
