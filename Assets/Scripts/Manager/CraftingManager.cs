using Assets.Scripts.Entity.Item;
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
    public class CraftingManager : MonoBehaviour
    {
        public static CraftingManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }


        private void Start()
        {
            CraftingRecipe axe = new CraftingRecipe();
            CraftingResource wood = new CraftingResource();
            wood.ItemId = 0;
            wood.Amount = 2;
            CraftingResource stone = new CraftingResource();
            stone.ItemId = 1;
            stone.Amount = 3;
            axe.ResultId = 6;
            axe.Resources = new List<CraftingResource>() { wood, stone };
            axe.Amount = 1;
            RegisterRecipe(axe);
            SaveRecipes();

        }

        public void SaveRecipes()
        {
            string json = JsonConvert.SerializeObject(_recipes.Values, Formatting.Indented);
            File.WriteAllText(@"D:\Unity Workspace\BasicRpg\Assets\Json\craftingrecipes.json", json);
        }

        public void LoadRecipes()
        {
            List<CraftingRecipe> recipes = JsonConvert.DeserializeObject<List<CraftingRecipe>>(File.ReadAllText(@"D:\Unity Workspace\BasicRpg\Assets\Json\craftingrecipes.json"));
            foreach (CraftingRecipe recipe in recipes)
            {
                _recipes[recipe.ResultId] = recipe;
            }
        }


        private SortedDictionary<int, CraftingRecipe> _recipes = new SortedDictionary<int, CraftingRecipe>();
 
        /// <summary>
        /// Get recipe for item with id
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public CraftingRecipe GetRecipesForItem(int itemid)
        {
            return _recipes[itemid];
        }

        /// <summary>
        /// Returns the sorted dictionary with all recipes
        /// Sorted by ID of ResultItem
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<int, CraftingRecipe> GetAllRecipes()
        {
            return _recipes;
        }


        /// <summary>
        /// Adds a new Crafting Recipe
        /// </summary>
        /// <param name="rec"></param>
        public void RegisterRecipe(CraftingRecipe rec)
        {
            _recipes[rec.ResultId] = rec;
        }

        /// <summary>
        /// Removes a given Crafting Recipe
        /// </summary>
        /// <param name="rec"></param>
        public void DeregisterRecipes(CraftingRecipe rec)
        {
            if (_recipes.ContainsKey(rec.ResultId))
            {
                _recipes.Remove(rec.ResultId);
            }
        }


    }




    [Serializable]
    public class CraftingRecipe
    {
        public int ResultId;
        public int Amount;
        public List<CraftingResource> Resources;
    }

    [Serializable]
    public class CraftingResource
    {
        public int ItemId;
        public int Amount;
    }
}
