using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ResourceMapManager : MonoBehaviour
    {

        Texture2D texture;
        public List<GameObject> redPrefabs = new List<GameObject>();
        public List<GameObject> greenPrefabs = new List<GameObject>();
        public List<GameObject> bluePrefabs = new List<GameObject>();
       
        Dictionary<string, List<GameObject>> map = new Dictionary<string, List<GameObject>>();

        private GameObject _resourceContainer;

        private void Awake()
        {
            map["r"] = redPrefabs;
            map["g"] = greenPrefabs;
            map["b"] = bluePrefabs;
            LoadResourceMap();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SpawnResources();
            }
        }

        private void LoadResourceMap()
        {
            texture = Resources.Load<Texture2D>("ResourceMaps/base");
        }

        public void SpawnResources()
        {
            if (texture == null)
                return;

            if (GameObject.Find("ResourceContainer") != null)
                Destroy(GameObject.Find("ResourceContainer"));

            Color[] colorArray = texture.GetPixels();

            _resourceContainer = new GameObject("ResourceContainer");

            int x = 0;
            int z = 0;

            foreach (Color c in colorArray)
            {
                float decider = UnityEngine.Random.Range(0f, 1f);

                float red = Mathf.Abs(decider - c.r);
                float green = Mathf.Abs(decider - c.g);
                float blue = Mathf.Abs(decider - c.b);

                string mode = "";

                if (red < green && red > blue)
                {
                    mode = "r";

                }
                if (green < blue && green < blue)
                {
                    mode = "g";
                }
                if (blue < red && blue < green)
                {
                    mode = "b";
                }
                if (!string.IsNullOrEmpty(mode))
                    SpawnResource(mode, c, x, z);

                x++;
                if (x == 1000)
                {
                    x = 0;
                    z++;
                }
            }


            //for(int x = 0; x < Terrain.activeTerrain.terrainData.size.x; x++)
            //{
            //    for(int z = 0; z < Terrain.activeTerrain.terrainData.size.z; z++)
            //    {
            //        Color c = texture.GetPixel(x, z);

            //        float decider = UnityEngine.Random.Range(0f, 1f);

            //        float red = Mathf.Abs(decider - c.r);
            //        float green = Mathf.Abs(decider - c.g);
            //        float blue = Mathf.Abs(decider - c.b);

            //        string mode = "";

            //        if (red < green && red > blue)
            //        {
            //            mode = "r";

            //        }
            //        if(green < blue && green < blue)
            //        {
            //            mode = "g";
            //        }
            //        if(blue < red && blue < green)
            //        {
            //            mode = "b";
            //        }
            //        if(!string.IsNullOrEmpty(mode))
            //            SpawnResource(mode, c,x,z);
            //    }
            //}
        }

        private void SpawnResource(string c, Color color, int x, int z)
        {
            float prob = UnityEngine.Random.Range(0f, 1f);
            Debug.Log(color.a);
            if (prob < color.a)
            {

                List<GameObject> possibleSpawns = map[c];

                int index = UnityEngine.Random.Range(0, possibleSpawns.Count);

                GameObject res = Instantiate(possibleSpawns[index]);

                Vector3 terrainPos = new Vector3(x, Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)), z);

                res.transform.position = terrainPos;
            }
            
        }

    }
}
