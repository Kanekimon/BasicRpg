using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public RectTransform inventory;
    public RectTransform crafting;



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
        if (Input.GetKeyDown(KeyCode.I))
        {
            WindowManager.Instance.OpenWindow("Inventory");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            WindowManager.Instance.OpenWindow("Crafting");
        }

    }

    private RectTransform GetWindow(string name)
    {
        foreach (RectTransform rect in windows)
        {
            if (rect.name.Equals(name))
                return rect;
        }
        return null;
    }

    public GameObject GetCanvas()
    {
        return GameObject.Find("Canvas");
    }

    public GameObject GetNotificationContainer()
    {
        return GameObject.Find("NotificationContainer");
    }


}
