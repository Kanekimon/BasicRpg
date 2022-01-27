using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpread : MonoBehaviour
{

    public int MaxNumberOfSeeds;
    [Range(0, 100f)]
    public float ProbForSeedToSpawn;
    [Range(0, 100f)]
    public float WindModifier;

    public bool IsActive;

    public float targetTime = 10f;
    public float timeCount = 0f;
    public bool spawnedSaplings = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && !spawnedSaplings)
        {
            timeCount += Time.deltaTime;

            if (timeCount > targetTime)
            {
                Spread();
                spawnedSaplings = true;
            }
        }
    }


    public void Spread()
    {

        Debug.Log(Terrain.activeTerrain.SampleHeight(new Vector3(-10, 0, -10)));

        int seeds = UnityEngine.Random.Range(0, MaxNumberOfSeeds);

        List<Vector3> seedSpawnPoints = new List<Vector3>();

        for (int i = 0; i < seeds; i++)
        {
            if (UnityEngine.Random.Range(0, 100f) < ProbForSeedToSpawn)
            {
                Vector3 spawnPoint = this.transform.position + GenerateRandomWindModifier() * WindModifier;

                if (!CheckIfInBounds(spawnPoint) || AlreadTree(spawnPoint))
                    continue;

                spawnPoint.y = (this.transform.position.y - (this.transform.localScale.y / 2));
                SeedManager.Instance.AddSeedToQueue(new SeedObject() { resourceName = "Plant/Tree/Sapling", spawnPoint = spawnPoint });

                //GameObject sapling = GameObject.Instantiate(Resources.Load<GameObject));
                //
                //sapling.transform.localPosition = spawnPoint;
            }
        }
    }

    bool AlreadTree(Vector3 spawn)
    {
        RaycastHit hit;

        if (Physics.SphereCast(spawn, 5f, Vector3.zero, out hit, 20f))
        {
            return true;
        }
        return false;
    }

    bool CheckIfInBounds(Vector3 spawn)
    {
        Vector3 tSize = Terrain.activeTerrain.terrainData.size;
        float x = spawn.x;
        float z = spawn.z;

        if (x < 0 || z < 0 || x > tSize.x || z > tSize.z)
            return false;
        return true;
    }

    public Vector3 GenerateRandomWindModifier()
    {
        int rand = UnityEngine.Random.Range(0, 8);
        float rPosX = UnityEngine.Random.Range(5f, 25f);
        float rPosZ = UnityEngine.Random.Range(5f, 25f);
        switch (rand)
        {
            case 0:
                return new Vector3(0, 0, rPosZ);
            case 1:
                return new Vector3(rPosX, 0, 0);
            case 2:
                return new Vector3(0, 0, -rPosZ);
            case 3:
                return new Vector3(-rPosX, 0, 0);
            case 4:
                return new Vector3(rPosX, 0, rPosZ);
            case 5:
                return new Vector3(rPosX, 0, -rPosZ);
            case 6:
                return new Vector3(-rPosX, 0, -rPosZ);
            case 7:
                return new Vector3(rPosZ, 0, -rPosX);
        }

        return Vector3.zero;
    }
}
