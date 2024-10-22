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
    private bool impactParticleNoAvailableObject;
    #endregion

    //Functions
    #region ImpactParticle
    // Function to be called on requiring a Vector3
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
                Debug.Log("Reactivating Impact Particle Element ("+i+")");
                // Controls the transportation, activeation & deactivation 
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
            // Instantiating Impact Particle 
            Debug.Log("Instantiating Impact Particle Element ("+impactParticlePool.Count+")");
            // Adds a clone of public game object to the pool
            impactParticlePool.Add(GameObject.Instantiate(impactParticleGameObject, impactPoint, Quaternion.identity));
            // Controls the transportation, activeation & deactivation
            StartCoroutine(ImpactParticleReturn(impactParticlePool.Count-1, impactPoint));
        }
    }
    // Function with timed cooldown requires the effected Int GameObject & Vector3 transform position 
    IEnumerator ImpactParticleReturn(int impactPointUsedGameObject, Vector3 impactPoint)
    {
        // Transfers position to ImpactPoint vector & activates Impact Particle 
        impactParticlePool[impactPointUsedGameObject].transform.position = impactPoint;
        impactParticlePool[impactPointUsedGameObject].SetActive(true);
        // Time between top and bottom Function
        yield return new WaitForSeconds(1);
        // Transfers position to Spawn Manager position & deactivates Impact Particle
        impactParticlePool[impactPointUsedGameObject].SetActive(false);
        impactParticlePool[impactPointUsedGameObject].transform.position = transform.position;
    }
    #endregion
}
