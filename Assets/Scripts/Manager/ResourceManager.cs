using Assets.Scripts.Entity.Item;
using Assets.Scripts.Entity.Resource;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject _nodePrefab;
    public int amount;

    public bool initialSpawn = true;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (initialSpawn)
        {
            if (ItemManager.Instance.GetItemById(0) != null)
            {
                //SpawnResources();
                initialSpawn = false;
            }


        }



    }

    public void SpawnResources()
    {
        float maxX = Terrain.activeTerrain.terrainData.size.x;
        float maxZ = Terrain.activeTerrain.terrainData.size.z;

        for (int i = 0; i < amount; i++)
        {
            GameObject g = Instantiate(_nodePrefab);
            ResourceNode rn = g.GetComponent<ResourceNode>();
            ItemData item = ItemManager.Instance.GetItemById(UnityEngine.Random.Range(0, 5));
            rn.SetType(item.ResourceType);
            rn.name = item.Name;
            g.GetComponent<InventorySystem>().AddItemToInventory(item, UnityEngine.Random.Range(0, 100));
            g.transform.position = new Vector3(UnityEngine.Random.Range(0f, maxX),1f, UnityEngine.Random.Range(0f, maxZ));
        }
    }

}

