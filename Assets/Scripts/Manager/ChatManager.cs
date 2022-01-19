using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Manager
{
    public class ChatManager : MonoBehaviour
    {

        public InputField input;
        public bool isChatOpen;
        public static ChatManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }


        public bool ToggleChat()
        {
            if (isChatOpen)
            {
                ParseChatMessage(input.textComponent.text);
            }

            isChatOpen = !isChatOpen;
            input.transform.parent.gameObject.SetActive(isChatOpen);

            if (isChatOpen)
            {
                input.Select();
                input.ActivateInputField();
            }
            GameManager.Instance.ToggleCursor(isChatOpen ? CursorLockMode.None : CursorLockMode.Locked);
            return isChatOpen;
        }


        private void ParseChatMessage(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            if (input.First() == '/')
            {
                string[] split = input.Split(new char[] { ' ' });

                string command = split[0].Remove(0,1);
                if (command.Equals("give"))
                    Give(split);

            }
        }

        private void Give(string[] para)
        {
            if (para.Length < 3)
                return;

            int id = int.Parse(para[1]);
            int amount = int.Parse(para[2]);

            GameManager.Instance.GetPlayer().GetComponent<InventorySystem>().AddItemToInventory(ItemManager.Instance.GetItemById(id), amount);
        }

    }
}
