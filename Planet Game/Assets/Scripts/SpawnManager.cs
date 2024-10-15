using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject impact;
    public GameObject[] particlePool;
    private int spawnQueue = 0;
    private int elseCount = 0;

    public void ClashImpact(Vector3 impactPoint)
    {
        if (spawnQueue == particlePool.Length)
        {
            spawnQueue = 0;
        }
        if (particlePool[spawnQueue].activeSelf == false)
        {
            particlePool[spawnQueue].transform.position = impactPoint;
            particlePool[spawnQueue].SetActive(true);
            spawnQueue++;
            elseCount = 0;
        } else if (elseCount < particlePool.Length)
        {
            spawnQueue++;
            ClashImpact(impactPoint);
            elseCount++;
        }
        else
        {
            particlePool[particlePool.Length] = Instantiate(impact);
            elseCount = 0;
            ClashImpact(impactPoint);
        }
        
        //decide which object to teleport


    }


}
