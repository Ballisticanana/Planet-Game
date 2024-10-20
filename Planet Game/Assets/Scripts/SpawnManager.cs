using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    public GameObject impact;
    public List<GameObject> particlePool;
    public int particleTotal = 1;
    public int spawnQueue = 0;
    public int activeSum = 0;
    private Vector3 savedImpactPoint;

    public void ClashImpactRetrieve(Vector3 impactPoint)
    {
        savedImpactPoint=impactPoint;
        if (spawnQueue == particleTotal)
        {
            spawnQueue = 0;
        }
        if (particlePool[spawnQueue].activeSelf == false)
        {
            StartCoroutine(ClashImpactReturn(spawnQueue));
            particlePool[spawnQueue].transform.position = savedImpactPoint;
            particlePool[spawnQueue].SetActive(true);
            activeSum++;
            spawnQueue++;
        }
        else if(activeSum < particleTotal)
        {
            spawnQueue++;
            ClashImpactRetrieve(savedImpactPoint);
        }
        else
        {
            particlePool.Add(GameObject.Instantiate(impact, impactPoint, Quaternion.identity));
            particleTotal++;
            spawnQueue++;
            activeSum++;
            particlePool[spawnQueue].SetActive(true);
            StartCoroutine(ClashImpactReturn(spawnQueue));
        }
    }
    IEnumerator ClashImpactReturn(int spawnQueue)
    {
        yield return new WaitForSeconds(0.5f);
        particlePool[spawnQueue].transform.position = transform.position;
        particlePool[spawnQueue].SetActive(false);
        activeSum--;
    }
    //StartCoroutine(ClashImpactReturn(spawnQueue));
}
