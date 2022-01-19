using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class InputManager : MonoBehaviour
    {
        bool IsChatOpen = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                IsChatOpen = ChatManager.Instance.ToggleChat();
            }
            if (!IsChatOpen)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    ItemManager iM = ItemManager.Instance;
                    InventorySystem playerInv = GameManager.Instance.GetPlayer().GetComponent<InventorySystem>();
                    playerInv.AddItemToInventory(iM.GetItemById(6), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(7), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(8), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(9), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(10), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(11), 1);
                    playerInv.AddItemToInventory(iM.GetItemById(12), 1);
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    ResourceMapManager.Instance.SpawnResources();
                }

                if (Input.GetKeyDown(KeyCode.I))
                {
                    WindowManager.Instance.OpenWindow("Inventory");
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    WindowManager.Instance.OpenWindow("Crafting");
                }
            }
        }


    }
}
