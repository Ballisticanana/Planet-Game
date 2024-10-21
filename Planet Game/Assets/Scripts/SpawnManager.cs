using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    //Variables
    #region Variables
    //Impact Particles
    public GameObject impactParticleGameObject;
    public List<GameObject> impactParticlePool;
    public bool impactParticleNoAvailableObject;
    #endregion

    //Functions
    #region ImpactParticle
    public void ImpactParticleRetrieve(Vector3 impactPoint)
    {
        // Reset bool for later change
        impactParticleNoAvailableObject = true;
        // Check every pooled object
        for (int i = 0; i < impactParticlePool.Count; i++)
        {
            // Check if object is deactivated
            if (!impactParticlePool[i].activeInHierarchy)
            {
                // Reactivating Impact Particle
                Debug.Log("Reactivating Impactparticle " + i);
                StartCoroutine(ImpactParticleReturn(i, impactPoint));
                // Set bool false stopping next step from Instantiating new game object in pool
                impactParticleNoAvailableObject = false;
                // Tells for function to end
                break;
            }
        }
        // checks bool if Impact Particle was inabled, if not Instantiate new Impact Particle in pool
        if (impactParticleNoAvailableObject == true)
        {
            impactParticlePool.Add(GameObject.Instantiate(impactParticleGameObject, impactPoint, Quaternion.identity));
            StartCoroutine(ImpactParticleReturn(impactParticlePool.Count-1, impactPoint));
        }
    }
    IEnumerator ImpactParticleReturn(int spawnQueue, Vector3 impactPoint)
    {
        impactParticlePool[spawnQueue].transform.position = impactPoint;
        impactParticlePool[spawnQueue].SetActive(true);
        yield return new WaitForSeconds(1);
        impactParticlePool[spawnQueue].SetActive(false);
        impactParticlePool[spawnQueue].transform.position = transform.position;
    }
    #endregion
}