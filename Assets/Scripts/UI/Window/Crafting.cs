using Assets.Scripts.Entity.Item;
using Assets.Scripts.Manager;
using Assets.Scripts.Systems.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.UI.Utility;

namespace Assets.Scripts.UI.Window
{
    public class Crafting : UiWindow
    {
        public GameObject craftingResource;

        private void Start()
        {
            this.Rect = this.GetComponent<RectTransform>();
        }

        public override void OnOpen()
        {
            base.OnOpen();
            this.Rect = this.GetComponent<RectTransform>();
            List<CraftingRecipe> recipes = CraftingManager.Instance.GetAllRecipes().Values.ToList();
            Utility.DeleteAllChildrenFromTransform(this.Rect.transform.Find("Inventory_Items"));

            foreach (CraftingRecipe recipe in recipes)
            {
                ItemData item = ItemManager.Instance.GetItemById(recipe.ResultId);
                ClickAction clickA = (() =>
                {
                    ShowItemInfo(item);
                });

                Utility.AddSlot(this.Rect.transform.Find("Inventory_Items"),item, recipe.Amount, clickA);
            }
        }

        public void AddCraftingResource(Transform resContainer, int amount, ItemData item)
        {
            GameObject resItem = Instantiate(craftingResource);
            resItem.transform.parent = resContainer;
            Transform icon = resItem.transform.Find("item_icon");
            icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.Sprite);
            resItem.transform.Find("name_amount").GetComponent<Text>().text = $"{amount}x {item.Name}";
        }



        public void ShowItemInfo(ItemData item)
        {
            CraftingRecipe recipe = CraftingManager.Instance.GetRecipesForItem(item.Id);


            Transform prev = this.Rect.Find("Crafting_Preview");

            foreach (Transform t in prev)
            {
                t.gameObject.SetActive(true);
            }

            prev.Find("ResultItem_Name").Find("ResultItem_Text").GetComponent<Text>().text = item.Name;

            Transform resContainer = this.Rect.Find("Crafting_Preview").Find("CraftingResources");

            Utility.DeleteAllChildrenFromTransform(resContainer);

            foreach (CraftingResource res in recipe.Resources)
            {
                AddCraftingResource(resContainer, res.Amount, ItemManager.Instance.GetItemById(res.ItemId));
            }

            Button craftButton = prev.Find("ButtonGroup").Find("Craft").GetComponent<Button>();
            craftButton.onClick.RemoveAllListeners();
            craftButton.onClick.AddListener(() =>
            {
                GameManager.Instance.GetPlayer().GetComponent<CraftingSystem>().CraftItem(item.Id);
            });

        }
    }
}
