using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{

    public int MaxGrowthSteps;
    public int CurrentGrowthStep = 0;

    public float targetTime = 10f;
    public float timeCount = 0f;
    public bool spawning = false;

    public float maxX;
    public float maxY;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (!spawning && timeCount > targetTime)
        {
            timeCount = 0f;
            if(CurrentGrowthStep < MaxGrowthSteps)
            {
                CurrentGrowthStep++;
                Vector3 current = transform.position;
                current.y -= (transform.localScale.y/2);



                Vector3 currentSize = transform.localScale;
                currentSize.x = (maxX / (float)MaxGrowthSteps) * (float)CurrentGrowthStep;
                currentSize.y = (maxY/(float)MaxGrowthSteps)*(float)CurrentGrowthStep;
                currentSize.z = (maxZ/(float)MaxGrowthSteps)*(float)CurrentGrowthStep;



                transform.transform.localScale = currentSize;
                current.y += currentSize.y / 2;
                transform.localPosition = current;
            }
            else
            {
                spawning = true;
                SpawnTree();
            }
            
        }
    }

    void SpawnTree()
    {
        this.gameObject.GetComponent<SeedSpread>().IsActive = true;
        this.gameObject.name = "Tree";
        Destroy(this);

    }
}
