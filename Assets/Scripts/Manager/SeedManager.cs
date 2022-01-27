using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class SeedObject
    {
        public Vector3 spawnPoint;
        public string resourceName;
    }

    public class SeedManager : MonoBehaviour
    {
        public static SeedManager Instance;
        public int ObjectsInQueue = 0;

        private Queue<SeedObject> seedObjects = new Queue<SeedObject>();

        public float targetTime = 10f;
        public float timeCount = 0f;

        public float MinDistanceBetweenSpawns = 5f;

        List<Vector2> spawned = new List<Vector2>();


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Update()
        {
            if (seedObjects.Count > 0)
            {
                timeCount += Time.deltaTime;

                if (timeCount > targetTime)
                {
                    timeCount = 0f;
                    targetTime = UnityEngine.Random.Range(1f, 4f);

                    int ten = ObjectsInQueue < 20 ? 1 : (int)((float)ObjectsInQueue / 10);


                    for(int i = 0; i < ten; i++)
                    {
                        SeedObject seedObject = seedObjects.Dequeue();
                        Vector2 xz = new Vector2(seedObject.spawnPoint.x, seedObject.spawnPoint.z);
                        if (!spawned.Contains(xz) && NoNearby(xz))
                        {
                            GameObject seed = GameObject.Instantiate(Resources.Load<GameObject>(seedObject.resourceName));
                            seedObject.spawnPoint.y += seed.transform.localScale.y / 2;
                            seed.transform.position = seedObject.spawnPoint;

                            spawned.Add(xz);
                        }
                        else
                        {
                            Debug.Log("Tree to close to another");
                        }
                        ObjectsInQueue--;
                    }
                }
            }
        }

        bool NoNearby(Vector2 xz)
        {
            foreach(Vector2 check in spawned)
            {
                if (Vector2.Distance(check, xz) < MinDistanceBetweenSpawns)
                    return false;
            }

            return true;
        }

        public void AddSeedToQueue(SeedObject seed)
        {
            seedObjects.Enqueue(seed);
            ObjectsInQueue++;
        }



    }
}
