using Assets.Scripts.Data;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ResourceMapManager : MonoBehaviour
    {

        public Texture2D texture;
        public List<GameObject> redPrefabs = new List<GameObject>();
        public List<GameObject> greenPrefabs = new List<GameObject>();
        public List<GameObject> bluePrefabs = new List<GameObject>();

        public List<ColorRegionData> sections = new List<ColorRegionData>();

        public int[,] regionMap;
        public int greenObject = 0;
        public int maxResources = 10000;
        [Range(0,100f)]
        public float regionSpawnPercentage = 0f;


        public Color[,] colorMap;

        public Dictionary<string, List<GameObject>> map = new Dictionary<string, List<GameObject>>();
        private List<SpawnRegion> regions = new List<SpawnRegion>();
        List<GameObject> spawned = new List<GameObject>();
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


            _resourceContainer = new GameObject("ResourceContainer");

            Color[] colorArray = texture.GetPixels();
            colorMap = To2DArray(colorArray);
            regionMap = new int[colorMap.GetLength(0), colorMap.GetLength(1)];
            regions = new List<SpawnRegion>();
            DrawRegions(colorMap);
            PopulateRegions();
        }

        private void PopulateRegions()
        {
            foreach(SpawnRegion reg in regions)
            {
                GameObject regionObject = new GameObject();
                regionObject.name = $"Region #{regions.IndexOf(reg)}";
                regionObject.transform.parent = _resourceContainer.transform;

                if (reg == null)
                    continue;

                int size = reg.colorCord.Count;
                int maxSpawns = (int)((float)size * (regionSpawnPercentage/100));
                int spawnCount = 0;
                while(spawnCount < maxSpawns)
                {
                    KeyValuePair<Vector2, Color> point = reg.colorCord.ElementAt(UnityEngine.Random.Range(0, reg.colorCord.Count));
                    Vector3 spawnPoint = new Vector3(point.Key.x, 0, point.Key.y);
                    spawnPoint.y = Terrain.activeTerrain.SampleHeight(spawnPoint);
                    SpawnResourceOfColor(point.Value, spawnPoint, regionObject);
                    spawnCount++;
                }

            }
        }

        private bool SpawnResourceOfColor(Color c, Vector3 coord, GameObject newParent)
        {
            float decider = UnityEngine.Random.Range(0f, 1f);

            float red = Mathf.Abs(decider - c.r);
            float green = Mathf.Abs(decider - c.g);
            float blue = Mathf.Abs(decider - c.b);

            string mode = "";

            if (red < green && red < blue)
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
                return SpawnResource(mode, c, coord, newParent);
            return false;
        }

        private bool SpawnResource(string c, Color color, Vector3 coord, GameObject newParent)
        {
            int prob = UnityEngine.Random.Range(0, 101);
            if (prob < (int)(color.a * 100))
            {
                GameObject toSpawn = null;

                switch (c)
                {
                    case "r":
                        toSpawn = GetSectionResource(sections[0].LoadData(), color.r);
                        break;
                    case "g":
                        toSpawn = GetSectionResource(sections[0].LoadData(), color.g);
                        break;
                    case "b":
                        toSpawn = GetSectionResource(sections[0].LoadData(), color.b);
                        break;
                }

                GameObject res = Instantiate(toSpawn);

                res.transform.position = coord;
                res.transform.parent = newParent.transform;
                greenObject++;
                spawned.Add(res);
                return true;
            }
            else
            {
                return false;
            }
        }

        public GameObject GetSectionResource(List<SectionObject> secs, float value)
        {
            value = Converter.ConvertToNormalColorValue(value);
            SectionObject selected = null;
            foreach(SectionObject sec in secs)
            {
                if (selected == null && sec.ColorValue < value)
                    selected = sec;

                if (sec.ColorValue < value && selected.ColorValue < sec.ColorValue)
                    selected = sec;
            }
            return selected.Prefab;
        }

        public List<SpawnRegion> GetRegions()
        {
            return regions;
        }

        private void DrawRegions(Color[,] colorMap)
        {
            for(int x = 0; x < colorMap.GetLength(0); x++)
            {
                for(int z = 0; z < colorMap.GetLength(1); z++)
                {

                    if (!HasColor(colorMap[x, z])) { 
                        regionMap[x,z] = 0;
                        continue;
                    }

                    if(regionMap[x,z] == 0)
                    {
                        Color c = colorMap[x,z];

                        if (HasColor(c))
                        {
                            CreateNewRegion(colorMap,x, z);
                        }
                    }
                }
            }

        }

        void CreateNewRegion(Color[,] colorMap, int x, int z)
        {
            SpawnRegion region = new SpawnRegion();
            regions.Add(region);
            List<Vector2> connectedPoints = new List<Vector2>();
            connectedPoints.Add(new Vector2(x,z));
            //FindNextPoint(connectedPoints, region, colorMap, x, z);


            while (connectedPoints.Count > 0)
            {
                Vector2 current = connectedPoints.First();
                AddConnectedPoints(connectedPoints, colorMap, (int)current.x, (int)current.y);
                regionMap[(int)current.x, (int)current.y] = 1;
                region.colorCord[current] = colorMap[(int)current.x, (int)current.y];
                connectedPoints.RemoveAt(0);
            }

        }

        void AddConnectedPoints(List<Vector2> con, Color[,] colorMap, int x, int z)
        {
            if (IsValidPoint((x+1),z, colorMap, con))
                con.Add(new Vector2(x + 1, z));
            if (IsValidPoint((x - 1), z, colorMap, con))
                con.Add(new Vector2(x - 1, z));
            if (IsValidPoint(x, (z+1), colorMap, con))
                con.Add(new Vector2(x, z + 1));
            if (IsValidPoint(x, (z - 1), colorMap, con))
                con.Add(new Vector2(x, z - 1));
        }


        bool IsValidPoint(int x, int z, Color[,] colorMap, List<Vector2> connected)
        {
            bool valid = true;

            if (!IsInBounds(x, z, colorMap))
                return false;

            valid &= !connected.Any(a => Vector2.Distance(a, new Vector2(x, z)) == 0);
            valid &= !InRegionMap(regionMap, x, z);
            valid &= HasColor(colorMap[x, z]);

            return valid;
        }
           

        private bool IsInBounds(int x, int z, Color[,] color)
        {
            return (x < color.GetLength(0) && z < color.GetLength(1) && x >= 0 && z >= 0);
        }

        private bool HasColor(Color c)
        {
            return (c.r != 0 || c.g != 0 || c.b != 0) && c.a > 0;
        }

        private bool InRegionMap(int[,] regionmap, int x, int z)
        {
            return regionmap[x, z] != 0;
        }

        private Color[,] To2DArray(Color[] c)
        {
            int x = 0;
            int z = 0;

            Color[,] colors = new Color[texture.width, texture.height];

            for(int i = 0; i < c.Length; i++)
            {
                colors[x, z] = c[i];
                x++;
                if(x == texture.width)
                {
                    z++;
                    x = 0;
                }
            }
            return colors;
        }

        private void OnDrawGizmos()
        {
            foreach(SpawnRegion region in regions)
            {
                foreach(KeyValuePair<Vector2, Color> point in region.colorCord)
                {
                    Gizmos.color = point.Value;
                    Vector3 nPoint = new Vector3(point.Key.x, 1, point.Key.y);
                    Gizmos.DrawWireCube(nPoint, new Vector3(1, 1, 1));
                }
            }
        }

    }


    public class SpawnRegion
    {
        public Dictionary<Vector2, Color> colorCord=    new Dictionary<Vector2, Color>();
        public List<GameObject> spawnedResources = new List<GameObject>();
    }
}
